using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public float EnginePower = 10f;
    public float TurnPower = 10f;

    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        ApplyThrust(vert);
        ApplyTorque(horiz);

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
}
