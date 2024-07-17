using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotPlayerManager : MonoBehaviour
{
    private static BotPlayerManager instance;
    public static BotPlayerManager Instance => instance;

    [SerializeField] Player[] players = new Player[2];
    int turnPlayerIndex;
    public int CurrentTurnPlayerIndex => turnPlayerIndex;
    public int winner;

    BotPlayer bot;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        turnPlayerIndex = 0;
        bot = (BotPlayer)players[1];
    }

    private void Update()
    {
        if (players[0].CurrentHP <= 0)
        {
            winner = 2;
            SceneManagement.instance.LoadEndScene();
        }
        else if (players[1].CurrentHP <= 0)
        {
            winner = 1;
            SceneManagement.instance.LoadEndScene();
        }
        else if (BotGameBoardManager.Instance.UnrevealedCard == null)
        {
            winner = GetWinner();
            SceneManagement.instance.LoadEndScene();
        }
    }

    public Player GetPlayer(int index)
    {
        return players[(turnPlayerIndex + index) % 2];
    }

    public void EndTurn(bool switchPlayer)
    {
        GetPlayer(0).UpdateEndTurn();

        if (switchPlayer)
        {
            turnPlayerIndex = (turnPlayerIndex + 1) % 2;
        }

        for (int i = 0; i < 2; i++)
        {
            UIManager.Instance.UpdateUI(players[i]);
        }

        // Continue bot's turn if it's the bot's turn and the bot has successfully matched a pair

        if (turnPlayerIndex == 1)
        {
            if (BotGameBoardManager.Instance.UnrevealedCard != null)
                bot.BotTurn();
        }
    }

    public int GetWinner()
    {
        if (winner != 0)
        {
            return winner;
        }

        if (players[0].CurrentHP > players[1].CurrentHP)
        {
            return 1;
        }
        else if (players[0].CurrentHP < players[1].CurrentHP)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }
}
