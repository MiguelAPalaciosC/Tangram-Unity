using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsS : MonoBehaviour
{
    public static GameEventsS current;
    private void Awake()
    {
        current = this;
    }

    public event Action<DraggableS> OnPieceUp;

    public void PieceUp(DraggableS draggable)
    {
        Debug.Log("Entro GameEvents  DR: " + draggable + "   ----- OPU: " + OnPieceUp);
        if (OnPieceUp != null && draggable != null)
        {
            OnPieceUp(draggable);
        }
    }
}
