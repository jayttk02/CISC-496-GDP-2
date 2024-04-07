const https = require('https');
const fs = require('fs');
const path = require('path');
const expressip = require('express-ip');
const express = require('express');

var ips = [];
var level = 2;
const maxPlayers = 2;
const controls = new Map([
    [1, "player1"],
    [2, "player2"]
]);

var privateKey  = fs.readFileSync('key.pem', 'utf8');
var certificate = fs.readFileSync('cert.pem', 'utf8');
var ip = fs.readFileSync(path.join(__dirname, 'public', 'ip.txt'));

var credentials = {key: privateKey, cert: certificate};

var app = express()
.use(express.static(path.join(__dirname, 'public')))
.use(expressip().getIpInfoMiddleware);

// Set EJS as the view engine
app.set('view engine', 'ejs');

// Define the directory where your HTML files (views) are located
app.set('views', path.join(__dirname, 'views'));

//pass in your express app and credentials to create an https server
var httpsServer = https.createServer(credentials, app);
httpsServer.listen(8443, ip);

var WebSocket = require('ws');
var WebSocketServer = WebSocket.Server;
var wss = new WebSocketServer({
    server: httpsServer
  });

wss.on('connection', function connection(ws, req) { 
 var ip = req.socket.remoteAddress;
 console.log('New client ' + ip +  ' connected!')
 ws.send('connection established')
 ws.on('close', () => {
  console.log('Client ' + ip + ' has disconnected!');
  // ips.delete(ip);
  const index = ips.indexOf(ip);
  if (index > -1) { // only splice array when item is found
    ips.splice(index, 1); // 2nd parameter means remove one item only
  }
  console.log(ips);
})
 ws.on('message', data => {
   wss.clients.forEach(client => {
     console.log(`distributing message: ${data}`)
     client.send(`${data}`)
   });
   if(data == "lvl1"){
    level = 1;
   }
   else if(data == "lvl2"){
    level = 2;
   }
   else if(data == "want # players"){
    wss.clients.forEach(client => {
      console.log(`distributing message: players: ${ips.length}`)
      client.send(`players: ${ips.length}`)
    });
   }
 })
});

app.get('/', function (req, res) {
  res.render("start");
});

app.post('/', function (req, res) {
  res.redirect(303, "/play");
});

app.get('/play', function (req, res) {
    const ip = req.ipInfo["ip"];
    if(ips.length > maxPlayers){
        res.sendStatus(404);
    }
    else{
      ips.push(ip);
      res.render(String(controls.get(ips.indexOf(ip) + 1)), {level: level});
    }
    console.log(ips);
  });