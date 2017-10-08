internal class MessageHandler_Command_PlayerMovementUpdate : IMessageHandlerCommandClient
{
    public void Handle(object objData)
    {
        var data = (Message_Command_PlayerMovementUpdate)objData;
        InGameWrapper.instance.playersWrapper.UpdateEnemy(data);
    }
}