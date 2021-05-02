using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{

    [Header("Enemy configuration")]
    [Tooltip("Unique ID to identify this object. Used for tracking quest progress.")]
    [SerializeField] int uniqueID;
    [Tooltip("Initial value of life points.")]
    [SerializeField] int maxLife;
    [Tooltip("Damage caused to player.")]
    [SerializeField] int damage;
    [Tooltip("How far can the enemy attack the player.")]
    [SerializeField] float attackRange;
    [Tooltip("Waiting time (in seconds) between walks.")]
    [SerializeField] float delayBetweenMoves;

    [Header("Objects related to the enemy")]
    [Tooltip("List of points that the enemy can walk to.")]
    [SerializeField] Transform [] walkPoint;
    [Tooltip("The Animator that controls enemy animation.")]
    [SerializeField] Animator anim;
    [Tooltip("Particle that is instantiated when enemy gets hit.")]
    [SerializeField] GameObject hitParticle;
    [Tooltip("Particle that is instantiated when enemy dies.")]
    [SerializeField] GameObject deathParticle;
    [Tooltip("The health bar of the enemy.")]
    [SerializeField] HealthBarController healthBar;
    [Tooltip("The sound the enemy makes when gets hit.")]
    [SerializeField] AudioClip hit;
    [Tooltip("Object that has the mesh to blink.")]
    [SerializeField] GameObject body;
    [Tooltip("Collider of the attack of the enemy")]
    [SerializeField] BoxCollider hitCollider;

    AudioSource audioSource;

    int life, nestID;

    NavMeshAgent agent;
    Rigidbody rb;

    bool walking, invulnerable, chasePlayer, attacking, dead;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();

        life = maxLife;
        healthBar.SetMaxHealth(maxLife);

        StartCoroutine(BlinkInvulnerable());

        agent.updatePosition = false;
        agent.updateRotation = false;
        agent.isStopped = true;
        rb.useGravity = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("vel", agent.velocity.magnitude);

        if (!dead)
        {
            if (!chasePlayer)
            {
                //Enemy walks randomly
                if (!walking && life > 0)
                {
                    agent.isStopped = false;
                    StartCoroutine(WalkToRandomPointAfterDelay(delayBetweenMoves));
                }
            }
            else
            {
                //Chase Player
                ChasePlayer();
            }
        }
    }

    private void ChasePlayer()
    {
        if (!attacking)
        {
            agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);

            Debug.DrawRay(transform.position, transform.forward * attackRange);
            RaycastHit hit;
            Ray attackRay = new Ray(transform.position, transform.forward);

            if (Physics.SphereCast(attackRay, .5f, out hit, attackRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    transform.LookAt(hit.transform);

                    agent.updatePosition = false;
                    agent.updateRotation = false;
                    agent.isStopped = true;
                    anim.SetTrigger("attack");
                    attacking = true;
                }
            }
        }
    }

    public void SetChasePlayer(bool chase)
    {
        chasePlayer = chase;
    }

    public void StartHitting()
    {
        hitCollider.enabled = true;
    }

    public void StopHitting()
    {
        hitCollider.enabled = false;
    }

    public void SetNotAttacking()
    {
        agent.updatePosition = true;
        agent.updateRotation = true;
        agent.isStopped = false;
        attacking = false;
    }
        
    public void DamageMe(int damage)
    {
        if (!invulnerable && life > 0)
        {
            StopHitting();
            SetNotAttacking();

            life -= damage;
            healthBar.SetHealth(life);

            if (audioSource.isPlaying)
                audioSource.Stop();
            audioSource.PlayOneShot(hit);

            HitStop.inst.Stop(0.2f);
            anim.SetTrigger("damage");
            hitParticle.SetActive(true);

            CheckDeath();
        }
        
    }

    public void SetNestID(int newNestID)
    {
        nestID = newNestID;
    }

    public int GetNestID()
    {
        return nestID;
    }

    private void CheckDeath()
    {
        if (life <= 0)
        {
            dead = true;

            agent.isStopped = true;

            GetComponent<BoxCollider>().enabled = false;

            anim.SetTrigger("death");

            StartCoroutine(DestroyAfterDelay());
        }
    }

    public void DestroyMe()
    {
        QuestManager.inst.UpdateQuestProgress(uniqueID);
        Destroy(transform.parent.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!invulnerable && life > 0)
            {
                other.gameObject.GetComponentInParent<PlayerBehaviour>().DamagePlayer(damage);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            agent.updatePosition = true;
            agent.updateRotation = true;
            agent.isStopped = false;
            rb.useGravity = false;
        }
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSecondsRealtime(2f);

        Instantiate(deathParticle, transform.position, Quaternion.identity);

        NestManager.inst.RegisterKill(nestID);

        DestroyMe();
    }

    IEnumerator WalkToRandomPointAfterDelay(float delay)
    {
        walking = true;

        int posNumber = Random.Range(0, walkPoint.Length - 1);

        agent.SetDestination(walkPoint[posNumber].position);

        yield return new WaitForSecondsRealtime(delay);
        walking = false;

    }

    IEnumerator BlinkInvulnerable()
    {
        invulnerable = true;

        for (int i = 0; i < 8; i++)
        {
            body.SetActive(false);
            yield return new WaitForSecondsRealtime(0.1f);

            body.SetActive(true);
            yield return new WaitForSecondsRealtime(0.1f);
        }

        invulnerable = false;
    }


}
