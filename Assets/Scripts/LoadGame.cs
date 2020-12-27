using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public Slider slider;
    public int levelIndex;
    private void Start()
    {
        StartCoroutine(LoadingGame());
    }
    IEnumerator LoadingGame()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(levelIndex);
        while (loading.progress < 1)
        {
            slider.value = loading.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
