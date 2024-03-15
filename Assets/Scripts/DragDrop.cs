using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private bool isDragging = false;
    private GameObject startParent;
    private Vector2 startPosition;

    private GameObject dropZone;
    private bool isOverDropZone;

    public GameObject DropZone;
    public GameObject Canvas;
    
    void Start()
    {
        Canvas = GameObject.Find("Main Canvas");
        DropZone = GameObject.Find("DropZone");
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
        isDragging = true;
        startParent = transform.parent.gameObject;
        startPosition = transform.position;
    }

    public void EndDrag()
    {
        isDragging = false;
        if (isOverDropZone)
        {
            transform.SetParent(dropZone.transform, false);
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
