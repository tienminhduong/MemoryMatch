using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotActions : MonoBehaviour
{
    Card previousSelect = null;
    List<Card> cardList;

    [SerializeField] float iq; // the chance of the bot guessing correctly
    public bool isSelecting = false;

    private void Update() {
        cardList = GameBoardManager.Instance.Cards;
<<<<<<< HEAD
        //if (cardList != null)
        //    Debug.Log(cardList[0].transform.rotation.eulerAngles.y);
=======
>>>>>>> e311ccd5d5a41a0cd380f245c3344eede5adc81f
    }

    public IEnumerator SelectCards() {
        yield return new WaitForSeconds(1.5f + GameBoardManager.Instance.RevealCount);
        SelectFirstCard();
        yield return new WaitForSeconds(1);
        if (Random.Range(0, 1f) <= iq)
            SelectCorrectCard();
        else
            SelectRandomCard();
        isSelecting = false;
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
