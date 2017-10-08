using System;
using System.Collections.Generic;

public class UnityClientConfig : IClientConfig
{
    Dictionary<string, string> values;
    public UnityClientConfig()
    {
        values = new Dictionary<string, string>();
        values.Add("IpOfMatch", "127.0.0.1");
        values.Add("IpOfMatchMaker", "127.0.0.1");
        values.Add("PortOfMatchMaker", "61497");
        values.Add("PortOfLocalMatch", "60050");
    }

    /*
     * Keys expected with default values
    <add key="IpOfMatch" value="127.0.0.1"></add>
    <add key="IpOfMatchMaker" value="127.0.0.1"></add>
    <add key="PortOfMatchMaker" value="61497"></add>
     */

    public int GetInt(string key)
    {
        return Int32.Parse(values[key]);
    }

    public string GetString(string key)
    {
        return values[key];
    }
}