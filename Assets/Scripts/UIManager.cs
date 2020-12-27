using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text slowTimerText;
    private TimeManager timeManager;
    public Text shieldCount;
    public GameObject pauseUI;
    public Image damagedScreen;

    public Sprite[] damagedScreenImages;
    Color imgShowColor = new Color(1f,1f,1f, 0.15f);
    Color imgDisabledColor = new Color(1f,1f,1f,0f);


    private void Start()
    {
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        pauseUI.SetActive(false);
        damagedScreen.sprite = null;
    }

    private void Update()
    {
        if(damagedScreen.sprite == null)
        {
            damagedScreen.color = imgDisabledColor;
        }
        else
        {
            damagedScreen.color = imgShowColor;
        }
    }

    public void SetShieldCount(int cnt)
    {
        shieldCount.text = "x" + cnt.ToString();
    }

    public void SetTimerCount(int cnt)
    {
        slowTimerText.text = "x" + cnt.ToString();
    }

    public void PauseUiActivate()
    {
        pauseUI.SetActive(true);
    }
    public void PauseUiDeActivate()
    {
        pauseUI.SetActive(false);
    }

    public void SetDamagedScreenImage(int cnt)
    {
        if(cnt < 0)
        {
            damagedScreen.sprite = null;
            return;
        }
        damagedScreen.sprite = damagedScreenImages[cnt];
    }
}
