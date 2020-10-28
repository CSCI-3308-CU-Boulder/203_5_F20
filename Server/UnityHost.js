const NAME_OF_SERVER_FILE = 'http_server'

const WebSocket = require('ws');
const Server= require('./' + NAME_OF_SERVER_FILE);

class UnityHost {
    constructor(hostWS, code){
        this.ws = hostWS;
        this.code = code;
        this.clients = [];

        let tempHost = this;

        // Forward subsequent messages to clients
        this.ws.addEventListener('message', function(messageObject){
            let realData = JSON.parse(messageObject.data);
            
            tempHost.clients.forEach(client => {
                client.ws.send(messageObject.data);
            }); 
        });

        // When host connection closes, close all client connections and remove
        // host from the map in the server
        this.ws.on('close', function closed(code, reason) {
            console.log("Host "+ tempHost.code + " connection terminated. Terminating client connections.")
            tempHost.clients.forEach(client => {
                let message = Server.generateErrorMessage(102);
                client.ws.send(JSON.stringify(message));
                client.ws.terminate();
            }); 
            Server.removeHostByCode(tempHost.code);
        });

        
    }

    // Called by the server when a new client connects to this host's game code.
    // Tell the host about the new client and tell each of the old clients about
    // the new client, as well as telling the new client about all the old clients.
    // However, this client to client communication should only be necessary for
    // the demo.
    // Add the new client to the host's list of clients
    addClient(newClient){
        console.log("Host " + this.code + " adding new client object: " + newClient.name);

        let message = {type: 1, username: newClient.name};
        this.ws.send(JSON.stringify(message));
        
        // Send old clients the new client and send the new client all the old
        // clients
        // THIS MIGHT ONLY BE USED FOR TESTING/DEMO, CLIENTS THEORETICALLY DON'T
        // NEED TO KNOW ABOUT EACH OTHER
        // =====================================================================
        this.clients.forEach(oldClient => {
            let oldMessage = {type: 1, username: newClient.name};
            oldClient.ws.send(JSON.stringify(oldMessage))

            let newMessage = {type: 1, username: oldClient.name};
            newClient.ws.send(JSON.stringify(newMessage));
        }); 
        // =====================================================================

        this.clients.push(newClient);
    }

    // Check if a host already has a client with a given name
    checkDuplicateUsername(name){
        let ret = false;
        this.clients.forEach(client => {
            if(client.name === name){
                ret = true;
                return;
            }   
        }); 
        return ret;
    }

    // Called by a client on close
    // Remove the client from the host's list and tell the host directly.
    // For the demo, tell each other client that the calling client disconnected
    removeClient(oldClient){

        this.clients.splice(this.clients.indexOf(oldClient), 1);

        let message = {type: 3, username: oldClient.name};
        this.ws.send(JSON.stringify(message));

        // Send other clients the removal of the old client
        // THIS MIGHT ONLY BE USED FOR TESTING/DEMO, CLIENTS THEORETICALLY DON'T
        // NEED TO KNOW ABOUT EACH OTHER
        // =====================================================================
        this.clients.forEach(client => {
            client.ws.send(JSON.stringify(message))
        }); 
        // =====================================================================
    }
}

module.exports = UnityHost;