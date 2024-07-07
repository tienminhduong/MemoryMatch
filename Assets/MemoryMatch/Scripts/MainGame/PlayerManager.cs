using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public int currentPlayer = 1;
    public int player1Score = 0;
    public int player2Score = 0;

    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    public TextMeshProUGUI player1HealthText;
    public TextMeshProUGUI player2HealthText;
    public TextMeshProUGUI player1ShieldText;
    public TextMeshProUGUI player2ShieldText;
    public TextMeshProUGUI player1PoisonText;
    public TextMeshProUGUI player2PoisonText;

    public TextMeshProUGUI currentPlayerText;

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

        currentPlayerText.text = "Player " + currentPlayer + "'s Turn";
    }

    private void Start()
    {
        UpdateScoreText();
        UpdateHealthText();
        UpdateShieldText();
        UpdatePoisonText();
    }

    public void SwitchPlayer()
    {
        HealthManager.instance.ApplyPoisonDamage(currentPlayer == 1 ? 2 : 1); // Apply poison damage at the start of the opponent's turn

        currentPlayer = (currentPlayer == 1) ? 2 : 1;
        currentPlayerText.text = "Player " + currentPlayer + "'s Turn";

        UpdateHealthText();
        UpdateShieldText();
        UpdatePoisonText();
    }

    public void AddScore()
    {
        if (currentPlayer == 1)
        {
            player1Score++;
        }
        else
        {
            player2Score++;
        }
        UpdateScoreText();
    }

    public void ModifyHealth(CardType cardType)
    {
        int healthChange = 0;
        int shieldChange = 0;
        int poisonStacks = 0;

        switch (cardType)
        {
            case CardType.Attack:
                healthChange = -2;
                break;
            case CardType.Shield:
                shieldChange = 1;
                break;
            case CardType.Heal:
                healthChange = 2;
                break;
            case CardType.Poison:
                poisonStacks = 1;
                break;
            case CardType.Shuffle:
                GameBoardManager.instance.ShuffleBoard();
                break;
        }

        if (cardType == CardType.Poison)
        {
            HealthManager.instance.AddPoisonStacks(currentPlayer == 1 ? 2 : 1, poisonStacks);
        }
        else if (cardType == CardType.Shuffle)
        {
            // Do nothing, the board is shuffled in the GameBoardManager
        }
        else if (cardType == CardType.Shield)
        {
            HealthManager.instance.ModifyShield(currentPlayer, shieldChange);
        }
        else if (cardType == CardType.Heal)
        {
            HealthManager.instance.ModifyHealth(currentPlayer, healthChange);
        }
        else
        {
            HealthManager.instance.ModifyHealth(currentPlayer == 1 ? 2: 1, healthChange);
        }


        UpdateHealthText();
        UpdateShieldText();
        UpdatePoisonText();
    }

    private void UpdateScoreText()
    {
        player1ScoreText.text = "Player 1 Score: " + player1Score;
        player2ScoreText.text = "Player 2 Score: " + player2Score;
    }

    private void UpdateHealthText()
    {
        player1HealthText.text = "Player 1 Health: " + HealthManager.instance.player1Health;
        player2HealthText.text = "Player 2 Health: " + HealthManager.instance.player2Health;
    }

    private void UpdateShieldText()
    {
        player1ShieldText.text = "Player 1 Shield: " + HealthManager.instance.player1Shield;
        player2ShieldText.text = "Player 2 Shield: " + HealthManager.instance.player2Shield;
    }

    private void UpdatePoisonText()
    {
        player1PoisonText.text = "Player 1 Poison: " + HealthManager.instance.player1PoisonStacks;
        player2PoisonText.text = "Player 2 Poison: " + HealthManager.instance.player2PoisonStacks;
    }
}
