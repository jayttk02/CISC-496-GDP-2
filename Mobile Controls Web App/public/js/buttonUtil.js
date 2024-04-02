export function createContainer(){
    var div = document.createElement("DIV");
    div.classList.add("container");
    return div;
}

function createButton(text, colour, webSocket, socketMsg, tap = true, socketMsgExit = ""){
    var button = document.createElement("BUTTON");
    button.classList.add("block");
    button.classList.add(colour);
    button.appendChild(createLabel(text));
    if(tap){
        button.addEventListener("touchstart", function() {webSocket.send(socketMsg);});
    }
    else{
        button.addEventListener('pointerdown', function() {
            webSocket.send(socketMsg);
        });
        button.addEventListener('pointerup', function() {
            webSocket.send(socketMsgExit);
        });
    }
    return button;
}

function createLabel(text){
    var punch = document.createElement("DIV");
    punch.classList.add("label");
    punch.innerText = text;
    return punch;
}

export function punch(webSocket){ return createButton("PUNCH", "red", webSocket, "p"); }

export function stepForward(webSocket){ return createButton("STEP\nFORWARD", "green", webSocket, "sf", false, "sfend"); }

export function player1Jump(webSocket){ return createButton("JUMP", "largepink", webSocket, "1j"); }

export function shoot(webSocket){ return createButton("SHOOT", "yellow", webSocket, "s"); }

export function kick(webSocket){ return createButton("KICK", "orange", webSocket, "k"); }

export function stepBackward(webSocket){ return createButton("STEP\nBACKWARD", "green", webSocket, "sb", false, "sbend"); }

export function player2Jump(webSocket){ return createButton("JUMP", "pink", webSocket, "2j"); }