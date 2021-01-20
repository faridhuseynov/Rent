"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

// invoke method that notifies about the other using typing the reply in the chat. Beforeinput only fires when in <textarea>, <input> and other contenteditable
// element the character button is pressed
$(document).on('beforeinput', function () {


    // id for user with whom currently chatting, active user
    var recipientId = $("#activeUser").val();

    // logged in user
    var senderId = $("#recipientUser").val();

    connection.invoke("SendTypingNotification", senderId, recipientId).catch(function (err) {
        return console.error(err.toString());
    });
});

connection.on("ReceiveTypingNotification", function (sender, recipient) {
    //id for user with whom currently chatting, active user
    var senderId = $("#activeUser").val();

    // logged in user
    var recipientId = $("#recipientUser").val();

    if (sender == senderId && recipient == recipientId) {
        var li = document.createElement("li");
        var senderName = $("#userName").val();
        li.textContent = senderName + " is typing a message..";
        $("#notificationMessage").removeAttr("hidden");
        //document.getElementById("msgHistory").appendChild(li);
        $("#msgHistory").scrollTop($("#msgHistory")[0].scrollHeight);
        setTimeout(function myfunction() {
            //document.getElementById("msgHistory").removeChild(li);
            $("#notificationMessage").attr("hidden", true);
        }, 1000)
    }
});