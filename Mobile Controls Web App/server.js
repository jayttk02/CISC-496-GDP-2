const https = require('https');
const fs = require('fs');
const path = require('path');
const expressip = require('express-ip');
const express = require('express');

var ips = new Map();
var players = 0;
var level = 2;
const maxPlayers = 2;
const controls = new Map([
    [1, "player1"],
    [2, "player2"]
]);

var privateKey  = fs.readFileSync('key.pem', 'utf8');
var certificate = fs.readFileSync('cert.pem', 'utf8');

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
httpsServer.listen(8443, "192.168.2.37");

var WebSocketServer = require('ws').Server;
var wss = new WebSocketServer({
    server: httpsServer
  });

wss.on('connection', function connection(ws) { 
 console.log('New client connected!')
 ws.send('connection established')
 ws.on('close', () => console.log('Client has disconnected!'))
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
    if(!ips.has(ip)){
        players += 1;
        if(players > maxPlayers){
            res.sendStatus(404);
        }
        else{
          ips.set(ip, players);
        }
    }
    if(players <= maxPlayers){
      res.render(String(controls.get(ips.get(ip))), {level: level});
    }
    console.log(ip);
    console.log(ips);
  });