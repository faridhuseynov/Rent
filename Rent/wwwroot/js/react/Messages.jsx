import UsersBox from './UsersBox.jsx';
import ChatBox from './ChatBox.jsx';
import NewMessage from './NewMessage.jsx';

class Messages extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            messagesList: [],
            currentUser: "",
            activeUser: "",
            activeMessageThread: [],
            activeMessageIndex: "0",
            newMessage:""
        };
        this.activeMessageThreadHandler = this.activeMessageThreadHandler.bind(this);
        this.messageSendHandler = this.messageSendHandler.bind(this);
        this.messageTypeHandler = this.messageTypeHandler.bind(this);
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
                //to get the first message thread in the inbox
                if (data.sortedMessages != null) {
                    var user =
                        (this.state.currentUser == data.sortedMessages[0].senderUsername
                            ? data.sortedMessages[0].recipientUsername : data.sortedMessages[0].senderUsername);
                    this.setState({
                        activeUser: user
                    }, () => {
                        this.activeMessageThreadHandler(this.state.activeUser, 0);
                    }
                    );
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

    activeMessageThreadHandler(userName, messageIndex) {
        var messages = [this.state.currentUser,
        ...this.state.messagesList];
        var sortedMessages = messages.filter
            (m => m.senderUsername == userName || m.recipientUsername == userName)
            .reverse();
        this.setState({
            activeMessageThread: sortedMessages,
            activeMessageIndex: messageIndex
        });
    }

    messageSendHandler() {
        var message = this.state.newMessage;
        var sender = this.state.currentUser;
        var messages = [...this.state.activeMessageThread];
        var dateSent = Date.now();
        var newMessage = {
            "senderUsername": sender,
            "content": message,
            "messageSent":dateSent
        };
        messages.push(newMessage);
        this.setState({
            activeMessageThread: [...messages],
            newMessage:""
        });
    }
        
    messageTypeHandler(event) {
        var message = event.target.value;
        this.setState({
            newMessage:message
        })
    }
    render() {
        return (
            <div className="messaging">
                <div className="inbox_msg">
                    <UsersBox messages={this.state.messagesList}
                        currentUser={this.state.currentUser}
                        activeIndex={this.state.activeMessageIndex}
                        newUserClicked={this.activeMessageThreadHandler} />
                    <div className="inbox-view-block">
                        <ChatBox messages={this.state.activeMessageThread}
                            user={this.state.currentUser} />
                        <NewMessage addmessage={this.messageSendHandler}
                            message={this.state.newMessage} messageTyped={this.messageTypeHandler}
                            messageSend={this.messageSendHandler}/>
                    </div>
                </div>
            </div>
        );
    }
}

ReactDOM.render(<Messages />, document.getElementById('root'));


