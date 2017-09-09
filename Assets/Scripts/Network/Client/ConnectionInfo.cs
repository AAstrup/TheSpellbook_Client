using System.Net;

/// <summary>
/// Class contains connection information for setting up a match
/// Send to client when creating a match so that they can connect to the match
/// </summary>
public class ConnectionInfo
{
    public string Ip;
    public int Port;
    public ConnectionInfo(int Port, string Ip)
    {
        this.Port = Port;
        this.Ip = Ip;
    }
    public static ConnectionInfo MatchMakerConnectionInfo()
    {
        return new ConnectionInfo(AppConfig.PortOfMatchMaker,AppConfig.IpOfMatchMaker);
    }
}