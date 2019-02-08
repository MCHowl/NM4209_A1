using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMover : MonoBehaviour {

	private float xGameArea = 7.5f;
	private float speed = 0.5f;

	private Vector3 positionLimit;

	void Start() {
		float xLength = transform.localScale.x;

        if (xLength < 3.5f) {
			positionLimit = new Vector3((xGameArea - xLength) / 2f, transform.position.y, 0);

			if (Random.value <= 0.5f) {
				positionLimit.x *= -1;
			}
		} else {
			Destroy(this);
		}
    }

	private void FixedUpdate() {
		float movementDelta = speed * Time.deltaTime;

		if (transform.position.x - positionLimit.x < 0.001f && positionLimit.x < 0) {
			positionLimit.x = Mathf.Abs(positionLimit.x);
		} else if (positionLimit.x - transform.position.x < 0.0001f && positionLimit.x > 0) {
			positionLimit.x = -1 * Mathf.Abs(positionLimit.x);
		}

		transform.position = Vector3.MoveTowards(transform.position, positionLimit, movementDelta);
	}
}
