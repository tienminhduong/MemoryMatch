using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BotPlayer : Player
{
    [SerializeField] private float thinkingTime = 1f;
    [SerializeField] private float accuracy = 0.5f;

    private bool isThinking;
    private Dictionary<int, List<Card>> cardPairs;

    protected override void Start()
    {
        base.Start();

        isThinking = false;
        cardPairs = new Dictionary<int, List<Card>>();
        GetCardPairs(BotGameBoardManager.Instance.Cards);
    }

    // get the card pairs from the board
    public void GetCardPairs(List<Card> cards)
    {
        cardPairs.Clear();

        foreach (Card card in cards)
        {
            if (!cardPairs.ContainsKey(card.CardValue))
            {
                cardPairs.Add(card.CardValue, new List<Card>());
            }

            cardPairs[card.CardValue].Add(card);
        }
    }

    // bot's turn
    public void BotTurn()
    {
        if (isThinking)
        {
            return;
        }

        isThinking = true;
        StartCoroutine(Think());
    }

    // bot's thinking process
    private IEnumerator Think()
    {
        yield return new WaitForSeconds(thinkingTime);

        if (Random.value < accuracy)
        {
            ChooseCard();
        }
        else
        {
            RandomChooseCards();
        }

        isThinking = false;
    }

    // choose a card pair
    public void ChooseCard()
    {
        foreach (var pair in cardPairs)
        {
            if (pair.Value.Count > 1)
            {
                pair.Value[0].OnMouseDown();
                pair.Value[1].OnMouseDown();
                Debug.Log("Bot chose a pair: " + pair.Key);

                // remove the pair from the dictionary
                cardPairs.Remove(pair.Key);
                return;
            }
        }
        // Fallback to random choice if no pairs are found
        RandomChooseCards();
        Debug.Log("Bot chose randomly as no pair was found");
    }

    private void RandomChooseCards()
    {
        // choose 2 random unrevealed cards from BotGameBoardManager.Instance.Cards

        int randomIndex1 = Random.Range(0, BotGameBoardManager.Instance.UnrevealedCard.Count);
        int randomIndex2 = Random.Range(0, BotGameBoardManager.Instance.UnrevealedCard.Count);

        while (randomIndex2 == randomIndex1)
        {
            randomIndex2 = Random.Range(0, BotGameBoardManager.Instance.UnrevealedCard.Count);
        }

        BotGameBoardManager.Instance.UnrevealedCard[randomIndex1].OnMouseDown();
        BotGameBoardManager.Instance.UnrevealedCard[randomIndex2].OnMouseDown();
        Debug.Log("Bot chose randomly");
    }

    public void ContinueBotTurn()
    {
        if (isThinking)
        {
            return;
        }

        isThinking = true;
        StartCoroutine(Think());
    }
}
