using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class new_MapButton : MonoBehaviour
{
    public List<new_Map_stageButton> stageButton_list;

    [SerializeField] 
    private Button GotoMain;

    private void Awake()
    {
        if(new_GameManager.ClearStage_Count < new_GameManager.NowStage_Count)
        {
            new_GameManager.ClearStage_Count = new_GameManager.NowStage_Count;
        }
    }

    private void Start()
    {
        Lock_Stage(new_GameManager.ClearStage_Count);
        GotoMain.onClick.AddListener(GoToMain);
    }

    private void Lock_Stage(ushort _stage)
    {
        for(int i = 0; i < stageButton_list.Count; i++)
        {
            if (_stage >= i)
            {
                stageButton_list[i].Lock.gameObject.SetActive(false);
            }
            if (_stage < i)
            {
                stageButton_list[i].TranslucenceImage.gameObject.SetActive(true);
            }
        }
    }

    private void GoToMain()
    {
        SceneManager.LoadScene("UI");
    }
}
