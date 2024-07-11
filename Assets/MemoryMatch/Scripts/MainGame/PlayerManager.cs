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
        turnPlayerIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 0 return current turn player, 1 return non-turn player
    public Player GetPlayer(int index) {
        return players[(turnPlayerIndex + index) % 2];
    }
    public void EndTurn() {
        GetPlayer(0).UpdateEndTurn();

        // Switch turn player
        turnPlayerIndex = (turnPlayerIndex + 1) % 2;

        // Player paralyzed, skip turn
        if (GetPlayer(turnPlayerIndex).AppliedEffect == StatusEffect.Paralyzed) {
            GetPlayer(turnPlayerIndex).UpdateEndTurn();
            turnPlayerIndex = (turnPlayerIndex + 1) % 2;
        }
        for (int i = 0; i < 2; i++) UIManager.Instance.UpdateUI(players[i]);
    }
}
