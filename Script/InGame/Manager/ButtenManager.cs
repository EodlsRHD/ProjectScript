using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtenManager : MonoBehaviour
{
    float time;

   //UI
   [Header("UI")]
    public GameObject inside;
    public Inventory inventory;

    //인벤토리
    [Header("인벤토리")]
    public Button insideOpenbtn;
    public Button insideClosebtn;

    //턴 종료
    [Header("턴 종료")]
    public Button TurnOff;

    //사용전 카드
    [Header("사용전 카드")]
    public Button OnDeck;
    public Image OnDeckArrayImage;
    public Button OnDeckBackButton;

    //사용한 카드
    [Header("사용한 카드")]
    public Button OffDeck;
    public Image OffDeckArrayImage;
    public Button OffDeckBackButton;

    [Header("Scroll")]
    public GameObject CardPool;
    public GameObject CardPosListprefab; //1세트 원본
    public GameObject DeckArrayPos;
    public RectTransform F_ContentPos;
    public RectTransform B_ContentPos;

    //설정
    [Header("설정")]
    public Button Setting;
    public Image SettingImage;
    public Button SettingBackButtion;

    //newCard,Item
    [Header("새로운 아이템 카드")]
    public Button Continue;

    //Die
    [Header("캐릭터가 죽었을 때")]
    public Button Close;

    public void Start()
    {
        //인벤토리
        insideOpenbtn.onClick.AddListener(OnInventoryInsideOpen);
        insideClosebtn.onClick.AddListener(OnInventoryInsideClose);

        //턴 종료
        TurnOff.onClick.AddListener(ButtonTurnOff);

        //사용전 카드
        OnDeck.onClick.AddListener(ButtonOnDeck);
        OnDeckBackButton.onClick.AddListener(OnBackbutton);

        //사용후 카드
        OffDeck.onClick.AddListener(ButtonOffDeck);
        OffDeckBackButton.onClick.AddListener(OffBackbutton);

        //설정
        Setting.onClick.AddListener(ButtonSetting);
        SettingBackButtion.onClick.AddListener(Settingbutton);

        Continue.onClick.AddListener(ButtonNew);

        Close.onClick.AddListener(CloseGame);
    }

    //인벤토리
    public void OnInventoryInsideOpen()
    {
        inventory.gameObject.SetActive(true);
        inside.SetActive(false);
    }
    public void OnInventoryInsideClose()
    {
        inventory.gameObject.SetActive(false);
        inside.gameObject.SetActive(true);
    }

    //턴 종료
    public void ButtonTurnOff()
    {
        TurnOff.gameObject.SetActive(false);
        GameManager.instance.USERTURE = false;
    }

    //사용전 카드
    public void ButtonOnDeck()
    {
        GameManager.instance.TF = 1;
        OnDeckArrayImage.gameObject.SetActive(true);
        MakeCardList(GameManager.instance.ForwardCarddataList, GameManager.instance.CardPosList, F_ContentPos);
    }
    public void OnBackbutton() //닫기
    {
        Backbutton(OnDeckArrayImage);
        GameManager.instance.TF = -1;
        foreach(var one in GameManager.instance.CardPosList)
        {
            GameManager.instance.DeckArrayPoslist.Add(one);
            one.transform.SetParent(DeckArrayPos.transform);
        }
        GameManager.instance.CardPosList.RemoveAll(o => true);
    }

    //사용후 카드
    public void ButtonOffDeck()
    {
        GameManager.instance.TF = 2;
        OffDeckArrayImage.gameObject.SetActive(true);
        MakeCardList(GameManager.instance.BackCarddataList, GameManager.instance.CardPosList, B_ContentPos);
    }
    public void OffBackbutton() //닫기
    {
        Backbutton(OffDeckArrayImage);
        GameManager.instance.TF = -1;
        foreach (var one in GameManager.instance.CardPosList)
        {
            GameManager.instance.DeckArrayPoslist.Add(one);
            one.transform.SetParent(DeckArrayPos.transform);
        }
        GameManager.instance.CardPosList.RemoveAll(o => true);
    }

    //설정
    public void ButtonSetting()
    {
        SettingImage.gameObject.SetActive(true);
    }
    public void Settingbutton() //닫기
    {
        Backbutton(SettingImage);
    }
   
    //new Card,Item
    public void ButtonNew()
    {
        SceneManager.LoadScene("Map");
    }

    //Die
    public void CloseGame()
    {
        SceneManager.LoadScene("UI");
        GameManager.instance.CHARACTERLIVE = false;
    }

    ///////////////////////////////////////////////////////////////////////////

    public void Backbutton(Image _image)
    {
        _image.gameObject.SetActive(false);
    }

    void Update()
    {
        if(GameManager.instance.USERTURE)
        {
            GameManager.instance.MONSTERTURN = false;
        }
        if(!GameManager.instance.USERTURE)
        {
            GameManager.instance.MONSTERTURN = true;
        }
        if(!GameManager.instance.MonAni_Attack)
        {
            if (GameManager.instance.USERTURE && !GameManager.instance.MONSTERTURN)
            {
                TurnOff.gameObject.SetActive(true);
            }
        }
    }

    void MakeCardList(List<CarddataInfo> _list, List<GameObject> _ObjList, RectTransform _parant) //리스트를 받아서 
    {
        if (_list.Count <= 5 && _list.Count > 0)
        {
            GameObject Poslist = Instantiate(CardPosListprefab);
            Poslist.transform.SetParent(_parant);
            GameManager.instance.CardPosList.Add(Poslist);
            PosCard(_list, GameManager.instance.CardPosList, GameManager.instance.IncardCardList);
        }
        else if (_list.Count > 5)
        {
            var Num = (_list.Count / 5) + (Mathf.CeilToInt(_list.Count % 5));
            for (int i = 0; i < Num; i++)
            {
                GameObject Poslist = Instantiate(CardPosListprefab);
                Poslist.transform.SetParent(_parant);
                GameManager.instance.CardPosList.Add(Poslist);
            }
            PosCard(_list, GameManager.instance.CardPosList, GameManager.instance.IncardCardList);
        }
    }

    void PosCard(List<CarddataInfo> _list, List<GameObject> _PosList, List<GameObject> _incardlist)
    {
        //  - _list : 사용한 카드, 사용전 카드의 '정보'
        //  - _PosList : 만들어진 카드 포지션 위치
        //  - _incardlist : 손패를 제외하고 나머지 카드들

        // * 만들어야 하는 것 : 카드의 수가 몇장이던지 정해진 포지션에 5장씩 나누어 포지셔닝을 해야함.
        // cardIns : 0  1  2  3  4  |  5  6  7  8  9
        //   pos   : 0              |  1
        // poslist : 0  1  2  3  4  |  0  1  2  3  4
        List<GameObject> cardIns = new List<GameObject>();
        for(int z = 0; z < _incardlist.Count; z++)
        {
            for(int x = 0; x < _list.Count; x++)
            {
                if(_incardlist[z].GetComponent<Card>().datainfo.index.Equals(_list[x].index))
                {
                    cardIns.Add(Instantiate(_incardlist[z]));
                }
            }
        }
        int posnum = 0;
        for (int z = 0; z < _PosList.Count; z++)
        {
            List<Transform> pos = _PosList[z].GetComponent<list>().Poslist;
            for (int x = 0; x < pos.Count; x++)
            {
                cardIns[(x + posnum)].transform.SetParent(pos[x]);
                cardIns[(x + posnum)].transform.position = pos[x].position;
                cardIns[(x + posnum)].transform.localScale = cardIns[(x + posnum)].transform.localScale * 1.5f;
                cardIns[(x + posnum)].SetActive(true);
                cardIns[(x + posnum)].GetComponent<Card>().enabled = false;
                if (x == (pos.Count - 1))
                {
                    posnum = posnum + 5;
                }
            }
        }
    }
}
