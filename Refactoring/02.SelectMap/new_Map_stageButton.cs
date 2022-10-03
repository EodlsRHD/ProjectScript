using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class new_Map_stageButton : MonoBehaviour
{
    public Image Lock;
    public Image TranslucenceImage;
    public Button stage;

    new_MapButton new_mapButton;

    private void Start()
    {
        new_mapButton = GetComponent<new_MapButton>();
        stage.onClick.AddListener(NextScene);
    }

    void NextScene()
    {
        if(Lock.gameObject.activeSelf == false)
        {
            new_GameManager.NowStage_Count = ushort.Parse(this.gameObject.name);
            new_Loading.LoadScene("NormalMode");
        }
    }
}
