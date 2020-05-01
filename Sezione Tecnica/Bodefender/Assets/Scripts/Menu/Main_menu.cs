﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu : MonoBehaviour
{
   public Main_menu main_menù;
  
   public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Quit()
    {
        Debug.Log("Arrivesos");
        Application.Quit();
    }
    public void Retry()
    { 
            SceneManager.LoadScene(Player.DeathScene);
    }
    public void BToM()
    {
        
        SceneManager.LoadScene(1);
    }
    public void Training()
    {
        SceneManager.LoadScene(7);
    }
    
   
   
}
