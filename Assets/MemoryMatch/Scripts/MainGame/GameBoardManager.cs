using System.Collections.Generic;
using UnityEngine;

public class GameBoardManager : MonoBehaviour
{
    public static GameBoardManager instance;

    public GameObject cardPrefab;
    public int rows = 4;
    public int cols = 5;
    public float offsetX = 3f;
    public float offsetY = 2f;
    public int totalCardValues = 5;
    private List<int> cardValues;
    private List<GameObject> cards;

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

    void Start()
    {
        cards = new List<GameObject>();
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

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                GameObject card = Instantiate(cardPrefab, new Vector3(startPos.x + j * offsetX, startPos.y - i * offsetY, 0), Quaternion.identity);
                int cardValue = cardValues[i * cols + j];
                CardType cardType = (CardType)(cardValue % totalCardValues);
                card.GetComponent<Card>().SetupCard(cardValue, cardType);
                cards.Add(card);
            }
        }
    }

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

    public void ShuffleBoard()
    {
        // set all cards to face down
        foreach (GameObject card in cards)
        {
            card.GetComponent<Card>().Unreveal();
        }

        // set new card values
        Shuffle(cardValues);
    }
}
