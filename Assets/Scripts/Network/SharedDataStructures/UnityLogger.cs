using UnityEngine;

internal class UnityLogger : ILogger
{
    public UnityLogger()
    {
    }

    public void DebugLog(string s)
    {
        //if (Application.isEditor)
        //    Debug.Log("Logger - " + s);
        //BUILDDEBUGTEXT.instance.Log(s);
    }

    public void Log(string s)
    {
        //if (Application.isEditor)   
        //    Debug.Log("Logger - " + s);
        //BUILDDEBUGTEXT.instance.Log(s);
    }
}