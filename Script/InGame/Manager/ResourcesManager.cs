using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public static class ResourcesManager
//���ҽ��� ������ ������ �ϴ� ��ũ��Ʈ
{
    //////////////////////////////////�÷��̾�//////////////////////////////////

    public static Transform PlayerPosition(GameObject _gmaeobject)
    {
        _gmaeobject.transform.localPosition = new Vector3(-34, -48, 90);
        _gmaeobject.transform.localEulerAngles = new Vector3(0, 90, 0);
        _gmaeobject.transform.localScale = new Vector3(10, 10, 10);

        Transform transform = _gmaeobject.transform;
        return transform;
    }

    public static GameObject PlayerPrefabLoad(string _name) // �÷��̾��� ������������� �ҷ��� GameObject SelectPlayer������
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


    ///////////////////////////////////����///////////////////////////////////

    public static GameObject[] FloorOneMonsterPrefabLoad() // 1�� ������ ������
    {
        GameObject[] monster = Resources.LoadAll<GameObject>(@"Prefab\FloorOneMonster");
        return monster;
    }

    public static GameObject[] FloorTwoMonsterPrefabLoad() // 2�� ������ ������
    {
        GameObject[] monster = Resources.LoadAll<GameObject>(@"Prefab\FloorTwoMonster");
        return monster;
    }

    public static GameObject[] FloorThreeMonsterPrefabLoad() // 3�� ������ ������
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


    //////////////////////////////////�׶���//////////////////////////////////

    public static GameObject[] GroundSpawn() //��� ������Ʈ�� ���������� �迭�� �����ϴ� �Լ�
    {
        GameObject[] ground = Resources.LoadAll<GameObject>(@"Prefab\Ground");
        return ground;
    }

    public static Transform GroundPosition(GameObject _ground) //��ġ
    {
        //Player
        _ground.transform.localPosition = new Vector3(0, 0, 0);
        _ground.transform.localEulerAngles = new Vector3(0, 0, 0);
        _ground.transform.localScale = new Vector3(1, 1, 1);

        Transform transform = _ground.transform;
        return transform;
    }


    ////////////////////////////////////ī��////////////////////////////////////

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

    public static Image[] CardBackImageLoad() //ī�� �޸� Image�� �� �ε��ؼ� ����
    {
        Image[] CardArray = Resources.LoadAll<Image>(@"Prefab\CardBack");
        return CardArray;
    }


    ///////////////////////////////////������///////////////////////////////////
    
    public static Sprite ItemImageLoad(string _name) //������ Image�� �� �ε��ؼ� ����
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