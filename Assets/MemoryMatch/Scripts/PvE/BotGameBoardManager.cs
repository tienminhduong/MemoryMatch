using System.Collections.Generic;
using UnityEngine;

public class BotGameBoardManager : MonoBehaviour
{
    private static BotGameBoardManager instance;
    public static BotGameBoardManager Instance => instance;

    [SerializeField] private Card cardPrefab;
    public CardConfigs cardConfigs;
    [SerializeField] private int rows = 7;
    [SerializeField] private int cols = 4;

    private float offsetX;
    private float offsetY;
    private Vector3 topLeftCornerPos;

    private List<Card> cards;
    public List<Card> UnrevealedCard;

    public int NumberCardCategory => cardConfigs.Stat.Count;
    public bool IsDelayed => BotGameManager.Instance.isCheckingMatch || revealCount > 0;
    private float revealCount;
    private bool[] isFlipped;

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

        cards = new List<Card>();

        offsetX = BotGameManager.ScreenWidth / instance.cols;
        offsetY = BotGameManager.ScreenHeight / instance.rows * 0.7f;
        topLeftCornerPos = new Vector3(-offsetX * (cols / 2f - 0.5f), offsetY * (rows / 2f - 0.5f), 0f);

        GenerateBoard();

        Shuffle();
    }

    private void Start()
    {
        UnrevealedCard = new List<Card>(cards);
    }

    private void Update()
    {
        if (revealCount > 0)
        {
            revealCount -= Time.deltaTime;
            if (revealCount <= 0)
            {
                FlipBackRevealed();
                BotPlayerManager.Instance.EndTurn(false);
            }
        }
    }

    private void GenerateBoard()
    {
        int totalCardValue = 9;
        bool[] checkExisted = new bool[totalCardValue];

        for (int i = 0; i < rows * cols / 2; i++)
        {
            int idValue = Random.Range(0, NumberCardCategory);
            while (idValue < totalCardValue && checkExisted[idValue])
            {
                idValue = Random.Range(0, NumberCardCategory);
            }

            if (idValue < totalCardValue)
            {
                checkExisted[idValue] = true;
            }

            Card card = Instantiate(cardPrefab);
            card.SetupCard(idValue);
            cards.Add(card);

            card = Instantiate(cardPrefab);
            card.SetupCard(idValue);
            cards.Add(card);
        }
    }

    public void Shuffle()
    {
        List<Card> temp = new List<Card>(cards);
        cards.Clear();

        while (temp.Count > 0)
        {
            Card c = temp[Random.Range(0, temp.Count)];
            cards.Add(c);
            temp.Remove(c);
        }

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                cards[i * cols + j].transform.position = topLeftCornerPos + new Vector3(j * offsetX, -i * offsetY, 0);
            }
        }
    }

    public List<Card> Cards => cards;

    public void RevealAllCardsInSeconds(float delay)
    {
        revealCount = delay;
        BotGameManager.Instance.ShuffleAfter = (int)delay + 1;
        isFlipped = new bool[cards.Count];
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].IsRevealing)
            {
                isFlipped[i] = true;
            }
            else
            {
                cards[i].Reveal();
            }
        }
    }

    private void FlipBackRevealed()
    {
        if (isFlipped == null)
        {
            return;
        }

        for (int i = 0; i < cards.Count; i++)
        {
            if (!isFlipped[i])
            {
                cards[i].FlipBack();
            }
        }
    }

    // Wait for 2 seconds before flipping back the revealed cards

}
