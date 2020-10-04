// IP address of AWS server
const IPADDR = '3.130.99.109';
const PORT = '443';

function display_help(){
    document.getElementById("help-overlay").style.display = "flex";
}

function hide_help(){
    document.getElementById("help-overlay").style.display = "none";
}

function submit_button(){
    // Get data from form
    let name = document.getElementById("username-input").value;
    let code = document.getElementById("game-code-input").value;
    console.log("Username: \"" + name + "\", code: \"" + code +"\"");

    // Establish websocket connection
    var aWebSocket = new WebSocket('wss://' + IPADDR + ':' + PORT);

    // Install event handlers
    aWebSocket.onclose = function(event) {
        console.log("WebSocket is closed");
    };

    aWebSocket.onopen = function(event) {
        console.log("Connected to server");
        aWebSocket.send(name + "," + code);
    };

    aWebSocket.onmessage = function(event) {
        console.debug("WebSocket message received: ", event);
        console.log("Message: " + event.data);
    };


    // 3 second timer before closing connection
    setTimeout(function(){ aWebSocket.close(1000, "Successfully closed communication"); }, 3000);
}

