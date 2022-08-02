using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StageButton : MonoBehaviour , IPointerDownHandler
{
    public Image Lock;
    public Image TranslucenceImage;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Lock.gameObject.activeSelf == false && TranslucenceImage.gameObject.activeSelf == false)
        {
            GameManager.instance.stagename = gameObject.name;
            SceneManager.LoadScene("NormalMode");
        }
    }
}
