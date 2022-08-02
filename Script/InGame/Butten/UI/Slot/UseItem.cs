using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UseItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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
    public ItemdataInfo itemdataInfo;
    int Startturn;

    public GameObject ItemInfo;
    public Image ItemImage;
    public Text ItemName;
    public Text ItemTurn;

    float remain;
    bool a = true;

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
            dataInfo.damage = Info.Value.damage;
        }
    }

    void Update()
    {
        if (a)
        {
            Startturn = GameManager.instance.TURNCOUNT;
            a = false;
        }
        remain = Startturn + dataInfo.turn - GameManager.instance.TURNCOUNT;
        if (remain <= 0)
        {
            gameObject.SetActive(false);
            GameManager.instance.UseItemList.Remove(itemdataInfo);
        }
        if (gameObject.activeSelf == false)
        {
            a = true;
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData _eventData)
    {
        if (Input.GetMouseButtonDown(1))
        {
            ItemImage.sprite = ResourcesManager.ItemImageLoad(dataInfo.name);
            ItemName.text = dataInfo.name;
            ItemTurn.text = remain + "턴 남았습니다.";
            ItemInfo.SetActive(true);
        }
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData _eventData)
    {
        ItemInfo.SetActive(false);
    }
}