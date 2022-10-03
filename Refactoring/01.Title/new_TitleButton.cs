using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class new_TitleButton : MonoBehaviour
{
    public GameObject Main_Obj;
    public Button Main_Continue;
    public Button Main_NewStart;
    public Button Main_CardBook;
    public Button Main_Exit;
    public Button Main_Setting;

    public GameObject Setting_Obj;
    public Button Setting_Back;

    public GameObject ModeSelect_Obj;
    public Button ModeSelect_Normal;
    public Button ModeSelect_Compete;

    public GameObject CardBook_Obj;
    public Button CardBook_Back;

    public GameObject JobSelect_Obj;
    public Button Wa;
    public Button Ro;
    public Button Ma;
    public Button As;

    private void Start()
    {
        Wa.onClick.AddListener(() => ClickButton(Wa.gameObject.name));
        Ro.onClick.AddListener(() => ClickButton(Ro.gameObject.name));
        Ma.onClick.AddListener(() => ClickButton(Ma.gameObject.name));
        As.onClick.AddListener(() => ClickButton(As.gameObject.name));

        Main_NewStart.onClick.AddListener(() => _Button(Main_Obj, ModeSelect_Obj));
        Main_CardBook.onClick.AddListener(() => _Button(Main_Obj, CardBook_Obj));
        Main_Setting.onClick.AddListener(() => _Button(Main_Obj, Setting_Obj));
        Main_Exit.onClick.AddListener(() => Exit(Main_Obj));

        Setting_Back.onClick.AddListener(() => _Button(Setting_Obj, Main_Obj));

        ModeSelect_Normal.onClick.AddListener(() => _Button(ModeSelect_Obj, JobSelect_Obj));
        ModeSelect_Compete.onClick.AddListener(() => _Button(ModeSelect_Obj, JobSelect_Obj));

        CardBook_Back.onClick.AddListener(() => _Button(CardBook_Obj, Main_Obj));
    }

    void ClickButton(string _str)
    {
        new_GameManager.instance.SetChar(_str);
        new_Loading.LoadScene("Map");
    }

    void _Button(GameObject _CloseMenu, GameObject _OpenMenu)
    {
        _CloseMenu.SetActive(false);
        _OpenMenu.SetActive(true);
    }

    void Exit(GameObject _CloseMenu)
    {
        _CloseMenu.SetActive(false);
    }
}
