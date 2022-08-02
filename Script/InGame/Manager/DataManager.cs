using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public struct ItemdataInfo
{
    public float index;
    public string name;
    public float type;
    public float turn;
    public string description;
    public float damage;
}
public struct CarddataInfo
{
    public float index;
    public string name;
    public float cost;
    public float cardclass;
    public string skill;
    public float attacktype;
    public float turn;
    public string description;
    public float damage;
}
public struct MonsterDataInfo
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
public struct PlayerDataInfo
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
public struct SkillDataInfo
{
    public float index;
    public string name;
    public string howuse;
    public float job; //몬스터는 -1, 직업공용은 -2
    public float buf; //버프, 디버프 구분 - 버프 : 1 , 디버프 : -1;
    public string description;
    public float damege;
}
public struct POSITION
{
    public float index;
    public string creature;
    public float trX;
    public float trY;
    public float trZ;
    public float roX;
    public float roY;
    public float roZ;
    public float seX;
    public float seY;
    public float seZ;
}

public class DataManager : SingleTon<DataManager>
{
    //매개변수와 일치하는 리스트 반환
    public ItemdataInfo? GetItemdataInfo(float _index)
    {
        foreach(ItemdataInfo one in LoadItemData())
        {
            if(one.index == _index)
            {
                return one;
            }
        }
        return null;
    }
    public CarddataInfo? GetCarddataInfo(float _index)
    {
        foreach (CarddataInfo one in LoadCardData())
        {
            if (one.index == _index)
            {
                return one;
            }
        }
        return null;
    }
    public MonsterDataInfo? GetMonsterdataInfo(string _name)
    {
        foreach (MonsterDataInfo one in LoadMonsterData())
        {
            if (one.name == _name)
            {
                return one;
            }
        }
        return null;
    }
    public PlayerDataInfo? GetPlayerdataInfo(string _name)
    {
        foreach (PlayerDataInfo one in LoadCharacterData())
        {
            if (one.job == _name)
            {
                return one;
            }
        }
        return null;
    }
    public PlayerDataInfo? GetSavePlayerDataInfo(float _index)
    {
        foreach (PlayerDataInfo one in LoadCharacterData())
        {
            if (one.index == _index)
            {
                return one;
            }
        }
        return null;
    }
    public SkillDataInfo? GetSkilldataInfo(float _index)
    {
        foreach (SkillDataInfo one in LoadSkillData())
        {
            if (one.index == _index)
            {
                return one;
            }
        }
        return null;
    }

    public CarddataInfo? GetAllCarddataInfo(string _name)
    {
        foreach (CarddataInfo one in LoadAllCardData())
        {
            if (one.name == _name)
            {
                return one;
            }
        }
        return null;
    }
    public ItemdataInfo? GetAllItemdataInfo(string _name)
    {
        foreach (ItemdataInfo one in LoadAllItemData())
        {
            if (one.name == _name)
            {
                return one;
            }
        }
        return null;
    }

