using UnityEngine;

public class InGame_GUIContainer
{
    private string prefix;
    public GameObject nameText;

    public InGame_GUIContainer(string prefix)
    {
        this.prefix = prefix;
        nameText = GameObject.Find(prefix + "NameText");
    }
}