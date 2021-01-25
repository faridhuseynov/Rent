"use strict";


var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

// receive message
connection.on("ReceivePrivateMessage", function (message) {

    // //id for user with whom currently chatting, active user
    //var senderId = $("#activeUser").val();

    //// logged in user
    //var recipientId = $("#recipientUser").val();

    //if (recipient == recipientId && sender == senderId) {

    //}
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

        // username for user with whom currently chatting, active user
        var recipUserName = $("#activeUser").val();

        // get connectionId for this user
        $.ajax({
            method: "GET",
            url: "/Messages/GetConnection",
            data: {
                "userName": recipUserName
            },
            success: function (recipient) {
                if (recipient != null) {
                    connection.invoke("SendPrivateMessage", recipient, message).catch(function (err) {
                        console.log('test');
                            return console.error(err.toString());
                        });
                }
            },
            error: function (error) {
                console.log(error);
            }
        })


        // logged in user
        var senderId = $("#recipientUser").val();

        event.preventDefault();
    }
});

connection.on("UserConnected", function (connectionId) {
    $.ajax({
        method: "POST",
        url: "/Messages/AddConnection",
        data: {
          "connectionId": connectionId,

        },
        success: function (result) {
            //console.log("user: "+result+" with id: ["+connectionId+"] added")
        },
        error: function (error) {
            console.log(error);
        }
    })
    //console.log(connectionId);
});

 /*



*/