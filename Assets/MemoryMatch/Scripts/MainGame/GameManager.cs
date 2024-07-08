using UnityEngine;
using System.Collections;

// Manages the game state, specifically checking for card matches
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        // Singleton pattern to ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    static public float ScreenHeight => 2f * Camera.main.orthographicSize;
    static public float ScreenWidth => ScreenHeight * Camera.main.aspect;

    public bool isCheckingMatch = false;
    private Card firstRevealed;
    private Card secondRevealed;


    // Called when a card is revealed
    public void CardRevealed(Card card)
    {
        if (firstRevealed == null)
        {
            firstRevealed = card;
        }
        else
        {
            secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    // Coroutine to check if two revealed cards match
    private IEnumerator CheckMatch()
    {
        isCheckingMatch = true;

        // If cards match
        if (firstRevealed.CardValue == secondRevealed.CardValue) {
            yield return new WaitForSeconds(1.0f);

            // Add score for matching
            //PlayerManager.instance.AddScore();

            // Apply the card type effect to the opponent
            //PlayerManager.instance.ModifyHealth(firstRevealed.cardType);
        }
        else {
            // If no match, unreveal the cards after a brief pause
            yield return new WaitForSeconds(1.0f);
            firstRevealed.Unreveal();
            secondRevealed.Unreveal();

            // Switch to the next player
            //PlayerManager.instance.SwitchPlayer();
        }

        // Reset for next turn
        firstRevealed = null;
        secondRevealed = null;
        isCheckingMatch = false;
    }
}
