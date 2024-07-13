using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Manages the game state, specifically checking for card matches
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] int Number_Of_Matched_Pair = 0;
    private void Awake() {
        // Singleton pattern to ensure only one instance of GameManager exists
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Return the camera's height
    static public float ScreenHeight => 2f * Camera.main.orthographicSize;
    // Return the camera's width
    static public float ScreenWidth => ScreenHeight * Camera.main.aspect;

    public bool isCheckingMatch = false;
    private Card firstRevealed;
    private Card secondRevealed;


    // Called when a card is revealed
    public void CardRevealed(Card card)
    {
        if (firstRevealed == null)
            firstRevealed = card;
        else {
            if (firstRevealed == card)
                return;
            secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    // Coroutine to check if two revealed cards match
    private IEnumerator CheckMatch()
    {
        isCheckingMatch = true;

        // If cards match
        if (firstRevealed.CardValue == secondRevealed.CardValue)
        {
            firstRevealed.PlayMatchedAnimation();
            secondRevealed.PlayMatchedAnimation();
            yield return new WaitForSeconds(1.0f);

            // Apply the card type effect
            firstRevealed.ActivateEffect();

            matchedCards += 2;
        }
        // If no match, unreveal the cards after a brief pause
        else
        {
            yield return new WaitForSeconds(1.0f);

            firstRevealed.FlipBack();
            secondRevealed.FlipBack();
        }
        // The turn ends after checking match
        PlayerManager.Instance.EndTurn();

        // Reset for next turn
        firstRevealed = null;
        secondRevealed = null;
        isCheckingMatch = false;
    }

    [SerializeField] int totalCards;
    int matchedCards = 0;

    private void Update()
    {
        // end game
        if (matchedCards == totalCards)
        {
            SceneManager.LoadScene(2);
        }
    }
}
