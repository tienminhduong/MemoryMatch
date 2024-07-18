using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represents a card in the game
public class Card : MonoBehaviour
{
    [SerializeField] protected int cardValue;

    GameObject cardBack;
    Animator animator;

    public int CardValue => cardValue;
    //public bool IsRevealing => !cardBack.activeSelf;
    public bool IsRevealing => transform.rotation.eulerAngles.y != 180;

    private void Awake() {
        animator = GetComponent<Animator>();
        cardBack = transform.GetChild(1).gameObject;
    }


    // Set up the card with id/cardValue
    public void SetupCard(int id)
    {
        cardValue = id;
        cardBack.SetActive(true);
    }

    private void OnMouseDown()
    {
        SelectCard();
    }

    public void SelectCard() {
        if (!cardBack.activeSelf || GameBoardManager.Instance.IsDelayed)
            return;

        // Reveal the card
        FlipFront();
        GameManager.instance.CardRevealed(this);
    }

    public void FlipBack() {
        animator.SetTrigger("unreveal");
    }
    public void FlipFront() {
        animator.SetTrigger("reveal");
    }
    public void PlayMatchedAnimation() {
        animator.SetTrigger("matched");
    }

    // Animation trigger, don't use this
    public void Unreveal() {
        cardBack.SetActive(true);
    }

    // Animation trigger, don't use this
    public void Reveal() {
        cardBack.SetActive(false);
    }

    public virtual void ActivateEffect(Player turnPlayer, Player nonturnPlayer) {
        // if gamemode == casual
        // return;
    }
}