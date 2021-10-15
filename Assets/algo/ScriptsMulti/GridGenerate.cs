using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerate<TListObject> : MonoBehaviour
{
    //private TListObject[,] gridArray;
    private Dictionary<TListObject, Vector2> pieces;
    public float[,] positions;
    private int _width;
    private int _height;
    public GridGenerate(int _width, int _height, int cellSize, GameObject cuadro, List<Transform> snaps)
    {
        this._width = _width;
        this._height = _height;
        this.pieces = new Dictionary<TListObject, Vector2>();
        this.positions = new float[_width, _height];

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                snaps.Add(Instantiate(cuadro, new Vector2((i), (j)), Quaternion.identity).transform);
            }
        }
    }

    public void resetDict()
    {
        this.pieces = new Dictionary<TListObject, Vector2>();
    }

    public void setValue(Vector2 pos, TListObject value)
    {
        this.pieces[value] = pos;
    }

    public Dictionary<TListObject,Vector2> getPieces()
    {
        return this.pieces;
    }

    public void deletePiece(TListObject obje)
    {
        try
        {
            this.pieces.Remove(obje);
        }
        catch (System.Exception) { }
    }
}
