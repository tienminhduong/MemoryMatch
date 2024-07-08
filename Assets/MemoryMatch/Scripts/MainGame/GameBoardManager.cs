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

    public Card cardPrefab;
    public CardConfigs cardConfigs;
    public int rows = 4;
    public int cols = 5;
    public float offsetX = 3f;
    public float offsetY = 2f;
    public int totalCardValues => cardConfigs.Stat.Count;
    private List<int> cardValues;
    private List<Card> cards;


    void Start()
    {
        cards = new List<Card>();
        cardValues = new List<int>();

        // Populate the card values list with pairs
        for (int i = 0; i < rows * cols / 2; i++)
        {
            cardValues.Add(i % totalCardValues);
            cardValues.Add(i % totalCardValues);
        }

        // Shuffle the card values list
        Shuffle(cardValues);

        Vector3 startPos = transform.position;

        // Instantiate cards and set their values and types
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Card card = Instantiate(cardPrefab, new Vector3(startPos.x + j * offsetX, startPos.y - i * offsetY, 0), Quaternion.identity);
                int cardValue = cardValues[i * cols + j];
                CardCategory cardType = (CardCategory)(cardValue % totalCardValues);
                card.SetupCard(cardValue);
                cards.Add(card);
            }
        }
    }

    // Shuffle the card values list
    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    // Shuffle the board by resetting all cards and assigning new values
    public void ShuffleBoard()
    {
        foreach (Card card in cards)
        {
            card.Unreveal();
        }

        Shuffle(cardValues);
    }
}
