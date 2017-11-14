using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {

	public GameObject explosion;

	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "Hat") {
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}
}
