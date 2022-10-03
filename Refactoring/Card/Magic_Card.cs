using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Magic_Card : AbCard_Class, IEndDragHandler, IBeginDragHandler, IDragHandler
{
    public override void Set_targetImage(Image _target)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        new_MouseTrack.SetMouseEventPos(eventData, this.gameObject);
    }
}
