using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public struct CharacterDataInfo
    {
        public float index;
        public string job;
        public float health;
        public float phyAttack;
        public float phyDefense;
        public float mageAttack;
        public float mageDefense;
        public float evasion;
        public float energy;
    }

    public CharacterDataInfo characterDataInfo;
    public Animator animator; 

    [Header("캐릭터")]
    public string PublicName;
    public float PublicIndex;
    public float PublicHealth;

    public Transform ClearPos;

    public Slider Hp;
    public Transform Canvase;
    Vector3 HpPosition;

    public float OriginalHp;

    void Awake()
    {
        animator = GetComponent<Animator>();
        characterDataInfo = new CharacterDataInfo();
        HpPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position) + new Vector3(0, -50f, 0);
        GameManager.instance.CharacterCanvasePos = HpPosition;
    }

    //캐릭터 데이터 정하기
    public void DataInfo(PlayerDataInfo? Info)
    {
        if (Info.HasValue)
        {
            characterDataInfo.index = Info.Value.index;
            characterDataInfo.job = Info.Value.job;
            characterDataInfo.health = Info.Value.health;
            characterDataInfo.phyAttack = Info.Value.phyAttack;
            characterDataInfo.phyDefense = Info.Value.phyDefense;
            characterDataInfo.mageAttack = Info.Value.mageAttack;
            characterDataInfo.mageDefense = Info.Value.mageDefense;
            characterDataInfo.evasion = Info.Value.evasion;
            characterDataInfo.energy = Info.Value.energy;
        }
        PublicName = characterDataInfo.job;
        PublicIndex = characterDataInfo.index;
        PublicHealth = characterDataInfo.health;

        Hp.maxValue = OriginalHp;
        Hp.transform.SetParent(Canvase);
        Hp.transform.position = HpPosition;
        Hp.gameObject.SetActive(true);
    }

    public void FirstDataInfo(PlayerDataInfo? _playerDataInfo)
    {
        characterDataInfo.index = _playerDataInfo.Value.index;
        characterDataInfo.job = _playerDataInfo.Value.job;
        characterDataInfo.health = _playerDataInfo.Value.health;
        characterDataInfo.phyAttack = _playerDataInfo.Value.phyAttack;
        characterDataInfo.phyDefense = _playerDataInfo.Value.phyDefense;
        characterDataInfo.mageAttack = _playerDataInfo.Value.mageAttack;
        characterDataInfo.mageDefense = _playerDataInfo.Value.mageDefense;
        characterDataInfo.evasion = _playerDataInfo.Value.evasion;
        characterDataInfo.energy = _playerDataInfo.Value.energy;

        Hp.maxValue = OriginalHp;
        Hp.transform.SetParent(Canvase);
        Hp.transform.position = HpPosition;
        Hp.gameObject.SetActive(true);
    }

    void Update()
    {
        Hp.value = characterDataInfo.health;
        if(GameManager.instance.CharAni_Attack)
        {
            animator.SetTrigger("Attack");
        }
        if(GameManager.instance.CharAni_GetHit)
        {
            animator.SetTrigger("GetHit");
        }
        if (GameManager.instance.character.characterDataInfo.index.Equals(characterDataInfo.index))
        {
            GameManager.instance.character.characterDataInfo.health = characterDataInfo.health;
        }
        if (characterDataInfo.health <= 0)
        {
            characterDataInfo.health = 0;
            animator.SetTrigger("Die");
            GameManager.instance.CharAni_Die = true;
        }
        if(GameManager.instance.CharAni_StageClear)
        {
            animator.SetTrigger("Run");
            gameObject.transform.position = Vector3.MoveTowards(transform.position, ClearPos.position, Time.deltaTime * 17);
        }
    }
}
