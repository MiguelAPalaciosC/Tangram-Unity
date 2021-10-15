using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake()
    {
        current = this;
    }

    public event Action<Draggable> OnPieceUp;

    public void PieceUp(Draggable draggable)
    {
        Debug.Log("Entro GameEvents  DR: " + draggable + "   ----- OPU: " + OnPieceUp);
        if (OnPieceUp != null && draggable != null)
        {
            OnPieceUp(draggable);
        }
    }
}
