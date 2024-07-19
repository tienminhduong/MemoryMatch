using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

// Manages the game state, specifically checking for card matches
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int shuffleAfter = 0;

    BotActions bot;
    //public BotActions botPrefab;
    public BotActions Bot => bot;

    private void Awake() {
        // Singleton pattern to ensure only one instance of GameManager exists
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start() {
        if (BotActions.Instance && BotActions.Instance.isActiveAndEnabled)
            bot = BotActions.Instance;
        else
            bot = null;

        //bot = Instantiate(botPrefab);
        //bot = null;
        //if (SceneMaganement.instance.HaveBot)
            //bot = SceneMaganement.instance.Bot;
        //else
            //bot = null;
    }
    public void LoadBot() {
        //bot = Instantiate(botPrefab);
    }

    // Return the camera's height
    static public float ScreenHeight => 2f * Camera.main.orthographicSize;
    // Return the camera's width
    static public float ScreenWidth => ScreenHeight * Camera.main.aspect;

    public bool isCheckingMatch = false;
    private Card firstRevealed;
    private Card secondRevealed;


    // Called when a card is revealed
    public void CardRevealed(Card card)
    {
        if (firstRevealed == null)
            firstRevealed = card;
        else {
            if (firstRevealed == card)
                return;
            secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    // Coroutine to check if two revealed cards match
    private IEnumerator CheckMatch()
    {
        isCheckingMatch = true;

        // If cards match
        if (firstRevealed.CardValue == secondRevealed.CardValue)
        {
            firstRevealed.PlayMatchedAnimation();
            secondRevealed.PlayMatchedAnimation();
            yield return new WaitForSeconds(1.0f);
            SoundManager.Instance.PlayAudioClip(0); // matched sound
            // Apply the card type effect
            firstRevealed.ActivateEffect(PlayerManager.Instance.GetPlayer(0), PlayerManager.Instance.GetPlayer(1));

            matchedCards += 2;
            EndTurn(false);
        }
        // If no match, unreveal the cards after a brief pause
        else
        {
            yield return new WaitForSeconds(1.0f);

            firstRevealed.FlipBack();
            secondRevealed.FlipBack();
            EndTurn(true);
        }
        // The turn ends after checking match

        // Reset for next turn
        firstRevealed = null;
        secondRevealed = null;
        isCheckingMatch = false;
    }

    private void EndTurn(bool switchPlayer) {
        PlayerManager.Instance.EndTurn(switchPlayer);
        if (PlayerManager.Instance.GetPlayer(0).AppliedEffect == StatusEffect.Paralyzed) {
            PlayerManager.Instance.EndTurn(true);
        }
        if (shuffleAfter > 0) {
            shuffleAfter--;
            if (shuffleAfter == 0)
                GameBoardManager.Instance.Shuffle();
        }

        if (bot && PlayerManager.Instance.CurrentTurnPlayerIndex == 1)
            StartCoroutine(bot.SelectCards());
    }

    [SerializeField] int totalCards;
    int matchedCards = 0;

    private void Update()
    {
        // End game
        if (matchedCards == totalCards)
        {
            PlayerManager.Instance.SaveWinnerData();
            SceneManager.LoadScene(2);
        }
    }
}
