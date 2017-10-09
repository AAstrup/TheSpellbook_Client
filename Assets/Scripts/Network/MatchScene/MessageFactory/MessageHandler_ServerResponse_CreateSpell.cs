using ClientServerSharedGameObjectMessages;

public class MessageHandler_ServerResponse_CreateSpell : IMessageHandlerCommandClient
{
    public void Handle(object objData)
    {
        var data = (Message_ServerResponse_CreateSpell)objData;
        bool isMine = UnityConfig.GetPersistentDataContainer().persistentData.PlayerInfo.GUID == data.request.ownerGUID;
        InGameWrapper.instance.spellsWrapper.SpawnSpell(data, isMine);
    }
}