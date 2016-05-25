//This script tracks the health of the tower and game status

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{
	public int numberOfLives = 3;	//How many hits tower can take
	public Image damageImage;		//Full screen red image

    int currentLives;				//Current number of lives
	AudioSource damageAudio;		//Audio feedback of hit
	bool alive = true;				//Is the tower alive?

    void Awake()
	{
		//Set current lives and get audio component reference
        currentLives = numberOfLives;
		damageAudio = GetComponent<AudioSource>();

    }

	void OnTriggerEnter(Collider other)
	{
		//Make sure we can only be hit by enemies and only if tower is alive
		if (other.tag != "Drone" || !alive)
			return;
		
		Destroy(other.gameObject);
        currentLives -= 1;
		Color col = damageImage.color;
		col.a = 1f - ((float)currentLives / (float)numberOfLives);
		damageImage.color = col;
		damageAudio.Play();

		//If we are out of lives...
		if(currentLives <= 0)
		{
			//...set alive to false and show the red damage image
			//This image will hide the gameplay for 3 seconds
			alive = false;
            if (damageImage)
            {
                col.a = 1f;
                damageImage.color = col;
            }

			//Restart the gameplay after 3 seconds
			Invoke("Restart", 3f);
		}
	}

	void Restart()
	{
		//While the red image is still up, and before gameplay resumes, find
		//all enemies in the scene and destroy them. It doesn't matter that Find() is
		//very slow since any stutter is hidden by the red image
        GameObject[] drones = GameObject.FindGameObjectsWithTag("Drone");
		for (int i = 0; i < drones.Length; i++)
			Destroy(drones[i]);

		//Reset lives and alive boolean
        currentLives = numberOfLives;
        alive = true;

		//Hide red image
        if (damageImage)
        {
            Color col = damageImage.color;
            col.a = 0f;
            damageImage.color = col;
		}
    }
}
