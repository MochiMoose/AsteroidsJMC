using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public GameObject[] AsteroidRefs;
	public float CheckInterval = 3f;
	public float PushForce = 100f;
	public int SpawnThreshold = 10;
	
	private float CheckTimer = 0f;
	
	private float Inaccuracy = 2f;
	
	public int TotalAsteroidValue()
	{
		Asteroid[] asteroids = FindObjectsByType<Asteroid>(FindObjectsSortMode.None);
		int value = 0;
		for(int n = 0; n < asteroids.Length; n++)
		{
			value += asteroids[n].SpawnValue;
		}
		return value;
	}
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckTimer += Time.deltaTime;
		if (CheckTimer > CheckTimer)
		{
			CheckTimer = 0f;
			
			if (TotalAsteroidValue() < SpawnThreshold)
			{
				SpawnNewAsteroid();
			}
		}
    }
	
	public void SpawnNewAsteroid()
	{
		int asteroidIndex = Random.Range(0, AsteroidRefs.Length);
		GameObject asteroidRef = AsteroidRefs[asteroidIndex];
		
		Vector3 SpawnPoint = OffScreenSpawnPoint();
		
		GameObject asteroid = Instantiate(asteroidRef, SpawnPoint, transform.rotation);
		
		Vector2 force = PushDirection(SpawnPoint) * PushForce;
		Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
		rb.AddForce (force);
	}
	
	public Vector3 OffScreenSpawnPoint()
	{
		Vector2 randomPos = Random.insideUnitCircle;
		Vector2 direction = randomPos.normalized;
		Vector2 finalPos = (Vector2) transform.position + direction * 2f;
		Vector3 result = Camera.main.ViewportToWorldPoint (finalPos);
		result.z = transform.position.z;
		
		return result;
	}
	
	public Vector2 PushDirection(Vector2 from)
	{
		Vector2 miss = Random.insideUnitCircle * Inaccuracy;
		Vector2 destination = (Vector2)transform.position + miss;
		
		Vector2 direction = (destination - from).normalized;
		return direction;
	}
}
