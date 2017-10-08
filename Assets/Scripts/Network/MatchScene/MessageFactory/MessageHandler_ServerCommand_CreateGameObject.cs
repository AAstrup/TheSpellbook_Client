using ClientServerSharedGameObjectMessages;
using UnityEngine;

public class MessageHandler_ServerCommand_CreateGameObject : IMessageHandlerCommandClient
{
    public void Handle(object objData)
    {
        var data = (Message_ServerCommand_CreateGameObject)objData;
        bool isMine = UnityConfig.GetPersistentDataContainer().persistentData.PlayerInfo.GUID == data.OwnerGUID;
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (isMine)
            cube.transform.position = new Vector3(2, 1, 2);
        else
            cube.transform.position = new Vector3(-2, 1, -2);

        Debug.Log("Spawning " + data.Type.ToString() + ", for me? is " 
            + (UnityConfig.GetPersistentDataContainer().persistentData.PlayerInfo.GUID == data.OwnerGUID));
    }
}