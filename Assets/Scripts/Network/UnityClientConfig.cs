using System;
using System.Collections.Generic;

public class UnityClientConfig : IClientConfig
{
    public static UnityClientConfig instance;
    private IClock clock;
    Dictionary<string, string> values;
    public UnityClientConfig(IClock clock)
    {
        instance = this;
        this.clock = clock;
        values = new Dictionary<string, string>();
        string address = "127.0.0.1";
        values.Add("IpOfMatch", address);
        values.Add("IpOfMatchMaker", address);
        values.Add("PortOfMatchMaker", "61500");
        values.Add("IpOfDB", address);
        values.Add("PortOfDB", "61499");
        //values.Add("MessageSender_FakeDelayInMiliSeconds", "0");
    }

    internal void SetValue(string key, string value)
    {
        if (!values.ContainsKey(key))
            values.Add(key,value);
        else
            values[key] = value;
    }

    public IClock GetClock()
    {
        return clock;
    }

    /*
     * Keys expected with default values
    <add key="IpOfMatch" value="127.0.0.1"></add>
    <add key="IpOfMatchMaker" value="127.0.0.1"></add>
    <add key="PortOfMatchMaker" value="61497"></add>

    Optional
    <add key="MessageSender_FakeDelayInMiliSeconds" value="0"></add> 
     */

    public int GetInt(string key)
    {
        if (!values.ContainsKey(key))
            return 0;
        return Int32.Parse(values[key]);
    }

    public string GetString(string key)
    {
        if (!values.ContainsKey(key))
            return "";
        return values[key];
    }
}