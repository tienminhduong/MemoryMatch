using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotActions : MonoBehaviour
{
    Card previousSelect = null;
    List<Card> cardList;

    [SerializeField] float iq; // the chance of the bot guessing correctly

    private void Update() {
        cardList = GameBoardManager.Instance.Cards;
    }

    public void SelectCards() {
        SelectFirstCard();
        if (Random.Range(0, 1f) < iq)
            SelectCorrectCard();
        else
            SelectRandomCard();
    }

    private void SelectFirstCard() {
        if (previousSelect != null) return;

        int id = Random.Range(0, cardList.Count);
        while (cardList[id].IsRevealing)
            id = Random.Range(0, cardList.Count);

        previousSelect = cardList[id];
        previousSelect.SelectCard();
    }
    private void SelectCorrectCard() {
        if (previousSelect == null) return;

        foreach (Card card in cardList)
            if (card.CardValue == previousSelect.CardValue && !card.IsRevealing) {
                card.SelectCard();
                previousSelect = null;
                break;
            }
    }
    private void SelectRandomCard() {
        if (previousSelect == null) return;

        int id = Random.Range(0, cardList.Count);
        while (cardList[id].IsRevealing)
            id = Random.Range(0, cardList.Count);

        cardList[id].SelectCard();
        previousSelect = null;
    }
}
