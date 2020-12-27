using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayGun : MonoBehaviour
{
    public float shootRate;
    private float m_shootRateTimeStamp;

    [SerializeField]
    private InputActionReference shoot;
    [SerializeField]
    private InputActionReference mouse;

    public GameObject m_shotPrefab;

    RaycastHit hit;
    [SerializeField]
    float range = 1000.0f;

    [SerializeField]
    private AudioClip shootSound;

    private AudioSource shootingSound;

    private GameManager gameManager;


    private void OnEnable()
    {
        shoot.action.Enable();
        mouse.action.Enable();
    }

    private void OnDisable()
    {
        shoot.action.Disable();
        mouse.action.Disable();
    }
    private void Start()
    {
        shootingSound = GameObject.Find("ShootingSound").GetComponent<AudioSource>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    private void Update()
    {
        shootingSound.pitch = Time.timeScale;

        if (shoot.action.triggered && !gameManager.IsHacking && !gameManager.isPaused && !gameManager.finishGame)
        {
            if (Time.time > m_shootRateTimeStamp)
            {
                shootingSound.clip = shootSound;
                shootingSound.Play();
;                shootRay();
                m_shootRateTimeStamp = Time.time + shootRate;
            }
        }
    }


    void shootRay()
    {
        GameObject laser = GameObject.Instantiate(m_shotPrefab, transform.position, transform.rotation) as GameObject;
    }



}