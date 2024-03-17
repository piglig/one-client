using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DragDrop : NetworkBehaviour
{
    private bool isDragging = false;
    private bool isDraggable = true;

    private GameObject startParent;
    private Vector2 startPosition;

    private GameObject dropZone;
    private bool isOverDropZone;

    public GameObject Canvas;
    
    public PlayerManager PlayerManager;
    void Start()
    {
        Canvas = GameObject.Find("Main Canvas");
        if (!isOwned) {
            isDraggable = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colliding!");
        isOverDropZone = true;
        dropZone = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Uncolliding!");
        isOverDropZone = false;
        dropZone = null;
    }

    public void StartDrag()
    {
        if (!isDraggable) {
            return;
        }

        isDragging = true;
        startParent = transform.parent.gameObject;
        startPosition = transform.position;
    }

    public void EndDrag()
    {
        if (!isDraggable) {
            return;
        }

        isDragging = false;
        if (isOverDropZone)
        {
            isDraggable = false;
            NetworkIdentity identity = NetworkClient.connection.identity;
            PlayerManager = identity.GetComponent<PlayerManager>();
            PlayerManager.PlayCard(gameObject);
        } else
        {
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging) 
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
    }
}
