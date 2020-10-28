// IP address of AWS server
const IPADDR = '3.130.99.109';
const PORT = '443';
var lobbyID = "";
var username = "";
var connected = false;
var server = false;

var aWebSocket;

function display_help(){
    document.getElementById("help-overlay").style.display = "flex";
}

function hide_help(){
    document.getElementById("help-overlay").style.display = "none";
}

function handleError(json){
    let errNum = json.errNum;

    if(errNum == 001){
        document.getElementById('error-message').innerHTML = "Error: Non-initialization comm sent";
    }
    if(errNum == 100){
        document.getElementById('error-message').innerHTML = "Error: Empty field(s)";
    }
    else if(errNum == 101){
        document.getElementById('error-message').innerHTML = "Error: Invalid Game ID";
    } 
    else if(errNum == 102){
        document.getElementById('error-message').innerHTML = "Error: Host Disconnected";
    } 
    else if(errNum == 103){
        document.getElementById('error-message').innerHTML = "Error: Duplicate Username";
    } 
    else {
        document.getElementById('error-message').innerHTML = "Error: Unknown error, Error Code: " + errNum;
    }
}

function clientConnect(json){
    let name = json.username;
    let code = json.gameCode;

    lobbyID = code;
    username = name;

    if(!connected){
        document.getElementById('lobby-page').style.display = "grid";
        document.getElementById('login-page').style.display = "none";
        document.getElementById('error-message').innerHTML = "";
        document.getElementById('lobby-id').innerHTML = lobbyID;
        connected = true;
    }

    let players = document.getElementById('lobby-players');
    players.innerHTML += "<div class=\"lobby-player\" id=\"" + name + "\">" + name +"</div>";

}

function startGame(json){
    document.getElementById('game-username').innerHTML = username;
    document.getElementById('lobby-page').style.display = "none";
    document.getElementById('login-page').style.display = "none";
    document.getElementById('game-page').style.display = "grid";
}

function serverConnect(json){
    let code = json.gameCode;

    lobbyID = code;

    if(!connected){
        document.getElementById('lobby-page').style.display = "grid";
        document.getElementById('login-page').style.display = "none";
        document.getElementById('error-message').innerHTML = "";
        document.getElementById('lobby-id').innerHTML = lobbyID;
        connected = true;
    }
}

function removeClient(json){
    let name = json.username;
    document.getElementById(name).outerHTML = "";
}

function submit_button(){
    // Get data from form
    let name = document.getElementById("username-input").value;
    let code = document.getElementById("game-code-input").value;
    console.log("Username: \"" + name + "\", gameCode: \"" + code +"\"");

    // Establish websocket connection
    aWebSocket = new WebSocket('ws://' + IPADDR + ':' + PORT);

    // Install event handlers
    aWebSocket.onclose = function(event) {
        console.log("WebSocket is closed");

        // Check to see if we have already joined a lobby
        if(document.getElementById("login-page").style.display == "none"){
            document.getElementById('lobby-page').style.display = "none";
            document.getElementById('game-page').style.display = "none";
            document.getElementById('login-page').style.display = "grid";
            document.getElementById('lobby-players').innerHTML = "";
            lobbyID = "";
            
            // Get rid of fake server input
            if(server){
                server = false;
                document.getElementById('server-send').style.display = "none";
            }
            connected = false;
        }
    };

    aWebSocket.onopen = function(event) {
        console.log("Connected to server");

        // THIS IS CODE TO FAKE BEING A SERVER SO WE DON'T NEED UNITY YET
        // =====================================================================
        if(code === "servertest"){
            server = true;
            document.getElementById('server-send').style.display = "flex";
            let message = {type: 2, gameCode: code, username: name}
            aWebSocket.send(JSON.stringify(message));
            return;
        }
        // =====================================================================
        let message = {type: 1, gameCode: code, username: name}
        aWebSocket.send(JSON.stringify(message));
    };

    aWebSocket.onmessage = function(event) {
        console.log("Message received: ", event);
        console.log("Message: " + event.data);

        let json = JSON.parse(event.data);

        // Error
        if(json.type == -1){
            handleError(json);
        }
        // Successful connection
        else if(json.type == 1){
            clientConnect(json);
        }
        // THE FOLLOWING IS FOR THE DEMO TO SHOW CONNECTED CLIENTS ON WEB APP
        else if(json.type == 2){
            serverConnect(json);
        }
        // Other client in lobby disconnects
        else if(json.type == 3){
            removeClient(json);
        }
        // Move from lobby page to game page
        else if(json.type == 4){
            gamePage(json);
        }
        
    };

}

function server_send(){
    let input = document.getElementById("server-input").value;
    let message = {type: input}
    console.log("Sending " + input);
    aWebSocket.send(JSON.stringify(message));
}