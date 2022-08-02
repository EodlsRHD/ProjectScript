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
    public CharacterJob currectCharacter; //무슨 캐릭터가 선택되었는지 저장되어있음.
    void Awake()
    {
        jobChoice = this;
    }
}
