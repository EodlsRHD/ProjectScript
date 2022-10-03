using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class new_DeckArray_List : MonoBehaviour
{
    public List<RectTransform> Poslist;

    private void Awake()
    {
        Poslist = new List<RectTransform>();
        for (int i = 0; i < 5; i++)
        {
            Poslist.Add((RectTransform)transform.GetChild(i).gameObject.transform);
        }
    }
}
