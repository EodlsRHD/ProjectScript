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

    //������ ǥ��
    // 1. ĳ������ �������� ������ �������� ���� �˾ƿ´�.
    // 2. ���ݽ� �ش� ũ������ ��ġ�� �׸�ŭ�� �������ؽ�Ʈ�� ����.
    // 3. �������ؽ�Ʈ�� ���� ������ �̵��ϸ鼭 �������. => ���� ���� : ���İ�����(0.1�ʿ� 0.1�� ��������),  �̵� : Y�� ����
    // 4. ������ �ؽ�Ʈ�� ������� false�ϰ� string.Empty�� �ʱ�ȭ

    void Update()
    {

    }
}
