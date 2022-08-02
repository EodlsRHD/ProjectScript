using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class ShopCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public struct dataInfo
    {
        public float index;
        public string name;
        public float cost;
        public float cardclass;
        public string skill;
        public float attacktype;
        public float turn;
        public string description;
        public float damage;
    }

    public dataInfo datainfo;

    public Text cardname;
    public Text description;
    public Text energy;
    public Image cardimage;

    public Transform ShopCardPos;

    void Awake()
    {
        datainfo = new dataInfo();
    }

    public void DataInfo(string _name)
    {
        CarddataInfo? Info = DataManager.instance.GetAllCarddataInfo(_name);
        if (Info.HasValue)
        {
            datainfo.index = GameManager.instance.CARDLIST.Count;
            datainfo.name = Info.Value.name;
            datainfo.cost = Info.Value.cost;
            datainfo.cardclass = Info.Value.cardclass;
            datainfo.skill = Info.Value.skill;
            datainfo.attacktype = Info.Value.attacktype;
            datainfo.turn = Info.Value.turn;
            datainfo.description = Info.Value.description;
            datainfo.damage = Info.Value.damage;
        }
        cardname.text = datainfo.name.ToString();
        description.text = datainfo.description.ToString();
        energy.text = datainfo.cost.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData _eventData) //¶®À»¶§ »ç¶óÁü
    {
        gameObject.SetActive(false);
    }
}
