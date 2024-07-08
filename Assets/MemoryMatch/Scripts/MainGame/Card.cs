using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represents a card in the game
public class Card : MonoBehaviour
{
    [SerializeField] int cardValue;
    [SerializeField] GameObject cardBack;
    [SerializeField] CardConfigs configs;
    [SerializeField] SpriteRenderer iconRenderer;

    public int CardValue => cardValue;

    private void Awake()
    {
        //spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Set up the card with a value and type
    public void SetupCard(int id)
    {
        //cardValue = value;
        //cardType = type;
        //spriteRenderer.sprite = cardBack;
        cardValue = id;
        iconRenderer.sprite = configs.Stat[id].Icon;
        cardBack.SetActive(true);
    }

    private void OnMouseDown()
    {
        if (!cardBack.activeSelf || GameManager.instance.isCheckingMatch)
            return;

        // Reveal the card
        cardBack.SetActive(false);
        GameManager.instance.CardRevealed(this);
    }

    // Unreveal the card
    public void Unreveal()
    {
        cardBack.SetActive(true);
    }

    public void ActivateEffect() {

    }
}


