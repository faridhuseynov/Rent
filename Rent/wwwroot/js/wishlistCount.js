"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/wishlistCountHub").build();

var currentCount = parseInt(document.getElementById("wishListCount").value);

connection.start().then(function () {
}).catch(function (error) {
    return console.error(error.toString());
});

connection.on("ReceiveUpdatedCount", function (updatedCount) {
    currentCount = updatedCount;

    //check with Sasha regarding the opacity set up

    var countElement = document.getElementById("wishListCount");
    countElement.value = updatedCount;
    if (updatedCount==0) {
        countElement.style.opacity="0";
    } else {
        countElement.style.opacity="1";
    }

})

var wishListIcon = document.getElementById("wishlistIcon");
if (wishListIcon) {
    document.getElementById("wishlistIcon").addEventListener("click", function (event) {

        // worth to note that once the heart button clicked it first will change the class
        // and then the event will be checked whether it has the fa-heart-o class or not
        // this means that if the product wasn't in wishlist it should have (fa-heart-o) and once clicked
        // it should give the true value for the element. However, in reality it will first change to 
        // fa-heart and then will come here as an event.target and as a result the element will be false
        var element = event.target.children[0].classList.contains("fa-heart-o");

        if (element == false) {
            connection.invoke("IncreaseFavoriteCount", currentCount);
        } else {
            connection.invoke("ReduceFavoriteCount", currentCount);
        }
    })
}

var wishHeart = document.getElementsByClassName("wish-heart"); 
if (wishHeart) {
    for (var i = 0; i < wishHeart.length; i++) {
        wishHeart[i].addEventListener("click", function () {
            connection.invoke("ReduceFavoriteCount", currentCount)
        });
    }
}
