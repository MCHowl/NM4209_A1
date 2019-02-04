using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

	private float cameraY, objY;
	private float killY = 10f;

	void Update() {
		cameraY = Camera.main.transform.position.y;
		objY = transform.position.y;

		if (cameraY - objY > killY) {
			Destroy(this.gameObject);
		}
	}

}
