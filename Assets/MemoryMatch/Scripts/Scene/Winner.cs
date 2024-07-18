using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Winner : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI winnerText;
    int tmp;
    void Start()
    {
        int winner = PlayerPrefs.GetInt("WinnerId");
        Debug.Log("winner id : " + winner);
        if (winner == 1)
            winnerText.text = "Player 1 won !!!";
        else if (winner == 2)
            winnerText.text = "Player 2 won !!!";
        else winnerText.text = "Drawww !!!";
    }
}
