using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public struct dataInfo
    {
        public float index;
        public string name;
        public float cost;
        public float cardclass;
        public string skill;
        public float attacktype;
        public float turn;
        public string description;
        public float damage;
    }

    public PRS originPRS; //움직임이 있어도 원본으로 다시 이동하기 위함

    public dataInfo datainfo;
    public Text cardname;
    public Text description;
    public Text energy;
    public Image cardimage;
    public Image targetimage;
    public RectTransform deathposition;

    public Transform Leftpos;
    public Transform Rightpos;
    public Transform cardpool;

    public Vector3 Vtmp;
    public Quaternion Qtmp;
    public Vector3 Stmp;

    GameObject target;
    Image cardImage;
    RectTransform RectTR;

    [Header("카드 정보")]
    public float inDex;
    public string cardName;
    public string Description;
    public string Energe;

    void Awake()
    {
        datainfo = new dataInfo();
        cardImage = GetComponent<Image>();
        RectTR = GetComponent<RectTransform>();

        Vtmp = transform.position;
        Qtmp = transform.rotation;
        Stmp = transform.localScale;
    }

    public void DataInfo(float _index)
    {
        CarddataInfo? Info = DataManager.instance.GetCarddataInfo(_index);
        if (Info.HasValue)
        {
            datainfo.index = Info.Value.index;
            datainfo.name = Info.Value.name;
            datainfo.cost = Info.Value.cost;
            datainfo.cardclass = Info.Value.cardclass;
            datainfo.skill = Info.Value.skill;
            datainfo.attacktype = Info.Value.attacktype;
            datainfo.turn = Info.Value.turn;
            datainfo.description = Info.Value.description;
            datainfo.damage = Info.Value.damage;
        }
        cardname.text = datainfo.name.ToString();
        description.text = datainfo.description.ToString();
        energy.text = datainfo.cost.ToString();

        inDex = datainfo.index;
        cardName = datainfo.name.ToString();
        Description = datainfo.description.ToString();
        Energe = datainfo.cost.ToString();
    }

    //유니티 에셋 "DOTween"사용하여 구성
    public void MovePosition(PRS prs, bool useDoTween, float dotweenTime)
    {
        if(useDoTween)
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }

    public void OnPointerDown(PointerEventData _eventData)
    {
        Vtmp.x = transform.position.x;
        Vtmp.y = transform.position.y + 380f;
        Qtmp = new Quaternion(0, 0, 0, Qtmp.w);
        Stmp = new Vector3(1.1f, 1.1f, 1.1f);
        PRS PRStmp = new PRS(Vtmp, Qtmp, Stmp);
        MovePosition(PRStmp, true, 0.3f);
    }

    public void OnBeginDrag(PointerEventData _eventData)
    {
        targetimage.gameObject.SetActive(true);
        targetimage.transform.position = _eventData.position;
    }

    public void OnDrag(PointerEventData _eventData)
    {
        targetimage.transform.position = _eventData.position;
    }

    public void OnEndDrag(PointerEventData _eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(_eventData.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            string Tag = hit.collider.tag;
            for (int i = 0; i < GameManager.instance.Livemonsterlist.Count; i++)
            {
                if (Tag == GameManager.instance.Livemonsterlist[i].tag)
                {
                    if (hit.collider.gameObject.name.Equals(GameManager.instance.Livemonsterlist[i].gameObject.name))
                    {
                        if (CardManager.instance.EFRONT < datainfo.cost) //에너지가 부족함
                        {
                            targetimage.gameObject.SetActive(false);
                        }
                        else
                        {
                            GameManager.instance.MonAni_HitName = GameManager.instance.Livemonsterlist[i].gameObject.name;
                            GameManager.instance.TargetPos = _eventData.position;
                            CardManager.instance.EFRONT = CardManager.instance.EFRONT - datainfo.cost;
                            PRS prstmp = new PRS(deathposition.transform.position, deathposition.transform.rotation, deathposition.transform.localScale);
                            MovePosition(prstmp, true, 0.3f);
                            GameManager.instance.USECARD = true;
                            GameManager.instance.CharAni_Attack = true;
                            GameManager.instance.USECARDINDEX = (int)datainfo.index;
                            GameManager.instance.History.Add(datainfo.name);
                            if (datainfo.skill != string.Empty) //스킬 DB에서 찾아서 하는걸로 차후에 수정
                            {
                                CardManager.instance.ReturnDamage(GameManager.instance.Livemonsterlist[i], datainfo.damage);
                            }
                            if (GameManager.instance.UseItemList != null)
                            {
                                CardManager.instance.ReturnUseItemDamage(GameManager.instance.Livemonsterlist[i], datainfo.damage, GameManager.instance.UseItemList);
                            }
                            CardManager.instance.RemoveCard(Leftpos, Rightpos);
                            List<PRS> list = new List<PRS>();
                            list = CardManager.instance.RoundCardPosition(Leftpos, Rightpos, GameManager.instance.HandcardCardList.Count, 0.5f, Vector3.one * 1.3f);
                            for (int j = 0; j < GameManager.instance.HandcardCardList.Count; j++)
                            {
                                var tmp = GameManager.instance.HandcardCardList[j].GetComponent<Card>();
                                tmp.originPRS = list[j];
                                tmp.MovePosition(list[j], true, 0.4f);
                            }
                            if (RectTR.transform.position == deathposition.transform.position)
                            {
                                gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData _eventData)
    {
        targetimage.gameObject.SetActive(false);
        MovePosition(originPRS, true, 0.3f);
    }

    void Update()
    {
        if (RectTR.transform.position == deathposition.transform.position)
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(cardpool);
        }
    }
}

