using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 3;
    private Vector2 movementInput;

    public Rigidbody2D RB2d;

    public Transform weaponArm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        //transform.position += new Vector3(movementInput.x * Time.deltaTime * movementSpeed, movementInput.y * Time.deltaTime * movementSpeed, 0f);

        RB2d.velocity = movementInput * movementSpeed;

        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        weaponArm.rotation = Quaternion.Euler(0, 0, angle);
    }
}
