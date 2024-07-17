using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BotGameManager : MonoBehaviour
{
    private static BotGameManager instance;
    public static BotGameManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private int shuffleAfter = 0;

    public static float ScreenHeight => 2f * Camera.main.orthographicSize;
    public static float ScreenWidth => ScreenHeight * Camera.main.aspect;

    public int ShuffleAfter { get => shuffleAfter; set => shuffleAfter = value; }

    public bool isCheckingMatch = false;
    private Card firstRevealed;
    private Card secondRevealed;

    public void CardRevealed(Card card)
    {
        if (firstRevealed == null)
        {
            firstRevealed = card;
        }
        else if (secondRevealed == null)
        {
            if (firstRevealed == card)
            {
                return;
            }
            secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        isCheckingMatch = true;

        if (firstRevealed.CardValue == secondRevealed.CardValue)
        {
            firstRevealed.PlayMatchedAnimation();
            secondRevealed.PlayMatchedAnimation();
            yield return new WaitForSeconds(1.0f);

            firstRevealed.ActivateEffect();

            // remove the cards from the unrevealed card list
            BotGameBoardManager.Instance.UnrevealedCard.Remove(firstRevealed);
            BotGameBoardManager.Instance.UnrevealedCard.Remove(secondRevealed);

            
            EndTurn(false);
        }
        else
        {
            yield return new WaitForSeconds(1.0f);
            firstRevealed.FlipBack();
            secondRevealed.FlipBack();
            EndTurn(true);
        }

        firstRevealed = null;
        secondRevealed = null;
        isCheckingMatch = false;
    }

    public void EndTurn(bool switchPlayer)
    {
        BotPlayerManager.Instance.EndTurn(switchPlayer);
        if (BotPlayerManager.Instance.GetPlayer(0).AppliedEffect == StatusEffect.Paralyzed)
        {
            BotPlayerManager.Instance.EndTurn(true);
        }

        if (shuffleAfter > 0)
        {
            shuffleAfter--;
            if (shuffleAfter == 0)
            {
                BotGameBoardManager.Instance.Shuffle();
            }
        }
    }
}
