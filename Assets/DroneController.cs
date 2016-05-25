using UnityEngine;
using System.Collections;

public class DroneController : MonoBehaviour 
{
	private NavMeshAgent nma;

	// Use this for initialization
	void Start () 
	{
		nma = gameObject.GetComponent<NavMeshAgent>();
		nma.destination = GameObject.Find("DroneTarget").transform.position;
	}
	
	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "Bullet")
		{
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
}
