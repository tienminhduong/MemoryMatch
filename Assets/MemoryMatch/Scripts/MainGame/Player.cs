using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
<<<<<<< Updated upstream
    [SerializeField] int maxHP;
    [SerializeField] int currentHP;

    StatusEffect appliedEffect;
    int numberTurnsEffectRemain;
=======
    [SerializeField] protected int index;
    [SerializeField] protected int maxHP;
    [SerializeField] protected int currentHP;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Slider playerHealthBar;
    [SerializeField] protected PlayerStatusUI status;
    [SerializeField] protected StatusEffect appliedEffect;
    protected int numberTurnsEffectRemain;
>>>>>>> Stashed changes

    public StatusEffect AppliedEffect => appliedEffect;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHP = maxHP;
        appliedEffect = StatusEffect.None;
        numberTurnsEffectRemain = 0;
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
=======
        if (PlayerManager.Instance != null)
        {
            spriteRenderer.enabled = PlayerManager.Instance.CurrentTurnPlayerIndex == index;
        }
        else if (BotPlayerManager.Instance != null)
        {
            spriteRenderer.enabled = BotPlayerManager.Instance.CurrentTurnPlayerIndex == index;
        }
    }

    public void UpdateStatsUI()
    {
>>>>>>> Stashed changes
        
    }

    public void UpdateEndTurn() {
        if (appliedEffect == StatusEffect.None)
            return;

        if (appliedEffect == StatusEffect.Poisoned)
            currentHP -= maxHP / 16;
        if (appliedEffect == StatusEffect.Burned)
            currentHP -= maxHP / 8;
        numberTurnsEffectRemain--;
        if (numberTurnsEffectRemain == 0)
            appliedEffect = StatusEffect.None;
    }

    public void SetStatusEffect(StatusEffect effect) {
        appliedEffect = effect;
        if (effect == StatusEffect.Poisoned) numberTurnsEffectRemain = 4;
        else if (effect == StatusEffect.Burned) numberTurnsEffectRemain = 2;
        else if (effect == StatusEffect.Paralyzed) numberTurnsEffectRemain = 1;
        else numberTurnsEffectRemain = 0;
    }
}

public enum StatusEffect
{
    None, Poisoned, Burned, Paralyzed
}