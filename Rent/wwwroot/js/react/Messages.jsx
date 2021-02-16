﻿class App extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            messagesList: [],
            currentUser: "",
            activeUser:"",
            activeMessageThread:[]
        };
        this.activeMessageThreadHandler = this.activeMessageThreadHandler.bind(this)
    }

    activeMessageThreadHandler(userName) {
        var messages = [this.state.currentUser,
            ...this.state.messagesList];
        var sortedMessages = messages.filter
            (m => m.senderUsername == userName || m.recipientUsername == userName)
            .reverse();
        this.setState({
            activeMessageThread: sortedMessages
        });
    }
    componentDidMount() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', "/Messages/GetMessages", true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            console.log(data);
            this.setState({
                messagesList: data.sortedMessages,
                currentUser: data.currentUser
            }, () => {
                    //this still doesn't work
                if (data.sortedMessages != null) {
                    var user =
                        (this.state.currentUser == data.sortedMessages[0].senderUsername
                            ? data.sortedMessages[0].recipientUsername : data.sortedMessages[0].senderUsername);
                    this.setState({
                        activeUser: user
                    })
                }
            });
        };
        xhr.send();
        //$.ajax({
        //    method: "GET",
        //    url: "/Messages/GetMessages",
        //    success: function (result) {
        //        console.log("no error");
        //        //this.setState({
        //        //    messagesList: result
        //        //});
        //    },
        //    error: function (error) {
        //        console.log("error fired");
        //        console.log(error);
        //    }
        //});
        //this.setState(
        //    {
        //        messagesList: messages
        //    }, () => console.log(this.state.messagesList));
        //console.log(this.state.messagesList);
    }
    render() {
        return (
            <div className="messaging">
                <div className="inbox_msg">
                    <UsersBox messages={this.state.messagesList}
                        currentUser={this.state.currentUser}
                        newUserClicked={this.activeMessageThreadHandler}/>
                    <ChatBox messages={this.state.activeMessageThread}
                        user={this.state.currentUser} />
                </div>
            </div>
        );
    }
}

const UsersBox = props => {
    var previousUser = "";
    return (

            <div className="headind_srch">
                <div className="srch_bar">
                    <div className="stylish-input-group">
                        <input type="text" className="search-bar" placeholder="Search" />

                        <button className="btn" type="button"> <i className="fa fa-search" aria-hidden="true"></i> </button>
                        <div className="inbox_chat">
                            {props.messages.map(message => {
                                var username =
                                    (props.currentUser == message.senderUsername
                                        ? message.recipientUsername : message.senderUsername);
                                var photo =
                                    (props.currentUser == message.senderUsername
                                        ? message.recipientMainPhotoUrl : message.senderMainPhotoUrl);
                                if (previousUser != username) {
                                    previousUser = username;
                                    return <UserBox key={message.id} content={message.content}
                                        photo={photo} name={username}
                                        userBoxClicked={props.newUserClicked}
                                        //date={message.messageSent}
                                    />
                                }
                            })
                            }
                        </div>
                    </div>
                </div>
        </div>
    );
}

const ChatBox = props => {
    return (
        <div className="mesgs">
            {props.messages.map(msg => {
                return <ChatMessage key={msg.id} message={msg} caller={props.user}/>
            })}
        </div>
    );
}
const ChatMessage = props => {
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
                <img src={"/Images/Users/"+props.message.senderMainPhotoUrl}
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

const newMessage = props => {
    return (
        //<div class="msg_history" id="msgHistory">

        //    @*being processed by the partial view*@

        //    </div>

        <div class="type_msg">
            <div class="input_msg_write">
                <textarea type="text" rows="5" class="write_msg" placeholder="Type a message"></textarea>
                <button class="msg_send_btn" type="button" id="sendButton">
                    <i class="fa fa-paper-plane-o"
                        aria-hidden="true"></i>
                </button>
            </div>
        </div>
    );
}

//const UserSearchBox = props => {
//    return (

//    );
//}

const UserBox = props => {
    return (
        <a onClick={()=> props.userBoxClicked(props.name)}>
            <div className="chat_list">
                <div className="chat_people">
                    <div className="chat_img">

                        <img src={"/Images/Users/" + props.photo} alt="" />
                    </div>
                    <div className="chat_ib">
                        <h5>{props.name} <span className="chat_date">{props.date}</span></h5>
                        <p>
                            {props.content}
                        </p>
                    </div>
                </div>
            </div>
        </a>
    );
}
ReactDOM.render(<App />, document.getElementById('root'));


