using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Manages player scores, health, shields, and poison stacks, and handles turn switching
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
        // Singleton pattern to ensure only one instance of PlayerManager exists
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

    // Switch the current player and apply poison damage to the opponent
    public void SwitchPlayer()
    {
        HealthManager.instance.ApplyPoisonDamage(currentPlayer == 1 ? 2 : 1); // Apply poison damage at the start of the opponent's turn
        currentPlayer = (currentPlayer == 1) ? 2 : 1;
        currentPlayerText.text = "Player " + currentPlayer + "'s Turn";
        UpdateHealthText();
        UpdateShieldText();
        UpdatePoisonText();
    }

    // Increment the current player's score
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

    // Modify health, shield, or poison stacks based on the card type
    //public void ModifyHealth(CardCategory cardType)
    //{
    //    int healthChange = 0;
    //    int shieldChange = 0;
    //    int poisonStacks = 0;

    //    switch (cardType)
    //    {
    //        case CardCategory.Attack:
    //            healthChange = -2;
    //            break;
    //        case CardCategory.Shield:
    //            shieldChange = 1;
    //            break;
    //        case CardCategory.Heal:
    //            healthChange = 2;
    //            break;
    //        case CardCategory.Poison:
    //            poisonStacks = 1;
    //            break;
    //        case CardCategory.Shuffle:
    //            GameBoardManager.instance.ShuffleBoard();
    //            break;
    //    }

    //    if (cardType == CardCategory.Poison)
    //    {
    //        HealthManager.instance.AddPoisonStacks(currentPlayer == 1 ? 2 : 1, poisonStacks);
    //    }
    //    else if (cardType == CardCategory.Shield)
    //    {
    //        HealthManager.instance.ModifyShield(currentPlayer, shieldChange);
    //    }
    //    else if (cardType == CardCategory.Heal)
    //    {
    //        HealthManager.instance.ModifyHealth(currentPlayer, healthChange);
    //    }
    //    else if (cardType == CardCategory.Attack)
    //    {
    //        HealthManager.instance.ModifyHealth(currentPlayer == 1 ? 2 : 1, healthChange);
    //    }

    //    UpdateHealthText();
    //    UpdateShieldText();
    //    UpdatePoisonText();
    //}

    // Update UI elements with the current score
    private void UpdateScoreText()
    {
        player1ScoreText.text = "Player 1 Score: " + player1Score;
        player2ScoreText.text = "Player 2 Score: " + player2Score;
    }

    // Update UI elements with the current health
    private void UpdateHealthText()
    {
        player1HealthText.text = "Player 1 Health: " + HealthManager.instance.player1Health;
        player2HealthText.text = "Player 2 Health: " + HealthManager.instance.player2Health;
    }

    // Update UI elements with the current shield
    private void UpdateShieldText()
    {
        player1ShieldText.text = "Player 1 Shield: " + HealthManager.instance.player1Shield;
        player2ShieldText.text = "Player 2 Shield: " + HealthManager.instance.player2Shield;
    }

    // Update UI elements with the current poison stacks
    private void UpdatePoisonText()
    {
        player1PoisonText.text = "Player 1 Poison: " + HealthManager.instance.player1PoisonStacks;
        player2PoisonText.text = "Player 2 Poison: " + HealthManager.instance.player2PoisonStacks;
    }
}
