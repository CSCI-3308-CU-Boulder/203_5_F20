# Server (hosted on AWS)

## Packages Needed

WebSocket: `npm install ws`
Forever: `npm install forever -g`

## Running the HTTP Server

Start the HTTP server in a detached mode by running `sudo forever http_server.js`
Use `forever stop [pid]` to stop the server and `forever list` to check if the server is running
