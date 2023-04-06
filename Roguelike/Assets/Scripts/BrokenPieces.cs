using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPieces : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Vector3 moveDirection;
    public float deceleration = 5f;

    public float lifeTime = 5f;
    public float vanishTime = 2f;
    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
        moveDirection.y = Random.Range(-moveSpeed, moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * Time.deltaTime;

        moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.deltaTime);

        lifeTime -= Time.deltaTime;

        if(lifeTime < 0)
            Destroy(gameObject);

        if (lifeTime < vanishTime)
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, lifeTime / vanishTime);
    }
}
