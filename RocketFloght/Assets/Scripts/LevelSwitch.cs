using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitch : MainClass
{
    [SerializeField]
    private byte idLvl;

    protected override void Start()
    {
        idLvl = (byte)SceneManager.GetActiveScene().buildIndex;
    }

    protected override void Update()
    {
        LevelSwitches();
    }

    private void LevelSwitches()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ++idLvl;
            SceneManager.LoadScene(idLvl);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            --idLvl;
            SceneManager.LoadScene(idLvl);
        }
    }
}
