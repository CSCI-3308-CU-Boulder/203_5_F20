// IP address of AWS server
// const IPADDR = 'localhost';
const IPADDR = '3.130.99.109';
// const PORT = '80';
const PORT = '80';
var lobbyID = "";
var userName = "";
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
    else if(errNum == 104){
        document.getElementById('error-message').innerHTML = "Error: Game in Progress";
    }
    else {
        document.getElementById('error-message').innerHTML = "Error: Unknown error, Error Code: " + errNum;
    }
}

function clientConnect(json){
    let name = json.userName;
    let code = json.gameCode;

    lobbyID = code;

    if(!connected){
        document.getElementById('lobby-page').style.display = "grid";
        document.getElementById('login-page').style.display = "none";
        document.getElementById('error-message').innerHTML = "";
        document.getElementById('lobby-id').innerHTML = lobbyID;
        userName = name;
        connected = true;
    }

    let players = document.getElementById('lobby-players');
    players.innerHTML += "<div class=\"lobby-player\" id=\"" + name + "\">" + name +"</div>";

}

function startGame(json){
    document.getElementById('game-username-name').innerHTML = userName;
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
    let name = json.userName;
    document.getElementById(name).outerHTML = "";
}

function submit_button_disable(){
    btn = document.getElementById("submit-button");
    btn.style.opacity = 0.4;
    btn.disabled = true;
    btn.style.cursor = "not-allowed";
}

function submit_button_enable(){
    btn = document.getElementById("submit-button");
    btn.style.opacity = 1.0;
    btn.disabled = false;
    btn.style.cursor = "pointer";
}

function submit_button(){
    // Get data from form
    let name = document.getElementById("username-input").value;
    let code = document.getElementById("game-code-input").value;
    console.log("Username: \"" + name + "\", gameCode: \"" + code +"\"");

    // Disable button
    submit_button_disable();

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
            enable_buttons();

            // Get rid of fake server input
            if(server){
                server = false;
                document.getElementById('server-send').style.display = "none";
            }
            connected = false;
        }
        submit_button_enable();
    };

    aWebSocket.onopen = function(event) {
        console.log("Connected to server");

        // THIS IS CODE TO FAKE BEING A SERVER SO WE DON'T NEED UNITY YET
        // =====================================================================
        if(code === "servertest"){
            server = true;
            document.getElementById('server-send').style.display = "flex";
            let message = {type: 2, gameCode: code, userName: name}
            aWebSocket.send(JSON.stringify(message));
            return;
        }
        // =====================================================================
        let message = {type: 1, gameCode: code, userName: name}
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
            startGame(json);
        }
        // Update question
        else if(json.type == 5){
            updateGame(json);
        }

    };

    aWebSocket.onerror = function(event){
        console.log("Websocket error");
        submit_button_enable();
    }

}

// Called when the fake server wants to send a message
function server_send(){
    let input = document.getElementById("server-input").value;
    let message;
    if(input == 5){
        message = {type: input, q_num: "4", q_text: "What is an apple?"};
    } else {
        message = {type: input};
    }
    console.log("Sending " + JSON.stringify(message));
    aWebSocket.send(JSON.stringify(message));
}

// Called when the host sends a new question
function updateGame(json){
    enable_buttons();
    document.getElementById("question-num").innerHTML = json.q_num;
    document.getElementById("question-text").innerHTML = json.q_text;

}

// Called when we press a button during gameplay
function game_select(option){
    // Dictionary of letters for actual UI
    var letters = ["A","B","C","D"];

    // Disable buttons on click
    children = document.getElementById("game-buttons").children;
    for(var i = 0; i < children.length; i++){
        if(children[i].innerHTML == letters[option]){
            children[i].style.opacity = 1.0;
        } else {
            children[i].style.opacity = 0.4;
        }
        children[i].disabled = true;
        children[i].style.cursor = "not-allowed";
    }

    // Send answer
    let message = {type: 5, data: option, userName: userName};
    console.log("Sending " + option);
    aWebSocket.send(JSON.stringify(message));
}

function enable_buttons(){
    // Enable buttons to click
    children = document.getElementById("game-buttons").children;
    for(var i = 0; i < children.length; i++){
        children[i].style.opacity = 1.0;
        children[i].disabled = false;
        children[i].style.cursor = "pointer";
    }
}
