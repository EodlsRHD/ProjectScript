using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SystemMessage : MonoBehaviour
{
    public Image SystemMessageImage;
    public Text SystemMessageText;

    float SystemMessageTime;

    void Update()
    {
        if (GameManager.instance.MESSAGE != string.Empty)
        {
            SystemMessageTime += Time.deltaTime;
            SystemMessageImage.gameObject.SetActive(true);
            SystemMessageText.text = GameManager.instance.MESSAGE;
            if (SystemMessageTime >= 1.5f)
            {
                GameManager.instance.MESSAGE = string.Empty;
                SystemMessageImage.gameObject.SetActive(false);
                SystemMessageTime = 0f;
            }
        }
    }
}
