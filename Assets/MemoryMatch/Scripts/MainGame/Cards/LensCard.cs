using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensCard : Card
{
    [SerializeField] float revealTime;

    public override void ActivateEffect(Player turnPlayer, Player nonturnPlayer) {
        base.ActivateEffect(turnPlayer, nonturnPlayer);
        GameBoardManager.Instance.RevealAllCardsInSeconds(revealTime);
    }
}
