using ClientServerSharedGameObjectMessages;
using System;

internal class MessageHandler_ClientCommand_GameObjectDied : IMessageHandlerCommandClient
{
    public Type GetMessageTypeSupported()
    {
        return typeof(Message_ClientCommand_GameObjectDied);
    }

    public void Handle(object objData)
    {
        var data = (Message_ClientCommand_GameObjectDied)objData;
        InGameWrapper.instance.playersWrapper.GetPlayerByGUID(data.DyingPlayerGUID).Die();
        if (data.KillerPlayerGUID.HasValue)
        {
            InGameWrapper.instance.playersWrapper.GetPlayerByGUID(data.KillerPlayerGUID.Value).playerScoreController.AddKill(data.DyingPlayerGUID);
        }
        InGameWrapper.instance.playersWrapper.GetPlayerByGUID(data.DyingPlayerGUID).playerScoreController.AddDeath(data.KillerPlayerGUID);

        int aliveOnlinePlayers =0;
        foreach (var onlinePlayer in InGameWrapper.instance.playersWrapper.GetOnlyOnlinePlayers())
        {
            if (onlinePlayer.IsAlive())
                aliveOnlinePlayers++;
        }
        if (aliveOnlinePlayers <= 1)
        {
            Reset();
        }
    }

    private void Reset()
    {
        var msg = new Message_ClientRequest_RoundEnded()
        {
        };
        Match_DotNetAdapter.instance.Send(msg);
    }
}