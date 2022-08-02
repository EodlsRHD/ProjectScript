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

    //�κ��丮
    [Header("�κ��丮")]
    public Button insideOpenbtn;
    public Button insideClosebtn;

    //�� ����
    [Header("�� ����")]
    public Button TurnOff;

    //����� ī��
    [Header("����� ī��")]
    public Button OnDeck;
    public Image OnDeckArrayImage;
    public Button OnDeckBackButton;

    //����� ī��
    [Header("����� ī��")]
    public Button OffDeck;
    public Image OffDeckArrayImage;
    public Button OffDeckBackButton;

    [Header("Scroll")]
    public GameObject CardPool;
    public GameObject CardPosListprefab; //1��Ʈ ����
    public GameObject DeckArrayPos;
    public RectTransform F_ContentPos;
    public RectTransform B_ContentPos;

    //����
    [Header("����")]
    public Button Setting;
    public Image SettingImage;
    public Button SettingBackButtion;

    //newCard,Item
    [Header("���ο� ������ ī��")]
    public Button Continue;

    //Die
    [Header("ĳ���Ͱ� �׾��� ��")]
    public Button Close;

    public void Start()
    {
        //�κ��丮
        insideOpenbtn.onClick.AddListener(OnInventoryInsideOpen);
        insideClosebtn.onClick.AddListener(OnInventoryInsideClose);

        //�� ����
        TurnOff.onClick.AddListener(ButtonTurnOff);

        //����� ī��
        OnDeck.onClick.AddListener(ButtonOnDeck);
        OnDeckBackButton.onClick.AddListener(OnBackbutton);

        //����� ī��
        OffDeck.onClick.AddListener(ButtonOffDeck);
        OffDeckBackButton.onClick.AddListener(OffBackbutton);

        //����
        Setting.onClick.AddListener(ButtonSetting);
        SettingBackButtion.onClick.AddListener(Settingbutton);

        Continue.onClick.AddListener(ButtonNew);

        Close.onClick.AddListener(CloseGame);
    }

    //�κ��丮
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

    //�� ����
    public void ButtonTurnOff()
    {
        TurnOff.gameObject.SetActive(false);
        GameManager.instance.USERTURE = false;
    }

    //����� ī��
    public void ButtonOnDeck()
    {
        GameManager.instance.TF = 1;
        OnDeckArrayImage.gameObject.SetActive(true);
        MakeCardList(GameManager.instance.ForwardCarddataList, GameManager.instance.CardPosList, F_ContentPos);
    }
    public void OnBackbutton() //�ݱ�
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

    //����� ī��
    public void ButtonOffDeck()
    {
        GameManager.instance.TF = 2;
        OffDeckArrayImage.gameObject.SetActive(true);
        MakeCardList(GameManager.instance.BackCarddataList, GameManager.instance.CardPosList, B_ContentPos);
    }
    public void OffBackbutton() //�ݱ�
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

    //����
    public void ButtonSetting()
    {
        SettingImage.gameObject.SetActive(true);
    }
    public void Settingbutton() //�ݱ�
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

    void MakeCardList(List<CarddataInfo> _list, List<GameObject> _ObjList, RectTransform _parant) //����Ʈ�� �޾Ƽ� 
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
        //  - _list : ����� ī��, ����� ī���� '����'
        //  - _PosList : ������� ī�� ������ ��ġ
        //  - _incardlist : ���и� �����ϰ� ������ ī���

        // * ������ �ϴ� �� : ī���� ���� �����̴��� ������ �����ǿ� 5�徿 ������ �����Ŵ��� �ؾ���.
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
