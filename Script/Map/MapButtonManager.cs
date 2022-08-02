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

    void LockStage(int _stage) //���� ���������� �ڹ��� ǥ�� ����
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
        //���� ��ư���� �ٲٰ� �����ϸ� ���� ������ ����
    }

    void Update()
    {
        if(GameManager.instance.stage >= GameManager.instance.Clearstage)
        {
            GameManager.instance.Clearstage = GameManager.instance.stage;
        }
    }
}
