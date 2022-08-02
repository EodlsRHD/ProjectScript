using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : SingleTon<CardManager>
{
    private Text EnergyFront;
    public Text ENERGYFRONT
    {
        get { return EnergyFront; }
        set { EnergyFront = value; }
    }
    private Text EnergyBack;
    public Text ENERGYBACK
    {
        get { return EnergyBack; }
        set { EnergyBack = value; }
    }
    private float Efront;
    public float EFRONT
    {
        get { return Efront; }
        set
        {
            Efront = value;
            ENERGYFRONT.text = value.ToString();
        }
    }
    private float Eback;
    public float EBACK
    {
        get { return Eback; }
        set
        {
            Eback = value;
            ENERGYBACK.text = value.ToString();
        }
    }

    public void FirstCardDrow() // 게임 최초 시작시 카드 5장을 카드 리스트에서 무작위로 뽑아온다.
    {
        for (int i = 0; i < GameManager.instance.ForwardCarddataList.Count; i++) //카드섞기
        {
            int ran = Random.Range(0, GameManager.instance.ForwardCarddataList.Count);
            CarddataInfo tmp = GameManager.instance.ForwardCarddataList[i];
            GameManager.instance.ForwardCarddataList[i] = GameManager.instance.ForwardCarddataList[ran];
            GameManager.instance.ForwardCarddataList[ran] = tmp;
        }

        for (int i = 0; i < GameManager.instance.DROWCARD; i++)
        {
            var tmp1 = GameManager.instance.ForwardCarddataList[i];
            GameManager.instance.HandCarddataList.Add(tmp1);
            GameManager.instance.ForwardCarddataList.Remove(tmp1);
        }
    }

    public void CardDrow(List<CarddataInfo> _back, List<CarddataInfo> _foward, List<CarddataInfo> _hand, List<GameObject> _inCard, List<GameObject> _handCard, RectTransform _spawnposition) //그 후
    {
        if (_foward.Count <= 5) //뽑을 카드가 5장 보다 작다면
        {
            for (int i = 0; i < _back.Count; i++)
            {
                _foward.Add(_back[i]);
            }
            for (int i = _back.Count - 1; i >= 0; i--)
            {
                _back.Remove(_back[i]);
            }
            VoitCard(_back, _foward, _hand, _inCard, _handCard, _spawnposition);
        }
        else if(_foward.Count > 5)//뽑을 카드가 5장보다 많다면
        {
            VoitCard(_back, _foward, _hand, _inCard, _handCard, _spawnposition);
        }
    }

    void VoitCard(List<CarddataInfo> _back, List<CarddataInfo> _foward, List<CarddataInfo> _hand, List<GameObject> _inCard, List<GameObject> _handCard, RectTransform _spawnposition)
    {
        for (int i = 0; i < _foward.Count; i++) //카드섞기
        {
            int ran = Random.Range(0, _foward.Count);
            CarddataInfo tmp = _foward[i];
            _foward[i] = _foward[ran];
            _foward[ran] = tmp;
        }
        for (int i = 0; i < GameManager.instance.DROWCARD; i++)
        {
            _hand.Add(_foward[i]);
            for (int a = 0; a < _inCard.Count; a++)
            {
                if (_foward[i].index.Equals(_inCard[a].GetComponent<Card>().datainfo.index))
                {
                    _handCard.Add(_inCard[a]);
                    _inCard[a].transform.position = _spawnposition.position;
                    _inCard[a].transform.SetParent(_spawnposition);
                }
            }
        }
        for (int i = 0; i < _hand.Count; i++)
        {
            for (int x = 0; x < _foward.Count; x++)
            {
                if (_hand[i].index.Equals(_foward[x].index))
                {
                    _foward.Remove(_foward[x]);
                }
            }
        }
        for (int i = 0; i < _handCard.Count; i++)
        {
            for (int x = 0; x < _inCard.Count; x++)
            {
                if (_handCard[i].GetComponent<Card>().datainfo.index.Equals(_inCard[x].GetComponent<Card>().datainfo.index))
                {
                    _inCard.Remove(_inCard[x]);
                }
            }
        }
    }

    public void CardSet(List<CarddataInfo> _hand, List<GameObject> _handCard, Transform _HandPositionLeft, Transform _HandPositionRight, Transform _parant)
    {
        List<PRS> list = new List<PRS>();
        list = RoundCardPosition(_HandPositionLeft, _HandPositionRight, _handCard.Count, 0.5f, Vector3.one * 1.3f);
        for (int i = 0; i < _handCard.Count; i++)
        {
            var one = _handCard[i].GetComponent<Card>();
            one.originPRS = list[i];
            _handCard[i].transform.SetParent(_parant);
            one.MovePosition(list[i], true, 1f);
            _handCard[i].SetActive(true);
        }
    }

    public List<PRS> RoundCardPosition(Transform _left, Transform _right, int _cardCount, float _hight, Vector3 _scale) // _hight : 반지름
    {
        //좌우를 받아서 다른 게임들처럼 펼쳐지게 만들기
        //"원의 방정식"
        //카드의 수만큼 원의 방정식을 적용시켜 포지션을 만든후 카드를 배치

        float[] objLerps = new float[_cardCount]; //포지션을 저장할 위치
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
                // Sqrt : 루트,    Pow(a,b) : a를 b만큼 곱하는것 = a의 b제곱
                targetPos.y += curve;
                targetRot = Quaternion.Slerp(_left.rotation, _right.rotation, objLerps[i]);
            }
            result.Add(new PRS(targetPos, targetRot, _scale));
        }
        return result;
    }

    public void RemoveCard(Transform _HandPositionLeft, Transform _HandPositionRight) //인덱스로 받은 카드를 HandCardList에서 지우고 CardInstantiate다시 실행
    {
        if (!GameManager.instance.Livemonsterlist.Count.Equals(0))
        {
            if (GameManager.instance.USECARD != true) { return; }
            else
            {
                for (int i = 0; i < GameManager.instance.HandcardCardList.Count; i++)
                {
                    if (GameManager.instance.HandcardCardList[i].GetComponent<Card>().datainfo.index.Equals(GameManager.instance.USECARDINDEX))
                    {
                        var tmp1 = GameManager.instance.HandcardCardList[i];
                        GameManager.instance.IncardCardList.Add(tmp1);
                        GameManager.instance.HandcardCardList.Remove(tmp1);
                        GameManager.instance.USECARDINDEX = -1;

                        var tmp2 = GameManager.instance.HandCarddataList[i];
                        GameManager.instance.BackCarddataList.Add(tmp2);
                        GameManager.instance.HandCarddataList.Remove(tmp2);
                    }
                }
                GameManager.instance.USECARD = false;
            }
        }
        else
        {
            Debug.Log(GameManager.instance.Livemonsterlist.Count);
        }
    }

    public void ReturnDamage(Monster _targetmonster, float _carddamage) //공격한 몬스터와 사용한 카드를 받아와서 몬스터의 체력을 깍는다.
    {
        if (_targetmonster.monsterDataInfo.size.Equals("small"))
        {
            GetDamage(_targetmonster, _carddamage, 0.8f);
        }
        if (_targetmonster.monsterDataInfo.size.Equals("medium"))
        {
            GetDamage(_targetmonster, _carddamage, 1f);
        }
        if (_targetmonster.monsterDataInfo.size.Equals("large"))
        {
            GetDamage(_targetmonster, _carddamage, 1.2f);
        }
    }

    public void ReturnUseItemDamage(Monster _targetmonster, float _carddamage, List<ItemdataInfo> _list)
    {
        if (_targetmonster.monsterDataInfo.size.Equals("small"))
        {
            GetDamage(_targetmonster, _carddamage, _list, 0.8f);
        }
        if (_targetmonster.monsterDataInfo.size.Equals("medium"))
        {
            GetDamage(_targetmonster, _carddamage, _list, 1f);
        }
        if (_targetmonster.monsterDataInfo.size.Equals("large"))
        {
            GetDamage(_targetmonster, _carddamage, _list, 1.2f);
        }
    }

    void GetDamage(Monster _targetmonster, float _carddamage, float _num)
    {
        float DAMAGE = BattleManager.Battle(_carddamage, GameManager.instance.character.characterDataInfo.phyAttack);
        float FinalDamage = DAMAGE * _num;
        GameManager.instance.CharAni_Attack = true;
        GameManager.instance.MonAni_GetHit = true;
        GameManager.instance.MESSAGE = "MonsterSize   : " + _targetmonster.monsterDataInfo.size + ",   DAMAGE   : " + DAMAGE + ",   FinalDamage   : " + FinalDamage;
        _targetmonster.monsterDataInfo.health -= FinalDamage;
        GameManager.instance.CharacterDamageNum = FinalDamage;
    }

    void GetDamage(Monster _targetmonster, float _carddamage, List<ItemdataInfo> _list, float _num)
    {
        float DAMAGE = BattleManager.Battle(_carddamage, GameManager.instance.character.characterDataInfo.phyAttack, _list);
        float FinalDamage = DAMAGE * _num;
        GameManager.instance.CharAni_Attack = true;
        GameManager.instance.MonAni_GetHit = true;
        GameManager.instance.MESSAGE = "MonsterSize   : " + _targetmonster.monsterDataInfo.size + ",   DAMAGE   : " + DAMAGE + ",   FinalDamage   : " + FinalDamage;
        _targetmonster.monsterDataInfo.health -= FinalDamage;
        GameManager.instance.CharacterDamageNum = FinalDamage;
    }
}
