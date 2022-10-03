using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class new_Character : Virtual_Character
{
    public Character_Info info_;
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

    public override void Notify(ushort _damage)
    {
        Debug.Log("Hit damage   : " + _damage);
        info_.health -= _damage;
    }

    public void Update_Death(ushort _type, GameObject _obj)
    {
        new_battleManager.Update_Death(_type, _obj);
    }

    public override void Set_Animation(string _behavior)
    {
        
    }

    private void Update()
    {
        if (info_.health <= 0 || 10000 < info_.health)
        {
            this.gameObject.SetActive(false);
            Update_Death(1, this.gameObject);
        }
    }
}
