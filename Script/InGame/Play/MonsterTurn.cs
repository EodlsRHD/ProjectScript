using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTurn : SingleTon<MonsterTurn>
{
    public void PhyMonsterAttack(Monster _monster) 
    {
        if (_monster.monsterDataInfo.size.Equals("small"))
        {
            GetPhyDamage(_monster, 0.5f);
        }
        if (_monster.monsterDataInfo.size.Equals("medium"))
        {
            GetPhyDamage(_monster, 1f);
        }
        if (_monster.monsterDataInfo.size.Equals("large"))
        {
            GetPhyDamage(_monster, 1f);
        }
    }

    public void MageMonsterAttack(Monster _monster)
    {
        if (_monster.monsterDataInfo.size.Equals("small"))
        {
            GetMageDamage(_monster, 1f);
        }
        if (_monster.monsterDataInfo.size.Equals("medium"))
        {
            GetMageDamage(_monster, 1f);
        }
        if (_monster.monsterDataInfo.size.Equals("large"))
        {
            GetMageDamage(_monster, 1.5f);
        }
    }

    void GetMageDamage(Monster _monster, float _num)
    {
        float DAMAGE = BattleManager.Battle(_monster.monsterDataInfo.damage, GameManager.instance.character.characterDataInfo.phyDefense, GameManager.instance.character.characterDataInfo.evasion);
        float FinalDamage = DAMAGE * _num;
        GameManager.instance.MonAni_Attack = true;
        GameManager.instance.character.characterDataInfo.health -= FinalDamage;
        GameManager.instance.MonsterDamageNum = FinalDamage;
        GameManager.instance.MESSAGE = "물리공격  ,  MonsterName   : " + _monster.monsterDataInfo.name + ",  MonsterFinalDamage   : " + FinalDamage;
    }

    void GetPhyDamage(Monster _monster, float _num)
    {
        float DAMAGE = BattleManager.Battle(_monster.monsterDataInfo.damage, GameManager.instance.character.characterDataInfo.phyDefense, GameManager.instance.character.characterDataInfo.evasion);
        float FinalDamage = DAMAGE * _num;
        GameManager.instance.MonAni_Attack = true;
        GameManager.instance.character.characterDataInfo.health -= FinalDamage;
        GameManager.instance.MonsterDamageNum = FinalDamage;
        GameManager.instance.MESSAGE = "물리공격  ,  MonsterName   : " + _monster.monsterDataInfo.name + ",  MonsterFinalDamage   : " + FinalDamage;
    }

    public void MonsterUseSkill(Monster _monster)
    {
        GameManager.instance.MESSAGE = "스킬사용  ,  MonsterName   : " + _monster.monsterDataInfo.name;
        GameManager.instance.MonAni_Attack = true;
        GameManager.instance.MonsterDamageNum = 0;
    }

    public void HowBehaviour(Monster _monster)
    {
        //1은 일반 공격, 2는 스킬 사용
        int Persent = 70; //일반 공격할 확률
        ushort[] monsterBehaviour = new ushort[100];
        for(int i = 0; i < Persent; i++)
        {
            monsterBehaviour[i] = 1;
        }
        for (int i = 0; i < monsterBehaviour.Length; i++)
        {
            if(monsterBehaviour[i] != 1)
            {
                monsterBehaviour[i] = 2;
            }
        }
        for (int i = 0; i < monsterBehaviour.Length; i++)
        {
            int j = Random.Range(0, monsterBehaviour.Length);

            var tmp = monsterBehaviour[i];
            monsterBehaviour[i] = monsterBehaviour[j];
            monsterBehaviour[j] = tmp;
        }
        if (monsterBehaviour[0] == 1)
        {
            if (_monster.monsterDataInfo.attacktype.Equals("phy"))
            {
                PhyMonsterAttack(_monster);
            }
            if (_monster.monsterDataInfo.attacktype.Equals("Mage"))
            {
                MageMonsterAttack(_monster);
            }
        }
        if(monsterBehaviour[0] == 2)
        {
            MonsterUseSkill(_monster);
        }

    }
}
