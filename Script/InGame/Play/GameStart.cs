using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour //스테이지 시작시 호출
{
    [Header("테스트용")]
    public int MonsterMaxCount; // -1해서 생각
    public int ItemUseCount;

    public Transform cardSpawnPoint;
    public RectTransform SpawnPosition;
    public RectTransform DeathPosition;
    public Transform parant;
    public Transform HandPositionLeft;
    public Transform HandPositionRight;
    public Transform CardPool;

    public Transform CharacterRunTarget;

    public Transform Canvase;

    public Image New; //스테이지 보상창
    public Image Die;

    public Image targetImage;

    public Text energeTextfront;
    public Text energeTextback;

    [Header("카드 리스트")]
    public float foward;
    public float hand;
    public float back;

    public List<Image> HistoryImageList;
    public List<Image> HistoryArrowImageList;

    public List<Image> StatusEffect; //4개

    public Slider OriginalHpBar; //원본

    void Awake()
    {
        GameManager.instance.CharAni_StageClear = false;
        GameManager.instance.CharAni_Die = false;
        DataManager.instance.CardData();
        GameManager.instance.CARDCOUNTING = (ushort)GameManager.instance.CARDLIST.Count;
        GameManager.instance.SAVECHARACTERDATA = true;
        ItemUseCount = 1;
        MonsterMaxCount = 3;
        foreach (var one in GameManager.instance.CARDLIST)
        {
            GameManager.instance.ForwardCarddataList.Add(one);
        }
        CardManager.instance.FirstCardDrow();
        CardSC(GameManager.instance.ForwardCarddataList);
        CardInstantiate(GameManager.instance.HandCarddataList);
        if(GameManager.instance.USERTURE)
        {
            SelectPrefab(GameManager.instance.JOBNAME);
            HowManyMonster(GameManager.instance.FLOOR);
            GroundSpawn(GameManager.instance.FLOOR);
        }

        CardManager.instance.ENERGYBACK = energeTextback;
        CardManager.instance.ENERGYFRONT = energeTextfront;
        var E = GameManager.instance.character.characterDataInfo.energy;
        CardManager.instance.EBACK = E;
        CardManager.instance.EFRONT = CardManager.instance.EBACK;
        CardManager.instance.ENERGYBACK.text = CardManager.instance.EBACK.ToString();
        CardManager.instance.ENERGYFRONT.text = CardManager.instance.ENERGYBACK.text;
    }

    void SelectPrefab(string _name) // 캐릭터 인스턴스
    {
        GameObject character = Instantiate(ResourcesManager.PlayerPrefabLoad(_name));
        character.transform.SetParent(ResourcesManager.PlayerPosition(character));
        var tmp = character.AddComponent<Character>();
        character.SetActive(true);
        CharacterSandDataInfo(character, _name);
    }

    void CharacterSandDataInfo(GameObject _gameObject, string _name)
    {
        var tmp = _gameObject.GetComponent<Character>();
        tmp.Hp = Instantiate(OriginalHpBar);
        tmp.transform.SetParent(tmp.transform);
        tmp.Canvase = Canvase;
        tmp.ClearPos = CharacterRunTarget;
        var stats = DataManager.instance.ReturnCharacterStats(_name);
        tmp.OriginalHp = stats.Value.health;
        if (GameManager.instance.stage == 1)
        {
            tmp.FirstDataInfo(stats);
        }
        var _index = DataManager.instance.GetSavePlayerDataInfo(GameManager.instance.Clearstage - 1);
        tmp.DataInfo(_index);
        GameManager.instance.character = tmp;
        GameManager.instance.CHARACTERLIVE = true;
    }

    void GroundSpawn(int _floor)
    {
        if (_floor == 1)
        {
            GameObject ground1 = Instantiate(ResourcesManager.GroundSpawn()[0]);
            ground1.transform.SetParent(ResourcesManager.GroundPosition(ground1));
            ground1.SetActive(true);
        }
        else if (_floor == 2)
        {
            GameObject ground2 = Instantiate(ResourcesManager.GroundSpawn()[1]);
            ground2.transform.SetParent(ResourcesManager.GroundPosition(ground2));
            ground2.SetActive(true);
        }
        else if (_floor == 3)
        {
            GameObject ground3 = Instantiate(ResourcesManager.GroundSpawn()[2]);
            ground3.transform.SetParent(ResourcesManager.GroundPosition(ground3));
            ground3.SetActive(true);
        }
    }

    void HowManyMonster(int _floor) // 스테이지에 얼마나 몬스터를 소환할지 결정해주는 함수
    {
        int SpawnMonster;
        SpawnMonster = Random.Range(1, MonsterMaxCount);
        //Debug.Log("SpawnMonster   : " + SpawnMonster);

        GameObject[] DecisionMonsterArray = new GameObject[SpawnMonster];
        List<GameObject> DecisionMonsterList = new List<GameObject>();

        if (_floor == 1) //1막
        {
            int[] randomNumber = new int[SpawnMonster];
            for (int i = 0; i < randomNumber.Length; i++)
            {
                randomNumber[i] = Random.Range(0, ResourcesManager.FloorOneMonsterPrefabLoad().Length);
                //Debug.Log("RandomNumber["+i+"]   : " + randomNumber[i]);
            }

            //Debug.Log("RandomNumber   : " + randomNumber.Length);

            for (int i = 0; i < randomNumber.Length; i++)
            {
                for (int j = 0; j < DecisionMonsterArray.Length; j++)
                {
                    DecisionMonsterArray[j] = ResourcesManager.FloorOneMonsterPrefabLoad()[randomNumber[i]];
                    //Debug.Log("DecisionMonsterArray[" + i + "]   : " + DecisionMonsterArray[j].gameObject.name);
                }
                DecisionMonsterList.Add(DecisionMonsterArray[i]);
                //Debug.Log("DecisionMonsterArray To List[" + i + "]   : " + DecisionMonsterArray[i].gameObject.name);
            }

            //Debug.Log("ResourcesManager.FloorOneMonsterPrefabLoad()   : " + ResourcesManager.FloorOneMonsterPrefabLoad().Length);
            //Debug.Log("DecisionMonsterArray   : " + DecisionMonsterArray.Length);
            //Debug.Log("DecisionMonsterList   : " + DecisionMonsterList.Count);

            callMonsterPrefab(DecisionMonsterList, DataManager.instance.LoadPositionData());
        }
        else if (_floor == 2) //2막
        {
            int[] randomNumber = new int[SpawnMonster];
            for (int i = 0; i < randomNumber.Length; i++)
            {
                randomNumber[i] = Random.Range(0, ResourcesManager.FloorTwoMonsterPrefabLoad().Length);
            }

            for (int i = 0; i < randomNumber.Length; i++)
            {
                for (int j = 0; j < DecisionMonsterArray.Length; j++)
                {
                    DecisionMonsterArray[j] = ResourcesManager.FloorTwoMonsterPrefabLoad()[randomNumber[i]];
                }
                DecisionMonsterList.Add(DecisionMonsterArray[i]);
            }

            callMonsterPrefab(DecisionMonsterList, DataManager.instance.LoadPositionData());
        }
        else if (_floor == 3) //3막
        {
            int[] randomNumber = new int[SpawnMonster];
            for (int i = 0; i < randomNumber.Length; i++)
            {
                randomNumber[i] = Random.Range(0, ResourcesManager.FloorThreeMonsterPrefabLoad().Length);
            }

            for (int i = 0; i < randomNumber.Length; i++)
            {
                for (int j = 0; j < DecisionMonsterArray.Length; j++)
                {
                    DecisionMonsterArray[j] = ResourcesManager.FloorThreeMonsterPrefabLoad()[randomNumber[i]];
                }
                DecisionMonsterList.Add(DecisionMonsterArray[i]);
            }

            callMonsterPrefab(DecisionMonsterList, DataManager.instance.LoadPositionData());
        }

    }

    void callMonsterPrefab(List<GameObject> _DecisionMonster, List<POSITION> _position)
    {
        for (int i = 0; i < _DecisionMonster.Count; i++)
        {
            if (i == 0)
            {
                GameObject obj1 = Instantiate(_DecisionMonster[0]);
                obj1.transform.SetParent(ResourcesManager.OneMonsterPosition(obj1));
                var mon = obj1.AddComponent<Monster>();
                obj1.SetActive(true);
                var tmp = obj1.GetComponent<Monster>();
                tmp.Hp = Instantiate(OriginalHpBar);
                tmp.Canvase = Canvase;
                tmp.DataInfo(_DecisionMonster[i].name);
                GameManager.instance.Livemonsterlist.Add(tmp);
            }
            if (i == 1)
            {
                GameObject obj2 = Instantiate(_DecisionMonster[1]);
                obj2.transform.SetParent(ResourcesManager.TwoMonsterPosition(obj2));
                Monster mon = obj2.AddComponent<Monster>();
                obj2.SetActive(true);
                var tmp = obj2.GetComponent<Monster>();
                tmp.Hp = Instantiate(OriginalHpBar);
                tmp.Canvase = Canvase;
                tmp.DataInfo(_DecisionMonster[i].name);
                GameManager.instance.Livemonsterlist.Add(tmp);
            }
            if (i == 2)
            {
                GameObject obj3 = Instantiate(_DecisionMonster[2]);
                obj3.transform.SetParent(ResourcesManager.ThreeMonsterPosition(obj3));
                Monster mon = obj3.AddComponent<Monster>();
                obj3.SetActive(true);
                var tmp = obj3.GetComponent<Monster>();
                tmp.Hp = Instantiate(OriginalHpBar);
                tmp.Canvase = Canvase;
                tmp.DataInfo(_DecisionMonster[i].name);
                GameManager.instance.Livemonsterlist.Add(tmp);
                //Debug.Log("Monster / name   : " + obj3.name + ",   Position   : " + obj3.transform.position + ",   i   : " + i);
            }
            if (i == 3)
            {
                GameObject obj4 = Instantiate(_DecisionMonster[3]);
                obj4.transform.SetParent(ResourcesManager.FourMonsterPosition(obj4));
                Monster mon = obj4.AddComponent<Monster>();
                obj4.SetActive(true);
                var tmp = obj4.GetComponent<Monster>();
                tmp.Hp = Instantiate(OriginalHpBar);
                tmp.Canvase = Canvase;
                tmp.DataInfo(_DecisionMonster[i].name);
                GameManager.instance.Livemonsterlist.Add(tmp);
                //Debug.Log("Monster / name   : " + obj4.name + ",   Position   : " + obj4.transform.position + ",   i   : " + i);
            }
        }
    }

    public void CardInstantiate(List<CarddataInfo> _cardList) //손카드에 있는 리스트를 인스턴스 하는 함수
    {
        List<PRS> list = new List<PRS>();
        list = CardManager.instance.RoundCardPosition(HandPositionLeft, HandPositionRight, _cardList.Count, 0.5f, Vector3.one * 1.3f);
        for (int i = 0; i < _cardList.Count; i++)
        {
            GameObject cardtmp = Instantiate(ResourcesManager.CardForwardPrefabLoad(), cardSpawnPoint.position, Utils.QI);
            cardtmp.GetComponent<ShopCard>().enabled = false;
            cardtmp.transform.SetParent(parant);
            var tmp = cardtmp.GetComponent<Card>();
            tmp.originPRS = list[i];
            tmp.MovePosition(list[i], true, 1f);
            tmp.DataInfo(_cardList[i].index);
            tmp.cardimage.sprite = ResourcesManager.CardImageLoad(_cardList[i].name);
            tmp.deathposition = DeathPosition;
            tmp.targetimage = targetImage;
            tmp.Leftpos = HandPositionLeft;
            tmp.Rightpos = HandPositionRight;
            tmp.cardpool = CardPool;
            GameManager.instance.HandcardCardList.Add(cardtmp);
        }
    }

    void CardSC(List<CarddataInfo> _cardList) //Card스크립트를 붙여서 다 인스턴스 해놓는다.
    {
        for (int i = 0; i < _cardList.Count; i++)
        {
            GameObject cardtmp = Instantiate(ResourcesManager.CardForwardPrefabLoad(), cardSpawnPoint.position, Utils.QI);
            var cardCom = cardtmp.GetComponent<ShopCard>();
            cardCom.enabled = false;
            cardtmp.transform.SetParent(CardPool);
            cardtmp.SetActive(false);
            var tmp = cardtmp.GetComponent<Card>();
            tmp.DataInfo(_cardList[i].index);
            tmp.cardimage.sprite = ResourcesManager.CardImageLoad(_cardList[i].name);
            tmp.deathposition = DeathPosition;
            tmp.targetimage = targetImage;
            tmp.Leftpos = HandPositionLeft;
            tmp.Rightpos = HandPositionRight;
            tmp.cardpool = CardPool;
            GameManager.instance.IncardCardList.Add(cardtmp);
        }
    }

    void Energy()
    {
        CardManager.instance.ENERGYBACK = energeTextback;
        CardManager.instance.ENERGYFRONT = energeTextfront;
        CardManager.instance.EFRONT = CardManager.instance.EBACK;
        CardManager.instance.ENERGYBACK.text = CardManager.instance.EBACK.ToString();
        CardManager.instance.ENERGYFRONT.text = CardManager.instance.ENERGYBACK.text;
    }

    void Update()
    {
        foward = GameManager.instance.ForwardCarddataList.Count;
        hand = GameManager.instance.HandCarddataList.Count;
        back = GameManager.instance.BackCarddataList.Count;
        BarUpdate();
        CharacterStatusEffectUpdate();

        if (GameManager.instance.USERTURE == false && GameManager.instance.MONSTERTURN == false) //턴 종료 버튼을 누르면
        {
            StartCoroutine("Turn");
            StopCoroutine("Turn");
            GameManager.instance.MONSTERTURN = true; //몬스터의 애니메이션이 끝나면 트루로 변경하기
        }

        if (GameManager.instance.Livemonsterlist.Count <= 0) //스테이지를 클리어 했을때
        {
            StageClear();
        }

        if(!GameManager.instance.CHARACTERLIVE) //캐릭터가 죽었을때
        {
            Die.gameObject.SetActive(true);
            if(GameManager.instance.CharAni_StageClear)
            {
                StageClear();
            }
        }
    }

    IEnumerator Turn()
    {
        PRS prs = new PRS(DeathPosition.position, DeathPosition.rotation, DeathPosition.localScale);
        for (int i = 0; i < GameManager.instance.HandCarddataList.Count; i++)
        {
            GameManager.instance.BackCarddataList.Add(GameManager.instance.HandCarddataList[i]);
            GameManager.instance.IncardCardList.Add(GameManager.instance.HandcardCardList[i]);
            GameManager.instance.HandcardCardList[i].GetComponent<Card>().MovePosition(prs, true, 0.3f);
        }
        for (int i = GameManager.instance.HandCarddataList.Count - 1; i >= 0; i--)
        {
            GameManager.instance.HandCarddataList.Remove(GameManager.instance.HandCarddataList[i]);
            GameManager.instance.HandcardCardList.Remove(GameManager.instance.HandcardCardList[i]);
        }
        MonsterAttack(GameManager.instance.Livemonsterlist); //몬스터 공격
        GameManager.instance.TURNCOUNT = GameManager.instance.TURNCOUNT + 1;
        if (GameManager.instance.USERTURE == false && GameManager.instance.MONSTERTURN == false)
        {
            CardManager.instance.CardDrow(GameManager.instance.BackCarddataList, GameManager.instance.ForwardCarddataList, GameManager.instance.HandCarddataList
                                        , GameManager.instance.IncardCardList, GameManager.instance.HandcardCardList, SpawnPosition);
            CardManager.instance.CardSet(GameManager.instance.HandCarddataList, GameManager.instance.HandcardCardList, HandPositionLeft, HandPositionRight, parant);
            Energy();
            GameManager.instance.UseItemCount = 1;
            GameManager.instance.USERTURE = true;
        }
        yield return null;
    }

    void MonsterAttack(List<Monster> _monsterlist)
    {
        for(int i = 0; i < _monsterlist.Count; i++)
        {
            MonsterTurn.instance.HowBehaviour(_monsterlist[i]);
        }
    }

    void StageClear()
    {
        GameManager.instance.CARDLIST.RemoveAll(o => true);

        GameManager.instance.HandcardCardList.RemoveAll(o => true);
        GameManager.instance.IncardCardList.RemoveAll(o => true);

        GameManager.instance.HandCarddataList.RemoveAll(o => true);
        GameManager.instance.ForwardCarddataList.RemoveAll(o => true);
        GameManager.instance.BackCarddataList.RemoveAll(o => true);

        GameManager.instance.History.RemoveAll(o => true);
        GameManager.instance.Livemonsterlist.RemoveAll(o => true);

        GameManager.instance.UseItemList.RemoveAll(o => true);

        GameManager.instance.DeckArrayPoslist.RemoveAll(o => true);
        GameManager.instance.CardPosList.RemoveAll(o => true);

        GameManager.instance.MESSAGE = string.Empty;
        GameManager.instance.TURNCOUNT = 1;

        GameManager.instance.CharAni_Attack = false;
        GameManager.instance.CharAni_GetHit = false;
        GameManager.instance.CharAni_StageClear = true;
        GameManager.instance.CharAni_Die = false;

        GameManager.instance.MonAni_HitName = string.Empty;
        GameManager.instance.MonAni_Attack = false;
        GameManager.instance.MonAni_GetHit = false;

        if (Die.gameObject.activeSelf == false)
        {
            New.gameObject.SetActive(true);
        }
        if (New.gameObject.activeSelf)
        {
            DataManager.instance.SaveCharacterData();
        }
    }

    void BarUpdate()
    {
        //게임을 플레이 하면서 사용한 카드, 아이템 이미지를 띄워준다.

        //진행순서
        //카드나 아이템을 사용하였을 경우 History리스트에 추가된다.
        //리스트의 수는 무한정으로 늘어나나, 나타낼수 잇는 이미지칸은 6개이다.
        //리스트의 수가 6보다 커지면 이미지칸을 한칸씩 움직여주어야 한다.
        // ex) [012345] + 6 => 0[123456]
        //image6은 항상 리스트의 마지막을 띄어주어야 한다.

        //조건 : "리스트의 수가 6이하일때" 와 "리스트의 수가 6보다 클때"
        // "리스트의 수가 6이하일때"   =>  List[0] -> ( image1 ) , List[1] -> ( image2 ) ...이런식
        // "리스트의 수가 6보다 클때"  =>  List.Count -> ( image6 ) , List.Count - 1 -> ( image5 ) ...이런식

        if (GameManager.instance.History.Count > 6)
        {
            for(int x = 0; x < HistoryImageList.Count; x++)
            {
                for (int i = GameManager.instance.History.Count - 1; i >= GameManager.instance.History.Count - 6; i--)
                {
                    if(x == 0 && i == GameManager.instance.History.Count - 6)
                    {
                        Sprite tmp = ResourcesManager.CardImageLoad(GameManager.instance.History[i]);
                        HistoryImageList[x].sprite = tmp;
                        HistoryImageList[x].gameObject.SetActive(true);
                    }
                    if (x == 1 && i == GameManager.instance.History.Count - 5)
                    {
                        Sprite tmp = ResourcesManager.CardImageLoad(GameManager.instance.History[i]);
                        HistoryImageList[x].sprite = tmp;
                        HistoryImageList[x].gameObject.SetActive(true);
                    }
                    if (x == 2 && i == GameManager.instance.History.Count - 4)
                    {
                        Sprite tmp = ResourcesManager.CardImageLoad(GameManager.instance.History[i]);
                        HistoryImageList[x].sprite = tmp;
                        HistoryImageList[x].gameObject.SetActive(true);
                    }
                    if (x == 3 && i == GameManager.instance.History.Count - 3)
                    {
                        Sprite tmp = ResourcesManager.CardImageLoad(GameManager.instance.History[i]);
                        HistoryImageList[x].sprite = tmp;
                        HistoryImageList[x].gameObject.SetActive(true);
                    }
                    if (x == 4 && i == GameManager.instance.History.Count - 2)
                    {
                        Sprite tmp = ResourcesManager.CardImageLoad(GameManager.instance.History[i]);
                        HistoryImageList[x].sprite = tmp;
                        HistoryImageList[x].gameObject.SetActive(true);
                    }
                    if (x == 5 && i == GameManager.instance.History.Count - 1)
                    {
                        Sprite tmp = ResourcesManager.CardImageLoad(GameManager.instance.History[i]);
                        HistoryImageList[x].sprite = tmp;
                        HistoryImageList[x].gameObject.SetActive(true);
                    }
                }
            }
        }
        else if (GameManager.instance.History.Count <= 6)
        {
            for (int x = 0; x < HistoryImageList.Count; x++)
            {
                for (int i = 0; i < GameManager.instance.History.Count; i++)
                {
                    if (x == 0 && i == 0)
                    {
                        Sprite tmp = ResourcesManager.CardImageLoad(GameManager.instance.History[i]);
                        HistoryImageList[x].sprite = tmp;
                        HistoryImageList[x].gameObject.SetActive(true);
                    }
                    if (x == 1 && i == 1)
                    {
                        Sprite tmp = ResourcesManager.CardImageLoad(GameManager.instance.History[i]);
                        HistoryImageList[x].sprite = tmp;
                        HistoryImageList[x].gameObject.SetActive(true);
                    }
                    if (x == 2 && i == 2)
                    {
                        Sprite tmp = ResourcesManager.CardImageLoad(GameManager.instance.History[i]);
                        HistoryImageList[x].sprite = tmp;
                        HistoryImageList[x].gameObject.SetActive(true);
                    }
                    if (x == 3 && i == 3)
                    {
                        Sprite tmp = ResourcesManager.CardImageLoad(GameManager.instance.History[i]);
                        HistoryImageList[x].sprite = tmp;
                        HistoryImageList[x].gameObject.SetActive(true);
                    }
                    if (x == 4 && i == 4)
                    {
                        Sprite tmp = ResourcesManager.CardImageLoad(GameManager.instance.History[i]);
                        HistoryImageList[x].sprite = tmp;
                        HistoryImageList[x].gameObject.SetActive(true);
                    }
                    if (x == 5 && i == 5)
                    {
                        Sprite tmp = ResourcesManager.CardImageLoad(GameManager.instance.History[i]);
                        HistoryImageList[x].sprite = tmp;
                        HistoryImageList[x].gameObject.SetActive(true);
                    }
                }
            }
        }

        for(int i = 0; i < HistoryImageList.Count; i++)
        {
            if(i < 5)
            {
                if (HistoryImageList[i].gameObject.activeSelf == true && HistoryImageList[i + 1].gameObject.activeSelf == true)
                {
                    HistoryArrowImageList[i].gameObject.SetActive(true);
                }
            }
            else
            {
                if (HistoryImageList[i].gameObject.activeSelf == true)
                {
                    HistoryArrowImageList[i].gameObject.SetActive(true);
                }
            }
        }
    }

    void CharacterStatusEffectUpdate()
    {
        //캐릭터가 무슨 아이템을 사용했는지 나타내는 곳
        //아이템에 있는 turn에 영향을 받음.

        //수정 필
        for(int i = 0; i < StatusEffect.Count; i++)
        {
            for(int x = 0; x < GameManager.instance.UseItemList.Count; x++)
            {
                if (i == 0 && x == 0)
                {
                    StatusEffect[i].sprite = ResourcesManager.ItemImageLoad(GameManager.instance.UseItemList[x].name);
                    var get = StatusEffect[i].gameObject.GetComponent<UseItem>();
                    get.ItemDataInfo(GameManager.instance.UseItemList[x].name);
                    get.itemdataInfo = GameManager.instance.UseItemList[x];
                    StatusEffect[i].gameObject.SetActive(true);
                    Debug.Log("0   :  " + StatusEffect[i].sprite.name);
                }
                if (i == 1 && x == 1)
                {
                    StatusEffect[i].sprite = ResourcesManager.ItemImageLoad(GameManager.instance.UseItemList[x].name);
                    var get = StatusEffect[i].gameObject.GetComponent<UseItem>();
                    get.ItemDataInfo(GameManager.instance.UseItemList[x].name);
                    get.itemdataInfo = GameManager.instance.UseItemList[x];
                    StatusEffect[i].gameObject.SetActive(true);
                    Debug.Log("1   :  " + StatusEffect[i].sprite.name);
                }
                if (i == 2 && x == 2)
                {
                    StatusEffect[i].sprite = ResourcesManager.ItemImageLoad(GameManager.instance.UseItemList[x].name);
                    var get = StatusEffect[i].gameObject.GetComponent<UseItem>();
                    get.ItemDataInfo(GameManager.instance.UseItemList[x].name);
                    get.itemdataInfo = GameManager.instance.UseItemList[x];
                    StatusEffect[i].gameObject.SetActive(true);
                    Debug.Log("2   :  " + StatusEffect[i].sprite.name);
                }
                if (i == 3 && x == 3)
                {
                    StatusEffect[i].sprite = ResourcesManager.ItemImageLoad(GameManager.instance.UseItemList[x].name);
                    var get = StatusEffect[i].gameObject.GetComponent<UseItem>();
                    get.ItemDataInfo(GameManager.instance.UseItemList[x].name);
                    get.itemdataInfo = GameManager.instance.UseItemList[x];
                    Debug.Log("2   :  " + StatusEffect[i].sprite.name);
                }
            }
        }
    }
}
