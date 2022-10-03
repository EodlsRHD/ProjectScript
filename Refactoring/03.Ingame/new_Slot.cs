using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class new_Slot : MonoBehaviour
{
    public Item_Info info_;
    public Image icon;
    public ushort Solt_Index;

    RectTransform rcTr;
    Rect rc;
    public Rect RECT
    {
        get
        {
            rc.x = rcTr.position.x - rcTr.rect.width * 0.5f;
            rc.y = rcTr.position.y + rcTr.rect.height * 0.5f;
            return rc;
        }
    }

    public string ICONNAME
    {
        get
        {
            if (icon.gameObject.activeSelf == true && icon.sprite != null)
            {
                return icon.sprite.name;
            }
            return string.Empty;
        }
    }

    void Start()
    {
        rcTr = GetComponent<RectTransform>();

        rc.xMax = rcTr.rect.width;
        rc.yMax = rcTr.rect.height;

        rc.width = rcTr.rect.width;
        rc.height = rcTr.rect.height;
    }

    public bool IsInRect(Vector2 _pos)
    {
        if (_pos.x >= RECT.x &&
            _pos.x <= RECT.x + RECT.width &&
            _pos.y <= RECT.y &&
            _pos.y >= RECT.y - RECT.height)
        {
            return true;
        }
        return false;
    }
}
