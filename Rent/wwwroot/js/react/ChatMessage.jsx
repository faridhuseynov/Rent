const chatMessage = props => {
    var receivedMessageClasses = {
        msgBlock: "incoming_msg",
        imgBlock: "incoming_msg_img",
        message: "received_msg",
        msg: "received_withd_msg"
    }

    var sentMessageClasses = {
        msgBlock: "outgoing_msg",
        imgBlock: "outgoing_msg_img",
        message: "sent_msg",
        msg: "sent_withd_msg"
    }
    var classes = (props.message.senderUsername == props.caller
        ? sentMessageClasses
        : receivedMessageClasses);
    return (
        <div className={classes.msgBlock}>
            <div className={classes.imgBlock}>
                <img src={"/Images/Users/" + props.message.senderMainPhotoUrl}
                    alt="" />
            </div>
            <div className={classes.message}>
                <div className={classes.msg}>
                    <p>
                        {props.message.content}
                    </p>
                    <span className="time_date">
                        {(new Date(props.message.messageSent)).toLocaleString()}
                    </span>
                </div>
            </div>
        </div>
    );
}

export default chatMessage;