using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapButtonManager : MonoBehaviour
{
    public StageButton[] stageButton;
    public Button GotoMain;

    void Awake()
    {
        GotoMain.onClick.AddListener(GoToMain);
        LockStage(GameManager.instance.Clearstage);
    }

    void LockStage(int _stage) //열린 스테이지만 자물쇠 표시 없음
    {
        for(int i = 0; i < stageButton.Length; i++)
        {
            if(_stage >= i)
            {
                stageButton[i].Lock.gameObject.SetActive(false);
                if (_stage > i)
                {
                    stageButton[i].TranslucenceImage.gameObject.SetActive(true);
                }
            }
        }
    }

    void GoToMain()
    {
        SceneManager.LoadScene("UI");
        //포기 버튼으로 바꾸고 포기하면 유저 프로필 제거
    }

    void Update()
    {
        if(GameManager.instance.stage >= GameManager.instance.Clearstage)
        {
            GameManager.instance.Clearstage = GameManager.instance.stage;
        }
    }
}
