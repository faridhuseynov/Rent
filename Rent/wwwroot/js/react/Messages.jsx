class App extends React.Component {
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


