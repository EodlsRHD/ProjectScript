using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Virtual_Character : MonoBehaviour, IObserver_Havior
{
    public abstract void AddObserver(new_BattleManager _sc);

    public abstract void Notify(ushort _Damage);

    public virtual void Notify_Death(ushort _type, GameObject _obj)
    {
        
    }

    public abstract void Set_Animation(string _behavior);
}
