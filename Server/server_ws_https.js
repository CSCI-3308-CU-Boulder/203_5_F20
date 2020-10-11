// Message syntax:
// Note: This is temporary and can change if needed
// 	ws.send('code' + ',' + 'message')
// Valid codes:
// 	Web client:
// 		0	Successfully connected to game session
// 		1	Invalid game ID input
// 		2	Invalid data format (username + "," + gameID)
// 		3	Invalid fields (empty)



// Equivalent to #include <foo>
const fs = require('fs');
const https = require('https');
const WebSocket = require('ws');
const crypto = require('crypto');
const WebClient = require('./WebClient');

// Generate unique IDs
// NOT CURRENTLY IN USE
function getUniqueID(){
	return crypto.randomBytes(2).toString('hex');
}

// Map for storing websocket connections and usernames
// Format: [ws, "username"}]
let clientMap = new Map();

// Map for storing valid game IDs
// Format: ["Game ID", bool]
// 123 is a dummy value for testing, the value can also be changed to
// 	whatever else is appropriate (ie. host websocket connections)
let gameIDMap = new Map();
gameIDMap.set('123', true);

// SSL cert info for https
const options = {
	cert: fs.readFileSync('SSL/cert.pem'),
	key: fs.readFileSync('SSL/key.pem'),
}

// server object which we'll attach a websocket server to
const server = https.createServer(options, function(request, response) {
	// If someone connects to the server in a browser, print something
	console.log('Received request for ' + request.url);
	response.writeHead(200, {'Content-Type': 'text/html'});
	response.end('TT-Games HTTPS server');
});

// Web socket server created for the server
const wss = new WebSocket.Server({ server });

// What happens when we handle a websocket connection
wss.on('connection', function connection(ws, req) {
	console.log("Established connection with IP " + req.socket.remoteAddress);

	ws.on('message', function incoming(data) {
		console.log('Received: %s', data);

		// NOTE: this code is for handling data from website clients
		// 	To handle Unity code, add separate logic
		// Extract data
		if(data.includes(',')){
			let gcode = data.substring(data.indexOf(',')+1);
			let uname = data.substring(0,data.indexOf(','));

			// Check if input is non-zero
			if(gcode == "" || uname == ""){
				console.log("Invalid input, empty fields");
				ws.send('3,');
				ws.terminate();
			} else if(gameIDMap.has(gcode)){
				// Check if game ID exists
				// Establish unique ID
				console.log("Valid Game ID");
				ws.send('0,');
				client = new WebClient(ws);
				clientMap.set(ws, uname);				
			} else {
				console.log("Invalid game ID");
				ws.send('1,');
				ws.terminate();
			}
		} else {
			console.log("Invalid data format");
			ws.send('2,');
			ws.terminate();
		}
	});

	ws.on('close', function closed(code, reason) {
		console.log('Connection closed. Code: %d, Reason: %s', code, reason);
		// Clean up client references
		// Note: the map.delete function works even if the key isn't present (returns false)
		clientMap.delete(ws);
	});
});

// IP address we're listening to
server.listen(8000);
console.log(server.address());