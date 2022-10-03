using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Phy_Card : AbCard_Class, IEndDragHandler, IBeginDragHandler, IDragHandler
{
    Image Target;

    public override void Set_targetImage(Image _target)
    {
        Target = _target;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Target.gameObject.SetActive(true);
        Target.transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Target.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Target.gameObject.SetActive(false);
        new_MouseTrack.SetMouseEventPos(eventData, this.gameObject);
    }
}