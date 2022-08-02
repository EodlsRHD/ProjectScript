using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public struct DataInfo
    {
        public float index;
        public string name;
        public float type;
        public float turn;
        public string description;
    }

    public DataInfo dataInfo;
    public Image icon;

    public float _IndeX;

    RectTransform rcTr;
    Rect rc;
    public Rect RECT // 원점을 좌 상단으로 위치 시킨다.
    {
        get
        {
            rc.x = rcTr.position.x - rcTr.rect.width * 0.5f;
            rc.y = rcTr.position.y + rcTr.rect.height * 0.5f;
            return rc;
        }
    }

    public string ICONNAME // 호출시 아이콘의 이름을 반환해준다.
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

        //원점 초기화 - 질문 : 왜 RectTransform으로 따로 초기화를 해주느냐
        rc.xMax = rcTr.rect.width;
        rc.yMax = rcTr.rect.height;

        rc.width = rcTr.rect.width;
        rc.height = rcTr.rect.height;
    }

    public void ItemDataInfo(string _name)
    {
        ItemdataInfo? Info = DataManager.instance.GetAllItemdataInfo(_name);
        if (Info.HasValue)
        {
            dataInfo.index = Info.Value.index;
            dataInfo.name = Info.Value.name;
            dataInfo.type = Info.Value.type;
            dataInfo.turn = Info.Value.turn;
            dataInfo.description = Info.Value.description;
        }
    }

    public bool IsInRect(Vector2 _pos) // _pos가 슬롯의 범위 안에 있냐 없냐를 bool값을 반환해준다.
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
