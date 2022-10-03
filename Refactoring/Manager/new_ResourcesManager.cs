using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class new_ResourcesManager : PoolManager_Order_abstructClass
{
    private static new_ResourcesManager new_resourcesManager;

    private List<GameObject> Character_Prefab_Pool;
    private List<GameObject> Monster_Prefab_Pool;
    private List<Sprite> CardSprite_Prefab_Pool;
    private List<Sprite> ItemSprite_Prefab_Pool;
    private List<Sprite> SkillSprite_Prefab_Poo;

    private List<Character_Info> CharacterInfo_list;
    private List<Monster_Info> MonsterInfo_list;
    private List<Card_Info> CardInfo_list;
    private List<Item_Info> ItemInfo_list;
    private List<Skill_Info> SkillInfo_list;

    private List<GameObject> Monster_Complete_Obj;
    private List<GameObject> Card_Complete_Obj;

    string character_stat_path;
    string monster_stat_path;
    string card_stat_path;
    string item_stat_path;
    string skill_stat_path;

    public GameObject Card_Prefab;

    private void Awake()
    {
        new_resourcesManager = this;

        Character_Prefab_Pool = new List<GameObject>();
        Monster_Prefab_Pool = new List<GameObject>();
        CardSprite_Prefab_Pool = new List<Sprite>();
        ItemSprite_Prefab_Pool = new List<Sprite>();
        SkillSprite_Prefab_Poo = new List<Sprite>();

        CharacterInfo_list = new List<Character_Info>();
        MonsterInfo_list = new List<Monster_Info>();
        CardInfo_list = new List<Card_Info>();
        ItemInfo_list = new List<Item_Info>();
        SkillInfo_list = new List<Skill_Info>();

        Monster_Complete_Obj = new List<GameObject>();
        Card_Complete_Obj = new List<GameObject>();

        DataPath();
        StartCoroutine(LOAD());
        DontDestroyOnLoad(this.gameObject);

    }
    private void DataPath()
    {
        character_stat_path = Application.dataPath + @"\Resources\PlayerData\CharacterStats.csv";
        monster_stat_path = Application.dataPath + @"\Resources\PlayerData\FloorOneMonsterCollector.csv";
        card_stat_path = Application.dataPath + @"\Resources\PlayerData\Card.csv";
        item_stat_path = Application.dataPath + @"\Resources\PlayerData\Item.csv";
        skill_stat_path = Application.dataPath + @"\Resources\PlayerData\Skill.csv";
    }

    IEnumerator LOAD()
    {
        yield return null;
        Load_CharacterData(character_stat_path);
        Character_Prefab_Load();

        yield return null;
        Load_MonsterData(monster_stat_path);
        Monster_Prefab_Load();

        yield return null;
        Load_CardData(card_stat_path);
        Card_Sprite_Load();

        yield return null;
        Load_ItemData(item_stat_path);
        Item_Sprite_Load();

        yield return null;
        Load_SkillData(skill_stat_path);
        Skill_Sprite_Load();

        yield return null;
        Fild_Prefab_Load();
    }

    private Sprite Find_ItemImage(string _name)
    {
        for (int i = 0; i < ItemSprite_Prefab_Pool.Count; i++)
        {
            if (_name.Equals(ItemSprite_Prefab_Pool[i]))
            {
                return ItemSprite_Prefab_Pool[i];
            }
        }
        return null;
    }

    ///////////////////////////////////////////////////////////////////////////////////////

    private void Character_Prefab_Load()
    {
        GameObject[] player = Resources.LoadAll<GameObject>(@"Prefab\PlayerCharacter");
        Character_Prefab_Pool = ArrayToList<GameObject>(player);
        Character_Ins(Character_Prefab_Pool, CharacterInfo_list);
    }

    private void Monster_Prefab_Load()
    {
        GameObject[] monster = Resources.LoadAll<GameObject>(@"Prefab\Monster");
        Monster_Prefab_Pool = ArrayToList<GameObject>(monster);
        Monster_Ins(Monster_Prefab_Pool, MonsterInfo_list);
    }

    private void Card_Sprite_Load()
    {
        Sprite[] card = Resources.LoadAll<Sprite>(@"Prefab\CardForward");
        CardSprite_Prefab_Pool = ArrayToList<Sprite>(card);
        Card_Ins(CardSprite_Prefab_Pool, CardInfo_list);
    }

    private void Item_Sprite_Load()
    {
        Sprite[] item = Resources.LoadAll<Sprite>(@"Prefab\Item");
        ItemSprite_Prefab_Pool = ArrayToList<Sprite>(item);
    }

    private void Skill_Sprite_Load()
    {
        Sprite[] item = Resources.LoadAll<Sprite>(@"Prefab\skill");
        SkillSprite_Prefab_Poo = ArrayToList<Sprite>(item);
    }

    private void Fild_Prefab_Load()
    {
        GameObject[] fild = Resources.LoadAll<GameObject>(@"Prefab\Ground");
        for(int i = 0; i < fild.Length; i++)
        {
            GameObject tmp = Instantiate(fild[i]);
            tmp.gameObject.name = tmp.gameObject.name.Replace("(Clone)", "");
            tmp.SetActive(false);
            Send_request(true, 4, string.Empty, 0, tmp, Receive_request);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////

    private void Character_Ins(List<GameObject> _obj_pool, List<Character_Info> _data_pool)
    {
        for (int i = 0; i < _obj_pool.Count; i++)
        {
            GameObject tmp = MonoBehaviour.Instantiate(_obj_pool[i]);
            tmp.gameObject.name = tmp.gameObject.name.Replace("(Clone)", "");
            tmp.AddComponent<new_Character>();
            var Com = tmp.GetComponent<new_Character>();
            for (int j = 0; j < _data_pool.Count; j++)
            {
                if (tmp.name == _data_pool[j].job)
                {
                    Com.info_ = _data_pool[j];
                }
            }
            tmp.SetActive(false);
            Send_request(true, 1, string.Empty, 0,tmp, Receive_request);
        }
    }

    private void Monster_Ins(List<GameObject> _obj_pool, List<Monster_Info> _data_pool)
    {
        ushort uniqueNum = 0;
        for (int i = 0; i < _obj_pool.Count; i++)
        {
            GameObject tmp = MonoBehaviour.Instantiate(_obj_pool[i]);
            tmp.gameObject.name = tmp.gameObject.name.Replace("(Clone)", "");
            tmp.AddComponent<new_Monster>();
            new_Monster Com = tmp.GetComponent<new_Monster>();
            for (int j = 0; j < _data_pool.Count; j++)
            {
                if (tmp.name == _data_pool[j].name)
                {
                    //Debug.Log("tmp.name   : " + tmp.name + ",  _data_pool[j].name   : " + _data_pool[j].name);
                    Com.info_ = _data_pool[j];
                    Com.info_.uniqueNum = uniqueNum;
                    uniqueNum++;
                    break;
                }
            }
            new_GameManager.instance.Monster_list.Add(Com);
            tmp.SetActive(false);
            Monster_Complete_Obj.Add(tmp);
            Send_request(true, 2, string.Empty, Com.info_.uniqueNum, tmp, Receive_request);
        }
    }

    private void Card_Ins(List<Sprite> _obj_pool, List<Card_Info> _data_pool)
    {
        ushort uniqueNum = 0;
        for (int i = 0; i < _obj_pool.Count; i++)
        {
            GameObject tmp = MonoBehaviour.Instantiate(Card_Prefab);
            tmp.gameObject.name = tmp.gameObject.name.Replace("(Clone)", "");
            var Com = tmp.GetComponent<new_Card>();
            for (int k = 0; k < _data_pool.Count; k++)
            {
                if (_obj_pool[i].name == _data_pool[k].name)
                {
                    Com.SetData(_data_pool[k], _obj_pool[i], uniqueNum);
                    uniqueNum++;
                    break;
                }
            }
            new_GameManager.instance.UserCard_list.Add(Com);
            Com.Setclass(new Real_cardClass());
            tmp.SetActive(false);
            Card_Complete_Obj.Add(tmp);
            Send_request(true, 3, string.Empty, Com.info_.uniqueNum, tmp, Receive_request);
        }
    }

    //private void Item_Ins(List<Sprite> _obj_pool, List<Item_Info> _data_pool)
    //{
    //    ushort uniqueNum = 0;
    //    for (int i = 0; i < _obj_pool.Count; i++)
    //    {
    //        GameObject tmp = MonoBehaviour.Instantiate(Card_Prefab);
    //        var Com = tmp.AddComponent<new_Item>();
    //        Com.sprite = _obj_pool[i];
    //        for (int j = 0; j < _data_pool.Count; j++)
    //        {
    //            if (_obj_pool[i].name == _data_pool[j].name)
    //            {
    //                Com.info_ = _data_pool[j];
    //                Com.info_.uniqueNum = uniqueNum;
    //                uniqueNum++;
    //                break;
    //            }
    //        }
    //        tmp.transform.SetParent(Card_Pool_Obj);
    //        tmp.SetActive(false);
    //    }
    //}

    //private void skill_Ins(List<Sprite> _obj_pool, List<Item_Info> _data_pool)
    //{
    //    ushort uniqueNum = 0;
    //    for (int i = 0; i < _obj_pool.Count; i++)
    //    {
    //        GameObject tmp = MonoBehaviour.Instantiate(Card_Prefab);
    //        var Com = tmp.AddComponent<new_Item>();
    //        Com.sprite = _obj_pool[i];
    //        for (int j = 0; j < _data_pool.Count; j++)
    //        {
    //            if (_obj_pool[i].name == _data_pool[j].name)
    //            {
    //                Com.info_ = _data_pool[j];
    //                Com.info_.uniqueNum = uniqueNum;
    //                uniqueNum++;
    //                break;
    //            }
    //        }
    //        tmp.transform.SetParent(Card_Pool_Obj);
    //        tmp.SetActive(false);
    //    }
    //}

    ///////////////////////////////////////////////////////////////////////////////////////

    private void Load_CharacterData(string _path)
    {
        using (StreamReader sr = new StreamReader(_path))
        {
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] datas = line.Split(',');
                Character_Info tmp = new Character_Info();
                int num = 0;
                tmp.index = ushort.Parse(datas[num++]);
                tmp.job = datas[num++];
                tmp.health = ushort.Parse(datas[num++]);
                tmp.phyAttack = ushort.Parse(datas[num++]);
                tmp.phyDefense = ushort.Parse(datas[num++]);
                tmp.mageAttack = ushort.Parse(datas[num++]);
                tmp.mageDefense = ushort.Parse(datas[num++]);
                tmp.evasion = ushort.Parse(datas[num++]);
                tmp.energy = ushort.Parse(datas[num++]);
                tmp.savecount = ushort.Parse(datas[num++]);

                CharacterInfo_list.Add(tmp);
            }
            sr.Close();
        }
    }

    private void Load_MonsterData(string _path)
    {
        using (StreamReader sr = new StreamReader(_path))
        {
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] datas = line.Split(',');
                Monster_Info tmp = new Monster_Info();
                int num = 0;
                tmp.index = ushort.Parse(datas[num++]);
                tmp.name = datas[num++];
                tmp.stage = ushort.Parse(datas[num++]);
                tmp.size = datas[num++];
                tmp.health = ushort.Parse(datas[num++]);
                tmp.skill = datas[num++];
                tmp.attacktype = datas[num++];
                tmp.type = datas[num++];
                tmp.damage = ushort.Parse(datas[num++]);

                MonsterInfo_list.Add(tmp);
            }
            sr.Close();
        }
    }

    private void Load_CardData(string _path)
    {
        using (StreamReader sr = new StreamReader(_path))
        {
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] datas = line.Split(',');
                Card_Info tmp = new Card_Info();
                int num = 0;
                tmp.index = ushort.Parse(datas[num++]);
                tmp.name = datas[num++];
                tmp.cost = ushort.Parse(datas[num++]);
                tmp.cardclass = ushort.Parse(datas[num++]);
                tmp.skill = datas[num++];
                tmp.attacktype = ushort.Parse(datas[num++]);
                tmp.turn = ushort.Parse(datas[num++]);
                tmp.description = datas[num++];
                tmp.damage = ushort.Parse(datas[num++]);

                CardInfo_list.Add(tmp);
            }
            sr.Close();
        }
    }

    private void Load_ItemData(string _path)
    {
        using (StreamReader sr = new StreamReader(_path))
        {
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] datas = line.Split(',');
                Item_Info tmp = new Item_Info();
                int num = 0;
                tmp.index = ushort.Parse(datas[num++]);
                tmp.name = datas[num++];
                tmp.type = ushort.Parse(datas[num++]);
                tmp.turn = ushort.Parse(datas[num++]);
                tmp.description = datas[num++];
                tmp.damage = ushort.Parse(datas[num++]);

                ItemInfo_list.Add(tmp);
            }
            sr.Close();
        }
    }

    private void Load_SkillData(string _path)
    {
        using (StreamReader sr = new StreamReader(_path))
        {
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] datas = line.Split(',');
                Skill_Info tmp = new Skill_Info();
                int num = 0;
                tmp.index = ushort.Parse(datas[num++]);
                tmp.name = datas[num++];
                tmp.howuse = datas[num++];
                tmp.buf = ushort.Parse(datas[num++]);
                tmp.description = datas[num++];
                tmp.damage = ushort.Parse(datas[num++]);

                SkillInfo_list.Add(tmp);
            }
            sr.Close();
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////

    private List<T> ArrayToList<T>(T[] _arr)
    {
        List<T> list = new List<T>();
        for (int i = 0; i < _arr.Length; i++)
        {
            list.Add(_arr[i]);
        }
        return list;
    }

    public static void GetCharacterData(string _name)
    {
        for (int i = 0; i < new_resourcesManager.CharacterInfo_list.Count; i++)
        {
            if(_name.Equals(new_resourcesManager.CharacterInfo_list[i].job))
            {
                new_GameManager.instance.UserChar_Info = new_resourcesManager.CharacterInfo_list[i];
                break;
            }
        }
    }

    public static void GetCharacterObj(string _name)
    {
        for(int i = 0; i < new_resourcesManager.Character_Prefab_Pool.Count; i++)
        {
            if(_name.Equals(new_resourcesManager.Character_Prefab_Pool[i].name))
            {
                new_GameManager.instance.UserChar_Obj = new_resourcesManager.Character_Prefab_Pool[i];
                break;
            }
        }
    }

    public static GameObject New_Instantiate(ushort _type, string _name)
    {
        switch(_type)
        {
            case 2: // Monster
                for(int i = 0; i < new_resourcesManager.Monster_Complete_Obj.Count; i++)
                {
                    if(new_resourcesManager.Monster_Complete_Obj[i].name.Equals(_name))
                    {
                        return Stead_Instantiate(new_resourcesManager.Monster_Complete_Obj[i]);
                    }
                }
                break;

            case 3: // Card
                for(int i = 0; i < new_resourcesManager.Card_Complete_Obj.Count; i++)
                {
                    if (new_resourcesManager.Card_Complete_Obj[i].name.Equals(_name))
                    {
                        return Stead_Instantiate(new_resourcesManager.Card_Complete_Obj[i]);
                    }
                }
                break;
        }
        return null;
    }

    public static GameObject Stead_Instantiate(GameObject _obj)
    {
        GameObject tmp = Instantiate(_obj);
        tmp.gameObject.name = tmp.gameObject.name.Replace("(Clone)", "");
        return tmp;
    }

    public override void Receive_request(Pool_request _Request)
    {
        //Dummy
    }
}