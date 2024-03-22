using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TargetClick : NetworkBehaviour
{
    public PlayerManager PlayerManager;

    public void OnTargetClick()
    {
        NetworkIdentity identity = NetworkClient.connection.identity;
        PlayerManager = identity.GetComponent<PlayerManager>();

        if (isOwned)
        {
            PlayerManager.CmdTargetSelfCard();
        } else
        {
            PlayerManager.CardTargetOtherCard(gameObject);
        }

    }
}
