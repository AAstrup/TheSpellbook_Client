using UnityEngine;
using UnityEngine.UI;

public class InGame_GUIContainer
{
    private string prefix;
    public Text nameText;

    public InGame_GUIContainer(string prefix)
    {
        this.prefix = prefix;
        nameText = GameObject.Find(prefix + "NameText").GetComponent<Text>();
    }
}