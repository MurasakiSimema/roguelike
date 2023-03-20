using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    public float movementSpeed = 2.5f;

    public float rangeOfVision = 8;
    private Vector3 movementDirection;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeOfVision)
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
    }
}
