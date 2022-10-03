using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class new_DeckList : MonoBehaviour
{
    public List<new_DeckArray_List> posArray_list;

    private void Awake()
    {
        posArray_list = new List<new_DeckArray_List>();
        for (int i = 0; i < 10; i++)
        {
            posArray_list.Add(transform.GetChild(i).gameObject.GetComponent<new_DeckArray_List>());
        }
    }
}
