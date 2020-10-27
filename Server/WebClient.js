const WebSocket = require('ws');

class WebClient {
    constructor(clientWS, hostObj, name){
        this.ws = clientWS;
        this.host = hostObj;
        this.name = name;
        
        // Install handler for when server receives a message from the client
        this.ws.addEventListener('message', this.receiveMessage);

        // Tell the host object that this client is disconnecting on close
        let tempClient = this;
        this.ws.on('close', function closed(code, reason) {
            hostObj.removeClient(tempClient);
        });
    }

    receiveMessage(messageObject){
        let realData = JSON.parse(messageObject.data);
        //console.log("Data from Client: ", realData);
        this.host.send(realData);
    }
}

module.exports = WebClient;