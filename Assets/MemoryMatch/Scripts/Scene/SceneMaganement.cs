using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMaganement : MonoBehaviour
{
    public static SceneMaganement instance;
    [SerializeField] GameObject settingMenu;
    [SerializeField] GameObject gamerulePanel;
    [SerializeField] GameObject botPrefab;
    [SerializeField] private bool isPaused = false;
    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        settingMenu.SetActive(false);
        gamerulePanel.SetActive(false);
    }



    public void OpenSettingMenu()
    {
        isPaused = true;
        settingMenu.SetActive(true);
    }

    public void CloseSettingMenu()
    {
        isPaused = false;
        settingMenu.SetActive(false); 
    }


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
    public void LoadMainSceneWithBot() {
        LoadMainScene();
        BotActions.Instance.gameObject.SetActive(true);
    }
    public void LoadMainSceneWithoutBot() {
        LoadMainScene();
        BotActions.Instance.gameObject.SetActive(false);
    }
    public void LoadHome() // start scene
    {
        SceneManager.LoadScene(0);
    }

    public void LoadEndScene()
    {
        SceneManager.LoadScene(2);
    }
    public bool get_isPaused()
    {
        return isPaused;
    }
}
