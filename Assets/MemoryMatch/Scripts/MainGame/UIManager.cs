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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateUI(Player player)
    {
        //player.PlayerHealthBar.value = player.CurrentHP;
        if (player.Status == null)
        {
            Debug.Log("player is NULLL");
            return;
        }
        Debug.Log("Player status: " + player.Status.ToString() + ", HP: " + player.CurrentHP);
        player.Status.slider.value = player.CurrentHP;
        player.Status.text.text = player.AppliedEffect.ToString();

        if(player.AppliedEffect == StatusEffect.None)
        {
            player.Status.color.color = Color.white;
        }
        else if (player.AppliedEffect == StatusEffect.Burned)
        {
            player.Status.color.color = new Color(255, 63, 0);
        }
        else if (player.AppliedEffect == StatusEffect.Paralyzed)
        {
            player.Status.color.color = Color.yellow;
        }
        else if (player.AppliedEffect == StatusEffect.Poisoned)
        {
            player.Status.color.color = new Color(147, 0, 255);
        }
    }
}
