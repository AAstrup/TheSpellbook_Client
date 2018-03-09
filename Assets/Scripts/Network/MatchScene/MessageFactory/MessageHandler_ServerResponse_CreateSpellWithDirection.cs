using System;
using ClientServerSharedGameObjectMessages;

public class MessageHandler_ServerResponse_CreateSpellWithDirection : IMessageHandlerCommandClient
{
    public Type GetMessageTypeSupported()
    {
        return typeof(Message_ServerResponse_CreateSpellWithDirection);
    }

    public void Handle(object objData)
    {
        var data = (Message_ServerResponse_CreateSpellWithDirection)objData;
        bool isMine = UnityConfig.GetPersistentDataContainer().persistentData.PlayerInfo.GUID == data.request.ownerGUID;
        InGameWrapper.instance.spellsWrapper.SpawnSpell(data, isMine);
    }
}