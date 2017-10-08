using System;
using System.Collections.Generic;

internal class MessageHandlerFactory : Dictionary<Type, IMessageHandlerCommandClient>
{
    public static Dictionary<Type, IMessageHandlerCommandClient> CreateDictionary()
    {
        var dictionary = new Dictionary<Type, IMessageHandlerCommandClient>();
        dictionary.Add(typeof(Message_Command_PlayerMovementUpdate), new Message_Command_Handler_PlayerMovementUpdate());
        return dictionary;
    }
}