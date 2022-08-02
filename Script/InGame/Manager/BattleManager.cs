using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using UnityEngine;

public class BattleManager
{
    //전투함수 작성하는 곳
    //몬스터는 몬스터이름을 받아서 스텟을 찾아서 검사


    //공격////////////////////////////아이템X//////////////////////////////////

    public static float Battle(float _cardDamage, float _userAttack) //유저가 공격했을시 최종 데미지를 반환
    {
        float FinalDamage;
        FinalDamage = _cardDamage + (_cardDamage * (_userAttack * 0.01f)); //카드 데미지에 유저 스텟공격력을 곱해준다

        return FinalDamage;
    }

    public static float Battle(float _monsterdamage, float _userDefense, float _userEvasion) //몬스터가 공격했을시 최종 데미지를 반환
    {
        float MonsterDamage;
        MonsterDamage = _monsterdamage + (_monsterdamage * (_userDefense * 0.01f)); //몬스터 데미지에 유저 방어력을 감소

        float FinalDamage;
        if(!Evasion(_userEvasion))
        {
            FinalDamage = 0;
            return FinalDamage;
        }
        return MonsterDamage;
    }

    public static float Battle(string _monsterSkill, float _userDefense) //(스킬사용)몬스터가 공격했을시 최종 데미지를 반환
    {
        float MonsterSkillDamege;
        MonsterSkillDamege = HowUseSkill(_monsterSkill);

        float FinalDamage;
        FinalDamage = (MonsterSkillDamege * (_userDefense * 0.01f)); //몬스터 데미지에 유저 방어력을 감소

        return FinalDamage;
    }


    //공격////////////////////////////아이템O//////////////////////////////////

    public static float Battle(float _cardDamage, float _userAttack, List<ItemdataInfo> _itemlist) //유저가 공격했을시 최종 데미지를 반환
    {
        float ItemDamege;
        ItemDamege = HowUseItem(_itemlist);

        float FinalDamage;
        FinalDamage = _cardDamage + ((_cardDamage * (ItemDamege * 0.01f)) + (_userAttack * 0.01f)); //카드 데미지에 유저 스텟공격력을 곱해준다

        return FinalDamage;
    }


    ////////////////////////////////////////////////////////////////////////////

    public static float HowUseItem(List<ItemdataInfo> _itemlist) //아이템 이름을 받아 총합데미지를 반환
    {
        float Damege = 0;
        for (int i = 0; i < _itemlist.Count; i++)
        {
            Damege += _itemlist[i].damage;
        }

        return Damege;
    }

    public static float HowUseSkill(string _skillName) //스킬 이름을 받아 데미지를 반환
    {
        float Damege;
        Damege = 1f;

        return Damege;
    }

    public static bool Evasion(float _evasion)
    {
        bool DamageControl = true;
        ushort[] ran = new ushort[100];
        for (int i = 0; i < _evasion; i++)
        {
            ran[i] = 1;
        }
        for (int i = 0; i < ran.Length; i++)
        {
            if(ran[i] != 1)
            {
                ran[i] = 0;
            }
        }
        for (int i = 0; i < ran.Length; i++)
        {
            int j = Random.Range(0, ran.Length);

            ushort tmp = ran[i];
            ran[i] = ran[j];
            ran[j] = tmp;
        }
        if(ran[0] == 1) //1이면 (_evasion/100)이므로
        {
            DamageControl = false;
            Debug.Log("Evasion");
            return DamageControl; //데미지 안받음
        }
        return DamageControl; //데미지 받음
    }
}
