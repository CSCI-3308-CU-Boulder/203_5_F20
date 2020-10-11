const WebSocket = require('ws');

class UnityHost {
    constructor(hostWS, code){
        this.ws = hostWS;
        this.code = code;
        this.clients = [];

        this.ws.addEventListener('message', this.receiveMessage);
    }

    addClient(newClient){
        console.log("Host adding new client object: " + newClient.name);

        let message = {type: 1, params: {username: newClient.name}};
        this.ws.send(JSON.stringify(message));
        
        // Send old clients the new client and send the new client all the old
        // clients
        // THIS MIGHT ONLY BE USED FOR TESTING/DEMO, CLIENTS THEORETICALLY DON'T
        // NEED TO KNOW ABOUT EACH OTHER
        // =====================================================================
        this.clients.forEach(oldClient => {
            let oldMessage = {type: 2, params: {username: newClient.name}};
            oldClient.ws.send(JSON.stringify(oldMessage))

            let newMessage = {type: 2, params: {username: oldClient.name}};
            newClient.ws.send(JSON.stringify(newMessage));
        }); 
        // =====================================================================

        this.clients.push(newClient);
    }

    removeClient(oldClient){

        this.clients.splice(this.clients.indexOf(oldClient), 1);

        // Send other clients the removal of the old client
        // THIS MIGHT ONLY BE USED FOR TESTING/DEMO, CLIENTS THEORETICALLY DON'T
        // NEED TO KNOW ABOUT EACH OTHER
        // =====================================================================
        this.clients.forEach(client => {
            let message = {type: 2, params: {username: "-" + oldClient.name}};
            client.ws.send(JSON.stringify(oldMessage))
        }); 
    }

    receiveMessage(messageObject){
        let realData = JSON.parse(messageObject.data);
        console.log("Data from Host: ", realData);
        //this.host.send(realData);
    }

    hostClose(){     
        this.clients.forEach(client => {
            console.log("Closing client connection with name: ", client.name);
            client.terminate();
        }); 
    }
}

module.exports = UnityHost;