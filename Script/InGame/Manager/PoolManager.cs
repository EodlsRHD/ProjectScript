using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo
{
    public string ObjectName;
    public GameObject Prefab;
    public int count;
}

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    ObjectInfo[] ObjectInfos;
    //������Ʈ Ǯ
    List<List<GameObject>> ObjectPool; //������Ʈ Ǯ ��ü�� ����������
    public List<GameObject> CharacterPool;
    public List<GameObject> MonsterPool;
    public List<GameObject> UIHpPool;

    public Transform ObjectPoolposition;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        ObjectPool = new List<List<GameObject>>();

        DontDestroyOnLoad(ObjectPoolposition.gameObject);
    }

    public void ObjectStats(GameObject _gameObject) //�������� �޾ƿ� ��ü���� ObjectPool�� �ִ´�.
    {
        if(ObjectInfos != null)
        {
            for(int i = 0; i < ObjectInfos.Length; i++)
            {
                ObjectPool.Add(InsertList(InsertObject(_gameObject)));
            }
        }
    }

    List<GameObject> InsertList(ObjectInfo _objectInfo)
    {
        //�������� �ν��Ͻ��Ͽ� �ʿ� �̸� �ε� ���ѳ���
        //�׷��� ���ؼ��� ���ҽ��Ŵ������� �ε���ѳ��� ��������� ObjrctInfo�� ���缭 �����ؾ��Ѵ�.
        for(int i = 0; i< _objectInfo.count; i++)
        {
            GameObject tmp = Instantiate(_objectInfo.Prefab);
            tmp.transform.SetParent(ObjectPoolposition);
            tmp.SetActive(false);
            if (tmp.gameObject.tag.Equals("Character"))
            {
                CharacterPool.Add(tmp);
                return CharacterPool;
            }
            else if (tmp.gameObject.tag.Equals("Monster"))
            {
                MonsterPool.Add(tmp);
                return MonsterPool;
            }
        }
        return null;
    }

    ObjectInfo InsertObject(GameObject _gameObject) //�������� �޾ƿ� ObjectInfo�� ������ �޾ƿ��ش�.
    {
        ObjectInfo info = new ObjectInfo();
        info.ObjectName = _gameObject.gameObject.name;
        info.Prefab = _gameObject;
        info.count = 1;
        
        return info;
    }

    //ĳ����
    //�����Ҷ� ������ ĳ���͸� ���ʿ� ������Ʈ Ǯ�� �ְ� �״����� ���������� �����Ҷ����� ����� ���.

}
