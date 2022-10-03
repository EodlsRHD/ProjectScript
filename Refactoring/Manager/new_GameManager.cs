using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class new_GameManager : MonoBehaviour
{
    private static new_GameManager _instance;
    public static new_GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<new_GameManager>();
                if (obj != null)
                {
                    _instance = obj;
                }
                else
                {
                    var newobj = new GameObject().AddComponent<new_GameManager>();
                    _instance = newobj;
                }
            }
            return _instance;
        }
    }

    protected delegate void SetPlayerData();
    protected SetPlayerData userdata;

    [HideInInspector] public User_Info user_info;

    [HideInInspector] public List<new_Card> UserCard_list; // User Have Cardlist

    [HideInInspector] public Character_Info UserChar_Info;
    [HideInInspector] public GameObject UserChar_Obj;
    [HideInInspector] public string SelectJob_Name;

    [HideInInspector] public List<new_Card> Card_list; // game inside All Card
    [HideInInspector] public List<new_Monster> Monster_list;
    [HideInInspector] public ushort MonsterSpawnNum;

    [HideInInspector]
    public static ushort ClearStage_Count;
    public static ushort NowStage_Count;
    public static ushort voitCard_Num;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        var objs = FindObjectsOfType<new_GameManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);

        ClearStage_Count = 0;
        NowStage_Count = 0;
        voitCard_Num = 4;
        UserCard_list = new List<new_Card>();
        Card_list = new List<new_Card>();
        Monster_list = new List<new_Monster>();
        MonsterSpawnNum = 2;
        SelectJob_Name = string.Empty;
    }

    public void SetChar(string _str)
    {
        SelectJob_Name = _str;
        new_ResourcesManager.GetCharacterData(SelectJob_Name);
        new_ResourcesManager.GetCharacterObj(SelectJob_Name);
    }
}
