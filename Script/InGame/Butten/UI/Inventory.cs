using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    SystemMessage systemMessage;

    public List<Slot> slots;

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

    void Start()
    {
        systemMessage = GetComponent<SystemMessage>();
        workingslot = -1;
        for(int i = 0; i < GameManager.instance.ItemList.Count; i++)
        {
            slots[i]._IndeX = i;
            slots[i].ItemDataInfo(GameManager.instance.ItemList[i].name);
            slots[i].icon.sprite = ResourcesManager.ItemImageLoad(GameManager.instance.ItemList[i].name);
            slots[i].icon.gameObject.SetActive(true);
        }
        UseItemButton.onClick.AddListener(UseItemButtonClick);
    }

    void UseItemButtonClick()
    {
        if(GameManager.instance.UseItemCount > 0)
        {
            for (int x = 0; x < slots.Count; x++)
            {
                if (UseItemIndex.Equals(slots[x].dataInfo.index))
                {
                    for (int i = 0; i < GameManager.instance.ItemList.Count; i++)
                    {
                        if (slots[x].dataInfo.name.Equals(GameManager.instance.ItemList[i].name))
                        {
                            GameManager.instance.MESSAGE = "사용 아이템   : " + GameManager.instance.ItemList[i].name;
                            GameManager.instance.UseItemNAME = GameManager.instance.ItemList[i].name;
                            GameManager.instance.UseItemList.Add(GameManager.instance.ItemList[i]);
                            UseItemObj.SetActive(false);
                            slots[x].icon.gameObject.SetActive(false);
                            GameManager.instance.CharacterStatusEffect.Add(GameManager.instance.ItemList[i].name);
                            GameManager.instance.ItemList.Remove(GameManager.instance.ItemList[i]);
                            GameManager.instance.UseItemCount = GameManager.instance.UseItemCount - 1;
                            break;
                        }
                    }
                }
            }
        }
        else if(GameManager.instance.UseItemCount <= 0)
        {
            GameManager.instance.MESSAGE = "이번턴에 이미 아이템을 사용하였습니다.";
        }
    }

    public void OnPointerDown(PointerEventData _eventdata) //아이템 슬롯을 눌럿을때
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (slots[i].IsInRect(_eventdata.position)) //클릭한곳의 위치가 슬롯의 위치에 있으면 실행
                {
                    if (slots[i].ICONNAME != string.Empty)
                    {
                        Color a = slots[i].icon.color;
                        a.a = 0.1f;
                        slots[i].icon.color = a;

                        Empty.transform.position = _eventdata.position;
                        Empty.sprite = ResourcesManager.ItemImageLoad(slots[i].ICONNAME);
                        Empty.gameObject.SetActive(true);

                        workingslot = i;

                        UseItemIndex = slots[i].dataInfo.index;
                        UseItemImage.sprite = ResourcesManager.ItemImageLoad(slots[i].ICONNAME);
                        UseItemObj.SetActive(true);
                    }
                    break;
                }
                UseItemObj.SetActive(false);
            }
            else if(Input.GetMouseButtonDown(1))
            {
                //아이템 설명표
                if (slots[i].icon.gameObject.activeSelf == true)
                {
                    if (slots[i].IsInRect(_eventdata.position))
                    {
                        string Name = slots[i].ICONNAME;
                        ItemExplanation.gameObject.SetActive(true);
                        ItemImage.sprite = ResourcesManager.ItemImageLoad(Name);
                        ItemName.text = Name.ToString();
                        foreach(var one in DataManager.instance.LoadItemData())
                        {
                            if(one.name.Equals(Name))
                            {
                                ItemEx.text = one.description.ToString();
                            }
                        }
                            
                    }
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData _eventdata) //드래그 시작
    {
        if (workingslot != -1)
        {
            Empty.transform.position = _eventdata.position;
        }
    }

    public void OnDrag(PointerEventData _eventdata) //드래그 중
    {
        if (workingslot != -1)
        {
            Empty.transform.position = _eventdata.position;
        }
    }

    public void OnEndDrag(PointerEventData _eventdata) //드래그 끝
    {
        if (workingslot == -1) { return; }

        for(int i = 0; i < slots.Count; i++)
        {
            if(slots[i].IsInRect(_eventdata.position))
            {
                if(workingslot != -1)
                {
                    //비어있는지
                    if (slots[i].ICONNAME.Equals(string.Empty))
                    {
                        Color a = slots[i].icon.color;
                        a.a = 1f;
                        slots[i].icon.color = a;

                        string workIconName = slots[workingslot].ICONNAME;
                        slots[workingslot].icon.gameObject.SetActive(false);
                        slots[workingslot].icon.sprite = null;

                        slots[i].icon.sprite = ResourcesManager.ItemImageLoad(workIconName);
                        slots[i].icon.gameObject.SetActive(true);

                        Empty.gameObject.SetActive(false);
                        Empty.sprite = null;
                        workingslot = -1;

                        return;
                    }

                    //비어있지 않으면
                    else if (slots[i].ICONNAME != string.Empty)
                    {
                        //해당하는 슬롯의 아이콘을 알아온다.
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

    public void OnPointerUp(PointerEventData _eventdata) //아이템 슬롯을 땐순간
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].IsInRect(_eventdata.position))
            {
                //동일한 슬롯에서 일어나는 이벤트
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
