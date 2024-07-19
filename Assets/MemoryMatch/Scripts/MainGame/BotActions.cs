using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BotActions : MonoBehaviour
{
    private static BotActions instance;
    public static BotActions Instance => instance;
    private void Awake() {
        // Singleton pattern to ensure only one instance of GameManager exists
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    Card previousSelect = null;
    List<Card> cardList;

    [SerializeField] float iq; // the chance of the bot guessing correctly
    public bool isSelecting = false;


    private void Update() {
        cardList = GameBoardManager.Instance != null ? GameBoardManager.Instance.Cards : null;
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
