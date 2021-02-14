class App extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            messagesList: []
        };
    }

    componentDidMount() {
        console.log("in function");
        const xhr = new XMLHttpRequest();
        xhr.open('get',"/Messages/GetMessages", true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ messagesList: data });
            console.log(data);
            console.log(this.state.messagesList);
        };
        xhr.send();
        //$.ajax({
        //    method: "GET",
        //    url: "/Messages/GetMessages",
        //    success: function (result) {
        //        console.log("no error");
        //        var jsonResult = JSON.parse(result);
        //        console.log(jsonResult);
        //    },
        //    error: function (error) {
        //        console.log("error fired");
        //        console.log(error);
        //    }
        //});
    }
    render() {
        return (
            <div className="messaging">
                <UsersBox />
            </div>
        );
    }
}


const UsersBox = props => {
    return (
        <div className="inbox_msg">
            <UserSearchBox />

        </div>
        );
}

const UserSearchBox = props => {
    return (
        <div className="headind_srch">
            <div className="srch_bar">
                <div className="stylish-input-group">
                    <input type="text" className="search-bar" placeholder="Search" />

                    <button className="btn" type="button"> <i className="fa fa-search" aria-hidden="true"></i> </button>
                    <div className="inbox_chat">

                    </div>
                </div>
            </div>
        </div>
    );
}

//const UserBox = props => {
//    return (

//    );
//}
ReactDOM.render(<App />, document.getElementById('root'));


