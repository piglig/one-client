using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;



public class PlayerManager : NetworkBehaviour 
{
    public GameObject Card1;
    public GameObject Card2;
    public GameObject PlayerArea;
    public GameObject EnemyArea;
    public GameObject DropZone;

    List<GameObject> cards = new List<GameObject>();
    
    public override void OnStartClient() {
        base.OnStartClient();

        PlayerArea = GameObject.Find("PlayerArea");
        EnemyArea = GameObject.Find("EnemyArea");
        DropZone = GameObject.Find("DropZone");
    }

    [Server]
    public override void OnStartServer() {
        base.OnStartServer();

        cards.Add(Card1);
        cards.Add(Card2);
        Debug.Log(cards);
    }

    [Command]
    public void CmdDealCards()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject card = Instantiate(cards[Random.Range(0, cards.Count)], new Vector2(0, 0), Quaternion.identity);
            NetworkServer.Spawn(card, connectionToClient);
            card.transform.SetParent(PlayerArea.transform, false);
            RpcShowCard(card, "Dealt");
        }
    }

    public void PlayCard(GameObject card)
    {
        CmdPlayCard(card);
    }

    [Command]
    public void CmdPlayCard(GameObject card)
    {
        RpcShowCard(card, "Played");
    }

    [ClientRpc]
    void RpcShowCard(GameObject card, string type) {
        if (type == "Dealt") {
            if (isOwned) {
                card.transform.SetParent(PlayerArea.transform, false);
            } else {
                card.transform.SetParent(EnemyArea.transform, false);
                card.GetComponent<CardFlipper>().Flip();
            }
        } else if (type == "Played") {
            card.transform.SetParent(DropZone.transform, false);
            if (!isOwned)
            {
                card.GetComponent<CardFlipper>().Flip();
            }
        }
    }
}
