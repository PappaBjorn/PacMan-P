using UnityEngine;
using System.Collections;

public class Points : MonoBehaviour 
{
	
	public GameObject Point;		  


	void Update () 
	{
							
	}

	void OnTriggerEnter(Collider other)						
	{														
		if (other.tag == "Pacman")							
		{
			Destroy(Point);
		}
	}
}
