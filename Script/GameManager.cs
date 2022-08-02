using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
        set
        {
            instance = value;
        }
    }

    public List<string> characterearlystatsList;

    private string jobname;
    public string JOBNAME
    {
        get { return jobname; }
        set { jobname = value; }
    }
    private float jobIndex;
    public float JOBINDEX
    {
        get { return jobIndex; }
        set { jobIndex = value; }
    }

    public int Clearstage;
    public int stage
    {
        get
        {
            if (stagename.Contains(1.ToString()))
            {
                return 1;
            }
            else if (stagename.Contains(2.ToString()))
            {
                return 2;
            }
            else if (stagename.Contains(3.ToString()))
            {
                return 3;
            }
            else if (stagename.Contains(4.ToString()))
            {
                return 4;
            }
            else if (stagename.Contains(5.ToString()))
            {
                return 5;
            }
            else if (stagename.Contains(6.ToString()))
            {
                return 6;
            }
            else if (stagename.Contains(7.ToString()))
            {
                return 7;
            }
            else if (stagename.Contains(8.ToString()))
            {
                return 8;
            }
            else if (stagename.Contains(9.ToString()))
            {
                return 9;
            }
            else if (stagename.Contains(10.ToString()))
            {
                return 10;
            }
            return 0;
        }
        set { stage = value; }
    }
    public string stagename;

    private int floor;
    public int FLOOR
    {
        get
        {
            if (0 <= stage && stage < 10)
            {
                floor = 1;
                return floor;
            }
            if (10 <= stage && stage < 20)
            {
                floor = 2;
                return floor;
            }
            if (20 <= stage && stage < 10)
            {
                floor = 3;
                return floor;
            }
            return floor;
        }
        set { floor = value; }
    }

    private int tf; // ScrollView.cs���� �����
    public int TF
    {
        get { return tf; }
        set { tf = value; }
    }

    private bool userturd; //true�� �÷��̾���
    public bool USERTURE
    {
        get { return userturd; }
        set { userturd = value; }
    }
    private bool monsterturn; // true�� ������
    public bool MONSTERTURN
    {
        get { return monsterturn; }
        set { monsterturn = value; }
    }
    private int turnCount;
    public int TURNCOUNT
    {
        get { return turnCount; }
        set { turnCount = value; }
    }

    private int usecardindex;
    public int USECARDINDEX
    {
        get{ return usecardindex; }
        set { usecardindex = value; }
    }
    private bool UseCard = false; //ī�带 ����ߴ��� ���ߴ���
    public bool USECARD
    {
        get { return UseCard; }
        set { UseCard = value; }
    }
    private bool CharacterLive = false; //ĳ���Ͱ� ����ִ��� �ƴ���
    public bool CHARACTERLIVE
    {
        get { return CharacterLive; }
        set { CharacterLive = value; }
    }
    private string characterjob;
    public string CharacterJob
    {
        get { return characterjob; }
        set { characterjob = value; }
    }
    public List<string> CharacterStatusEffect;
    private ushort CardCounting;
    public ushort CARDCOUNTING
    {
        get { return CardCounting; }
        set { CardCounting = value; }
    }
    private bool savecharacterdata = true; //������ �̷�� ������ �ƴ��� Ȯ�� (false�� ����Ȱ�)
    public bool SAVECHARACTERDATA
    {
        get { return savecharacterdata; }
        set { savecharacterdata = value; }
    }

    private ushort Drowcard;
    public ushort DROWCARD
    {
        get { return Drowcard; }
        set { Drowcard = value; }
    }

    public List<string> History;

    public Character character; //ĳ���� ����
    public List<Monster> Livemonsterlist; //����ִ� ���� ����Ʈ

    public List<CarddataInfo> ForwardCarddataList; //����� ī��
    public List<CarddataInfo> BackCarddataList; //����� ī��
    public List<CarddataInfo> HandCarddataList; //�տ� �ִ� ī��

    public List<GameObject> IncardCardList;
    public List<GameObject> HandcardCardList;

    public List<ItemdataInfo> ItemList;
    public List<ItemdataInfo> UseItemList;
    public int UseItemCount { get; set; }
    public string UseItemNAME { get; set; }
    public ushort ITEMCOUNTING
    {
        get { return (ushort)ItemList.Count; }
    }

    //���� ���
    public string PositionDataPath;
    public string CardDataPath;
    public string ItemDataPath;
    public string SkillDAtaPath;
    public string oneMonsterDataPath;
    public string twoMonsterDataPath; 
    public string threeMonsterDataPath;
    public string PlayerDataPath;
    public string UserCardListDataPath;
    public string CharacterStatsDataPath;
    public string UserItemListDataPath;

    //ī�� ����Ʈ
    private List<CarddataInfo> cardList; //�÷��̾ �߰��� ī�� ����Ʈ (���Ͽ��� �˻��ؿ´�.)
    public List<CarddataInfo> CARDLIST
    {
        get { return cardList; }
        set { cardList = value; }
    }

    public CarddataInfo SHOPcarddataInfo;
    public ItemdataInfo SHOPitemdataInfo;

    //ĵ���� ������
    public Vector3 CharacterCanvasePos;
    public List<Vector3> MonsterCanvasePoslist;
    public Vector3 TargetPos;

    private int charactersaveindex;
    public int CHARACTERSAVEINDEX
    {
        get { return charactersaveindex; }
        set { charactersaveindex = value; }
    }
    private float characterdamegenum;
    public float CharacterDamageNum
    {
        get { return characterdamegenum; }
        set { characterdamegenum = value; }
    }
    private float monsterdamegenum;
    public float MonsterDamageNum
    {
        get { return monsterdamegenum; }
        set { monsterdamegenum = value; }
    }

    //SystemMessage
    private string message;
    public string MESSAGE
    {
        get { return message; }
        set { message = value; }
    }

    //Animation
    public bool CharAni_Attack { get; set; }
    public bool CharAni_GetHit { get; set; }
    public bool CharAni_StageClear { get; set; }
    public bool CharAni_Die { get; set; }

    public string MonAni_HitName { get; set; }
    public bool MonAni_Attack { get; set; }
    public bool MonAni_GetHit { get; set; }
    public bool MonAni_Die { get; set; }

    public List<GameObject> CardPosList;
    public List<GameObject> DeckArrayPoslist;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        Clearstage = stage;
        floor = 1;
        Drowcard = 4;
        usecardindex = -1;
        UseItemCount = 1;
        turnCount = 1;
        characterdamegenum = -1;
        monsterdamegenum = -1;
        tf = -1;
        charactersaveindex = 1;
        UseCard = false;
        userturd = true;
        monsterturn = false;
        message = string.Empty;

        History = new List<string>();

        Livemonsterlist = new List<Monster>();

        cardList = new List<CarddataInfo>(); //������ �߰��� ī��
        cardList.RemoveAll(o => true);

        characterearlystatsList = new List<string>()
        {
            { "Index" },
            { "Job" },
            { "Health" },
            { "PhyAttack" },
            { "PhyDefense" },
            { "MagaAttack" },
            { "MagaDefense" },
            { "Evasion" },
            { "Energy" }
        };
        CharacterStatusEffect = new List<string>();

        ForwardCarddataList = new List<CarddataInfo>();
        BackCarddataList = new List<CarddataInfo>();
        HandCarddataList = new List<CarddataInfo>();

        IncardCardList = new List<GameObject>();
        HandcardCardList = new List<GameObject>();

        ItemList = new List<ItemdataInfo>();
        UseItemList = new List<ItemdataInfo>();

        MonsterCanvasePoslist = new List<Vector3>();

        CardPosList = new List<GameObject>();
        DeckArrayPoslist = new List<GameObject>();

        CharAni_Attack = false;
        CharAni_GetHit = false;
        CharAni_StageClear = false;
        CharAni_Die = false;

        MonAni_HitName = string.Empty;
        MonAni_Attack = false;
        MonAni_GetHit = false;

        DataPath();
        createFile();
        DataManager.instance.ItemData();
    }

    void DataPath()
    {
        CharacterStatsDataPath = Application.dataPath + @"\Resources\PlayerData\CharacterStats.csv";
        PositionDataPath = Application.dataPath + @"\Resources\PlayerData\CreaturePosition.csv";
        CardDataPath = Application.dataPath + @"\Resources\PlayerData\Card.csv";
        ItemDataPath = Application.dataPath + @"\Resources\PlayerData\Item.csv";
        SkillDAtaPath = Application.dataPath + @"\Resources\PlayerData\Skill.csv";
        oneMonsterDataPath = Application.dataPath + @"\Resources\PlayerData\FloorOneMonsterCollector.csv";
        twoMonsterDataPath = Application.dataPath + @"\Resources\PlayerData\FloorTweMonsterCollector.csv";
        threeMonsterDataPath = Application.dataPath + @"\Resources\PlayerData\FloorThreeMonsterCollector.csv";

        PlayerDataPath = Application.dataPath + @"\Resources\PlayerData\UserProfile.csv";
        UserCardListDataPath = Application.dataPath + @"\Resources\PlayerData\UserCardList.csv";
        UserItemListDataPath = Application.dataPath + @"\Resources\PlayerData\UserItemList.csv";
    }

    public void createFile()
    {
        using (FileStream fs = new FileStream(PlayerDataPath, FileMode.Create))
        using (StreamWriter sr = new StreamWriter(fs))
        {
            char option = ',';
            foreach (string one in characterearlystatsList)
            {
                sr.Write(one);
                sr.Write(option);

            }
            sr.Write('\n');
            sr.Close();
            fs.Close();
        }

        using (FileStream fs = new FileStream(UserItemListDataPath, FileMode.Create))
        using (StreamWriter sr = new StreamWriter(fs))
        {
            char option = ',';
            sr.Write("index");
            sr.Write(option);
            sr.Write("name");
            sr.Write(option);
            sr.Write("type");
            sr.Write(option);
            sr.Write("turn");
            sr.Write(option);
            sr.Write("description");
            sr.Write(option);
            sr.Write("damage");
            sr.Write(option);
            sr.Write('\n');
            sr.Close();
            fs.Close();
        }
    }
}