    //데이터테이블 읽기
    public List<ItemdataInfo> LoadItemData()
    {
        List<ItemdataInfo> itemdataInfolist = new List<ItemdataInfo>();
        using (StreamReader sr = new StreamReader(GameManager.instance.ItemDataPath))
        {
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] datas = line.Split(',');
                ItemdataInfo tmp = new ItemdataInfo();
                int num = 0;
                tmp.index = float.Parse(datas[num++]);
                tmp.name = datas[num++];
                tmp.type = float.Parse(datas[num++]);
                tmp.turn = float.Parse(datas[num++]);
                tmp.description = datas[num++];
                tmp.damage = float.Parse(datas[num++]);

                itemdataInfolist.Add(tmp);
            }
            sr.Close();
        }
        return itemdataInfolist;
    }
    public List<CarddataInfo> LoadCardData()
    {
        List<CarddataInfo> carddataInfolist = new List<CarddataInfo>();
        using (StreamReader sr = new StreamReader(GameManager.instance.UserCardListDataPath))
        {
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] datas = line.Split(',');
                CarddataInfo tmp = new CarddataInfo();
                int num = 0;
                tmp.index = float.Parse(datas[num++]);
                tmp.name = datas[num++];
                tmp.cost = float.Parse(datas[num++]);
                tmp.cardclass = float.Parse(datas[num++]);
                tmp.skill = datas[num++];
                tmp.attacktype = float.Parse(datas[num++]);
                tmp.turn = float.Parse(datas[num++]);
                tmp.description = datas[num++];
                tmp.damage = float.Parse(datas[num++]);

                carddataInfolist.Add(tmp);
            }
            sr.Close();
        }
        return carddataInfolist;
    }
    public List<MonsterDataInfo> LoadMonsterData()
    {
        List<MonsterDataInfo> list = new List<MonsterDataInfo>();
        using (StreamReader sr = new StreamReader(GameManager.instance.oneMonsterDataPath))
        {
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] datas = line.Split(',');
                MonsterDataInfo tmp = new MonsterDataInfo();
                int num = 0;
                tmp.index = float.Parse(datas[num++]);
                tmp.name = datas[num++];
                tmp.stage = float.Parse(datas[num++]);
                tmp.size = datas[num++];
                tmp.health = float.Parse(datas[num++]);
                tmp.skill = datas[num++];
                tmp.attacktype = datas[num++];
                tmp.type = datas[num++];
                tmp.damage = float.Parse(datas[num++]);

                list.Add(tmp);
            }
            sr.Close();
        }
        
        /*
        using (StreamReader sr = new StreamReader(GameManager.instance.twoMonsterDataPath))
        {
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] datas = line.Split(',');
                MonsterDataInfo tmp = new MonsterDataInfo();
                int num = 0;
                tmp.index = float.Parse(datas[num++]);
                tmp.name = datas[num++];
                tmp.normaldamage = float.Parse(datas[num++]);
                tmp.size = datas[num++];
                tmp.health = float.Parse(datas[num++]);
                tmp.skill = datas[num++];
                tmp.attacktype = datas[num++];
                tmp.type = datas[num++];

                carddataInfolist.Add(tmp);
            }
            sr.Close();
        }

        using (StreamReader sr = new StreamReader(GameManager.instance.threeMonsterDataPath))
        {
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] datas = line.Split(',');
                MonsterDataInfo tmp = new MonsterDataInfo();
                int num = 0;
                tmp.index = float.Parse(datas[num++]);
                tmp.name = datas[num++];
                tmp.normaldamage = float.Parse(datas[num++]);
                tmp.size = datas[num++];
                tmp.health = float.Parse(datas[num++]);
                tmp.skill = datas[num++];
                tmp.attacktype = datas[num++];
                tmp.type = datas[num++];

                carddataInfolist.Add(tmp);
            }
            sr.Close();
        }
        */
        return list;
    }
    public List<PlayerDataInfo> LoadCharacterData()
    {
        List<PlayerDataInfo> playerDataInfolist = new List<PlayerDataInfo>();
        using (StreamReader sr = new StreamReader(GameManager.instance.PlayerDataPath))
        {
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] datas = line.Split(',');
                PlayerDataInfo tmp = new PlayerDataInfo();
                int num = 0;
                tmp.index = float.Parse(datas[num++]);
                tmp.job = datas[num++];
                tmp.health = float.Parse(datas[num++]);
                tmp.phyAttack = float.Parse(datas[num++]);
                tmp.phyDefense = float.Parse(datas[num++]);
                tmp.mageAttack = float.Parse(datas[num++]);
                tmp.mageDefense = float.Parse(datas[num++]);
                tmp.evasion = float.Parse(datas[num++]);
                tmp.energy = float.Parse(datas[num++]);

                playerDataInfolist.Add(tmp);
            }
            sr.Close();
        }
        return playerDataInfolist;
    }
    public List<SkillDataInfo> LoadSkillData()
    {
        List<SkillDataInfo> skillDataInfolist = new List<SkillDataInfo>();
        using (StreamReader sr = new StreamReader(GameManager.instance.SkillDAtaPath))
        {
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] datas = line.Split(',');
                SkillDataInfo tmp = new SkillDataInfo();
                int num = 0;
                tmp.index = float.Parse(datas[num++]);
                tmp.name = datas[num++];
                tmp.howuse = datas[num++];
                tmp.job = float.Parse(datas[num++]);
                tmp.buf = float.Parse(datas[num++]);
                tmp.description = datas[num++];
                tmp.damege = float.Parse(datas[num++]);

                skillDataInfolist.Add(tmp);
            }
            sr.Close();
        }
        return skillDataInfolist;
    }

    public List<CarddataInfo> LoadAllCardData()
    {
        List<CarddataInfo> carddataInfolist = new List<CarddataInfo>();
        using (StreamReader sr = new StreamReader(GameManager.instance.CardDataPath))
        {
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] datas = line.Split(',');
                CarddataInfo tmp = new CarddataInfo();
                int num = 0;
                tmp.index = float.Parse(datas[num++]);
                tmp.name = datas[num++];
                tmp.cost = float.Parse(datas[num++]);
                tmp.cardclass = float.Parse(datas[num++]);
                tmp.skill = datas[num++];
                tmp.attacktype = float.Parse(datas[num++]);
                tmp.turn = float.Parse(datas[num++]);
                tmp.description = datas[num++];
                tmp.damage = float.Parse(datas[num++]);

                carddataInfolist.Add(tmp);
            }
            sr.Close();
        }
        return carddataInfolist;
    }
    public List<ItemdataInfo> LoadAllItemData()
    {
        List<ItemdataInfo> carddataInfolist = new List<ItemdataInfo>();
        using (StreamReader sr = new StreamReader(GameManager.instance.UserItemListDataPath))
        {
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] datas = line.Split(',');
                ItemdataInfo tmp = new ItemdataInfo();
                int num = 0;
                tmp.index = float.Parse(datas[num++]);
                tmp.name = datas[num++];
                tmp.type = float.Parse(datas[num++]);
                tmp.turn = float.Parse(datas[num++]);
                tmp.description = datas[num++];
                tmp.damage = float.Parse(datas[num++]);

                carddataInfolist.Add(tmp);
            }
            sr.Close();
        }
        return carddataInfolist;
    }

    public List<POSITION> LoadPositionData()
    {
        List<POSITION> POSITIONlist = new List<POSITION>();
        using (StreamReader sr = new StreamReader(GameManager.instance.PositionDataPath))
        {
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] datas = line.Split(',');
                POSITION tmp;  
                int num = 0;
                tmp.index = float.Parse(datas[num++]);
                tmp.creature = datas[num++];
                tmp.trX = float.Parse(datas[num++]);
                tmp.trY = float.Parse(datas[num++]);
                tmp.trZ = float.Parse(datas[num++]);
                tmp.roX = float.Parse(datas[num++]);
                tmp.roY = float.Parse(datas[num++]);
                tmp.roZ = float.Parse(datas[num++]);
                tmp.seX = float.Parse(datas[num++]);
                tmp.seY = float.Parse(datas[num++]);
                tmp.seZ = float.Parse(datas[num++]);

                POSITIONlist.Add(tmp);
            }
            sr.Close();
        }

        return POSITIONlist;
    }
    public PlayerDataInfo? ReturnCharacterStats(string _name)
    {
        List<PlayerDataInfo> list = new List<PlayerDataInfo>();
        using (StreamReader sr = new StreamReader(GameManager.instance.CharacterStatsDataPath))
        {
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] datas = line.Split(',');
                PlayerDataInfo tmp = new PlayerDataInfo();
                int num = 0;
                tmp.index = float.Parse(datas[num++]);
                tmp.job = datas[num++];
                tmp.health = float.Parse(datas[num++]);
                tmp.phyAttack = float.Parse(datas[num++]);
                tmp.phyDefense = float.Parse(datas[num++]);
                tmp.mageAttack = float.Parse(datas[num++]);
                tmp.mageDefense = float.Parse(datas[num++]);
                tmp.evasion = float.Parse(datas[num++]);
                tmp.energy = float.Parse(datas[num++]);

                list.Add(tmp);
            }
            sr.Close();
        }
        foreach(var one in list)
        {
            if(one.job.Equals(_name))
            {
                return one;
            }
        }
        return null;
    }

    public void CardData() //사용자가 발견한 리스트"CardList" 에 카드를 넣어주는 함수
    {
        using (StreamReader sr = new StreamReader(GameManager.instance.UserCardListDataPath))
        {
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] datas = line.Split(',');
                CarddataInfo tmp = new CarddataInfo();
                int num = 0;
                tmp.index = float.Parse(datas[num++]);
                tmp.name = datas[num++];
                tmp.cost = float.Parse(datas[num++]);
                tmp.cardclass = float.Parse(datas[num++]);
                tmp.skill = datas[num++];
                tmp.attacktype = float.Parse(datas[num++]);
                tmp.turn = float.Parse(datas[num++]);
                tmp.description = datas[num++];
                tmp.damage = float.Parse(datas[num++]);

                GameManager.instance.CARDLIST.Add(tmp);
            }
            sr.Close();
        }
    }
    public void AddCardCSV(CarddataInfo _card)
    {
        float _index = GameManager.instance.CARDCOUNTING;
        using (FileStream fs = new FileStream(GameManager.instance.UserCardListDataPath, FileMode.Append))
        using (StreamWriter sr = new StreamWriter(fs))
        {
            char option = ',';
            char op = '\n';
            sr.Write(_index);
            sr.Write(option);
            sr.Write(_card.name);
            sr.Write(option);
            sr.Write(_card.cost);
            sr.Write(option);
            sr.Write(_card.cardclass);
            sr.Write(option);
            sr.Write(_card.skill);
            sr.Write(option);
            sr.Write(_card.attacktype);
            sr.Write(option);
            sr.Write(_card.turn);
            sr.Write(option);
            sr.Write(_card.description);
            sr.Write(option);
            sr.Write(_card.damage);
            sr.Write(option);
            sr.Write(op);

            sr.Close();
            fs.Close();
        }
    }
    public void ItemData()
    {
        using (StreamReader sr = new StreamReader(GameManager.instance.UserItemListDataPath))
        {
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] datas = line.Split(',');
                ItemdataInfo tmp = new ItemdataInfo();
                int num = 0;
                tmp.index = float.Parse(datas[num++]);
                tmp.name = datas[num++];
                tmp.type = float.Parse(datas[num++]);
                tmp.turn = float.Parse(datas[num++]);
                tmp.description = datas[num++];
                tmp.damage = float.Parse(datas[num++]);

                GameManager.instance.ItemList.Add(tmp);
            }
            sr.Close();
        }
    }
    public void AddItemCSV(ItemdataInfo _item)
    {
        float _index = GameManager.instance.ITEMCOUNTING;
        using (FileStream fs = new FileStream(GameManager.instance.UserItemListDataPath, FileMode.Append))
        using (StreamWriter sr = new StreamWriter(fs))
        {
            char option = ',';
            char op = '\n';
            sr.Write(_index);
            sr.Write(option);
            sr.Write(_item.name);
            sr.Write(option);
            sr.Write(_item.type);
            sr.Write(option);
            sr.Write(_item.turn);
            sr.Write(option);
            sr.Write(_item.description);
            sr.Write(option);
            sr.Write(_item.damage);
            sr.Write(option);
            sr.Write(op);

            sr.Close();
            fs.Close();
        }
    }
    public void SaveCharacterData()
    {
        if (GameManager.instance.SAVECHARACTERDATA)
        {
            using (FileStream fs = new FileStream(GameManager.instance.PlayerDataPath, FileMode.Append))
            using (StreamWriter sr = new StreamWriter(fs))
            {
                char option = ',';
                char op = '\n';
                sr.Write(GameManager.instance.CHARACTERSAVEINDEX);
                sr.Write(option);
                sr.Write(GameManager.instance.character.characterDataInfo.job);
                sr.Write(option);
                sr.Write((int)GameManager.instance.character.characterDataInfo.health);
                sr.Write(option);
                sr.Write(GameManager.instance.character.characterDataInfo.phyAttack);
                sr.Write(option);
                sr.Write(GameManager.instance.character.characterDataInfo.phyDefense);
                sr.Write(option);
                sr.Write(GameManager.instance.character.characterDataInfo.mageAttack);
                sr.Write(option);
                sr.Write(GameManager.instance.character.characterDataInfo.mageDefense);
                sr.Write(option);
                sr.Write(GameManager.instance.character.characterDataInfo.evasion);
                sr.Write(option);
                sr.Write(GameManager.instance.character.characterDataInfo.energy);
                sr.Write(op);
                sr.Close();
                fs.Close();
            }
            GameManager.instance.SAVECHARACTERDATA = false;
        }
        GameManager.instance.CHARACTERSAVEINDEX++;
    }
}
