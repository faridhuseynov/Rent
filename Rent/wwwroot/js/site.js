//import { signalR } from "../lib/aspnet/signalr/dist/browser/signalr";

// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(() => {
    let connection = new signalR.HubConnectionBuilder().withUrl("/hubService").build();

    connection.on("updateMessagesCount", function (userId, messageCount) {
        //loadData();
        if (messageCount==0) {
            $("#wishListCount").attr("hidden", true)

        } else {
            $("#wishListCount").removeAttr("hidden").text(result);

        }
    })
    connection.start()

    loadData();

    //function loadData() {
    //    //connection.invoke("")

    //    $.ajax({
    //        url: '/Messages/GetMessagesCount',
    //        method: 'GET',
    //        success: function (result) {
    //            if (result>0) {
    //                $("#wishListCount").removeAttr("hidden").text(result);
    //            } else {
    //                $("#wishListCount").attr("hidden",true)
    //            }
    //        },
    //        error: function (err) {
    //            console.log(err);
    //        }
    //    });
    //}


});