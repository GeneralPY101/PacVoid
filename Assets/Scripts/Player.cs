using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]
    private InputActionReference movementControl;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 5f;
    [SerializeField]
    private InputActionReference timeControls; 

    [SerializeField]
    private float life = 100f;

    [SerializeField]
    private int shieldCount = 0;

    [SerializeField]
    private int slowTimerCount = 0;

    [SerializeField]
    private GameObject shield;

    public HealthBar healthBar;

    private bool isShieldActive;

    private bool canMove = true;

    public TimeManager timeManager;

    private UIManager uiManager;

    public bool isSomethingAttached;
    public float distanceLimit;

    public GameObject guideImageAttach;
    public float shieldTimer;
   
    private GameManager gameManager;

    private void OnEnable()
    {
        movementControl.action.Enable();
        timeControls.action.Enable();
    }
    private void OnDisable()
    {
        movementControl.action.Disable();
        timeControls.action.Disable();
    }

    private void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        timeManager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        controller = gameObject.GetComponent<CharacterController>();
        timeControls.action.performed += _ => SlowMotionManager();
        shield.SetActive(false);
        healthBar.SetMaxValue(life);
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uiManager.SetShieldCount(shieldCount);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void SlowMotionManager()
    {
        if (slowTimerCount > 0 && !gameManager.slowingTime)
        {
            slowTimerCount--;
            uiManager.SetTimerCount(slowTimerCount);
            timeManager.DoSlowMotion();
            gameManager.slowingTime = true;
        }
        else
        {
            gameManager.PlayErrorSound();
        }
    }

    void Update()
    {
        if(life <= 0)
        {
            gameManager.lost = true;
        }
        if (!gameManager.isPaused && !gameManager.IsHacking && !gameManager.won && !gameManager.lost)
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            if (canMove)
            {
                Movement();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                if(shieldCount > 0 && !isShieldActive)
                {
                    isShieldActive = true;
                    shieldCount--;
                    uiManager.SetShieldCount(shieldCount);
                    shield.SetActive(true);
                    StartCoroutine(ShieldCountDown());
                }
                else
                {
                    gameManager.PlayErrorSound();
                }
            }
        }
        if (life > 70)
        {
            uiManager.SetDamagedScreenImage(-1);
        }
    }

    IEnumerator ShieldCountDown()
    {
        yield return new WaitForSeconds(shieldTimer);
        shield.SetActive(false);
        isShieldActive = false;
    }

    void Movement()
    {
        Vector2 movement = movementControl.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move.y = 0;
        controller.Move(move * Time.unscaledDeltaTime * playerSpeed);


        playerVelocity.y += gravityValue * Time.unscaledDeltaTime;
        controller.Move(playerVelocity * Time.unscaledDeltaTime);

        if (movement != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.unscaledDeltaTime * rotationSpeed);
        }
    }

    public void DamagePlayer(float amt)
    {
        if(!isShieldActive)
        {
            life -= amt;
            healthBar.SetHealth(life);
        }
        else
        {
            shield.SetActive(false);
        }
        if(life < 25)
        {
            uiManager.SetDamagedScreenImage(2);
        }
        else if(life < 45)
        {
            uiManager.SetDamagedScreenImage(1);
        }
        else if (life < 70)
        {
            uiManager.SetDamagedScreenImage(0);
        }
    }

    public void StopMovement()
    {
        canMove = false;
    }

    public void StartMovement()
    {
        canMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ShieldPowerup")
        {
            shieldCount++;
            Destroy(other.gameObject);
            uiManager.SetShieldCount(shieldCount);
        }
        if(other.tag == "HealthPowerup")
        {
            life = 100f;
            Destroy(other.gameObject);
            healthBar.SetHealth(life);
        }
        if(other.tag == "TimePowerup")
        {
            slowTimerCount++;
            Destroy(other.gameObject);
            uiManager.SetTimerCount(slowTimerCount);
        }
    }
}
