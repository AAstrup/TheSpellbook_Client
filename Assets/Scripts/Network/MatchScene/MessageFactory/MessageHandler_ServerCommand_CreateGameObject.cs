using ClientServerSharedGameObjectMessages;
using UnityEngine;

public class MessageHandler_ServerCommand_CreateGameObject : IMessageHandlerCommandClient
{
    public void Handle(object objData)
    {
        var data = (Message_ServerCommand_CreateGameObject)objData;
        bool isMine = UnityConfig.GetPersistentDataContainer().persistentData.PlayerInfo.GUID == data.OwnerGUID;
        InGameWrapper.instance.playersWrapper.SpawnPlayer(data,isMine);

        Debug.Log("Spawning " + data.Type.ToString() + ", for me? is " 
            + (UnityConfig.GetPersistentDataContainer().persistentData.PlayerInfo.GUID == data.OwnerGUID));
    }
}