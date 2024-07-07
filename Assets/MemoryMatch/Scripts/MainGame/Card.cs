using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        spriteRenderer.sprite = cardFronts[cardValue];
        GameManager.instance.CardRevealed(this);
    }

    public void Unreveal()
    {
        spriteRenderer.sprite = cardBack;
    }
}

public enum CardType
{
    Attack = 0,
    Shield = 1,
    Heal = 2,
    Poison = 3,
    Shuffle = 4
}