using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isCheckingMatch = false;
    private Card firstRevealed;
    private Card secondRevealed;

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

    private IEnumerator CheckMatch()
    {
        isCheckingMatch = true;

        if (firstRevealed.cardValue == secondRevealed.cardValue)
        {
            

            // Wait for a moment to show the matched cards
            yield return new WaitForSeconds(1.0f);

            // Add score for matching
            PlayerManager.instance.AddScore();

            // Here you can add logic to remove matched cards if needed

            // Cards match, apply the card type effect
            PlayerManager.instance.ModifyHealth(firstRevealed.cardType);

        }
        else
        {
            // No match, unreveal the cards after a brief pause
            yield return new WaitForSeconds(1.0f);
            firstRevealed.Unreveal();
            secondRevealed.Unreveal();
            
            // Switch to the next player
            PlayerManager.instance.SwitchPlayer();
        }

        // Reset for next turn
        firstRevealed = null;
        secondRevealed = null;
        isCheckingMatch = false;
    }
}
