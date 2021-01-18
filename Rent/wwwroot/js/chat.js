"use strict";

//var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var li = document.createElement("li");
    li.textContent = msg;

    //var div = document.createElement(
    //    <div class="incoming_msg">
    //        <div class="received_msg">
    //            <div class="received_withd_msg">
    //                <p>
    //                    hello
    //                    </p>
    //                <span class="time_date">2-Jan-2021</span>
    //            </div>
    //        </div>
    //    </div>
    //)

    document.getElementById("msgHistory").appendChild(li);
    console.log(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = $(".write_msg").val();
    var recipient = $(".active_chat").find("img").attr("alt");
    if (message != "") {
        $.ajax({
            url: "/Messages/Create",
            method: "POST",
            data: {
                recipientUserName: recipient,
                content: message
            },
            success: function () {
                $(".write_msg").val("");
                getMessageThread(recipient);

            },
            error: function (err) {
                console.log(err);
            }
        });
        var recipientId = $("#activeUser").val();
        console.log(recipientId);
        connection.invoke("SendPrivateMessage", recipientId, message).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    }
});