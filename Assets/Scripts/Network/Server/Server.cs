/// <summary>
/// The class that starts everything up for the server
/// Everything is started in its constructor
/// </summary>
public class Server
{
    public Server_GameInfo gameInfo;
    public Server_ClientManager clientManager;
    public Server_MessageHandler messageHandler;
    public Server_Connection connection;
    public Server_MessageReciever messageReciever;
    public Server_MessageSender messageSender;

    public Server()
    {
        gameInfo = new Server_GameInfo();
        messageHandler = new Server_MessageHandler(this);
        clientManager = new Server_ClientManager(this);
        connection = new Server_Connection(clientManager);
        messageReciever = new Server_MessageReciever(connection,messageHandler);
        messageSender = new Server_MessageSender(clientManager);
    }

    /// <summary>
    /// This method checks and handles messages from the client
    /// </summary>
    public void Update()
    {
        messageReciever.CheckForPlayerRequestMessages();
    }
}