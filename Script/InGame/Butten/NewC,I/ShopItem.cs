using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public struct DataInfo
    {
        public float index;
        public string name;
        public float type;
        public float turn;
        public string description;
        public float damage;
    }

    public DataInfo dataInfo;
    public Image icon;

    public void ItemDataInfo(float _index)
    {
        ItemdataInfo? Info = DataManager.instance.GetItemdataInfo(_index);
        if (Info.HasValue)
        {
            dataInfo.index = GameManager.instance.ItemList.Count;
            dataInfo.name = Info.Value.name;
            dataInfo.type = Info.Value.type;
            dataInfo.turn = Info.Value.turn;
            dataInfo.description = Info.Value.description;
            dataInfo.damage = Info.Value.damage;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }
}
