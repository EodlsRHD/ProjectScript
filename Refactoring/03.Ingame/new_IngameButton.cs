using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class new_IngameButton : MonoBehaviour
{
    public GameObject Inventory_Obj;
    public Button Inventory_Close_Button;
    public Button Inventory_Open_Button;

    public Button TurnOff_Button;
    private float Timer = 0;

    public GameObject UseCardDack_Obj;
    public Button UseCardDack_Button;
    public Button UseCardDack_Back_Button;

    public GameObject RestCardDack_Obj;
    public Button RestCardDack_Button;
    public Button RestCardDack_Back_Button;

    public GameObject Setting_Obj;
    public Button Setting_Button;
    public Button Setting_Back_Button;

    public Button StageClear_Continue;

    public Button CharacterDie_Close;

    private void Start()
    {
        Inventory_Close_Button.onClick.AddListener(() => _Button(Inventory_Open_Button, Inventory_Close_Button, Inventory_Obj, false));
        Inventory_Open_Button.onClick.AddListener(() => _Button(Inventory_Open_Button, Inventory_Close_Button, Inventory_Obj, true));

        TurnOff_Button.onClick.AddListener(ButtonTurnOff);

        UseCardDack_Button.onClick.AddListener(ButtonOnDeck);
        UseCardDack_Back_Button.onClick.AddListener(OnBackbutton);

        RestCardDack_Button.onClick.AddListener(ButtonOffDeck);
        RestCardDack_Back_Button.onClick.AddListener(OffBackbutton);

        Setting_Button.onClick.AddListener(ButtonSetting);
        Setting_Back_Button.onClick.AddListener(Settingbutton);

        StageClear_Continue.onClick.AddListener(ButtonNew);

        CharacterDie_Close.onClick.AddListener(CloseGame);
    }

    public void ButtonTurnOff()
    {
        TurnOff_Button.gameObject.SetActive(false);
        new_IngameManager.MonsterTurn();
    }

    private void Update()
    {
        if (TurnOff_Button.gameObject.activeSelf == true)
        {
            Timer += 0;
        }
        if (TurnOff_Button.gameObject.activeSelf == false)
        {
            Timer += Time.deltaTime;
            if (Timer > 3)
            {
                TurnOff_Button.gameObject.SetActive(true);
                new_IngameManager.PlayerTurn();
                Timer = 0;
            }
        }
    }

    public void ButtonOnDeck()
    {
        UseCardDack_Obj.SetActive(true);
    }
    public void OnBackbutton()
    {
        UseCardDack_Obj.SetActive(false);
    }

    public void ButtonOffDeck()
    {
        RestCardDack_Obj.SetActive(true);
    }
    public void OffBackbutton()
    {
        RestCardDack_Obj.SetActive(false);
    }

    public void ButtonSetting()
    {
        Setting_Obj.SetActive(true);
    }
    public void Settingbutton()
    {
        Setting_Obj.SetActive(false);
    }

    public void ButtonNew()
    {
        SceneManager.LoadScene("Map");
    }

    public void CloseGame()
    {
        SceneManager.LoadScene("UI");
    }

    void _Button(GameObject _Close, GameObject _Open)
    {
        _Close.SetActive(false);
        _Open.SetActive(true);
    }

    void _Button(Button _falsebt, Button _truebt, GameObject _obj, bool _bool)
    {
        _falsebt.gameObject.SetActive(!_bool);
        _truebt.gameObject.SetActive(_bool);
        _obj.gameObject.SetActive(_bool);
    }

    void BackButton(GameObject _obj)
    {
        _obj.SetActive(false);
    }
}
