import UserBox from './UserBox.jsx';

const usersBox = props => {
    var previousUser = "";
    return (

        <div className="headind_srch">
            <div className="srch_bar">
                <div className="stylish-input-group">
                    <input type="text" className="search-bar" placeholder="Search" />

                    <button className="btn" type="button"> <i className="fa fa-search" aria-hidden="true"></i> </button>
                    <div className="inbox_chat">
                        {props.messages.map((message, i) => {
                            var username =
                                (props.currentUser == message.senderUsername
                                    ? message.recipientUsername : message.senderUsername);
                            var photo =
                                (props.currentUser == message.senderUsername
                                    ? message.recipientMainPhotoUrl : message.senderMainPhotoUrl);
                            if (previousUser != username) {
                                previousUser = username;
                                return <UserBox key={message.id} content={message.content}
                                    photo={photo} name={username} index={i} active={props.activeIndex}
                                    userBoxClicked={props.newUserClicked}
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

export default usersBox;