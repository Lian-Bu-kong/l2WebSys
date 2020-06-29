"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

// Connect BackEnd and FrontEnd Init
connection.start().then(function () {
    // Connect成功


}).catch(function (err) {
    return console.error(err.toString());
});


connection.on("ReceiveMessage", function (user, message) {

    // ChatHub 廣播
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " Actor Receive " + msg;
    var li = document.createElement("li");

    li.textContent = encodedMsg;

    document.getElementById("ActorMessages").appendChild(li);
});




