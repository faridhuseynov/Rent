"use strict";

//var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

//// id for user with whom currently chatting, active user
//var senderUserId = $("#activeUser").val();

//// logged in user
//var myId = $("#recipientUser").val();

// receive message
connection.on("ReceivePrivateMessage", function (recipient, sender, message) {

     //id for user with whom currently chatting, active user
    var senderId = $("#activeUser").val();

    // logged in user
    var recipientId = $("#recipientUser").val();

    if (recipient == recipientId && sender == senderId) {

        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

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

        var li = document.createElement("li");
        li.textContent = msg;
        document.getElementById("msgHistory").appendChild(li);
        $("#msgHistory").scrollTop($("#msgHistory")[0].scrollHeight);

    }
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

//send message
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

        // id for user with whom currently chatting, active user
        var recipientId = $("#activeUser").val();

        // logged in user
        var senderId = $("#recipientUser").val();

        connection.invoke("SendPrivateMessage", senderId, recipientId, message).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    }
});

 /*



*/