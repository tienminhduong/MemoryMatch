using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class AttackCard : Card
{
    [SerializeField] int damage;

    public override void ActivateEffect(Player turnPlayer, Player nonturnPlayer) {
        base.ActivateEffect(turnPlayer, nonturnPlayer);
        nonturnPlayer.ModifyHP(-damage);
        SoundManager.Instance.PlayAudioClip(1);
    }
}
