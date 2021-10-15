using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ListObject : MonoBehaviour
{
    public List<DragAndDropController> listObj;

    public List<Transform> snapPoints;
    public List<DragAndDropController> draggableObjects;
    public List<Vector3> auxDraggables;
    public GameObject pieza;
    //public TextMeshProUGUI text;
    //public Button resetButton;
    public float snapRange = 0.5f;
    //private int Depth = 5;

    //private GridGenerate<DragAndDropController> grid;
    private ArrayList goodPositions;
    // Start is called before the first frame update
    void Start()
    {
        foreach (DragAndDropController item in listObj)
        {
            item.dragEndedCallback = OnDragEnded;
        }
    }

    private void OnDragEnded(DragAndDropController draggableObject)
    {
        float closestDistance = -1;
        Transform closestSnapPoint = null;
        foreach (Transform snapPoint in snapPoints)
        {
            float currentDistance = Vector2.Distance(draggableObject.transform.localPosition, snapPoint.localPosition);
            if (closestSnapPoint == null || currentDistance < closestDistance)
            {
                closestSnapPoint = snapPoint;
                closestDistance = currentDistance;
            }
        }

        if (closestSnapPoint != null && closestDistance <= snapRange)
        {
            Vector2 fixedSnapPoint = new Vector2(closestSnapPoint.localPosition.x, closestSnapPoint.localPosition.y);
            draggableObject.transform.localPosition = fixedSnapPoint;
            //grid.setValue(fixedSnapPoint, draggableObject);
        }
        if (!win())
        {
            Debug.Log("");
            DragAndDropController auxPiece = getOkPiece();
            //Debug.Log(auxPiece);
            var goodPosToAuxPiece = getSamePieces(auxPiece);
            if (goodPositions.Count >= 2)
            {
                foreach (Vector2 dra in goodPosToAuxPiece)
                {
                    auxPiece.transform.localPosition = dra;
                    //grid.setValue(dra, auxPiece);
                }
            }
            //Debug.Log(grid.getPieces().Keys.Count);
        }
        else
        {
            Debug.Log("finish.....");
            //7/7/var Keys = grid.getPieces().Keys.Select(x => x.name).ToList();
            //var Values = grid.getPieces().Values.ToList();
            Dictionary<string, Vector2> auxDict = new Dictionary<string, Vector2>();
            //for (int i = 0; i < Keys.Count; i++)
            //{
               // auxDict[Keys[i]] = Values[i];
            //}
            if (!goodPositions.Contains(auxDict.ToString()))
            {
                goodPositions.Add(auxDict);
            }
        }
    }

    public bool win()
    {
        return collicions() && inMarco();
    }

    bool collicions()
    {
        bool list = false;
        foreach (DragAndDropController item in listObj)
        {
            if (item.coliderObject)
            {
                list = true;
            }
        }
        Debug.Log("----->>>>> collicion" + list);
        return list;
    }
    bool inMarco()
    {
        Debug.Log("----->>> count" + (listObj.Count == CountObjects.l));
        return listObj.Count == CountObjects.l;
    }
    private void resetDraggables()
    {
        for (int i = 0; i < listObj.Count; i++)
        {
            listObj[i].transform.localPosition = auxDraggables[i];
            //grid.resetDict();
        }
    }
    private DragAndDropController getRandomPiece(int i)
    {
        return listObj[i];
    }

    private DragAndDropController getOkPiece()
    {
        DragAndDropController d = default(DragAndDropController);
        for (int i = 0; i < 15; i++)
        {
            d = getRandomPiece(i);
            //if (!grid.getPieces().ContainsKey(d)) break;
        }
        return d;
    }

    private ArrayList getSamePieces(DragAndDropController wantedPiece)
    {
        ArrayList samePieces = new ArrayList();
        foreach (Dictionary<string, Vector2> dic in goodPositions)
        {
            samePieces.Add(dic[wantedPiece.name]);
        }
        return samePieces;
    }
}
