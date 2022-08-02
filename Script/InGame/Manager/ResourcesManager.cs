using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public static class ResourcesManager
//리소스를 가져와 보관만 하는 스크립트
{
    //////////////////////////////////플레이어//////////////////////////////////

    public static Transform PlayerPosition(GameObject _gmaeobject)
    {
        _gmaeobject.transform.localPosition = new Vector3(-34, -48, 90);
        _gmaeobject.transform.localEulerAngles = new Vector3(0, 90, 0);
        _gmaeobject.transform.localScale = new Vector3(10, 10, 10);

        Transform transform = _gmaeobject.transform;
        return transform;
    }

    public static GameObject PlayerPrefabLoad(string _name) // 플레이어의 직업프리펩들을 불러와 GameObject SelectPlayer에저장
    {
        GameObject tmp = null;
        GameObject[] player = Resources.LoadAll<GameObject>(@"Prefab\PlayerCharacter\");
        for(int i = 0; i < player.Length; i++)
        {
            if(player[i].gameObject.name.Equals(_name))
            {
                tmp = player[i];
                return tmp;
            }
        }
        return tmp;
    }


    ///////////////////////////////////몬스터///////////////////////////////////

    public static GameObject[] FloorOneMonsterPrefabLoad() // 1층 몬스터의 프리펩
    {
        GameObject[] monster = Resources.LoadAll<GameObject>(@"Prefab\FloorOneMonster");
        return monster;
    }

    public static GameObject[] FloorTwoMonsterPrefabLoad() // 2층 몬스터의 프리펩
    {
        GameObject[] monster = Resources.LoadAll<GameObject>(@"Prefab\FloorTwoMonster");
        return monster;
    }

    public static GameObject[] FloorThreeMonsterPrefabLoad() // 3층 몬스터의 프리펩
    {
        GameObject[] monster = Resources.LoadAll<GameObject>(@"Prefab\FloorThreeMonster");
        return monster;
    }

    public static Transform OneMonsterPosition(GameObject _gmaeobject)
    {
        _gmaeobject.transform.localPosition = new Vector3(33, -48, 102);
        _gmaeobject.transform.localEulerAngles = new Vector3(0, -105, 0);
        _gmaeobject.transform.localScale = new Vector3(_gmaeobject.transform.localScale.x, _gmaeobject.transform.localScale.y, _gmaeobject.transform.localScale.z);

        Transform transform = _gmaeobject.transform;
        return transform;
    }

    public static Transform TwoMonsterPosition(GameObject _gmaeobject)
    {
        _gmaeobject.transform.localPosition = new Vector3(52, -48, 84);
        _gmaeobject.transform.localEulerAngles = new Vector3(0, -105, 0);
        _gmaeobject.transform.localScale = new Vector3(_gmaeobject.transform.localScale.x, _gmaeobject.transform.localScale.y, _gmaeobject.transform.localScale.z);

        Transform transform = _gmaeobject.transform;
        return transform;

    }

    public static Transform ThreeMonsterPosition(GameObject _gmaeobject)
    {
        _gmaeobject.transform.localPosition = new Vector3(0, -48, 0);
        _gmaeobject.transform.localEulerAngles = new Vector3(0, -105, 0);
        _gmaeobject.transform.localScale = new Vector3(5, 5, 5);

        Transform transform = _gmaeobject.transform;
        return transform;
    }

    public static Transform FourMonsterPosition(GameObject _gmaeobject)
    {
        _gmaeobject.transform.localPosition = new Vector3(0, 0, 0);
        _gmaeobject.transform.localEulerAngles = new Vector3(0, -105, 0);
        _gmaeobject.transform.localScale = new Vector3(5, 5, 5);

        Transform transform = _gmaeobject.transform;
        return transform;
    }


    //////////////////////////////////그라운드//////////////////////////////////

    public static GameObject[] GroundSpawn() //배경 오브젝트를 프리펩으로 배열에 저장하는 함수
    {
        GameObject[] ground = Resources.LoadAll<GameObject>(@"Prefab\Ground");
        return ground;
    }

    public static Transform GroundPosition(GameObject _ground) //위치
    {
        //Player
        _ground.transform.localPosition = new Vector3(0, 0, 0);
        _ground.transform.localEulerAngles = new Vector3(0, 0, 0);
        _ground.transform.localScale = new Vector3(1, 1, 1);

        Transform transform = _ground.transform;
        return transform;
    }


    ////////////////////////////////////카드////////////////////////////////////

    public static Sprite CardImageLoad(string name)
    {
        Sprite[] asd = Resources.LoadAll<Sprite>(@"Prefab\CardForward");
        for (int i = 0; i < asd.Length; i++)
        {
            if(name.Equals(asd[i].name))
            {
                return asd[i];
            }
        }
        return null;
    }

    public static GameObject CardForwardPrefabLoad()
    {
        GameObject card = Resources.Load<GameObject>(@"Prefab\CardForwardPrefab\CardPrefab");
        return card;
    }

    public static Image[] CardBackImageLoad() //카드 뒷면 Image을 다 로드해서 저장
    {
        Image[] CardArray = Resources.LoadAll<Image>(@"Prefab\CardBack");
        return CardArray;
    }


    ///////////////////////////////////아이템///////////////////////////////////
    
    public static Sprite ItemImageLoad(string _name) //아이템 Image을 다 로드해서 저장
    {
        Sprite tmp = null;
        Sprite[] ImageArray = Resources.LoadAll<Sprite>(@"Prefab\Item");
        for(int i = 0; i < ImageArray.Length; i++)
        {
            if(ImageArray[i].name.Equals(_name))
            {
                tmp = ImageArray[i];
                return tmp;
            }
        }
        return tmp;
    }
}