using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropController : MonoBehaviour
{
    public delegate void DragEndedDelegate(DragAndDropController draggableObject);

    public DragEndedDelegate dragEndedCallback;

    public float gridSize = 0.5f;
    public bool snapToGrid = true;
    public bool smartDrag = true;
    public bool isDraggable = true;
    public bool isDragged = false;
    Vector2 initialPositionMouse;
    Vector2 initialPositionObject;
    public static int w = 4;
    public static int h = 4;
    public static Transform[,] grid = new Transform[w, h];
    public bool validatePosition;
    public bool coliderObject;
    public bool coliderAllObjects;
    public int cont;
    public int listOb;
    private List<Collider2D> listaObjetos;
    private Dictionary<Collider2D, bool> dicObjetos;
    public ListObject LO;

    private void Start()
    {
        
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
        if(isDraggable && Input.GetMouseButtonDown(0))
        {
            if (smartDrag)
            {
                initialPositionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                initialPositionObject = transform.position;
            }
            isDragged = true;
        }
    }

    private void OnMouseUp()
    {
        isDragged = false;
        cont++;
        //Debug.Log(cont);
    }

    private bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Playefield.roundVec2(child.position);

            // Not inside Border?
            if (!Playefield.insideBorder(v))
                return false;

            // Block in grid cell (and not part of same group)?
            if (Playefield.grid[(int)v.x, (int)v.y] != null &&
                Playefield.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        coliderObject = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
         coliderObject = false;
    }

    private void updateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < Playefield.h; ++y)
            for (int x = 0; x < Playefield.w; ++x)
                if (Playefield.grid[x, y] != null)
                    if (Playefield.grid[x, y].parent == transform)
                        Playefield.grid[x, y] = null;

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 v = Playefield.roundVec2(child.position);
            Playefield.grid[(int)v.x, (int)v.y] = child;
        }
    }
}
