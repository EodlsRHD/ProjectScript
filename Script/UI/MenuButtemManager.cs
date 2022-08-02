using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtemManager : MonoBehaviour
{
    //����ȭ��
    [Header("Main Menu")]
    public GameObject Main_Menu;
    public Button Main_Continue; //�������� �����ÿ��� Ȱ��ȭ
    public Button Main_NewStart;
    public Button Main_CardBook;
    public Button Main_Exit;
    public Button Main_Setting;

    //����
    [Header("Setting Menu")]
    public GameObject Setting_Menu;
    public Button Setting_Back;

    //��� ����
    [Header("ModeSelect Menu")]
    public GameObject ModeSelect_Menu;
    public Button ModeSelect_Normal;
    public Button ModeSelect_Compete;

    //ī���
    [Header("CardBook Menu")]
    public GameObject CardBook_Menu;
    public Button Cardbook_back;

    //ĳ���� ����â
    [Header("CharacterSelect Menu")]
    public GameObject CharacterScelct_Menu;

    void Start()
    {
        SaveData();
        //����ȭ��
        Main_NewStart.onClick.AddListener(() => _Button(Main_Menu, ModeSelect_Menu));
        Main_CardBook.onClick.AddListener(() => _Button(Main_Menu, CardBook_Menu));
        Main_Setting.onClick.AddListener(() => _Button(Main_Menu, Setting_Menu));
        Main_Exit.onClick.AddListener(() => Exit(Main_Menu));

        //����
        Setting_Back.onClick.AddListener(() => _Button(Setting_Menu, Main_Menu));

        //��� ����
        ModeSelect_Normal.onClick.AddListener(() => _Button(ModeSelect_Menu, CharacterScelct_Menu));
        ModeSelect_Compete.onClick.AddListener(() => _Button(ModeSelect_Menu, CharacterScelct_Menu));

        //ī�� ��
        Cardbook_back.onClick.AddListener(() => _Button(CardBook_Menu, Main_Menu));
    }

    void _Button(GameObject _CloseMenu, GameObject _OpenMenu )
    {
        _CloseMenu.SetActive(false);
        _OpenMenu.SetActive(true);
    }

    void SaveData()
    {
        //if(GameManager.instance.SaveData == true)
        //{
        //    Main_Continue.gameObject.SetActive(true);
        //}
    }

    void Exit(GameObject _CloseMenu)
    {
        Debug.Log("���α׷��� �����մϴ�");
        _CloseMenu.SetActive(false);
    }
}
