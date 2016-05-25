using UnityEngine;
using System.Collections;

public class FireSingleShot : MonoBehaviour 
{
	public AudioClip shootClip;
	public GameObject bulletPrefab;
	public float shootForce;


	void Update()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		if(Input.GetTouch(0).phase == TouchPhase.Began)
		{
			GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
			bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(0,0,shootForce));
			gameObject.GetComponent<AudioSource>().PlayOneShot(shootClip);
		}
#endif
	
	}
}
