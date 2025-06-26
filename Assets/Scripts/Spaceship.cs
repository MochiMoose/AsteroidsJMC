using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public float EnginePower = 10f;
    public float TurnPower = 10f;

    private float HealthMax = 3f;
    private float HealthCurrent;

    public GameObject BulletRef;
    public float BulletSpeed = 100f;

    public float FiringRiate = 0.33f;
    private float FireTimer = 0f;

    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        HealthCurrent = HealthMax;
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        ApplyThrust(vert);
        ApplyTorque(horiz);
        updatefiring();

    }

   public void ApplyThrust(float amount)
   {
        Vector2 thrust = transform.up * EnginePower * Time.deltaTime * amount;
        rigidBody.AddForce(thrust);
   }

    public void ApplyTorque(float amount)
    {
        float torque = amount * TurnPower * Time.deltaTime;
        rigidBody.AddTorque(torque);
    }

    public void takedamage(float damage)
    {
        HealthCurrent = HealthCurrent - damage;
        if (HealthCurrent <= 0f)
        {
            explode();
        }
    }
    public void explode()
    {
        Debug.Log("Game Over!");
        Destroy(gameObject);
    }

    public void FireBullet()
    {
        GameObject bullet = Instantiate(BulletRef, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 force = transform.up * BulletSpeed;
        rb.AddForce(force);
    }
    
    private void updatefiring()
    {
        bool isfiring = Input.GetButton("Fire1");
        FireTimer = FireTimer - Time.deltaTime;
        if ( isfiring && FireTimer <= 0f)
        {
            FireBullet();
            FireTimer = FiringRiate;
        }
    }
}
