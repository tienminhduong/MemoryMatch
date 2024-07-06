using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardValue;
    public Sprite cardBack;
    public Sprite[] cardFronts;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetupCard(int value)
    {
        cardValue = value;
        // set card visual to card back
        spriteRenderer.sprite = cardBack;
    }

    private void OnMouseDown()
    {
        // if the card is already revealed, do nothing
        if (spriteRenderer.sprite == cardFronts[cardValue] || GameManager.instance.isCheckingMatch) // Add the check for isCheckingMatch
        {
            return;
        }

        // reveal the card
        // set card visual to card front
        spriteRenderer.sprite = cardFronts[cardValue];

        // notify the GameManager that this card is revealed
        GameManager.instance.CardRevealed(this);
    }

    public void Unreveal()
    {
        // set card visual to card back
        spriteRenderer.sprite = cardBack;
    }
}
