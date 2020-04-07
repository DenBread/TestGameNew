using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorkWothBtn : MainClass {

    public void SwitchLal(int lvl)
    {
        SceneManager.LoadScene(lvl);
    }

    public void ExitGame()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
