using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class new_Inventory : new_IngameManager, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    new_PoolManager new_poolManager;

    public List<new_Slot> slots;

    public Image Empty;
    public Image ItemExplanation;

    [Header("UseItem")]
    public GameObject UseItemObj;
    public Image UseItemImage;
    public Button UseItemButton;
    public float UseItemIndex;

    [Header("Item")]
    public Text ItemName;
    public Text ItemEx;
    public Image ItemImage;

    int workingslot;

    void Awake()
    {
        new_poolManager = GetComponent<new_PoolManager>();
        UseItemButton.onClick.AddListener(UseItemButtonClick);
    }

    IEnumerator Start()
    {
        workingslot = -1;
        if (Player_HaveItem_list != null)
        {
            for (int i = 0; i < Player_HaveItem_list.Count; i++)
            {
                slots[i].Solt_Index = (ushort)i;
                slots[i].info_ = Player_HaveItem_list[i];
                //slots[i].icon.sprite = new_poolManager.Find_ItemImage(Player_HaveItem_list[i].name);
                slots[i].icon.gameObject.SetActive(true);
            }
        }
        yield return null;
    }

    void UseItemButtonClick()
    {
        if (Item_Count > 0)
        {
            for (int x = 0; x < slots.Count; x++)
            {
                if (UseItemIndex.Equals(slots[x].info_.index))
                {
                    for (int i = Player_HaveItem_list.Count - 1; i >= 0; i--)
                    {
                        if (slots[x].info_.name.Equals(Player_HaveItem_list[i].name))
                        {
                            UseItemObj.SetActive(false);
                            slots[x].icon.gameObject.SetActive(false);
                            Player_UseItem_queue.Enqueue(Player_HaveItem_list[i]);
                            Player_HaveItem_list.Remove(Player_HaveItem_list[i]);
                            Item_Count -= 1;
                            break;
                        }
                    }
                }
            }
        }
        //else if (GameManager.instance.UseItemCount <= 0)
        //{
        //    GameManager.instance.MESSAGE = "???????? ???? ???????? ??????????????.";
        //}
    }

    public void OnPointerDown(PointerEventData _eventdata)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (slots[i].IsInRect(_eventdata.position))
                {
                    if (slots[i].ICONNAME != string.Empty)
                    {
                        Color a = slots[i].icon.color;
                        a.a = 0.1f;
                        slots[i].icon.color = a;

                        Empty.transform.position = _eventdata.position;
                        Empty.sprite = slots[i].icon.sprite;
                        Empty.gameObject.SetActive(true);

                        workingslot = i;

                        UseItemIndex = slots[i].info_.index;
                        UseItemImage.sprite = slots[i].icon.sprite;
                        UseItemObj.SetActive(true);
                    }
                    break;
                }
                UseItemObj.SetActive(false);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (slots[i].icon.gameObject.activeSelf == true)
                {
                    if (slots[i].IsInRect(_eventdata.position))
                    {
                        ItemExplanation.gameObject.SetActive(true);
                        ItemImage.sprite = slots[i].icon.sprite;
                        ItemName.text = slots[i].info_.name;
                    }
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData _eventdata)
    {
        if (workingslot != -1)
        {
            Empty.transform.position = _eventdata.position;
        }
    }

    public void OnDrag(PointerEventData _eventdata)
    {
        if (workingslot != -1)
        {
            Empty.transform.position = _eventdata.position;
        }
    }

    public void OnEndDrag(PointerEventData _eventdata)
    {
        if (workingslot == -1) { return; }

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].IsInRect(_eventdata.position))
            {
                if (workingslot != -1)
                {
                    if (slots[i].ICONNAME.Equals(string.Empty))
                    {
                        Color a = slots[i].icon.color;
                        a.a = 1f;
                        slots[i].icon.color = a;

                        slots[i].icon.sprite = slots[workingslot].icon.sprite;
                        slots[i].icon.gameObject.SetActive(true);

                        slots[workingslot].icon.sprite = null;
                        slots[workingslot].icon.gameObject.SetActive(false);

                        Empty.gameObject.SetActive(false);
                        Empty.sprite = null;
                        workingslot = -1;

                        return;
                    }
                    else if (slots[i].ICONNAME != string.Empty)
                    {
                        Sprite tmp = slots[i].icon.sprite;
                        slots[i].icon.sprite = slots[workingslot].icon.sprite;
                        slots[workingslot].icon.sprite = tmp;

                        Empty.gameObject.SetActive(false);
                        Empty.sprite = null;
                        workingslot = -1;

                        return;
                    }
                }
            }
        }
        Empty.gameObject.SetActive(false);
        Empty.sprite = null;
        workingslot = -1;
    }

    public void OnPointerUp(PointerEventData _eventdata)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].IsInRect(_eventdata.position))
            {
                if (workingslot == i)
                {
                    Color a = slots[i].icon.color;
                    a.a = 1f;
                    slots[i].icon.color = a;

                    Empty.gameObject.SetActive(false);
                    Empty.sprite = null;
                    workingslot = -1;
                    break;
                }
            }

            if (slots[i].icon.gameObject.activeSelf == true)
            {
                ItemExplanation.gameObject.SetActive(false);
            }
        }

    }
}
