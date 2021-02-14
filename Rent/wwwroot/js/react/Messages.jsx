class App extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            messagesList: [],

        };
    }

    componentDidMount() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', "/Messages/GetMessages", true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            console.log(data);
            this.setState({ messagesList: data });
            console.log(this.state.messagesList);
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
                <UsersBox messages={ this.state.messagesList}/>
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
                                return <UserBox key={ message.id } content={message.content} />
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
        <div>{props.content}</div>
    );
}
ReactDOM.render(<App />, document.getElementById('root'));


