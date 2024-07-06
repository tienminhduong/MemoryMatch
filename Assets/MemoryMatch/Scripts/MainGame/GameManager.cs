using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Card firstRevealed;
    private Card secondRevealed;
    public bool isCheckingMatch = false; // Add this line

    private void Awake()
    {
        instance = this;
    }

    public void CardRevealed(Card card)
    {
        if (isCheckingMatch) return; // Add this line

        if (firstRevealed == null)
        {
            firstRevealed = card;
        }
        else if (secondRevealed == null)
        {
            secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        isCheckingMatch = true; // Add this line

        if (firstRevealed.cardValue == secondRevealed.cardValue)
        {
            Debug.Log("Match!");
        }
        else
        {
            yield return new WaitForSeconds(1f);
            firstRevealed.Unreveal();
            secondRevealed.Unreveal();
        }

        firstRevealed = null;
        secondRevealed = null;
        isCheckingMatch = false; // Add this line
    }
}
