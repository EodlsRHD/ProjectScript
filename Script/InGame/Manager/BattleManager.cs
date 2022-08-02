using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using UnityEngine;

public class BattleManager
{
    //�����Լ� �ۼ��ϴ� ��
    //���ʹ� �����̸��� �޾Ƽ� ������ ã�Ƽ� �˻�


    //����////////////////////////////������X//////////////////////////////////

    public static float Battle(float _cardDamage, float _userAttack) //������ ���������� ���� �������� ��ȯ
    {
        float FinalDamage;
        FinalDamage = _cardDamage + (_cardDamage * (_userAttack * 0.01f)); //ī�� �������� ���� ���ݰ��ݷ��� �����ش�

        return FinalDamage;
    }

    public static float Battle(float _monsterdamage, float _userDefense, float _userEvasion) //���Ͱ� ���������� ���� �������� ��ȯ
    {
        float MonsterDamage;
        MonsterDamage = _monsterdamage + (_monsterdamage * (_userDefense * 0.01f)); //���� �������� ���� ������ ����

        float FinalDamage;
        if(!Evasion(_userEvasion))
        {
            FinalDamage = 0;
            return FinalDamage;
        }
        return MonsterDamage;
    }

    public static float Battle(string _monsterSkill, float _userDefense) //(��ų���)���Ͱ� ���������� ���� �������� ��ȯ
    {
        float MonsterSkillDamege;
        MonsterSkillDamege = HowUseSkill(_monsterSkill);

        float FinalDamage;
        FinalDamage = (MonsterSkillDamege * (_userDefense * 0.01f)); //���� �������� ���� ������ ����

        return FinalDamage;
    }


    //����////////////////////////////������O//////////////////////////////////

    public static float Battle(float _cardDamage, float _userAttack, List<ItemdataInfo> _itemlist) //������ ���������� ���� �������� ��ȯ
    {
        float ItemDamege;
        ItemDamege = HowUseItem(_itemlist);

        float FinalDamage;
        FinalDamage = _cardDamage + ((_cardDamage * (ItemDamege * 0.01f)) + (_userAttack * 0.01f)); //ī�� �������� ���� ���ݰ��ݷ��� �����ش�

        return FinalDamage;
    }


    ////////////////////////////////////////////////////////////////////////////

    public static float HowUseItem(List<ItemdataInfo> _itemlist) //������ �̸��� �޾� ���յ������� ��ȯ
    {
        float Damege = 0;
        for (int i = 0; i < _itemlist.Count; i++)
        {
            Damege += _itemlist[i].damage;
        }

        return Damege;
    }

    public static float HowUseSkill(string _skillName) //��ų �̸��� �޾� �������� ��ȯ
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
        if(ran[0] == 1) //1�̸� (_evasion/100)�̹Ƿ�
        {
            DamageControl = false;
            Debug.Log("Evasion");
            return DamageControl; //������ �ȹ���
        }
        return DamageControl; //������ ����
    }
}
