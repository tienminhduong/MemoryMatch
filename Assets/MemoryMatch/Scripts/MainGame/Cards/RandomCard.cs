using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCard : Card
{
    public override void ActivateEffect(Player turnPlayer, Player nonturnPlayer) {
        base.ActivateEffect(turnPlayer, nonturnPlayer);

        int id = Random.Range(0, (int)CardCategory.Random);
        while (id == CardValue)
            id = Random.Range(0, (int)CardCategory.Random);

        Debug.Log("Activate " + ((CardCategory)id).ToString() + " effect!");

        Card card = Instantiate(GameBoardManager.Instance.CardPrefabs[id]);
        card.ActivateEffect(turnPlayer, nonturnPlayer);
        Destroy(card.gameObject);
    }
}
