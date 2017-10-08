internal class Message_Command_Handler_PlayerMovementUpdate : IMessageHandlerCommandClient
{
    public void Handle(object objData)
    {
        var data = (Message_Command_PlayerMovementUpdate)objData;
        InGameWrapper.instance.playersWrapper.UpdateEnemy(data);
    }
}