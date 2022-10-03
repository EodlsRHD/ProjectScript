using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Real_cardClass : MonoBehaviour // factory Pattern
{
    AbCard_Class vir;

    public AbCard_Class Set_TargetImage(string _skill, GameObject _obj)
    {
        if(_skill.Equals("null"))
        {
            _obj.AddComponent<Phy_Card>();
            vir = _obj.GetComponent<Phy_Card>();
        }
        else
        {
            _obj.AddComponent<Magic_Card>();
            vir = _obj.GetComponent<Magic_Card>();
        }
        return vir;
    }
}