using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void GoToFisrtLevel()
    {
        SceneManager.LoadScene("Level One");
    }
}
