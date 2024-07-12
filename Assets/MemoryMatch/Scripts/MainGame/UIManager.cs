using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    #region Singleton
    static UIManager instance;
    public static UIManager Instance => instance;
    private void Awake()
    {
        // Singleton pattern to ensure only one instance of GameManager exists
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    [SerializeField] TextMeshProUGUI player1Text;
    [SerializeField] TextMeshProUGUI player2Text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string text1 = "Player1:\n";
        //text1 += "HP: " + 
    }

    public void UpdateUI(Player player)
    {
        //player.PlayerHealthBar.value = player.CurrentHP;
        player.Status.slider.value = player.CurrentHP;
        player.Status.text.text = player.AppliedEffect.ToString();

        if(player.AppliedEffect == StatusEffect.None)
        {
            player.Status.color.color = Color.white;
        }
        else if (player.AppliedEffect == StatusEffect.Burned)
        {
            player.Status.color.color = Color.red;
        }
        else if (player.AppliedEffect == StatusEffect.Paralyzed)
        {
            player.Status.color.color = Color.yellow;
        }
        else if (player.AppliedEffect == StatusEffect.Poisoned)
        {
            player.Status.color.color = Color.green;
        }
    }
}
