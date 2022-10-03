using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class new_Monster : Virtual_Character
{
    public Monster_Info info_;
    protected Animator ani_;

    new_BattleManager new_battleManager = null;

    private void Awake()
    {
        ani_ = GetComponent<Animator>();
    }

    public override void AddObserver(new_BattleManager _sc)
    {
        new_battleManager = _sc;
        new_battleManager.RegisterObserver(this);
    }

    public void SetHit(ushort _damage)
    {
        Debug.Log("Hit damage   : " + _damage);
        info_.health -= _damage;
    }

    public void UpdateData(ushort _damege)
    {
        new_battleManager.UpdateData(_damege);
        //Animation
    }

    public void Update_Death(ushort _type, GameObject _obj)
    {
        new_battleManager.Update_Death(_type, _obj);
        //Animation
    }

    public override void Set_Animation(string _behavior)
    {
        
    }

    private void Update()
    {
        if(info_.health <= 0 || 10000 < info_.health)
        {
            this.gameObject.SetActive(false);
            Update_Death(2, this.gameObject);
        }
    }

    public override void Notify(ushort _Damage)
    {
        //Dummy
    }
}
