using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDeleter : MonoBehaviour {

	void Start() {
		Destroy(gameObject, 4.5f);
		transform.Translate(0,0,-2);
	}
}
