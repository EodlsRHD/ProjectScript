using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public Text OriginalDamageText;
    public Transform Patrent;

    private void Awake()
    {
        
    }

    //데미지 표시
    // 1. 캐릭터의 데미지와 몬스터의 데미지를 각각 알아온다.
    // 2. 공격시 해당 크리쳐의 위치에 그만큼의 데미지텍스트를 띄운다.
    // 3. 데미지텍스트는 위로 서서히 이동하면서 사라진다. => 투명도 조절 : 알파값조절(0.1초에 0.1씩 옅어지게),  이동 : Y값 조절
    // 4. 데미지 텍스트가 사라지면 false하고 string.Empty로 초기화

    void Update()
    {

    }
}
