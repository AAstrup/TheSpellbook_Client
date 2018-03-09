using ClientServerSharedGameObjectMessages;
using System;

internal class MessageHandler_ServerCommand_StartGame : IMessageHandlerCommandClient
{
    public Type GetMessageTypeSupported()
    {
        return typeof(Message_ServerComand_StartGame);
    }

    public void Handle(object objData)
    {
        var data = (Message_ServerComand_StartGame)objData;
        InGameWrapper.instance.resetLogic.QueueNewRound(data.timeRemainingBeforeStart, "First round in ");
    }
}