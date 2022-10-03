using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class new_Loading : MonoBehaviour
{
    static string NextScene;

    [SerializeField]
    private Image FildBar;

    public static void LoadScene(string _scenename)
    {
        NextScene = _scenename;
        SceneManager.LoadScene("Loading");
    }

    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation ao =  SceneManager.LoadSceneAsync(NextScene);
        ao.allowSceneActivation = false;

        float timer = 0f;
        while(!ao.isDone)
        {
            yield return null;

            if(ao.progress < 0.9f)
            {
                FildBar.fillAmount = ao.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                FildBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if(FildBar.fillAmount >= 1f)
                {
                    ao.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
