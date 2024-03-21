function createSocket(){
    const webSocket = new WebSocket('wss://10.216.112.17:8443/');
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