using UnityEngine;
using System.Collections;

public class Minimapcamera : MonoBehaviour {

	public Transform pacman;

	void Update () {
		transform.position = new Vector3(pacman.position.x, 8.0f, pacman.position.z);
	}
}