using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] int maxHP;
    [SerializeField] int currentHP;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] StatusEffect appliedEffect;
    int numberTurnsEffectRemain;

    public StatusEffect AppliedEffect => appliedEffect;
    public int CurrentHP => currentHP;
    public int MaxHP => maxHP;

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