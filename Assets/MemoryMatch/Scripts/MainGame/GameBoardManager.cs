using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public int rows = 4;
    public int cols = 5;
    public float offsetX = 3f;
    public float offsetY = 2f;
    public int totalCardValues = 5;
    private List<int> cardValues;
    private List<GameObject> cards;

    void Start()
    {
        cards = new List<GameObject>();
        cardValues = new List<int>();

        //int oldValue = 0;
        //int value = 0;
        for (int i = 0; i < rows * cols / 2; i++)
        {
            //// choose a random value for the card
            //do
            //{
            //    value = Random.Range(0, totalCardValues);
            //} while (oldValue == value);

            //oldValue = value;
            //cardValues.Add(value);
            //cardValues.Add(value);
            cardValues.Add(i % totalCardValues);
            cardValues.Add(i % totalCardValues);

        }

        Shuffle(cardValues);

        Vector3 startPos = transform.position;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                GameObject card = Instantiate(cardPrefab, new Vector3(startPos.x + j * offsetX, startPos.y - i * offsetY, 0), Quaternion.identity);
                card.GetComponent<Card>().SetupCard(cardValues[i * cols + j]);
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
}
