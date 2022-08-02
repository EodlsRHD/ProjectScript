using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JobButtonManager : JobChoice
{
    public CharacterJob characterJob;

    void OnMouseUpAsButton()
    {
        JobChoice.jobChoice.currectCharacter = characterJob;
        GameManager.instance.JOBNAME = characterJob.ToString();
        SceneManager.LoadScene("Map");
    }
}
