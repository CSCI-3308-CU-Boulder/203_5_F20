const WebSocket = require('ws');

class WebClient {
    constructor(clientWS, hostObj, name){
        this.ws = clientWS;
        this.host = hostObj;
        this.name = name;

        let tempClient = this;
        
        // Forward messages from clients to hosts
        this.ws.addEventListener('message', function(messageObject){
            let realData = JSON.parse(messageObject.data);
            //console.log("Data from Client: ", realData);
            tempClient.host.ws.send(messageObject.data);
        });

        // Tell the host object that this client is disconnecting on close  
        this.ws.on('close', function closed(code, reason) {
            hostObj.removeClient(tempClient);
        });
    }
}

module.exports = WebClient;