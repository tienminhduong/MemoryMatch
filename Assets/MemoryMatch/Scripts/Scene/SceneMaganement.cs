using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMaganement : MonoBehaviour
{
    [SerializeField] GameObject settingMenu;
    [SerializeField] GameObject gamerulePanel;

    private void Start()
    {
        settingMenu.SetActive(false);
        gamerulePanel.SetActive(false);
    }



    public void OpenSettingMenu()
    {
        settingMenu.SetActive(true);
    }

    public void CloseSettingMenu()
    { settingMenu.SetActive(false); }


    public void OpenGamerulePanel()
    {
        gamerulePanel.SetActive(true);
    }
    public void CloseGamerulePanel()
    {
        gamerulePanel.SetActive(false);
    }

    public void LoadMainScene()  // PvP mode
    {
        SceneManager.LoadScene(1);
    }
    public void LoadHome() // start scene
    {
        SceneManager.LoadScene(0);
    }
    public void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
