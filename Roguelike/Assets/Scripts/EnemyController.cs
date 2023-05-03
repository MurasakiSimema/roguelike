using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    public float movementSpeed = 2.5f;

    public float rangeOfVision = 8;
    private Vector3 movementDirection;
    private float originX;

    public Animator animator;

    public int maxHP = 150;
    private int currentHP;
    private bool isDead = false;
    public Color dmgColor = new Color(1, 0.2971698f, 0.2971698f);
    public SpriteRenderer sprite;
    public float dmgColorDuration = 0.2f;
    private float dmgColorCooldown;
    private bool dmgColorActive = false;

    public bool canShoot = false;
    public GameObject bullet;
    public Transform fireDirection;
    public float fireRate = 0.8f;
    private float fireCooldown;
    //private float shotCounter = 0;
    private float shootAnimationCounter;
    public float range = 7;

    public bool IsAlive
    {
        get => !isDead;
    }

    // Start is called before the first frame update
    void Start()
    {
        originX = transform.localScale.x;
        currentHP = maxHP;
        animator.SetBool("isMoving", false);
        animator.SetBool("isDead", false);
        fireCooldown = 1f / fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (dmgColorActive && dmgColorCooldown > 0)
        {
            dmgColorCooldown -= Time.deltaTime;
        }
        else if (dmgColorActive)
        {
            dmgColorActive = false;
            sprite.color = Color.white;
        }

        if (isDead || PlayerController.instance.isDead)
        {
            rigidbody2d.velocity = Vector2.zero;
            return;
        }

        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeOfVision)
        {
            movementDirection = PlayerController.instance.transform.position - transform.position;
        }
        else
        {
            movementDirection = Vector3.zero;
        }

        movementDirection.Normalize();
        rigidbody2d.velocity = movementDirection * movementSpeed;

        if (movementDirection != Vector3.zero)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);

        if (movementDirection.x > 0)
        {
            transform.localScale = new Vector3(-1f * originX, transform.localScale.y, transform.localScale.z);
        }
        else if (movementDirection.x < 0)
        {
            transform.localScale = new Vector3(originX, transform.localScale.y, transform.localScale.z);
        }

        //shotCounter -= Time.deltaTime;
        shootAnimationCounter -= Time.deltaTime;

        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < range && !animator.GetBool("isAttacking"))
        {
            animator.SetBool("isAttacking", true);
            shootAnimationCounter = canShoot ? 1f * fireCooldown : 0.6f * fireCooldown;
            animator.speed = 1f / fireCooldown;
        }

        if (shootAnimationCounter <= 0 && animator.GetBool("isAttacking"))
        {
            animator.SetBool("isAttacking", false);
            //shotCounter = fireCooldown;
            animator.speed = 1;
            if (canShoot) {
                AudioManager.instance.PlaySFX(13);
                Instantiate(bullet, fireDirection.position, fireDirection.rotation);
            }
            else
            {
                AudioManager.instance.PlaySFX(16);
                PlayerHealthController.instance.DamagePlayer();
            }
        }
    }

    public void DamageEnemy(int dmg)
    {
        currentHP -= dmg;
        dmgColorCooldown = dmgColorDuration;
        dmgColorActive = true;
        sprite.color = dmgColor;

        if (currentHP <= 0)
        {
            AudioManager.instance.PlaySFX(2);
            animator.speed = 1;
            isDead = true;
            animator.SetBool("isDead", true);

            rigidbody2d.velocity = Vector2.zero;
        }
        else
            AudioManager.instance.PlaySFX(3);
    }
}
