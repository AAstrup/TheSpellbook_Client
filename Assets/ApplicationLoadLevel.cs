using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationLoadLevel : MonoBehaviour {

    public void LoadScene_MM()
    {
        SceneManager.LoadScene("MMScene");
    }
}
