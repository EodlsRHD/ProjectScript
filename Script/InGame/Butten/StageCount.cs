using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageCount : MonoBehaviour
{
    public Text text;
    int sta;

    void Start()
    {
        sta = GameManager.instance.stage;
        text.text = sta.ToString();
    }
}
