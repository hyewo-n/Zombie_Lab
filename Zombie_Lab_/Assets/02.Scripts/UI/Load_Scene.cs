using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load_Scene : MonoBehaviour
{
    public Slider slider;
    bool IsDone = false;
    float fTime = 0f;

    AsyncOperation async_operation;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLoad("StartUI"));
    }

    // Update is called once per frame
    void Update()
    {
        fTime += Time.deltaTime;
        slider.value = fTime;

        if(fTime >= 4)
        {
            async_operation.allowSceneActivation = true;
        }
    }

    public IEnumerator StartLoad(string strSceneName)
    {
        async_operation = SceneManager.LoadSceneAsync(strSceneName);
        async_operation.allowSceneActivation = false;

        if(IsDone == false)
        {
            IsDone = true;

            while (async_operation.progress < 0.9f)
            {
                slider.value = async_operation.progress;
                yield return true;
            }
        }
        
    }
}

