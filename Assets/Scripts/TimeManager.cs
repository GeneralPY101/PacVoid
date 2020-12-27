using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    public float slowDownFactor = .05f;
    public float slowDownTime = 2f;
    public GameManager gameManager;


    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent < GameManager > ();
    }

    public void DoSlowMotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        StartCoroutine(ResumeTime());
    }

    private void Update()
    {
        Time.timeScale += (1f / slowDownTime) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    IEnumerator ResumeTime()
    {
        yield return new WaitForSeconds(slowDownTime);
        gameManager.slowingTime = false;
    }
}
