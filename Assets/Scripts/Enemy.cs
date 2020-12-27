using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float damageAmount = 10f;
    bool destroyed;
    private NavMeshAgent agent;
    public enum EnemyType { Collide,Laser};
    public EnemyType enemyType;
    private GameObject player;

    public Material fadeMaterial;
    public GameObject laser;
    public GameObject muzzle;
    public GameObject gunPart;

    public Vector2 m_Offset;
    private float m_FireTime = 0.0f;

    public float maxShootDistance;
    public GameManager gameManager;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (!gameManager.isPaused && !gameManager.finishGame)
        {
            agent.enabled = true;
            transform.LookAt(player.transform);
            if (enemyType == EnemyType.Collide)
            {
                if (player != null && !destroyed)
                {
                    agent.SetDestination(player.transform.position);
                }
            }
            else if (enemyType == EnemyType.Laser)
            {
                ShootLaser();
            }

            if (health <= 0)
            {
                DestroySelf();
            }
        }
        else
        {
            agent.enabled = false;
        }
    }

    private void ShootLaser()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        
        if (distance < maxShootDistance && Time.time > this.m_FireTime)
        {
            m_FireTime = Time.time + (float)Random.Range(this.m_Offset.x, this.m_Offset.y);
            GameObject.Instantiate(laser, muzzle.transform.position, transform.rotation);
        }
    }

    public void Damage(int amt)
    {
        health -= amt;
    }

    public void StopMovement()
    {
        agent.enabled = false ;
    }

    public void StartMovement()
    {
        agent.enabled = true;
    }

    private void DestroySelf()
    {
        destroyed = true;
        agent.isStopped = true;
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        gunPart.GetComponent<MeshRenderer>().material = fadeMaterial;
        renderer.material = fadeMaterial;
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !destroyed && enemyType == EnemyType.Collide)
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.DamagePlayer(damageAmount);
            DestroySelf();  
        }
    }
}
