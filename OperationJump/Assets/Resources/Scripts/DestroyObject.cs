using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

	private float cameraY, objY;

	void Update() {
		cameraY = Camera.main.transform.position.y;
		objY = transform.position.y;

		if (cameraY - objY > 6) {
			Destroy(this.gameObject);
		}
	}

}
