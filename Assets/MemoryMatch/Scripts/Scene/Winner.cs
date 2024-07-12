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
        if (!PlayerManager.Instance) return;
        if (PlayerManager.Instance.GetWinner() == 1)
            winnerText.text = "Player 1 won !!!";
        else if (PlayerManager.Instance.GetWinner() == 2)
            winnerText.text = "Player 2 won !!!";
        else winnerText.text = "Drawww !!!";
    }
}
