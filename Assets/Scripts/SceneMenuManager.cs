using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneMenuManager: MonoBehaviour
{    

    //private static SceneMenuManager instance;

    //public static SceneMenuManager Instance { get; private set; }

    //private void Awake()
    //{
    //    if (instance != null && instance != this)
    //    {
    //        Destroy(this);

    //    }
    //    else
    //    {
    //        instance = this;
    //    }
    //}

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    
    public void QuitButton()
    {
        Application.Quit();
    }



}
