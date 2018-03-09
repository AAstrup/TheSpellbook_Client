using ClientServerSharedGameObjectMessages;
using System;

public class MessageHandler_ServerResponse_CreateSpellInStaticPosition : IMessageHandlerCommandClient
{
    public Type GetMessageTypeSupported()
    {
        return typeof(Message_ServerResponse_CreateSpellInStaticPosition);
    }

    public void Handle(object objData)
    {
        var data = (Message_ServerResponse_CreateSpellInStaticPosition)objData;
        bool isMine = UnityConfig.GetPersistentDataContainer().persistentData.PlayerInfo.GUID == data.request.ownerGUID;
        InGameWrapper.instance.spellsWrapper.SpawnSpell(data, isMine);
    }
}