public enum MessageType
{
    ProcessParam,
    ExecuteDelayDelegate,
    None,
}


public class MessageData
{
    private MessageType messageType;
    private int size;

    public MessageData() {
        this.messageType = MessageType.None;
        this.size = 0;
    }

    public MessageData(MessageType messageType, int size) {
        this.messageType = messageType;
        this.size = size;
    }

    public MessageType MessageType => messageType;
    public virtual void SendInfo() { }
}
