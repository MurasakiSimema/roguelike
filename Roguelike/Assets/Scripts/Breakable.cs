using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject[] brokenPieces;
    public int maxPieces = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log('a');
        if ((other.gameObject.CompareTag("Player") && PlayerController.instance.isDashing))
        {
            Destroy(gameObject);

            int nPieces = Random.Range(1, maxPieces);
            for(int i = 0; i < nPieces; i++)
                Instantiate(brokenPieces[Random.Range(0, brokenPieces.Length)], transform.position, transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet") || other.CompareTag("EnemyBullet"))
        {
            Destroy(gameObject);

            int nPieces = Random.Range(1, maxPieces);
            for (int i = 0; i < nPieces; i++)
                Instantiate(brokenPieces[Random.Range(0, brokenPieces.Length)], transform.position, transform.rotation);
        }
    }
}