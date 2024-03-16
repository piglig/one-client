using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DrawCards : NetworkBehaviour
{

    public PlayerManager PlayerManager;

    public void OnClick()
    {
        NetworkIdentity identity = NetworkClient.connection.identity;
        PlayerManager = identity.GetComponent<PlayerManager>();
        PlayerManager.CmdDealCards();
    }
}
