const express = require('express')
const webserver = express()
 .use(express.static('public'))
 .use((req, res) =>
   res.sendFile('index.html', { root: __dirname })
 )
 .listen(8080, "10.216.102.109", () => console.log(`Listening on ${8080}`))
const { WebSocketServer } = require('ws')
const sockserver = new WebSocketServer({ host: "10.216.102.109", port: 443 })
sockserver.on('connection', ws => {
 console.log('New client connected!')
 ws.send('connection established')
 ws.on('close', () => console.log('Client has disconnected!'))
 ws.on('message', data => {
   sockserver.clients.forEach(client => {
     console.log(`distributing message: ${data}`)
     client.send(`${data}`)
   })
 })
 ws.onerror = function () {
   console.log('websocket error')
 }
})
