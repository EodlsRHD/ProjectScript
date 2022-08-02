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
    //오브젝트 풀
    List<List<GameObject>> ObjectPool; //오브젝트 풀 전체를 가지고있음
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

    public void ObjectStats(GameObject _gameObject) //프리펩을 받아와 전체젝인 ObjectPool에 넣는다.
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
        //프리펩을 인스턴스하여 맵에 미리 로드 시켜놓음
        //그러기 위해서는 리소스매니저에서 로드시켜놓은 프리펩들을 ObjrctInfo에 맞춰서 저장해야한다.
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

    ObjectInfo InsertObject(GameObject _gameObject) //프리펩을 받아와 ObjectInfo의 정보를 받아와준다.
    {
        ObjectInfo info = new ObjectInfo();
        info.ObjectName = _gameObject.gameObject.name;
        info.Prefab = _gameObject;
        info.count = 1;
        
        return info;
    }

    //캐릭터
    //시작할때 선택한 캐릭터를 최초에 오브젝트 풀에 넣고 그다음에 스테이지가 시작할때마다 꺼내어서 사용.

}
