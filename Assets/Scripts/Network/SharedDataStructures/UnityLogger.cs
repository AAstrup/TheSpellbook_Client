using UnityEngine;

internal class UnityLogger : ILogger
{
    public UnityLogger()
    {
    }

    public void Log(string s)
    {
        Debug.Log("Logger - " + s);
    }
}