using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class JobManager : MonoBehaviour
{
    public CharacterJob characterJob;

    private string Choicename;
    private ushort Index;
    public float INDEX
    {
        get { return Index; }
        set { Index = (ushort)value; }
    }
    string path;

    void Awake()
    {
        GameManager.instance.JOBINDEX = Index;
        if (GameManager.instance.Clearstage == 1)
        {
            SelectJob();
        }
    }

    void SelectJob() //선택한 직업이 무엇인지? 초기 스텟까지 지정.
    {
        switch (JobChoice.jobChoice.currectCharacter)
        {
            case CharacterJob.Warrior:
                Choicename = "Warrior";
                var tmp1 = DataManager.instance.ReturnCharacterStats(Choicename);
                Jobsubstitution(tmp1);
                break;

            case CharacterJob.Rouge:
                Choicename = "Rouge";
                var tmp2 = DataManager.instance.ReturnCharacterStats(Choicename);
                Jobsubstitution(tmp2);
                break;

            case CharacterJob.Mage:
                Choicename = "Mage";
                var tmp3 = DataManager.instance.ReturnCharacterStats(Choicename);
                Jobsubstitution(tmp3);
                break;

            case CharacterJob.astrologer:
                Choicename = "astrologer";
                var tmp4 = DataManager.instance.ReturnCharacterStats(Choicename);
                Jobsubstitution(tmp4);
                break;

            case CharacterJob.Null:
                break;
        }
        GameManager.instance.JOBNAME = Choicename;
    }

    void Jobsubstitution(PlayerDataInfo? _Jobarr)
    {
        using (FileStream fs = new FileStream(GameManager.instance.PlayerDataPath, FileMode.Append))
        using (StreamWriter sr = new StreamWriter(fs))
        {
            char option = ',';
            char op = '\n';
            sr.Write(GameManager.instance.Clearstage - 1);
            sr.Write(option);
            sr.Write(_Jobarr.Value.job);
            sr.Write(option);
            sr.Write(_Jobarr.Value.health);
            sr.Write(option);
            sr.Write(_Jobarr.Value.phyAttack);
            sr.Write(option);
            sr.Write(_Jobarr.Value.phyDefense);
            sr.Write(option);
            sr.Write(_Jobarr.Value.mageAttack);
            sr.Write(option);
            sr.Write(_Jobarr.Value.mageDefense);
            sr.Write(option);
            sr.Write(_Jobarr.Value.evasion);
            sr.Write(option);
            sr.Write(_Jobarr.Value.energy);
            sr.Write(op);
            sr.Close();
            fs.Close();
        }
        ++Index;
    }
}
