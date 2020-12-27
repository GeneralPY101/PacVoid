using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour
{
    public Vector3 m_target;
    public GameObject collisionExplosion;
    public float speed;
    public int hits = 3;
    public AudioClip explosionClip;
    public enum LaserType { EnemyLaser,PlayerLaser};
    public LaserType laserType;
    public int damageAmount;

    private GameManager gameManager;

    Rigidbody rb;
    Vector3 oldVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = speed * transform.forward * Time.timeScale;
        oldVelocity = rb.velocity;
        if(gameManager.isPaused)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    public void setTarget(Vector3 target)
    {
        m_target = target;
    }

    void explode()
    {

        if (collisionExplosion != null)
        {
            AudioSource.PlayClipAtPoint(explosionClip, Camera.main.transform.position, .1f);
            GameObject explosion = (GameObject)Instantiate(
                collisionExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(explosion, 2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && laserType == LaserType.PlayerLaser)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.Damage(damageAmount);
        }
        else if (other.tag=="Player" && laserType == LaserType.EnemyLaser)
        {
            Player player = other.GetComponent<Player>();
            player.DamagePlayer(damageAmount);
        }
        explode();
    }

    private void OnCollisionEnter(Collision collision)
    {
        hits--;
        ContactPoint point = collision.contacts[0];
        Vector3 reflectedVelocity = Vector3.Reflect(oldVelocity, point.normal);
        rb.velocity = reflectedVelocity;
        Quaternion rotation = Quaternion.FromToRotation(oldVelocity, reflectedVelocity);
        transform.rotation = rotation * transform.rotation;
        if (hits < 0)
        {
            explode();
        }
    }

}