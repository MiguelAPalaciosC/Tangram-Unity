using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class DragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{

    private RectTransform rectTrans;
    public Canvas myCanvas;
    private CanvasGroup canvasGroup;

    void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clik");

    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        Debug.Log("BeginDrag");
        //canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {

        Debug.Log("Ondrag");
        rectTrans.anchoredPosition += eventData.delta;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
        //canvasGroup.alpha = .1f;
        canvasGroup.blocksRaycasts = true;

    }


    void Update()
    {
        
    }
}
