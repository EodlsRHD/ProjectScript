using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour //�������� ���۽� ȣ��
{
    [Header("�׽�Ʈ��")]
    public int MonsterMaxCount; // -1�ؼ� ����
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

    public Image New; //�������� ����â
    public Image Die;

    public Image targetImage;

    public Text energeTextfront;
    public Text energeTextback;

    [Header("ī�� ����Ʈ")]
    public float foward;
    public float hand;
    public float back;

    public List<Image> HistoryImageList;
    public List<Image> HistoryArrowImageList;

    public List<Image> StatusEffect; //4��

    public Slider OriginalHpBar; //����

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

    void SelectPrefab(string _name) // ĳ���� �ν��Ͻ�
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

    void HowManyMonster(int _floor) // ���������� �󸶳� ���͸� ��ȯ���� �������ִ� �Լ�
    {
        int SpawnMonster;
        SpawnMonster = Random.Range(1, MonsterMaxCount);
        //Debug.Log("SpawnMonster   : " + SpawnMonster);

        GameObject[] DecisionMonsterArray = new GameObject[SpawnMonster];
        List<GameObject> DecisionMonsterList = new List<GameObject>();

        if (_floor == 1) //1��
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
        else if (_floor == 2) //2��
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
        else if (_floor == 3) //3��
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

    public void CardInstantiate(List<CarddataInfo> _cardList) //��ī�忡 �ִ� ����Ʈ�� �ν��Ͻ� �ϴ� �Լ�
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

    void CardSC(List<CarddataInfo> _cardList) //Card��ũ��Ʈ�� �ٿ��� �� �ν��Ͻ� �س��´�.
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

        if (GameManager.instance.USERTURE == false && GameManager.instance.MONSTERTURN == false) //�� ���� ��ư�� ������
        {
            StartCoroutine("Turn");
            StopCoroutine("Turn");
            GameManager.instance.MONSTERTURN = true; //������ �ִϸ��̼��� ������ Ʈ��� �����ϱ�
        }

        if (GameManager.instance.Livemonsterlist.Count <= 0) //���������� Ŭ���� ������
        {
            StageClear();
        }

        if(!GameManager.instance.CHARACTERLIVE) //ĳ���Ͱ� �׾�����
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
        MonsterAttack(GameManager.instance.Livemonsterlist); //���� ����
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
        //������ �÷��� �ϸ鼭 ����� ī��, ������ �̹����� ����ش�.

        //�������
        //ī�峪 �������� ����Ͽ��� ��� History����Ʈ�� �߰��ȴ�.
        //����Ʈ�� ���� ���������� �þ��, ��Ÿ���� �մ� �̹���ĭ�� 6���̴�.
        //����Ʈ�� ���� 6���� Ŀ���� �̹���ĭ�� ��ĭ�� �������־�� �Ѵ�.
        // ex) [012345] + 6 => 0[123456]
        //image6�� �׻� ����Ʈ�� �������� ����־�� �Ѵ�.

        //���� : "����Ʈ�� ���� 6�����϶�" �� "����Ʈ�� ���� 6���� Ŭ��"
        // "����Ʈ�� ���� 6�����϶�"   =>  List[0] -> ( image1 ) , List[1] -> ( image2 ) ...�̷���
        // "����Ʈ�� ���� 6���� Ŭ��"  =>  List.Count -> ( image6 ) , List.Count - 1 -> ( image5 ) ...�̷���

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
        //ĳ���Ͱ� ���� �������� ����ߴ��� ��Ÿ���� ��
        //�����ۿ� �ִ� turn�� ������ ����.

        //���� ��
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
