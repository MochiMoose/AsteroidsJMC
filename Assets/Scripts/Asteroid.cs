using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	public int SpawnValue = 3;
	
    public float collisionDamage = 1f;

    public float HealthMax;

    private float healthCurrent;

    public GameObject[] ChunksRef;
    public int ChunksMin = 0;
    public int ChunksMax = 4;
    public float ExplodeDist = 0.5f;
    public float ExplosionForce = 10f;
    public GameObject ExplosionRef;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Spaceship ship = collision.gameObject.GetComponent<Spaceship>();
        if (ship != null )
        {
            ship.takedamage(collisionDamage);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        healthCurrent = HealthMax;    
    }

    public void TakeDamage(float damage)
    {
        healthCurrent = healthCurrent - damage;
        if (healthCurrent <= 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        int numChunks = Random.Range(ChunksMin, ChunksMax);

        if (ChunksRef.Length > 0)
        {
            for (int i = 0; i < numChunks; i++)
            {
                CreateAsteroidChunk();
            }
        }

        
        Instantiate(ExplosionRef, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void CreateAsteroidChunk ()
    {
        int randomInxed = Random.Range(0, ChunksRef.Length);
        GameObject chunkRef = ChunksRef[randomInxed];

        Vector2 SpawnPos = transform.position;
        SpawnPos.x += Random.Range(-ExplodeDist, ExplodeDist);
        SpawnPos.y += Random.Range(-ExplodeDist, ExplodeDist);

        GameObject chunk = Instantiate(chunkRef, SpawnPos, transform.rotation);

        Vector2 dir = (SpawnPos - (Vector2)transform.position).normalized;

        Rigidbody2D rb = chunk.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * ExplosionForce);

    }
}
