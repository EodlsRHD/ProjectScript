using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class new_Card : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public Card_Info info_;
    public Text card_name;
    public Text card_description;
    public Image card_image;
    public Text card_energyCost;

    public PRS _originPRS;
    protected Vector3 Vectmp;
    protected Quaternion Quatmp;
    protected Vector3 Sectmp;  

    AbCard_Class ac;

    //TestCode
    public string CardName;
    public string CardSkill;

    public void SetData(Card_Info _info, Sprite _sp, ushort _num)
    {
        info_ = _info;
        card_name.text = _info.name;
        card_description.text = _info.description;
        card_energyCost.text = _info.cost.ToString();

        card_image.sprite = _sp;
        info_.uniqueNum = _num;

        //TestCode
        CardName = info_.name;
        CardSkill = info_.skill;
    }

    public void Setclass(Real_cardClass _real)
    {
        ac = _real.Set_TargetImage(info_.skill, this.gameObject);
    }

    public void SetTargetImage(Image _image)
    {
        ac.Set_targetImage(_image);
    }

    public void OnPointerDown(PointerEventData _eventData)
    {
        Vectmp.x = transform.position.x;
        Vectmp.y = transform.position.y + 200f;
        Quatmp = new Quaternion(0, 0, 0, Quatmp.w);
        Sectmp = new Vector3(1.1f, 1.1f, 1.1f);
        PRS PRStmp = new PRS(Vectmp, Quatmp, Sectmp);
        MovePosition(PRStmp, true, 0.3f);
        transform.position = _eventData.position;
    }

    public void OnPointerUp(PointerEventData _eventData)
    {
        ReturnHandPos();
    }

    public void ReturnHandPos()
    {
        MovePosition(_originPRS, true, 0.3f);
    }

    public void MovePosition(PRS prs, bool useDoTween, float dotweenTime)
    {
        if (useDoTween)
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
}
