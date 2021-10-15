using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class DraggableS : MonoBehaviour
{
    public delegate void DragEndedDelegate(DraggableS draggableObject);

    public float gridSize = 0.5f;
    public bool snapToGrid = true;
    public DragEndedDelegate dragEndedCallback;
    public DraggableS dragS;
    public bool smartDrag = true;
    private bool isDragged = false;
    public bool isDraggable = true;
    Vector2 initialPositionMouse;
    Vector2 initialPositionObject;
    private Vector3 mouseDragStartPosition;
    private Vector3 spriteDragStartPosition;
    private bool isOverSomething = false;
    public int punto;
    private SnapControllS snapC;
    private GameEventsS geS;

    private void Start()
    {
        snapC = GameObject.Find("GameObject").GetComponent<SnapControllS>();
        geS = new  GameEventsS();
    }
    void Update()
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

    private void OnMouseOver()
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

    private void OnMouseDown()
    {
        isDragged = true;
        mouseDragStartPosition = GetMousePosition();
        spriteDragStartPosition = new Vector2(Mathf.RoundToInt(transform.localPosition.x), Mathf.RoundToInt(transform.localPosition.y));
        geS.PieceUp(dragS);
    }

    private void OnMouseDrag()
    {
        if (isDragged)
        {
            transform.localPosition = spriteDragStartPosition + GetMousePosition() - mouseDragStartPosition;
        }
        
    }

    private void OnMouseUp()
    {
       
        isDragged = false;
        Debug.Log("hizo algo :V");
        //if (this != null)
        //{
        dragEndedCallback(dragS);
        //}
        snapC.actualizarScore(punto);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colisiono");
        isOverSomething = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("No Colisiono");
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
