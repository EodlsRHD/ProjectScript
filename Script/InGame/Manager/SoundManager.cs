using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SoundManager();
            }
            return _instance;
        }
        set
        {
            instance = value;
        }
    }

    void Awake()
    {
        
    }



}
