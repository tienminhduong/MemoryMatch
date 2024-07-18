using System.Collections.Generic;
using UnityEngine;

// Manages the game board, including creating and shuffling cards
public class GameBoardManager : MonoBehaviour
{
    #region Singleton
    static GameBoardManager instance;
    public static GameBoardManager Instance => instance;
    private void Awake()
    {
        // Singleton pattern to ensure only one instance of GameBoardManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] List<Card> cardPrefabs;
    public CardConfigs cardConfigs;
    [SerializeField] int rows = 7;
    [SerializeField] int cols = 4;

    float offsetX;
    float offsetY;
    Vector3 topLeftCornerPos;

    private List<Card> cards;
    public List<Card> Cards => cards;
    public int NumberCardCategory => cardConfigs.Stat.Count;
    public bool IsDelayed => GameManager.instance.isCheckingMatch || revealCount > 0;

    public List<Card> CardPrefabs => cardPrefabs;


    void Start()
    {
        cards = new List<Card>();

        offsetX = GameManager.ScreenWidth / instance.cols;
        offsetY = GameManager.ScreenHeight / instance.rows * 0.7f;
        topLeftCornerPos = new Vector3(-offsetX * (cols / 2f - 0.5f), offsetY * (rows / 2f - 0.5f), 0f);

        // Populate the card values list with pairs
        GenerateBoard();

        // Shuffle the card values list
        Shuffle();
    }

    void Update() {
        if (revealCount > 0) {
            revealCount -= Time.deltaTime;
            if (revealCount <= 0) {
                FlipBackRevealed();
            }
        }
    }

    void GenerateBoard() {
        // Cards with id from 0 to 8 can only be chosen once
        bool[] checkExisted = new bool[9];
        for (int i = 0; i < rows * cols / 2; ++i) {

            int idValue = Random.Range(0, NumberCardCategory);
            while (idValue < 9 && checkExisted[idValue])
                idValue = Random.Range(0, NumberCardCategory);

            if (idValue < 9) checkExisted[idValue] = true;

            //Card card = Instantiate(cardPrefab);
            Card card = Instantiate(cardPrefabs[idValue]);
            cards.Add(card);
            card = Instantiate(cardPrefabs[idValue]);
            cards.Add(card);

            //card.SetupCard(idValue); cards.Add(card);
            //card.SetupCard(idValue); cards.Add(card);
        }
    }

    // Shuffle the card values list
    public void Shuffle() {
        List<Card> temp = cards;
        cards = new List<Card>();
        while (temp.Count > 0) {
            Card c = temp[Random.Range(0, temp.Count)];
            cards.Add(c);
            temp.Remove(c);
        }
        for (int i = 0; i < rows; ++i)
            for (int j = 0; j < cols; ++j) {
                cards[i * cols + j].transform.position = new Vector3(topLeftCornerPos.x + j * offsetX, topLeftCornerPos.y - i * offsetY, 0);
            }
    }

    float revealCount = 0;
    bool[] isFlipped;

    // Reveal all cards in x seconds
    public void RevealAllCardsInSeconds(float second) {
        revealCount = second;
        GameManager.instance.shuffleAfter = (int)second + 1;
        isFlipped = new bool[cards.Count];
        for (int i = 0; i < cards.Count; ++i) {
            if (cards[i].IsRevealing)
                isFlipped[i] = true;
            else
                cards[i].FlipFront();
        }
    }

    // Flip all revealed cards back, except those had already been revealed before
    void FlipBackRevealed() {
        if (isFlipped == null)
            return;
        for (int i = 0; i < cards.Count; ++i)
            if (!isFlipped[i])
                cards[i].FlipBack();
        isFlipped = null;
    }
}
