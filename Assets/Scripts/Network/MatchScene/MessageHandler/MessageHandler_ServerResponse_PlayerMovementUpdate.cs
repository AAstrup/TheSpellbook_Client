using System;
using UnityEngine;

public class MessageHandler_ServerResponse_PlayerMovementUpdate : IMessageHandlerCommandClient
{

    public Type GetMessageTypeSupported()
    {
        return typeof(Message_ServerResponse_PlayerMovementUpdate);
    }

    public void Handle(object objData)
    {
        var data = (Message_ServerResponse_PlayerMovementUpdate)objData;
        var player = InGameWrapper.instance.playersWrapper.GetPlayerByGUID(data.GMJGUID);
        Vector3 targetPos = new Vector3(data.moveTargetXPos, player.GetGmj().transform.position.y, data.moveTargetZPos);
        player.SetTargetPos(targetPos);
        Vector3 currentPos = new Vector3(data.currentXPos, player.GetGmj().transform.position.y, data.currentZPos);
        player.SetCurrentPos(currentPos);
        float timeDifference = (float)((InGameWrapper.instance.clockWrapper.GetTimeInMiliSeconds() - data.TimeStartedMovingWithPing) / 1000);
        player.Update(timeDifference);
    }
}