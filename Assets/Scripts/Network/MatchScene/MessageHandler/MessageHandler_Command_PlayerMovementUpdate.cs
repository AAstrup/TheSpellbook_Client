using UnityEngine;

internal class MessageHandler_Command_PlayerMovementUpdate : IMessageHandlerCommandClient
{
    public void Handle(object objData)
    {
        var data = (Message_Command_PlayerMovementUpdate)objData;
        var player = InGameWrapper.instance.playersWrapper.GetPlayerByGUID(data.GMJGUID);
        Vector3 targetPos = new Vector3(data.moveTargetXPos, player.GetGmj().transform.position.y, data.moveTargetZPos);
        player.SetTargetPos(targetPos);
        Vector3 currentPos = new Vector3(data.currentXPos, player.GetGmj().transform.position.y, data.currentZPos);
        player.SetCurrentPos(currentPos);
    }
}