using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class new_MouseTrack : MonoBehaviour, IObserver_Havior
{
    private static new_MouseTrack new_mouseTrack;
    new_BattleManager new_battleManager;

    private new_Card new_card;

    private void Awake()
    {
        new_mouseTrack = this;
        new_battleManager = this.gameObject.GetComponent<new_BattleManager>();
    }

    void Start()
    {
        new_battleManager.RegisterObserver(this);
    }

    public static void SetMouseEventPos(PointerEventData _eventData, GameObject _useCardObj)
    {
        new_mouseTrack.new_card = _useCardObj.GetComponent<new_Card>();
        new_mouseTrack.TrackPointer(_eventData.position);
    }

    public void TrackPointer(Vector3 _vc3)
    {
        Ray ray = Camera.main.ScreenPointToRay(_vc3);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            string Tag = hit.collider.tag;
            if (Tag.Equals("Monster"))
            {
                if (new_EnergyControll.GetMana() >= new_card.info_.cost)
                {
                    hit.collider.gameObject.GetComponent<new_Monster>().SetHit(new_card.info_.damage);
                    new_EnergyControll.SetMana((ushort)(new_EnergyControll.GetMana() - new_card.info_.cost));
                    new_CardManager.Goto_UseCardDeck(new_card);
                }
                else
                {
                    new_card.ReturnHandPos();
                }
            }
        }
    }

    public void Notify(ushort _Damage, GameObject _DefenderObj) // receive
    {

    }

    public void Notify(ushort _Damage, new_Character _DefenderSC)
    {
        //Dummy
    }

    public void Notify(ushort _Damage)
    {
        //Dummy
    }

    public void Notify_Death(ushort _type, GameObject _obj)
    {
        //Dummy
    }
}
