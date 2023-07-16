using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    // Start is called before the first frame update
    private RectTransform rectTransform;
    CanvasGroup canvasGroup;
    public static GameObject itemBeingDragged;
    Vector3 startPos;
    Transform startParent;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        startPos = transform.position;
        startParent = transform.parent;
        transform.SetParent(transform.root);
        itemBeingDragged = gameObject;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       itemBeingDragged = null;

       if (transform.parent == startParent || transform.parent == transform.root)
       {
        transform.position = startPos;
        transform.SetParent(startParent);
       }
       canvasGroup.alpha = 1f;
       canvasGroup.blocksRaycasts = true;
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
