using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct Card_Info
{
    public ushort uniqueNum;
    public ushort index;
    public string name;
    public ushort cost;
    public ushort cardclass;
    public string skill;
    public ushort attacktype;
    public ushort turn;
    public string description;
    public ushort damage;
}

public struct Item_Info
{
    public ushort uniqueNum;
    public ushort index;
    public string name;
    public ushort type;
    public ushort turn;
    public string description;
    public ushort damage;
}

public struct Monster_Info
{
    public ushort uniqueNum;
    public ushort index;
    public string name;
    public ushort stage;
    public string size;
    public ushort health;
    public ushort max_health;
    public string skill;
    public string attacktype;
    public string type;
    public ushort damage;
}

public struct Character_Info
{
    public ushort index;
    public string job;
    public ushort health;
    public ushort phyAttack;
    public ushort phyDefense;
    public ushort mageAttack;
    public ushort mageDefense;
    public ushort evasion;
    public ushort energy;
    public ushort savecount;
}

public struct Skill_Info
{
    public ushort index;
    public string name;
    public string howuse;
    public ushort buf;
    public string description;
    public ushort damage;
}

public struct User_Info
{
    public string ID;
    public UnityAction<string> action;
}

public struct Pool_request
{
    public bool bool_; // true : save , false : 
    public ushort type; // 1 : character , 2 : monster , 3 : card
    public string requestObjName;
    public ushort uniqueNum;
    public GameObject obj;
    public UnityAction<Pool_request> action;

    public Pool_request(bool _bool, ushort _type, string _str, ushort _uni, GameObject _obj, UnityAction<Pool_request> _action)
    {
        bool_ = _bool;
        type = _type;
        requestObjName = _str;
        uniqueNum = _uni;
        obj = _obj;
        action = _action;
    }
}