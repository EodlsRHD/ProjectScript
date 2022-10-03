using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class new_CardManager : PoolManager_Order_abstructClass
{
    private static new_CardManager new_cardManager;

    private List<new_Card> WorkCard_list;
    private Queue<new_Card> RestCard_queue;
    private Queue<new_Card> UseCard_queue;

    private List<Sprite> UseCardHistory_Sprite_list;
    public List<Image> HistoryBarCard_Image_list;
    public List<Image> HistoryBarArrow_Image_list;

    [SerializeField] private RectTransform Card_Pool;
    [SerializeField] private RectTransform Spawn_Pos;
    [SerializeField] private RectTransform Hand_leftpos;
    [SerializeField] private RectTransform Hand_rightpos;
    [SerializeField] private RectTransform RestCard_Pos;
    [SerializeField] private RectTransform UseCard_Pos;

    PRS UseCardDeck_prs;

    public Image target_Image;

    public new_DeckList Rest_Deck;
    public new_DeckList Use_Deck;

    private void Awake()
    {
        new_cardManager = this;

        WorkCard_list = new List<new_Card>();
        RestCard_queue = new Queue<new_Card>();
        UseCard_queue = new Queue<new_Card>();

        UseCardHistory_Sprite_list = new List<Sprite>();

        UseCardDeck_prs = new PRS(new_cardManager.UseCard_Pos.position, new_cardManager.UseCard_Pos.rotation, new_cardManager.UseCard_Pos.localScale);

        //TestCode
        for (int i = 0; i < new_GameManager.instance.UserCard_list.Count; i++)
        {
            new_Card tmp = new_GameManager.instance.UserCard_list[i];
            Send_request(false, 3, tmp.info_.name, tmp.info_.uniqueNum, null, Receive_request);
            new_cardManager.RestCard_queue.Enqueue(tmp);
        }
        //
    }

    public static void voitCard(ushort _num)
    {
        if(_num >= new_cardManager.RestCard_queue.Count)
        {
            CardHandOver();
        }
        for (int i = 0; i < _num; i++)
        {
            new_Card tmp = new_cardManager.RestCard_queue.Dequeue();
            tmp.gameObject.transform.SetParent(new_cardManager.Spawn_Pos);
            tmp.gameObject.SetActive(true);
            tmp.gameObject.transform.position = new_cardManager.Spawn_Pos.position;
            new_cardManager.WorkCard_list.Add(tmp);
        }
        RefreshCardPos(new_cardManager.WorkCard_list);
    }


    public static void CardHandOver()
    {
        for(int i = new_cardManager.UseCard_queue.Count - 1; i >= 0; i--)
        {
            new_Card tmp = new_cardManager.UseCard_queue.Dequeue();
            new_cardManager.RestCard_queue.Enqueue(tmp);
        }
    }

    public static void ShakeCard()
    {
        for (int i = 0; i < new_cardManager.RestCard_queue.Count; i++)
        {
            int ran = Random.Range(0, new_cardManager.RestCard_queue.Count);

            //new_Card tmp = new_cardManager.RestCard_queue.Dequeue();
            //new_cardManager.RestCard_queue[i] = new_cardManager.RestCard_queue[ran];
            //new_cardManager.RestCard_queue[ran] = tmp;
        }
    }

    public static void HandCard_AllRemove()
    {
        for(int i = new_cardManager.WorkCard_list.Count - 1; i >= 0; i--)
        {
            new_Card tmp = new_cardManager.WorkCard_list[i];
            new_cardManager.UseCard_queue.Enqueue(tmp);
            tmp.MovePosition(new_cardManager.UseCardDeck_prs, true, 0.3f);
            new_cardManager.Send_request(true, 3, string.Empty, tmp.info_.uniqueNum, tmp.gameObject, new_cardManager.Receive_request);
        }
        new_cardManager.WorkCard_list.RemoveAll(o => true);
    }

    public static void Goto_UseCardDeck(new_Card _UsecardSC)
    {
        new_cardManager.UseCard_queue.Enqueue(_UsecardSC);
        new_cardManager.CardHistory(_UsecardSC);
        _UsecardSC.MovePosition(new_cardManager.UseCardDeck_prs, true, 0.3f);
        _UsecardSC.gameObject.transform.SetParent(new_cardManager.Card_Pool);

        new_cardManager.Send_request(true, 3, string.Empty, _UsecardSC.info_.uniqueNum, _UsecardSC.gameObject, new_cardManager.Receive_request);
        new_cardManager.WorkCard_list.Remove(_UsecardSC);

        RefreshCardPos(new_cardManager.WorkCard_list);
    }

    private void CardHistory(new_Card _UsecardSC)
    {
        UseCardHistory_Sprite_list.Add(_UsecardSC.card_image.sprite);
        for(int i = 0; i < HistoryBarCard_Image_list.Count - 1; i++)
        {
            HistoryBarCard_Image_list[i].sprite = UseCardHistory_Sprite_list[(UseCardHistory_Sprite_list.Count - 1) - (HistoryBarCard_Image_list.Count - i)];
        }
    }

    public static void RefreshCardPos(List<new_Card> _list)
    {
        List<PRS> prs_list = new_cardManager.RoundCardPosition(new_cardManager.Hand_leftpos, new_cardManager.Hand_rightpos, _list.Count, 0.5f, Vector2.one * 1.3f);
        for (int i = 0; i < _list.Count; i++)
        {
            _list[i].SetTargetImage(new_cardManager.target_Image);
            _list[i]._originPRS = prs_list[i];
            _list[i].MovePosition(prs_list[i], true, 0.7f);
        }
    }

    public List<PRS> RoundCardPosition(Transform _left, Transform _right, int _cardCount, float _hight, Vector3 _scale)
    {
        float[] objLerps = new float[_cardCount];
        List<PRS> result = new List<PRS>(_cardCount);

        switch (_cardCount)
        {
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0.27f, 0.73f }; break;
            case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
            default:
                float interval = 1f / (_cardCount - 1);
                for (int i = 0; i < _cardCount; i++)
                {
                    objLerps[i] = interval * i;
                }
                break;
        }

        for (int i = 0; i < _cardCount; i++)
        {
            var targetPos = Vector3.Lerp(_left.position, _right.position, objLerps[i]);
            var targetRot = Quaternion.identity;
            if (_cardCount > 3)
            {
                float curve = Mathf.Sqrt(Mathf.Pow(_hight, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                targetPos.y += curve;
                targetRot = Quaternion.Slerp(_left.rotation, _right.rotation, objLerps[i]);
            }
            result.Add(new PRS(targetPos, targetRot, _scale));
        }
        return result;
    }

}
