using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Draggable : MonoBehaviour
{
    public delegate void DragEndedDelegate(Draggable draggableObject);

    public float gridSize = 0.5f;
    public bool snapToGrid = true;
    public DragEndedDelegate dragEndedCallback;
    public Draggable dragb;
    public bool smartDrag = true;
    private bool isDragged = false;
    public bool isDraggable = true;
    Vector2 initialPositionMouse;
    Vector2 initialPositionObject;
    private Vector3 mouseDragStartPosition;
    private Vector3 spriteDragStartPosition;
    private bool isOverSomething = false;
    PhotonView view;
    public int punto;
    private SnapControll snapCt;
    private GameEvents geS;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        snapCt = GameObject.Find("GameObject").GetComponent<SnapControll>();
        geS = new GameEvents();
    }
    void Update()
    {
        if (view.IsMine)
        {
            if (isDragged)
            {
                if (!smartDrag)
                {
                    transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                else
                {
                    transform.position = initialPositionObject +
                    (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - initialPositionMouse;
                }
                if (snapToGrid)
                {

                    transform.position = new Vector2(Mathf.RoundToInt(transform.position.x / gridSize) * gridSize,
                    Mathf.RoundToInt(transform.position.y / gridSize) * gridSize);
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    transform.Rotate(0, 0, -90);
                }
            }
        }
    }
        
    

    private void OnMouseOver()
    {
        if (view.IsMine)
        {
            if (isDraggable && Input.GetMouseButtonDown(0))
            {
                if (smartDrag)
                {
                    initialPositionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    initialPositionObject = transform.position;
                }
                isDragged = true;

            }
        }
    }

    private void OnMouseDown()
    {
        if (view.IsMine)
        {
            isDragged = true;
            mouseDragStartPosition = GetMousePosition();
            spriteDragStartPosition = new Vector2(Mathf.RoundToInt(transform.localPosition.x), Mathf.RoundToInt(transform.localPosition.y));
            geS.PieceUp(dragb);
            Debug.Log("Mouseown");
        }
    }

    private void OnMouseDrag()
    {
        if (view.IsMine)
        {
            if (isDragged)
            {
                transform.localPosition = spriteDragStartPosition + GetMousePosition() - mouseDragStartPosition;
            }
        }
        
        
    }

    private void OnMouseUp()
    {
        if (view.IsMine)
        {
            isDragged = false;
            dragb = this;
            dragEndedCallback(dragb);
            snapCt.actualizarScore(punto);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOverSomething = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isOverSomething = false;
    }

    public bool collides()
    {
        return isOverSomething;
    }

    private Vector3 GetMousePosition()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 rounded = new Vector2(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y));
        //Debug.Log(rounded);
        return rounded;
    }
}
