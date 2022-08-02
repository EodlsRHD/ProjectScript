using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtemManager : MonoBehaviour
{
    //시작화면
    [Header("Main Menu")]
    public GameObject Main_Menu;
    public Button Main_Continue; //저장기록이 있을시에만 활성화
    public Button Main_NewStart;
    public Button Main_CardBook;
    public Button Main_Exit;
    public Button Main_Setting;

    //설정
    [Header("Setting Menu")]
    public GameObject Setting_Menu;
    public Button Setting_Back;

    //모드 선택
    [Header("ModeSelect Menu")]
    public GameObject ModeSelect_Menu;
    public Button ModeSelect_Normal;
    public Button ModeSelect_Compete;

    //카드북
    [Header("CardBook Menu")]
    public GameObject CardBook_Menu;
    public Button Cardbook_back;

    //캐릭터 선택창
    [Header("CharacterSelect Menu")]
    public GameObject CharacterScelct_Menu;

    void Start()
    {
        SaveData();
        //시작화면
        Main_NewStart.onClick.AddListener(() => _Button(Main_Menu, ModeSelect_Menu));
        Main_CardBook.onClick.AddListener(() => _Button(Main_Menu, CardBook_Menu));
        Main_Setting.onClick.AddListener(() => _Button(Main_Menu, Setting_Menu));
        Main_Exit.onClick.AddListener(() => Exit(Main_Menu));

        //설정
        Setting_Back.onClick.AddListener(() => _Button(Setting_Menu, Main_Menu));

        //모드 선택
        ModeSelect_Normal.onClick.AddListener(() => _Button(ModeSelect_Menu, CharacterScelct_Menu));
        ModeSelect_Compete.onClick.AddListener(() => _Button(ModeSelect_Menu, CharacterScelct_Menu));

        //카드 북
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
        Debug.Log("프로그램을 종료합니다");
        _CloseMenu.SetActive(false);
    }
}
