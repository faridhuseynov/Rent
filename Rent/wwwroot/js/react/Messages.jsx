class App extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            messagesList: [],
            currentUser:""
        };
    }

    componentDidMount() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', "/Messages/GetMessages", true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            console.log(data);
            this.setState({
                messagesList: data.sortedMessages,
                currentUser:data.currentUser
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
                <UsersBox messages={this.state.messagesList} />
            </div>
        );
    }
}


const UsersBox = props => {
    return (
        <div className="inbox_msg">
            <div className="headind_srch">
                <div className="srch_bar">
                    <div className="stylish-input-group">
                        <input type="text" className="search-bar" placeholder="Search" />

                        <button className="btn" type="button"> <i className="fa fa-search" aria-hidden="true"></i> </button>
                        <div className="inbox_chat">
                            {props.messages.map(message => {
                                return <UserBox key={message.id} content={message.content}
                                    photo={message.senderMainPhotoUrl} name={message.senderUsername}
                                    date={message.messageSent}
                                />
                            })
                            }

                        </div>
                    </div>
                </div>
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
        <div className="chat_list">
            <div className="chat_people">
                <div className="chat_img">
                    
                    <a>
                        <img src={"/Images/Users/"+props.photo} alt="" />
                    </a>
                </div>
                <div className="chat_ib">
                    <h5>{props.name} <span className="chat_date">{props.date}</span></h5>
                        <p>
                            {props.content}
                        </p>
                    </div>
            </div>
        </div>
    );
}
ReactDOM.render(<App />, document.getElementById('root'));


