// IP address of AWS server
const IPADDR = '3.130.99.109';
const PORT = '443';
var lobbyID = "";
var username = "";

function display_help(){
    document.getElementById("help-overlay").style.display = "flex";
}

function hide_help(){
    document.getElementById("help-overlay").style.display = "none";
}

function submit_button(){
    // Get data from form
    let name = document.getElementById("username-input").value;
    let gamecode = document.getElementById("game-code-input").value;
    console.log("Username: \"" + name + "\", game code: \"" + gamecode +"\"");

    // Establish websocket connection
    var aWebSocket = new WebSocket('wss://' + IPADDR + ':' + PORT);

    // Install event handlers
    aWebSocket.onclose = function(event) {
        console.log("WebSocket is closed");

        // Check to see if we have already joined a lobby
        if(document.getElementById("login-page").style.display == "none"){
            document.getElementById('lobby-page').style.display = "none";
            document.getElementById('login-page').style.display = "grid";
            document.getElementById('error-message').innerHTML = "Error: Connection closed";
            document.getElementById('lobby-players').innerHTML = "";
            lobbyID = "";
        }
    };

    aWebSocket.onopen = function(event) {
        console.log("Connected to server");
        aWebSocket.send(name + "," + gamecode);
    };

    aWebSocket.onmessage = function(event) {
        console.log("WebSocket message received: ", event);
        console.log("Message: " + event.data);
        let statuscode = event.data.substring(0, event.data.indexOf(','));
        if(statuscode == "1"){
            document.getElementById('error-message').innerHTML = "Error: Invalid Game ID";
        } else if (statuscode == "2"){
            document.getElementById('error-message').innerHTML = "Error: Invalid data format (dev only)";
        } else if (statuscode == "3"){
            document.getElementById('error-message').innerHTML = "Error: Empty field(s)";
        } else if (statuscode != "0"){
            document.getElementById('error-message').innerHTML = "Error: Internal server error";
        } else {
            lobbyID = gamecode;
            username = name;
            document.getElementById('lobby-page').style.display = "grid";
            document.getElementById('login-page').style.display = "none";
            document.getElementById('error-message').innerHTML = "";
            document.getElementById('lobby-id').innerHTML = lobbyID;

            let players = document.getElementById('lobby-players');
            players.innerHTML += "<div class=\"lobby-player\" id=\"" + username + "\">" + username +"</div>"
        }
    };

}

