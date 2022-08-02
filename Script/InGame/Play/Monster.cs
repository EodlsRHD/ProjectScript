using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public struct MonsterDatainfo
    {
        public float index;
        public string name;
        public float stage;
        public string size;
        public float health;
        public string skill;
        public string attacktype;
        public string type;
        public float damage;
    }

    public MonsterDatainfo monsterDataInfo;
    public Animator animator;

    [Header("∏ÛΩ∫≈Õ Ω∫≈›")]
    public string PublicName;
    public float PublicIndex;
    public float PublicHealth;

    public Slider Hp;
    public Transform Canvase;
    Vector3 HpPosition;

    void Awake()
    {
        GameManager.instance.MonAni_Die = false;
        animator = GetComponent<Animator>();
        monsterDataInfo = new MonsterDatainfo();
        HpPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position) + new Vector3(0,-50f,0);
        GameManager.instance.MonsterCanvasePoslist.Add(HpPosition);
    }

    public void DataInfo(string _name)
    {
        MonsterDataInfo? Info = DataManager.instance.GetMonsterdataInfo(_name);
        if (Info.HasValue)
        {
            monsterDataInfo.index = Info.Value.index;
            monsterDataInfo.name = Info.Value.name;
            monsterDataInfo.stage = Info.Value.stage;
            monsterDataInfo.size = Info.Value.size;
            monsterDataInfo.health = Info.Value.health;
            monsterDataInfo.skill = Info.Value.skill;
            monsterDataInfo.attacktype = Info.Value.attacktype;
            monsterDataInfo.type = Info.Value.type;
            monsterDataInfo.damage = Info.Value.damage;
        }
        PublicName = monsterDataInfo.name;
        PublicIndex = monsterDataInfo.index;
        PublicHealth = monsterDataInfo.health;

        Hp.maxValue = monsterDataInfo.health;
        Hp.transform.SetParent(Canvase);
        Hp.transform.position = HpPosition;
        Hp.gameObject.SetActive(true);
    }

    void Update()
    {
        Hp.value = monsterDataInfo.health;
        if (GameManager.instance.MonAni_GetHit)
        {
            if (monsterDataInfo.name.Equals(GameManager.instance.MonAni_HitName))
            {
                animator.SetTrigger("GetHit");
            }
        }
        if (GameManager.instance.MonAni_Attack)
        {
            animator.SetTrigger("Attack");
        }
        if (monsterDataInfo.health <= 0)
        {
            animator.SetTrigger("Die");
            monsterDataInfo.health = 0;
            Hp.gameObject.SetActive(false);
            GameManager.instance.Livemonsterlist.Remove(gameObject.GetComponent<Monster>());
        }
    }
}