// Equivalent to #include <foo>
const fs = require('fs');
const https = require('https');
const WebSocket = require('ws');
const crypto = require('crypto');
const WebClient = require('./WebClient');
const UnityHost = require('./UnityHost');

// Map for correlating game Code's to host objects
// Format: String: gameCode => UnityHost: host object
// 123 is a dummy value for testing, the value can also be changed to
// whatever else is appropriate (ie. host websocket connections)
var codeToHost = new Map();


//let dummyCode = "123";
//var dummyHost= new UnityHost("This is a websocket", dummyCode)
//codeToHost.set(dummyCode, dummyHost);

// Generate unique game codes
// Create a random 4 digit (2 byte) hex string and makes sure it isn't used
function generateUniqueCode(){
    let code = crypto.randomBytes(2).toString('hex');
    while(codeToHost.has(code)){
        code = crypto.randomBytes(2).toString('hex');
    }
	return code;
}

function generateErrorMessage(err){
    return {type: -1, params: {errNum: err}};
}

// SSL cert info for https
const options = {
	cert: fs.readFileSync('SSL/cert.pem'),
	key: fs.readFileSync('SSL/key.pem'),
}

// server object which we'll attach a websocket server to
const server = https.createServer(options, function(request, response) {
    // If someone connects to the server in a browser, log the request on the
    // server and display some text for the web connection
	console.log('Received request for ' + request.url);
	response.writeHead(200, {'Content-Type': 'text/html'});
	response.end('TT-Games HTTPS server');
});

// Web socket server created for the server
const wss = new WebSocket.Server({ server });

// Install handler for a new socket (ws) connecting to the wever
wss.on('connection', function connection(ws, req) {
	console.log("Established connection with IP " + req.socket.remoteAddress);

    // Handler for initialization message received by server
    // Creates object to hold the socket info and install a new handler
	ws.addEventListener('message', function incoming(event) {
        let json = JSON.parse(event.data);
		console.log('Received new connection with type: %s', json.type);

        // Intialize client
        if(json.type == 1){
            
            let code = json.params.gameCode;
            let name = json.params.username;

            // One or more inital client inputs is empty
            if(code == "" || name == ""){
                console.log("Client Error: Invalid input(s)");
                let message = generateErrorMessage(100);
                ws.send(JSON.stringify(message));
                ws.terminate();
            } 
            // Game code belongs to a host
            else if(codeToHost.has(code)){
                console.log("Valid Game code from client. Creating WebClient class");
                let message = {type: 1, params: {gameCode: code, username: name}};
                ws.send(JSON.stringify(message));
                client = new WebClient(ws, codeToHost.get(code), name);
                codeToHost.get(code).addClient(client);		
            } 
            // Game code does not exist
            else {
                console.log("Client Error: Game code doesn't exist");
                let message = generateErrorMessage(101);
                ws.send(JSON.stringify(message));
				ws.terminate();
			}
        }
        // Intialize host
        else if(json.type == 2){
            let code = generateUniqueCode();
            host = new UnityHost(ws, code);
            codeToHost.set(code,host);

            let message = {type: 2, params: {gameCode: code}};
            ws.send(JSON.stringify(message));

            console.log("Created new host with game code: ", code);
        }
        else{
            // Reaching this means client/host object was created incorrectly
            // or the initialization message sent had the wrong type value.
            console.log("General Error: Attempted non-initialization comm");
            let message = generateErrorMessage(001);
            ws.send(JSON.stringify(message));            
        }

    // This is very VERY important. This handler will only ever run once, on 
    // the firstmessage received by the server. By creating a WebClient or 
    // UnityHost object, a new handler is installed for receiving messages and 
    // we do NOT want this one to run again.
	}, {once: true});

    // Install handler for when a socket connection closes, log data about close
	ws.on('close', function closed(code, reason) {
        console.log('Connection closed. Code: %d, Reason: %s', code, reason);
    });
});

// Allow the server to accept new connections over port 8000
server.listen(8000);
console.log("Server Info: ", server.address());