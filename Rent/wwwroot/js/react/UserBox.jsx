const userBox = props => {
    return (
        <a onClick={() => props.userBoxClicked(props.name, props.index)}>
            <div className={(props.index == props.active ? "chat_list active_chat" : "chat_list")}>
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

export default userBox;