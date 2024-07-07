using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages the health, shield, and poison stacks of players
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
        // Singleton pattern to ensure only one instance of HealthManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Modify player's health, positive amount heals, negative amount deals damage
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

    // Absorb damage using shield first, then health
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

    // Modify player's shield, ensuring it does not exceed max shield
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

    // Apply poison damage to a player
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

    // Add poison stacks to a player
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

    // Remove all poison stacks from a player
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
