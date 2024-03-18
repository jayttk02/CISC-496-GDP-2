export function createContainer(){
    var div = document.createElement("DIV");
    div.classList.add("container");
    return div;
}

function createButton(text, colour, webSocket, socketMsg, tap = true){
    var button = document.createElement("BUTTON");
    button.classList.add("block");
    button.classList.add(colour);
    button.appendChild(createLabel(text));
    if(tap){
        button.addEventListener("touchstart", function() {webSocket.send(socketMsg);});
    }
    else{
        var timer;
        button.addEventListener('pointerdown', function() {
            timer=setInterval(function(){
                webSocket.send(socketMsg);
            }, 0)
        });
        button.addEventListener('pointerup', function() {if(timer) clearInterval(timer);});
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

export function stepForward(webSocket){ return createButton("STEP FORWARD", "green", webSocket, "sf", false); }

export function player1Jump(webSocket){ return createButton("JUMP", "pink", webSocket, "1j"); }

export function shoot(webSocket){ return createButton("SHOOT", "yellow", webSocket, "s"); }

export function kick(webSocket){ return createButton("KICK", "orange", webSocket, "k"); }

export function stepBackward(webSocket){ return createButton("STEP BACKWARD", "green", webSocket, "sb", false); }

export function player2Jump(webSocket){ return createButton("JUMP", "pink", webSocket, "2j"); }