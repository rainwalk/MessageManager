using System;

public class MessageProcessExecuteDelay : MessageData
{
    public Action ActionToExecute { get; }
    public float DelayTime { get; }

    public MessageProcessExecuteDelay() : base(MessageType.ExecuteDelayDelegate, 0) { }
    public MessageProcessExecuteDelay(Action actionToExecute, float delayTime) : base(MessageType.ExecuteDelayDelegate, sizeof(float)) {
        ActionToExecute = actionToExecute;
        DelayTime = delayTime;
    }
}
