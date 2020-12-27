using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource bgMusic;
    public bool slowingTime;
    public AudioSource errorSound;
    public bool isPaused;
    public bool IsHacking;
    public static bool GameIsPaused;
    private UIManager uiManager;
    private float oldTimeScale;
    public  string[] PlacesHacked = new string[2];
    public GameObject computer, player,wm200;
    public GameObject wonScreen;
    public GameObject lostScreen;
    public float hackableDistance = 50f;
    public GameObject toolTip;
    public bool won = false;
    public bool lost = false;
    public AudioSource victorySound;
    public AudioSource lostSound;
    public bool finishGame;
    public GameObject[] thingsToDisable;

    public GameObject loadingScreen;

    void Start()
    {
        //RenderSettings.ambientLight = Color.black;
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        computer = GameObject.FindGameObjectWithTag("Computer");
        wonScreen.SetActive(false);
        lostScreen.SetActive(false);
    }

    void Update()
    {
        bgMusic.pitch = Time.timeScale;   
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if(!GameIsPaused && PlacesHacked.Contains("library") && PlacesHacked.Contains("station"))
        {
            won = true;   
        }

        float dist = Vector3.Distance(player.transform.position, computer.transform.position);
        if (dist < hackableDistance)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) && !IsHacking)
            {
                toolTip.SetActive(true);
                wm200.SetActive(true);
                IsHacking = true;
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl) && IsHacking)
            {
                IsHacking = false;
                wm200.SetActive(false);
                toolTip.SetActive(true);
            }
            else if (IsHacking)
            {
                toolTip.SetActive(false);
            }
        }
        else
        {
            toolTip.SetActive(false);
        }
        if (won == true && !finishGame)
        {
            StartCoroutine(Victory());
        }
        if(lost == true && !finishGame)
        {
            StartCoroutine(Lost());
        }
    }
    
    IEnumerator Victory()
    {
        GameIsPaused = true;
        yield return new WaitForSeconds(1f);
        victorySound.Play();
        VictoryScreen();
        finishGame = true;
    }
    void VictoryScreen()
    {
        wonScreen.SetActive(true);
        DisableUnusedUI(thingsToDisable);
        bgMusic.Stop();
        wm200.SetActive(false);
    }
    
    IEnumerator Lost()
    {
        GameIsPaused = true;
        yield return new WaitForSeconds(1f);
        lostSound.Play();
        LostScreen();
        finishGame = true;
    }
    void LostScreen()
    {
       
        lostScreen.SetActive(true);
        DisableUnusedUI(thingsToDisable);
        bgMusic.Stop();
        wm200.SetActive(false);
    }

    public void Pause()
    {
        oldTimeScale = Time.timeScale;
        Time.timeScale = 0;
        uiManager.PauseUiActivate();
        bgMusic.Pause();
        isPaused = true;
        GameIsPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = oldTimeScale;
        uiManager.PauseUiDeActivate();
        bgMusic.Play();
        isPaused = false;
        GameIsPaused = false;
    }

    public void PlayErrorSound()
    {
        StartCoroutine(ErrorSoundPlayer());
    }

    IEnumerator ErrorSoundPlayer()
    {
        bgMusic.Pause();
        errorSound.Play();
        yield return new WaitForSeconds(errorSound.clip.length);
        bgMusic.Play();
        errorSound.Stop();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        loadingScreen.SetActive(true);
    }

    public void DisableUnusedUI(GameObject[] items)
    {
        foreach(var item in items)
        {
            item.SetActive(false);
        }
    }
    
}
