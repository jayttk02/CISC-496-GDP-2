var ip = await fetch('ip.txt').then(res => res.text());

function createSocket(){
    // var ip;

    // fetch('ip.txt')
    // .then(response => response.text())
    // .then((data) => {
    //   ip = data;
    // })

    

    const webSocket = new WebSocket('wss://' + ip + ':8443/');
    webSocket.onmessage = (event) => {
        console.log(event);
        console.log('Message from server: ' + event.data + "<br>");
    };
    webSocket.addEventListener("open", () => {
        console.log("We are connected");
    });
    return webSocket;
}

export { createSocket };