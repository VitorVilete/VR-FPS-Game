using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DroneSpawner : MonoBehaviour 
{
	public List<GameObject> spawnPoints = new List<GameObject>{};
	public GameObject drone;
	public float timeToSpawn = 8f;

	private float spawnVariance;				//A variance which will be used to randomize the spawn times slightly

	// Use this for initialization
	void Start () 
	{
		//Variance is always be half of the total interval (Tip: multiplying by .5 is faster than dividing by 2)
		spawnVariance = timeToSpawn * .5f;
		Invoke("SpawnDrone", timeToSpawn + Random.Range(-spawnVariance, spawnVariance));
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Our minimum spawn time is 1 second
		if (timeToSpawn > 1f)
		{
			//every 50 seconds of gameplay reduces the timer by 1 second
			float timeReduction = Time.deltaTime / 50;

			//Ensure we don't go below 1 second and recalculate variance
			timeToSpawn = Mathf.Max(1f, timeToSpawn - timeReduction);
			spawnVariance = timeToSpawn * .5f;
		}
	}

	void SpawnDrone()
	{
		int position = Random.Range(0, spawnPoints.Count);
		GameObject enemyObj = Instantiate(drone, spawnPoints[position].transform.position, spawnPoints[position].transform.rotation) as GameObject;
		enemyObj.transform.parent = spawnPoints[position].transform;

		Invoke("SpawnDrone", timeToSpawn + Random.Range(-spawnVariance, spawnVariance));
	}
}
