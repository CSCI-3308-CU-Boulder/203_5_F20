const WebSocket = require('ws');

class WebClient {
    constructor(clientWS, hostObj, name){
        this.ws = clientWS;
        this.host = hostObj;
        this.name = name;
        
        this.ws.addEventListener('message', this.receiveMessage);

        // For some reason, when the close function is called, the Host Obj is
        // undefined which prevents us from telling the host to remove us from
        // thier list
        //this.ws.addEventListener('close', this.clientClose);
    }

    receiveMessage(messageObject){
        let realData = JSON.parse(messageObject.data);
        console.log("Data from Client: ", realData);
        //this.host.send(realData);
    }

    clientClose(){
        this.host.removeClient(this);
    }
}

module.exports = WebClient;