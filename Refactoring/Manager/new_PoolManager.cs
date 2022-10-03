using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Events;

public interface IPoolManager_Order
{
    void Send_request(bool _bool, ushort _type, string orderName, ushort uni, GameObject _obj, UnityAction<Pool_request> _action);
    void Receive_request(Pool_request _Request);
}

public abstract class PoolManager_Order_abstructClass : MonoBehaviour, IPoolManager_Order
{
    public void Send_request(bool _bool, ushort _type, string orderName, ushort uni, GameObject _obj, UnityAction<Pool_request> _action)
    {
        Pool_request re = new Pool_request(_bool, _type, orderName, uni, _obj, _action);
        new_PoolManager.Enqueue_Request(re);
    }

    public virtual void Receive_request(Pool_request _Request)
    {
        // Use UnityAction<> Mathod.
    }
}

public class new_PoolManager : PoolManager_Order_abstructClass
{
    private static new_PoolManager new_poolManager;
    Queue<Pool_request> Request_queue;

    [HideInInspector]
    public List<GameObject> Character_Obj_Pool;
    [HideInInspector]
    public List<GameObject> Monster_Obj_Pool;
    [HideInInspector]
    public List<GameObject> Card_Obj_Pool;
    [HideInInspector]
    public List<GameObject> Fild_Obj_Pool;
    [HideInInspector]
    public List<GameObject> DeckPos_Pool;

    public Transform Character_Pool_Obj;
    public Transform Monster_Pool_Obj;
    public RectTransform Card_Pool_Obj;
    public Transform Fild_Pool_Obj;

    private void Awake()
    {
        new_poolManager = this;
        Request_queue = new Queue<Pool_request>();

        Character_Obj_Pool = new List<GameObject>();
        Monster_Obj_Pool = new List<GameObject>();
        Card_Obj_Pool = new List<GameObject>();
        Fild_Obj_Pool = new List<GameObject>();
        DontDestroyOnLoad(this.gameObject);
    }

    public static void Enqueue_Request(Pool_request _request)
    {
        new_poolManager.Request_queue.Enqueue(_request);
    }

    private void Update()
    {
        if(Request_queue.Count > 0)
        {
            var tmp = Request_queue.Dequeue();

            if(tmp.bool_)
            {
                SaveObj(tmp);
            }
            else
            {
                RequestObj(tmp);
            }
        }
    }

    private void SaveObj(Pool_request _request)
    {
        switch(_request.type)
        {
            case 1 : // character
                Character_Obj_Pool.Add(_request.obj);
                _request.obj.transform.SetParent(Character_Pool_Obj);
                _request.obj.SetActive(false);
                break;
            case 2: // monster
                Monster_Obj_Pool.Add(_request.obj);
                _request.obj.transform.SetParent(Monster_Pool_Obj);
                _request.obj.SetActive(false);
                break;
            case 3: // card
                Card_Obj_Pool.Add(_request.obj);
                _request.obj.transform.position = Card_Pool_Obj.position;
                _request.obj.transform.rotation = Utils.QI;
                _request.obj.transform.SetParent(Card_Pool_Obj);
                _request.obj.SetActive(false);
                break;
            case 4: // Fild
                Fild_Obj_Pool.Add(_request.obj);
                _request.obj.transform.SetParent(Fild_Pool_Obj);
                _request.obj.SetActive(false);
                break;
        }
    }

    private void RequestObj(Pool_request _request)
    {
        switch(_request.type)
        {
            case 1: // character
                GameObject obj1 = FindObj(_request.type, Character_Obj_Pool, _request.requestObjName);
                Pool_request re1 = new Pool_request(_request.bool_, _request.type, _request.requestObjName, _request.uniqueNum, obj1, _request.action);
                re1.action(re1);
                break;
            case 2: // monster
                GameObject obj2 = FindObj(_request.type, Monster_Obj_Pool, _request.requestObjName);
                Pool_request re2 = new Pool_request(_request.bool_, _request.type, _request.requestObjName, _request.uniqueNum,  obj2, _request.action);
                re2.action(re2);
                break;
            case 3: // card
                GameObject obj3 = FindCard(_request.type, Card_Obj_Pool, _request.requestObjName);
                Pool_request re3 = new Pool_request(_request.bool_, _request.type, _request.requestObjName, _request.uniqueNum,  obj3, _request.action);
                re3.action(re3);
                break;
            case 4: // Fild
                GameObject obj4 = FindObj(_request.type, Fild_Obj_Pool, _request.requestObjName);
                Pool_request re4 = new Pool_request(_request.bool_, _request.type, _request.requestObjName, _request.uniqueNum,  obj4, _request.action);
                re4.action(re4);
                break;
        }
    }

    private GameObject FindObj(ushort _type, List<GameObject> _list, string _name)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            if (_list[i].gameObject.name.Equals(_name))
            {
                return _list[i];
            }
        }
        GameObject tmp = new_ResourcesManager.New_Instantiate(_type, _name);
        _list.Add(tmp);
        return tmp;
    }

    private GameObject FindCard(ushort _type, List<GameObject> _list, string _name)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            if (_list[i].GetComponent<new_Card>().info_.name.Equals(_name))
            {
                return _list[i];
            }
        }
        GameObject tmp = new_ResourcesManager.New_Instantiate(_type, _name);
        _list.Add(tmp);
        return tmp;
    }
}