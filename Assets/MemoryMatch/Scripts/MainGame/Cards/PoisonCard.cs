using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCard : AttackCard
{
    public override void ActivateEffect(Player turnPlayer, Player nonturnPlayer) {
        base.ActivateEffect(turnPlayer, nonturnPlayer);
        nonturnPlayer.SetStatusEffect(StatusEffect.Poisoned);
        SoundManager.Instance.PlayAudioClip(5);
    }
}
