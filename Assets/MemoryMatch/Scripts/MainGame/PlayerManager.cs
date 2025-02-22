using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    static PlayerManager instance;
    public static PlayerManager Instance => instance;
    private void Awake() {
        // Singleton pattern to ensure only one instance of GameManager exists
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    [SerializeField] Player[] players = new Player[2];
    int turnPlayerIndex;
    public int CurrentTurnPlayerIndex => turnPlayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        ResetPlayer();
    }

    public void ResetPlayer() {
        turnPlayerIndex = 0;
        players[0].ResetStat();
        players[1].ResetStat();
    }

    // Update is called once per frame
    void Update()
    {
        if (players[0].CurrentHP < 0 || players[1].CurrentHP < 0)
        {
            SaveWinnerData();
            SceneMaganement.instance.LoadEndScene();
        }
    }

    // 0 return current turn player, 1 return non-turn player
    public Player GetPlayer(int index) {
        return players[(turnPlayerIndex + index) % 2];
    }

    public void EndTurn(bool switchPlayer) {
        GetPlayer(0).UpdateEndTurn();

        // Switch turn player
        if (switchPlayer)
            turnPlayerIndex = (turnPlayerIndex + 1) % 2;

        // Update UI
        for (int i = 0; i < 2; i++) UIManager.Instance.UpdateUI(players[i]);
    }

    public void SaveWinnerData()
    {
        if (players[0].CurrentHP > players[1].CurrentHP)
        {
            PlayerPrefs.SetInt("WinnerId", 1);
            Debug.Log("save winner data : 1");
        }
        else if (players[0].CurrentHP < players[1].CurrentHP)
        {
            PlayerPrefs.SetInt("WinnerId", 2);
            Debug.Log("save winner data : 2");
        }
        else
        {
            PlayerPrefs.SetInt("WinnerId", 0);
            Debug.Log("save winner data : 0");
        }
    }
}
