using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoadingManagement : MonoBehaviour
{
    public Text progressText;
    public Image image;
    public Image startImage;
    public bool isStart = false;
    bool isLoadStart = false;
    float transparebtTimer = 0;
    float startTransparebtTimer = 1;
    static string loadSceneName;
    const int loadSceneMeneID = 2;
    [Header("本对象是加载场景菜单对象:")]
    public bool isLoadSceneMeneObj;
    public static SceneLoadingManagement single;
    private void Start()
    {
        single = this;

        isLoadStart = true;
        if (startImage != null)
        {
            startImage.gameObject.SetActive(true);
        }

        //启用协程:
        if (isLoadSceneMeneObj)
        {
            StartCoroutine(LoadingLevel());
        }
    }
    //按钮调用:
    public void LoadingSceneByName(string sceneName)
    {
        if (sceneName.Equals("") != true && sceneName != null)
        {
            loadSceneName = sceneName;
        }
        else
        {
            loadSceneName = "Main Menu";
        }
        isStart = true;
        Invoke("GoToLoadSceneMene", 5);
    }
    private void Update()
    {
        if (isStart)
        {
            Color newColor = image.color;
            transparebtTimer += Time.deltaTime;
            newColor.a = transparebtTimer / 3;
            image.color = newColor;
        }

        if (isLoadStart && startImage!=null)
        {
            Color newColor = startImage.color;
            startTransparebtTimer -= Time.deltaTime;
            newColor.a = startTransparebtTimer / 1;
            startImage.color = newColor;
            if (startTransparebtTimer > 1)
            {
                startTransparebtTimer = 1;
                isLoadStart = false;
            }
        }
    }
    float waitingTime = 3;
    //加载水平:
    private IEnumerator LoadingLevel()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(loadSceneName);

        operation.allowSceneActivation = false;
        string[] posArray = { ".", "..", "..." };
        while(operation.isDone == false)
        {
            int loadingProgress = Mathf.RoundToInt(operation.progress * 100);
            int posIndex = (loadingProgress / 5) % 3;
            progressText.text =loadingProgress.ToString("d2")+ "% " + "Loading" + posArray[posIndex];

            if(operation.progress >= 0.9f)
            {
                isStart = true;
                loadingProgress =Mathf.RoundToInt(90 + 10 * (3 - waitingTime) / 3);
                progressText.text = loadingProgress.ToString("d2") + "% " + "Loading" + posArray[posIndex];
                waitingTime -= Time.deltaTime;
                if (waitingTime < 0)
                {
                    Debug.Log("3秒已过,调用,进入场景的名称:" + loadSceneName);
                    waitingTime = 3;
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

    private void GoToLoadSceneMene()
    {
        SceneManager.LoadScene(loadSceneMeneID);
    }
}
