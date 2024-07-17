using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BombCard : AttackCard
{
    public override void ActivateEffect(Player turnPlayer, Player nonturnPlayer) {
        base.ActivateEffect(turnPlayer, nonturnPlayer);
        if (Random.Range(0, 1) == 0)
            nonturnPlayer.SetStatusEffect(StatusEffect.Burned);
    }
}
