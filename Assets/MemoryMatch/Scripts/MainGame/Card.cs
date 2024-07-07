using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represents a card in the game
public class Card : MonoBehaviour
{
    public int cardValue;
    public Sprite cardBack;
    public Sprite[] cardFronts;
    public SpriteRenderer spriteRenderer;
    public CardType cardType;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Set up the card with a value and type
    public void SetupCard(int value, CardType type)
    {
        cardValue = value;
        cardType = type;
        spriteRenderer.sprite = cardBack;
    }

    private void OnMouseDown()
    {
        if (spriteRenderer.sprite == cardFronts[cardValue] || GameManager.instance.isCheckingMatch)
        {
            return;
        }

        // Reveal the card
        spriteRenderer.sprite = cardFronts[cardValue];
        GameManager.instance.CardRevealed(this);
    }

    // Unreveal the card
    public void Unreveal()
    {
        spriteRenderer.sprite = cardBack;
    }
}

// Enum to represent different types of cards
public enum CardType
{
    Attack = 0,
    Shield = 1,
    Heal = 2,
    Poison = 3,
    Shuffle = 4
}
