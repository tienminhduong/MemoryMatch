using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public int player1Health = 10;
    public int player2Health = 10;
    public int player1Shield = 0;
    public int player2Shield = 0;
    public int player1PoisonStacks = 0;
    public int player2PoisonStacks = 0;
    public int maxHealth = 10;
    public int maxShield = 5;

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

    public void ModifyHealth(int player, int amount)
    {
        if (amount >= 0)
        {
            if (player == 1)
            {
                player1Health = Mathf.Clamp(player1Health + amount, 0, maxHealth);
            }
            else
            {
                player2Health = Mathf.Clamp(player2Health + amount, 0, maxHealth);
            }
        }
        else
        {
            if (player == 1)
            {
                AbsorbDamage(ref player1Health, ref player1Shield, amount);
            }
            else
            {
                AbsorbDamage(ref player2Health, ref player2Shield, amount);
            }
        }
    }

    private void AbsorbDamage(ref int health, ref int shield, int damage)
    {
        int remainingDamage = Mathf.Abs(damage);

        if (shield > 0)
        {
            int damageAbsorbed = Mathf.Min(shield, remainingDamage);
            shield -= damageAbsorbed;
            remainingDamage -= damageAbsorbed;
        }

        if (remainingDamage > 0)
        {
            health = Mathf.Clamp(health - remainingDamage, 0, maxHealth);
        }
    }

    public void ModifyShield(int player, int amount)
    {
        if (player == 1)
        {
            player1Shield = Mathf.Clamp(player1Shield + amount, 0, maxShield);
        }
        else
        {
            player2Shield = Mathf.Clamp(player2Shield + amount, 0, maxShield);
        }
    }

    public void ApplyPoisonDamage(int player)
    {
        if (player == 1 && player1PoisonStacks > 0)
        {
            ModifyHealth(1, -player1PoisonStacks);
            player1PoisonStacks = 0;
        }
        else if (player == 2 && player2PoisonStacks > 0)
        {
            ModifyHealth(2, -player2PoisonStacks);
            player2PoisonStacks = 0;
        }
    }

    public void AddPoisonStacks(int player, int stacks)
    {
        if (player == 1)
        {
            player1PoisonStacks += stacks;
        }
        else
        {
            player2PoisonStacks += stacks;
        }
    }

    public void RemovePoisonStacks(int player)
    {
        if (player == 1)
        {
            player1PoisonStacks = 0;
        }
        else
        {
            player2PoisonStacks = 0;
        }
    }
}
