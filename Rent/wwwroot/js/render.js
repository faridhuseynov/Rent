function renderPage() {

    let chatters = {
        sender: sender,
        recipient: "@User.Identity.Name"
    };

    let template = $(".msg_history").html;
    var rendered = Mustache.render(template, chatters);
    console.log(rendered);
    document.getElementById("msgHistory").html = rendered;
}