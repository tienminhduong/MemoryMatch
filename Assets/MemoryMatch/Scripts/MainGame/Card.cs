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
    [SerializeField] Animator animator;

    public int CardValue => cardValue;
    public bool IsRevealing => !cardBack.activeSelf;

    CardConfig Stat => configs.Stat[cardValue];

    // Set up the card with id/cardValue
    public void SetupCard(int id)
    {
        cardValue = id;
        iconRenderer.sprite = Stat.Icon;
        cardBack.SetActive(true);
    }

    private void OnMouseDown()
    {
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

    public void ActivateEffect() {
        //Debug.Log("Activate " + Stat.Category.ToString() + " effect!");

        //CardCategory resolveCategory = Stat.Category;
        //int damage = Stat.Damage;
        CardConfig resolveCard = Stat;

        if (resolveCard.Category == CardCategory.Random) {
            resolveCard = configs.Stat[Random.Range(0, 7)];
            //resolveCategory = (CardCategory)Random.Range(0, 7);

            //if (resolveCategory == CardCategory.Attack) damage = 50;
            //else if (resolveCategory == CardCategory.Heal) damage = -50;

            Debug.Log("Random rolls into " + resolveCard.Category.ToString());
        }

        Player target = PlayerManager.Instance.GetPlayer((int)resolveCard.Target);
        // Inflict damage or heal
        target.ModifyHP(resolveCard.Damage);

        switch (resolveCard.Category) {
            case CardCategory.Bomb:
                if (Random.Range(0, 100) < 50)
                    target.SetStatusEffect(StatusEffect.Burned);
                break;
            case CardCategory.Paralyze:
                target.SetStatusEffect(StatusEffect.Paralyzed);
                break;
            case CardCategory.Poison:
                target.SetStatusEffect(StatusEffect.Poisoned);
                break;
            case CardCategory.Lens:
                GameBoardManager.Instance.RevealAllCardsInSeconds(2);
                break;
            case CardCategory.Potion:
                target.SetStatusEffect(StatusEffect.None);
                break;
            default:
                break;
        }
    }
}