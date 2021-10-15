using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playefield : MonoBehaviour
{
    public static int w = 3;
    public static int h = 3;
    public static Transform[,] grid = new Transform[w, h];

    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.RoundToInt(v.x),
                           Mathf.RoundToInt(v.y));
    }

    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 &&
                (int)pos.x < w &&
                (int)pos.y >= 0 &&
                (int)pos.y < h);
    }

    public static bool isArrayFull()
    {
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                Debug.Log(x + "," + y + "= " + grid[x, y]);
                if (grid[x, y] == null)
                    return false;
            }
        }
        return true;
    }
}
