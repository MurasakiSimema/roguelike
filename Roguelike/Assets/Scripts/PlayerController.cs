using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

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
    private float originDashBarX;
    private float originDashBarY;

    public Rigidbody2D RB2d;
    public Transform weaponArm;
    private Camera mainCamera;
    public Animator animator;

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
                dashBarText.SetActive(false);
            }
        }
        if(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if(dashCounter <= 0)
            {
                activeMovementSpeed = movementSpeed;
                dashCooler = dashCooldown;
            }
        }

        if (dashCooler > 0)
        {
            dashCooler -= Time.deltaTime;
            dashBar.localScale = new Vector3((1f - (dashCooler / dashCooldown)) * originDashBarX, originDashBarY);
        }
        
        if(dashCounter <= 0 && dashCooler <= 0 && !dashBarText.activeInHierarchy)
        {
            Debug.Log(dashCooler);
            dashBarText.SetActive(true);
        }
    }
}
