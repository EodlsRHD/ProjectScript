using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum CharacterJob : byte
{
    Warrior,
    Rouge,
    Mage,
    astrologer,
    Null
}

public class JobChoice : MonoBehaviour
{
    public static JobChoice jobChoice;
    public CharacterJob currectCharacter; //���� ĳ���Ͱ� ���õǾ����� ����Ǿ�����.
    void Awake()
    {
        jobChoice = this;
    }
}
