using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionCard : HealCard
{
    public override void ActivateEffect(Player turnPlayer, Player nonturnPlayer) {
        base.ActivateEffect(turnPlayer, nonturnPlayer);
        turnPlayer.SetStatusEffect(StatusEffect.None);
    }
}
