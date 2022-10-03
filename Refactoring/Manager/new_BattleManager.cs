using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IObserver_Havior
{
    void Notify(ushort _Damage);
    void Notify_Death(ushort _type, GameObject _obj);
}

public interface Observer_Subjectga
{
    void RegisterObserver(IObserver_Havior _o);
    void RemoveObserver(IObserver_Havior _o);
    void NotifyObserver(bool _bool);
}

public class new_BattleManager : MonoBehaviour, Observer_Subjectga
{
    private List<IObserver_Havior> Ob_list;
    private ushort damage;
    private ushort type;
    private GameObject obj;

    void Awake()
    {
        Ob_list = new List<IObserver_Havior>();
    }

    public void RegisterObserver(IObserver_Havior _o)
    {
        Ob_list.Add(_o);
    }

    public void RemoveObserver(IObserver_Havior _o)
    {
        Ob_list.Remove(_o);
    }

    public void NotifyObserver(bool _bool)
    {
        if(_bool == true)
        {
            foreach (var one in Ob_list)
            {
                one.Notify(damage);
            }
            damage = 0;
        }
        if(_bool == false)
        {
            foreach (var one in Ob_list)
            {
                one.Notify_Death(type, obj);
            }
            type = 0;
            obj = null;
        }
    }

    public void UpdateData(ushort _damege)
    {
        damage = _damege;
        NotifyObserver(true);
    }

    public void Update_Death(ushort _type, GameObject _obj)
    {
        type = _type;
        obj = _obj;
        NotifyObserver(false);
    }
}