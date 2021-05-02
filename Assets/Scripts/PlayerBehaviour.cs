using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour inst;

    [Header("Player Configuration")]
    [Tooltip("Life the player starts with")]
    [SerializeField] int initialMaxLife;
    [Tooltip("Top Speed of Player walk")]
    [SerializeField] float maxSpeed;
    [Tooltip("Speed of the rotation of player when moving")]
    [SerializeField] float desiredRotationSpeed;

    [Header("Objects related to the Player")]
    [Tooltip("Player Animator")]
    [SerializeField] Animator charAnim;
    [Tooltip("Sword Hit Collider, where it hits the enemies")]
    [SerializeField] CapsuleCollider swordHit;
    [Tooltip("Splash effect object, used when player gets hit")]
    [SerializeField] GameObject hitSplash;
    [Tooltip("Sword Trail effect")]
    [SerializeField] TrailRenderer swordTrail;
    [Tooltip("Slash Sound")]
    [SerializeField] AudioClip slash;
    [Tooltip("Hit Sound")]
    [SerializeField] AudioClip hit;
    [Tooltip("Health Bar script")]
    [SerializeField] HealthBarController healthBar;
    [Tooltip("Player Mesh. Used for blinking")]
    [SerializeField] GameObject body;

    AudioSource audioSource;
       
    Vector3 moveVector;

    bool isGrounded, isAttacking, isDamaged, isCloseToNPC, invulnerable, isTalking;

    int weaponDamage, maxLife, life;

    float horizontal, vertical, vel, verticalVel;

    CharacterController charController;

    Vector3 startPoint = Vector3.zero;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if (startPoint != Vector3.zero)
        {
            SetPosition();
        }
        
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {

        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);        

        maxLife = initialMaxLife;
        life = maxLife;

        healthBar.SetMaxHealth(maxLife);
        healthBar.SetHealth(life);

        GameController.inst.SetPlayerHP(life, maxLife);

        vel = maxSpeed;

        charController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();

        swordHit.enabled = false;
    }

    void FixedUpdate()
    {
        if (!isTalking && !isAttacking && !isDamaged && life > 0)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(horizontal, 0, vertical);

            //Vector3 movement = transform.TransformDirection(direction) * vel;
            //if (direction != Vector3.zero)
            //    transform.rotation = Quaternion.LookRotation(direction);

            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), desiredRotationSpeed);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTalking && !isAttacking && !isDamaged && life > 0)
        {
            horizontal = Input.GetAxis("Horizontal");

            vertical = Input.GetAxis("Vertical");

            if (!isCloseToNPC)
                CheckAttack();

            MovePlayer();

            AnimatePlayer();
        }

        CheckGrounded();
    }

    private void CheckAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isAttacking = true;
            charAnim.SetTrigger("attack");
        }
    }

    private void AnimatePlayer()
    {
        if (Mathf.Abs(horizontal) > Mathf.Epsilon || Mathf.Abs(vertical) > Mathf.Epsilon)
        {
            charAnim.SetBool("walk", true);
        }
        else
        {
            charAnim.SetBool("walk", false);
        }
    }

    private void CheckGrounded()
    {
        isGrounded = charController.isGrounded;
        if (isGrounded)
        {
            verticalVel -= 0;
        }
        else
        {
            verticalVel -= 1;
        }
        moveVector = new Vector3(0, verticalVel * .2f * Time.deltaTime, 0);
        charController.Move(moveVector);
    }

    void MovePlayer()
    {
        Vector3 direction;

        direction = new Vector3(horizontal, 0, vertical);

        charController.Move(direction * vel * Time.deltaTime);

    }

    public void SetPosition() 
    {
        
        charController.enabled = false;
        transform.position = startPoint;
        charController.enabled = true;
    }

    public void SetNotAttacking()
    {
        isDamaged = false;
        isAttacking = false;
        

    }

    public void SetNotHitting()
    {
        swordHit.enabled = false;
        swordTrail.gameObject.SetActive(false);
    }

    public void SetAttacking()
    {
        swordHit.enabled = true;
        swordTrail.gameObject.SetActive(true);
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.PlayOneShot(slash);
    }

    public void SetNotDamaged()
    {
        isDamaged = false;
        isAttacking = false;
        swordHit.enabled = false;
    }

    public void SetTalking(bool state)
    {
        isTalking = state;
        charAnim.SetBool("walk", false);
    }

    public void SetWeapon (int newWeaponDamage, CapsuleCollider newSwordHit, TrailRenderer newSwordTrail)
    {
        weaponDamage = newWeaponDamage;
        swordHit = newSwordHit;
        swordTrail = newSwordTrail;
    }

    public void SetArmor (int lifeBonus)
    {
        maxLife = initialMaxLife + lifeBonus;
        life += lifeBonus;

        if (life > maxLife)
            life = maxLife;

        healthBar.SetMaxHealth(maxLife);
        healthBar.SetHealth(life);

        if (GameController.inst != null)
            GameController.inst.SetPlayerHP(life, maxLife);
    }

    public void ResetPlayerHealth()
    {
        maxLife = initialMaxLife;
        life = maxLife;

        charAnim.SetTrigger("reset");

        healthBar.SetMaxHealth(maxLife);
        healthBar.SetHealth(life);

        GameController.inst.SetPlayerHP(life, maxLife);

        isDamaged = false;
        isAttacking = false;
    }

    public void DamagePlayer(int damage)
    {
        if (!invulnerable && life > 0)
        {
            life -= damage;

            healthBar.SetHealth(life);
            GameController.inst.SetPlayerHP(life, maxLife);

            SetNotHitting();
            SetNotAttacking();

            isDamaged = true;
            charAnim.SetTrigger("damage");

            if (audioSource.isPlaying)
                audioSource.Stop();
            audioSource.PlayOneShot(hit);

            hitSplash.SetActive(true);

            StartCoroutine(MakePlayerInvulnerable());

            CheckDeath();
        }
    }

    private void CheckDeath()
    {
        if (life <= 0)
        {
            charAnim.SetTrigger("death");
        }
    }

    public void SetStartPoint(Vector3 newStartPoint)
    {
        startPoint = newStartPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBehaviour>().DamageMe(weaponDamage);
           //other.gameObject.SendMessage("DamageMe", weaponDamage);
        } else if (other.gameObject.CompareTag("NPC") || other.gameObject.CompareTag("Chest"))
        {
            isCloseToNPC = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC") || other.gameObject.CompareTag("Chest"))
        {
            isCloseToNPC = false;
        }
    }

    IEnumerator MakePlayerInvulnerable()
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
