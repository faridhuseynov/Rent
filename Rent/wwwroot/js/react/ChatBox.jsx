import ChatMessage from './ChatMessage.jsx';

const chatBox = props => {
    return (
        <div className="mesgs">
            {props.messages.map(msg => {
                return <ChatMessage key={msg.id} message={msg} caller={props.user} />
            })}
        </div>
    );
}

export default chatBox;