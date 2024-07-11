using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] int maxHP;
    [SerializeField] int currentHP;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Slider playerHealthBar;

    [SerializeField] StatusEffect appliedEffect;
    int numberTurnsEffectRemain;

    public StatusEffect AppliedEffect => appliedEffect;
    public int CurrentHP => currentHP;
    public int MaxHP => maxHP;
    public Slider PlayerHealthBar => playerHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        appliedEffect = StatusEffect.None;
        numberTurnsEffectRemain = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.enabled = PlayerManager.Instance.CurrentTurnPlayerIndex == index;
    }

    public void UpdateStatsUI()
    {
        UIManager.Instance.HealthUpdate(this);
    }

    public void UpdateEndTurn() {
        if (appliedEffect == StatusEffect.None)
            goto ui;

        if (appliedEffect == StatusEffect.Poisoned)
            ModifyHP(maxHP / 16);
        if (appliedEffect == StatusEffect.Burned)
            ModifyHP(maxHP / 8);
        numberTurnsEffectRemain--;
        if (numberTurnsEffectRemain == 0)
            appliedEffect = StatusEffect.None;
        ui:
        UpdateStatsUI();
    }

    public void SetStatusEffect(StatusEffect effect) {
        appliedEffect = effect;
        if (effect == StatusEffect.Poisoned) numberTurnsEffectRemain = 5;
        else if (effect == StatusEffect.Burned) numberTurnsEffectRemain = 3;
        else if (effect == StatusEffect.Paralyzed) numberTurnsEffectRemain = 2;
        else numberTurnsEffectRemain = 0;
    }

    public void ModifyHP(int amount) {
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }
}

public enum StatusEffect
{
    None, Poisoned, Burned, Paralyzed
}