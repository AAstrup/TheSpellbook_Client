using ClientServerSharedGameObjectMessages;
using System;
using System.Collections.Generic;

internal class MessageHandlerFactory : Dictionary<Type, IMessageHandlerCommandClient>
{
    public static Dictionary<Type, IMessageHandlerCommandClient> CreateMessageHandlerExpansion()
    {
        var dictionary = new Dictionary<Type, IMessageHandlerCommandClient>();
        dictionary.Add(typeof(Message_Command_PlayerMovementUpdate), new MessageHandler_Command_PlayerMovementUpdate());
        dictionary.Add(typeof(Message_ServerCommand_CreateGameObject), new MessageHandler_ServerCommand_CreateGameObject());
        return dictionary;
    }
}