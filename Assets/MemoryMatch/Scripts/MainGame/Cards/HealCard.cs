using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCard : Card
{
    [SerializeField] int healAmount;

    public override void ActivateEffect(Player turnPlayer, Player nonturnPlayer) {
        base.ActivateEffect(turnPlayer, nonturnPlayer);
        turnPlayer.ModifyHP(healAmount);
        SoundManager.Instance.PlayAudioClip(3);
    }
}
