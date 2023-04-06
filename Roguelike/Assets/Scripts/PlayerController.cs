using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public bool isDead;

    public float movementSpeed = 3;
    public float dashSpeed = 15;
    public float dashLenght = 0.15f;
    public float dashCooldown = 10;
    private float activeMovementSpeed;
    private float dashCooler = 0;
    private float dashCounter = 0;
    private Vector2 movementInput;
    public Transform dashBar;
    public GameObject dashBarText;
    public Color dashColor = new Color(0.3f, 0.5f, 0.75f);
    public bool isDashing = false;
    private float originDashBarX;
    private float originDashBarY;

    public Rigidbody2D RB2d;
    public Transform weaponArm;
    private Camera mainCamera;
    public Animator animator;
    public SpriteRenderer sprite;
    public Color dmgColor = new Color(1, 0.3f, 0.3f);
    public Color healColor = new Color(0.3f, 1, 0.3f);
    public float dmgColorDuration = 1f;
    public float healColorDuration = 0.5f;
    private float colorCooldown;
    private bool dmgColorActive = false;
    private float colorDuration;

    public GameObject bullet;
    public Transform fireDirection;
    public float fireRate = 0.8f;
    private float shotCounter = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        activeMovementSpeed = movementSpeed;
        originDashBarX = dashBar.localScale.x;
        originDashBarY = dashBar.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput.Normalize();

        RB2d.velocity = movementInput * activeMovementSpeed;

        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.localPosition);

        if (mousePosition.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            weaponArm.localScale = new Vector3(-1f, -1f, 1f);
            if (Mathf.Abs(dashBar.localScale.x) == originDashBarX)
                dashBar.localScale = new Vector3(-originDashBarX, originDashBarY);
        }
        else
        {
            transform.localScale = Vector3.one;
            weaponArm.localScale = Vector3.one;
            if (Mathf.Abs(dashBar.localScale.x) == originDashBarX)
                dashBar.localScale = new Vector3(originDashBarX, originDashBarY);
        }

        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        weaponArm.rotation = Quaternion.Euler(0, 0, angle);

        shotCounter -= Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            if(shotCounter <= 0)
            {
                Instantiate(bullet, fireDirection.position, fireDirection.rotation);
                shotCounter = fireRate;
            }
        }

        if (movementInput != Vector2.zero)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);

        if (Input.GetButtonDown("Dash"))
        {
            if(dashCooler <= 0 && dashCounter <= 0)
            {
                activeMovementSpeed = dashSpeed;
                dashCounter = dashLenght;
                animator.SetBool("isDashing", true);
                animator.speed = 1f / dashLenght;
                dashBarText.SetActive(false);
                PlayerHealthController.instance.MakeInvincible(dashLenght);
                sprite.color = dashColor;
                isDashing = true;
            }
        }
        if(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            sprite.color = new Color(dashColor.r / (dashCounter / dashLenght), dashColor.g / (dashCounter / dashLenght), dashColor.b / (dashCounter / dashLenght)); ;

            if (dashCounter <= 0)
            {
                activeMovementSpeed = movementSpeed;
                dashCooler = dashCooldown;
                animator.SetBool("isDashing", false);
                animator.speed = 1;
                sprite.color = Color.white;
                isDashing = false;
            }
        }

        if (dashCooler > 0)
        {
            dashCooler -= Time.deltaTime;
            dashBar.localScale = new Vector3((1f - (Mathf.Max(dashCooler, 0) / dashCooldown)) * originDashBarX, originDashBarY);
        }
        
        if(dashCounter <= 0 && dashCooler <= 0 && !dashBarText.activeInHierarchy)
        {
            dashBarText.SetActive(true);
        }

        if (dmgColorActive && colorCooldown > 0)
        {
            colorCooldown -= Time.deltaTime;
            sprite.color = new Color(sprite.color.r / (colorCooldown / colorDuration), sprite.color.g / (colorCooldown / colorDuration), sprite.color.b / (colorCooldown / colorDuration));
        }
        else if (dmgColorActive)
        {
            dmgColorActive = false;
            sprite.color = Color.white;
        }
    }

    public void OnDamagePlayer(int currentHP)
    {
        colorCooldown = dmgColorDuration;
        colorDuration = dmgColorDuration;
        dmgColorActive = true;
        sprite.color = dmgColor;

        if (currentHP <= 0)
            isDead = true;
    }

    public void OnHealPlayer()
    {
        colorCooldown = healColorDuration;
        colorDuration = healColorDuration;
        dmgColorActive = true;
        sprite.color = healColor;
    }
}
