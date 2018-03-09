using ClientServerSharedGameObjectMessages;
using System;
using System.Collections.Generic;

internal class MessageHandlerFactory : Dictionary<Type, IMessageHandlerCommandClient>
{

    public static Dictionary<Type, IMessageHandlerCommandClient> CreateMessageHandlerExpansion()
    {
        var dictionary = new Dictionary<Type, IMessageHandlerCommandClient>();
        Add(dictionary, new MessageHandler_ServerResponse_PlayerMovementUpdate());
        Add(dictionary, new MessageHandler_ServerCommand_CreateGameObject());
        Add(dictionary, new MessageHandler_ServerResponse_CreateSpellWithDirection()); 
        Add(dictionary, new MessageHandler_ServerResponse_CreateSpellInStaticPosition());
        Add(dictionary, new MessageHandler_ClientCommand_SpellHit());
        Add(dictionary, new MessageHandler_ClientCommand_GameObjectDied());
        Add(dictionary, new MessageHandler_ServerResponse_RoundEnded());
        Add(dictionary, new MessageHandler_ServerCommand_StartNewRound());
        Add(dictionary, new MessageHandler_ServerCommand_PlayerLeft());
        Add(dictionary, new MessageHandler_ServerCommand_StartGame());
        return dictionary;
    }

    static void Add(Dictionary<Type, IMessageHandlerCommandClient> dicionary,IMessageHandlerCommandClient handler)
    {
        dicionary.Add(handler.GetMessageTypeSupported(), handler);
    }
}