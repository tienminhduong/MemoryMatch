using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMaganement : MonoBehaviour
{
    [SerializeField] GameObject settingMenu;

    private void Start()
    {
        settingMenu.SetActive(false);
    }


    public void LoadMainScene()  // PvP mode
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettingMenu()
    {
        settingMenu.SetActive(true);
    }

    public void CloseSettingMenu()
    { settingMenu.SetActive(false);}

    public void LoadHome() // start scene
    {
        SceneManager.LoadScene(0);
    }   
}
