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

    public void OnMouseDown()
    {
<<<<<<< Updated upstream
        if (!cardBack.activeSelf || GameManager.instance.isCheckingMatch)
            return;

        // Reveal the card
        cardBack.SetActive(false);
        GameManager.instance.CardRevealed(this);
    }

    // Unreveal the card
=======
        if (GameBoardManager.Instance != null)
        {
            if (!cardBack.activeSelf || GameBoardManager.Instance.IsDelayed)
                return;
            // Reveal the card
            FlipFront();
            GameManager.instance.CardRevealed(this);
        }
        else if (BotGameBoardManager.Instance != null)
        {
            if (!cardBack.activeSelf || BotGameBoardManager.Instance.IsDelayed)
                return;
            // Reveal the card
            FlipFront();
            BotGameManager.Instance.CardRevealed(this);
        }
        else
        {
            return;
        }
    }

    public void FlipBack()
    {
        animator.SetTrigger("unreveal");
    }
    public void FlipFront()
    {
        animator.SetTrigger("reveal");
    }
    public void PlayMatchedAnimation()
    {
        animator.SetTrigger("matched");
    }

    // Animation trigger, don't use this
>>>>>>> Stashed changes
    public void Unreveal()
    {
        cardBack.SetActive(true);
    }

<<<<<<< Updated upstream
    public void ActivateEffect() {

=======
    // Animation trigger, don't use this
    public void Reveal()
    {
        cardBack.SetActive(false);
    }

    public void ActivateEffect()
    {
        CardConfig resolveCard = Stat;

        if (resolveCard.Category == CardCategory.Random)
        {
            resolveCard = configs.Stat[Random.Range(0, 7)];

            Debug.Log("Random rolls into " + resolveCard.Category.ToString());
        }

        Player target = null;
        if (PlayerManager.Instance != null)
        {
            target = PlayerManager.Instance.GetPlayer((int)resolveCard.Target);
        }
        else if (BotPlayerManager.Instance != null)
        {
            target = BotPlayerManager.Instance.GetPlayer((int)resolveCard.Target);
        }
        else
        {
            return;
        }

        // Inflict damage or heal
        target.ModifyHP(resolveCard.Damage);

        switch (resolveCard.Category)
        {
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
                if (GameBoardManager.Instance != null)
                    GameBoardManager.Instance.RevealAllCardsInSeconds(2);
                else if (BotGameBoardManager.Instance != null)
                {
                    BotGameBoardManager.Instance.RevealAllCardsInSeconds(2);
                }
                break;
            case CardCategory.Potion:
                target.SetStatusEffect(StatusEffect.None);
                break;
            default:
                break;
        }
>>>>>>> Stashed changes
    }
}


