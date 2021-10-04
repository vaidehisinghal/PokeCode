using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class welcomeScript : MonoBehaviour
{
    public void onButton()
    {
        SceneManager.LoadScene("INSTRUCTIONS");
    }

    public void onInstruction()
    {
        SceneManager.LoadScene("Login");
    }
}
