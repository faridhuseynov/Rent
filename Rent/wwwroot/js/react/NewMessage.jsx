const newMessage = props => {
    return (
        <div className="type_msg">
            <div className="input_msg_write">
                <textarea value={props.message} type="text" rows="5" className="write_msg"
                    onChange={props.messageTyped}
                    placeholder="Type a message"></textarea>
                <button className="msg_send_btn" type="button" id="sendButton"
                    onClick={props.messageSend}>
                    <i className="fa fa-paper-plane-o"
                        aria-hidden="true"></i>
                </button>
            </div>
        </div>
    );
}

export default newMessage;