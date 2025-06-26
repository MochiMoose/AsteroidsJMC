using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float collisionDamage = 1f;

    public float HealthMax;

    private float healthCurrent;




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
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
