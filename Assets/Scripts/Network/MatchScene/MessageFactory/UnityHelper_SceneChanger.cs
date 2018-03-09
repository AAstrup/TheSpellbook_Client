using UnityEngine;
using UnityEngine.SceneManagement;

public class UnityHelper_SceneChanger : MonoBehaviour
{
    public void LoadScene_MainMenu()
    {
        SceneManager.LoadScene("MMScene");
    }
}