using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Shop : MonoBehaviour
{
    public Transform CardPos;
    public Transform ItemPos;
    public Image ItemImage;

    public Text StatsUpgrade;

    void Awake()
    {
        if(GameManager.instance.CHARACTERLIVE == true)
        {
            card();
            item();
            Upgrade();
        }
    }

    void card()
    {
        List<CarddataInfo> list = new List<CarddataInfo>();
        for (int i = 0; i < DataManager.instance.LoadAllCardData().Count; i++)
        {
            list.Add(DataManager.instance.LoadAllCardData()[i]);
        }
        for (int i = 0; i < list.Count; i++) //카드섞기
        {
            int ran = Random.Range(0, list.Count);
            CarddataInfo tmp = list[i];
            list[i] = list[ran];
            list[ran] = tmp;
        }
        GameObject cardObj = Instantiate(ResourcesManager.CardForwardPrefabLoad());
        cardObj.GetComponent<Card>().enabled = false;
        cardObj.transform.position = CardPos.position;
        cardObj.transform.SetParent(CardPos);
        var cardTMP = cardObj.GetComponent<ShopCard>();
        cardTMP.DataInfo(list[0].name);
        cardTMP.cardimage.sprite = ResourcesManager.CardImageLoad(list[0].name);
        cardTMP.datainfo.index = GameManager.instance.CARDLIST.Count;
        DataManager.instance.AddCardCSV(list[0]);
        GameManager.instance.CARDLIST.Add(list[0]);
    }

    void item()
    {
        List<ItemdataInfo> list = new List<ItemdataInfo>();
        for (int i = 0; i < DataManager.instance.LoadItemData().Count; i++)
        {
            list.Add(DataManager.instance.LoadItemData()[i]);
        }
        for (int i = 0; i < list.Count; i++) //아이템섞기
        {
            int ran = Random.Range(0, list.Count);
            ItemdataInfo tmp = list[i];
            list[i] = list[ran];
            list[ran] = tmp;
        }
        var itemtmp = list[0].name;
        ItemImage.sprite = ResourcesManager.ItemImageLoad(itemtmp);
        ItemImage.gameObject.AddComponent<ShopItem>();
        var Com = ItemImage.gameObject.GetComponent<ShopItem>();
        Com.dataInfo.index = GameManager.instance.ItemList.Count;
        GameManager.instance.SHOPitemdataInfo = list[0];
        DataManager.instance.AddItemCSV(list[0]);
        GameManager.instance.ItemList.Add(list[0]);
    }

    void Upgrade()
    {
        ushort _health = SelectStatsNum(50, 25, 10);
        ushort _phyAttack = SelectStatsNum(65, 15, 5);
        ushort _phyDefense = SelectStatsNum(65, 15, 5);
        ushort _mageAttack = SelectStatsNum(65, 15, 5);
        ushort _mageDefense = SelectStatsNum(65, 15, 5);
        ushort _evasion = SelectStatsNum(65, 15, 5);
        GameManager.instance.character.characterDataInfo.health += _health;
        GameManager.instance.character.characterDataInfo.phyAttack += _phyAttack;
        GameManager.instance.character.characterDataInfo.phyDefense += _phyDefense;
        GameManager.instance.character.characterDataInfo.mageAttack += _mageAttack;
        GameManager.instance.character.characterDataInfo.mageDefense += _mageDefense;
        GameManager.instance.character.characterDataInfo.evasion += _evasion;
        StatsUpgrade.text = "Health : +" + _health + "\n" +
                            "phyAttack : +" + _phyAttack + "\n" +
                            "phyDefense : +" + _phyDefense + "\n" +
                            "mageAttack : +" + _mageAttack + "\n" +
                            "mageDefense : +" + _mageDefense + "\n" +
                            "evasion : +" + _evasion + "\n";
    }

    ushort SelectStatsNum(int _one, int _two, int _three) //0~3까지 무작위 수 하나 반환
    {
        int one = _one;
        int two = _two;
        int three = _three;
        ushort[] Hundred = new ushort[100];
        for(var z = 0; z < Hundred.Length; z++) // 0
        {
            Hundred[z] = 0;
        }
        for(var z = 0; z < one; z++) // 1
        {
            Hundred[z] = 1;
        }
        for(var z = 0; z < Hundred.Length; z++) //2
        {
            if(!Hundred[z].Equals(1))
            {
                Hundred[z] = 2;
                if(z == one + two -1)
                {
                    break;
                }
            }
        }
        for(var z = 0; z < Hundred.Length; z++) //3
        {
            if(!Hundred[z].Equals(1) && !Hundred[z].Equals(2))
            {
                Hundred[z] = 3;
                if (z == one + two + three -1)
                {
                    break;
                }
            }
        }
        for(var z = 0; z < Hundred.Length; z++) // 섞기
        {
            var ran = Random.Range(0, 100);
            ushort tmp = Hundred[z];
            Hundred[z] = Hundred[ran];
            Hundred[ran] = tmp;
        }
        var ranNum = Random.Range(0, 100);
        return Hundred[ranNum];
    }
}
