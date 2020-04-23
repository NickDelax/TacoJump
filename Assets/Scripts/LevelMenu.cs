using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public void LevelGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void LevelsGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

public void MenuGame()

  {
    Application.LoadLevel("Menu");
  }
}