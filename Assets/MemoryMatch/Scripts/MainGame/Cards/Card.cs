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
    public bool IsRevealing => !cardBack.activeSelf;

    private void Awake() {
        animator = GetComponent<Animator>();
        cardBack = transform.GetChild(1).gameObject;
    }


    private void OnMouseDown()
    {
        if (SceneMaganement.instance.get_isPaused() == false)
        {
            // If it's bot turn, return
            if (GameManager.instance.Bot != null && PlayerManager.Instance.CurrentTurnPlayerIndex == 1)
                return;

            SelectCard();
        }
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
    public void FlipFront() 
    {
        SoundManager.Instance.PlayAudioClip(7);
        animator.SetTrigger("reveal");
    }
    public void PlayMatchedAnimation()
    {
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

    public virtual void ActivateEffect(Player turnPlayer, Player nonturnPlayer) { }
}
public enum CardCategory
{
    Heal, Hammer, Sword, Bomb, Paralyze, Poison, Lens, Potion, Random, Red, Blue, Green, Yellow
}
