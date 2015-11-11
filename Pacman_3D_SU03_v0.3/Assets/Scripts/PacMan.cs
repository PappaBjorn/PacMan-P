using UnityEngine;
using System.Collections;

public class PacMan : MonoBehaviour {
	
	// Initialising variables.
	bool move = false; // To start out without movement.
	float speed = 1.0f; // Initial speed for pacman.
	float rayLengthX = 0.6f; // The ray is cast from pacmans center, with 0.5f to closest SIDE-wall, so I went 0.6 to be sure.
	float rayLengthZ = 0.505f; // This ray is for forward-wall detection. I found .505f to be more precise than .5f.
	float powerupspeedseconds = 10.0f;
	
	void Update() { //First, I initialise three Rays going forward and sideways to check wall-collisions.
		Ray rayForward = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
		Ray rayRight = new Ray(transform.position, transform.TransformDirection(0.6f, 0.0f, -0.1f)); // Vector for direction is set manually since I want it to reach a bit behind pacmans center,
		Ray rayLeft = new Ray(transform.position, transform.TransformDirection(-0.6f, 0.0f, -0.1f)); // to only allow turning when he is more centered = more realistic.
		RaycastHit hitinfo;
		
		// Then, I move the player forward, unless the ray detects a collision near the front of pacman, and only if 'move' is set to 'true'.
		if (!Physics.Raycast(rayForward, out hitinfo, rayLengthZ)) {
			if (move) {
				transform.Translate(Vector3.forward * Time.deltaTime * speed);
			}
			if (Input.GetKeyDown(KeyCode.UpArrow)) {
				move = true;
			}
		} else {
			if (hitinfo.collider.gameObject.tag == "PowerUpSpeed") {
				Destroy(hitinfo.collider.gameObject);
				StartCoroutine(SpeedUp(5.0f));
			}
		}
		
		// This checks for any walls to the left, which disallows a left-turn.
		if (!Physics.Raycast(rayLeft, rayLengthX)) {
			if (Input.GetKeyDown(KeyCode.LeftArrow)) {
				transform.Rotate(Vector3.down * 90.0f);
				transform.position = new Vector3(Mathf.Round(transform.position.x),
				                                 transform.position.y,
				                                 Mathf.Round(transform.position.z));
			}
		}
		// This checks for any walls to the right, which disallows a right-turn.
		if (!Physics.Raycast(rayRight, rayLengthX)) {
			if (Input.GetKeyDown(KeyCode.RightArrow)) {
				transform.Rotate(Vector3.up * 90.0f);
				transform.position = new Vector3(Mathf.Round(transform.position.x),
				                                 transform.position.y,
				                                 Mathf.Round(transform.position.z));
			}
		}
	}
	
	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Walls") {
			move = false;
			transform.position = new Vector3(Mathf.Round(transform.position.x),
			                                 Mathf.Round(transform.position.y),
			                                 Mathf.Round(transform.position.z));
		}
	}

	IEnumerator SpeedUp(float duration) {
		// Doubles PacMan's speed for <duration> time.
		Debug.Log("Speed up!");
		speed *= 2.0f;
		yield return new WaitForSeconds(duration);
		Debug.Log("Speed down..");
		speed /= 2.0f;
	}

	IEnumerator SpeedUpp() {
		float duration = 0.2f;
		float framesTime = 0.0f;
		while (framesTime < duration) {
			speed *= Time.deltaTime;
			yield return 0;
			framesTime += Time.deltaTime;
		}
	}
}