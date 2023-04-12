using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickup : MonoBehaviour
{
    public int healAmount = 1;

    public float spawnTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTime > 0)
            spawnTime -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (spawnTime > 0)
            return;

        if (other.CompareTag("Player")) {
            PlayerHealthController.instance.HealPlayer(healAmount);

            Destroy(gameObject);
        }
    }
}
