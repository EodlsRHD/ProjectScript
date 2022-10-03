using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class new_IngameManager : PoolManager_Order_abstructClass, IObserver_Havior
{
    private static new_IngameManager new_ingameManager;

    [SerializeField] private Transform Character_Pos;
    [SerializeField] private Transform Monster1_Pos;
    [SerializeField] private Transform Monster2_Pos;
    [SerializeField] private RectTransform Card_Pool;
    [SerializeField] private RectTransform Spawn_Pos;

    [SerializeField] private Transform Fild_Pos;
    [SerializeField] private GameObject GmaeTile;

    private new_Character Character_Live_SC;
    private Character_Info Character_Live_info;
    new_BattleManager new_battleManager;

    private ushort MonsterSpawnNum;
    private List<new_Monster> MonsterLive_list;
    private int Num;

    private List<new_Card> Voitcard_Obj_list;

    protected ushort Item_Count;
    protected Queue<Item_Info> Player_UseItem_queue;
    protected List<Item_Info> Player_HaveItem_list;

    protected bool Turn;

    private void Awake()
    {
        new_ingameManager = this;

        Character_Live_info = new_GameManager.instance.UserChar_Info;
        new_battleManager = this.gameObject.GetComponent<new_BattleManager>();

        MonsterSpawnNum = 2;
        MonsterLive_list = new List<new_Monster>();
        Num = 0;

        Voitcard_Obj_list = new List<new_Card>();

        Item_Count = 1;
        Player_UseItem_queue = new Queue<Item_Info>();
        Player_HaveItem_list = new List<Item_Info>();

        Turn = false;
        StartCoroutine(SaveData());
    }

    private void Start()
    {
        GameSet();
    }

    private void GameSet()
    {
        Send_request(false, 4, GmaeTile.name, 0, null, Receive_request);
        Send_request(false, 1, Character_Live_info.job, 0, null, Receive_request);
        int SpawnMonster = Random.Range(1, MonsterSpawnNum + 1);
        for (int i = 0; i < SpawnMonster; i++)
        {
            int a = Random.Range(1, new_GameManager.instance.Monster_list.Count + 1) - 1;
            Send_request(false, 2, new_GameManager.instance.Monster_list[i].name, 0, null, Receive_request);
        }
        new_CardManager.ShakeCard();
        new_CardManager.voitCard(new_GameManager.voitCard_Num);
        new_EnergyControll.SettingMana();
    }

    IEnumerator SaveData()
    {
        if(MonsterLive_list.Count == 0)
        {
            Character_Live_info.savecount += 1;
            new_GameManager.instance.UserChar_Info = Character_Live_info;
        }
        //if(Character_Live_SC == null)
        //{
        //    Debug.Log("GameOver");
        //}
        yield break;
    }

    public static void PlayerTurn()
    {
        new_CardManager.voitCard(new_GameManager.voitCard_Num);
        new_EnergyControll.SettingMana();
    }

    public static void MonsterTurn()
    {
        new_CardManager.HandCard_AllRemove();
        for (int i = 0; i < new_ingameManager.MonsterLive_list.Count; i++)
        {
            new_ingameManager.MonsterLive_list[i].UpdateData(new_ingameManager.MonsterLive_list[i].info_.damage);
        }
    }

    public override void Receive_request(Pool_request _Request)
    {
        switch (_Request.type)
        {
            case 1: // Character
                var tmp1= _Request.obj.GetComponent<new_Character>();
                Character_Live_SC = tmp1;
                if (Character_Live_info.savecount > tmp1.info_.savecount)
                {
                    tmp1.info_ = Character_Live_info;
                    tmp1.AddObserver(new_battleManager);
                }
                Set_parant(_Request.obj, Character_Pos);
                break;
            case 2: // Monster
                var tmp2 = _Request.obj.GetComponent<new_Monster>();
                MonsterLive_list.Add(tmp2);
                tmp2.AddObserver(new_battleManager);
                switch (Num)
                {
                    case 0:
                        Set_parant(_Request.obj, Monster1_Pos);
                        Num++;
                        break;
                    case 1:
                        Set_parant(_Request.obj, Monster2_Pos);
                        Num++;
                        break;
                }
                break;
            case 3: // Card
                Voitcard_Obj_list.Add(_Request.obj.GetComponent<new_Card>());
                _Request.obj.transform.position = Spawn_Pos.position;
                _Request.obj.transform.SetParent(Spawn_Pos);
                if (Voitcard_Obj_list.Count == new_GameManager.voitCard_Num)
                {
                    new_CardManager.RefreshCardPos(Voitcard_Obj_list);
                    Voitcard_Obj_list.RemoveAll(o => true);
                }
                break;
            case 4: // Fild
                Set_parant(_Request.obj, Fild_Pos);
                break;

        }
    }

    private void Set_parant(GameObject _obj, Transform _trans)
    {
        _obj.SetActive(true);
        _obj.transform.SetParent(_trans);
        _obj.transform.position = _trans.position;
    }

    public void Notify_Death(ushort _type, GameObject _obj)
    {
        switch(_type)
        {
            case 1:
                Character_Live_SC = null;
                break;

            case 2:
                MonsterLive_list.Remove(_obj.GetComponent<new_Monster>());
                break;
        }
    }

    public void Notify(ushort _damage)
    {
        //Dummy
    }
}
