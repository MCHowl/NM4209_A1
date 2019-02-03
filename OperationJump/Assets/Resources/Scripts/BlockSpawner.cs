using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
	public GameObject Block;

	private float avgBlockLength = 4;
	private float minBlockLength = 1.5f;

	private float nextYSpawn = -2.6f;
	private float spawnDistance = 2.4f;

	private CameraController game;

	void Start() {
		game = GetComponent<CameraController>();
	}

	void Update() {
        if (game.isGame) {
			if (transform.position.y + 6 > nextYSpawn) {
				// Spawn a new block
				Vector3 newBlockPosition = new Vector3 (Random.Range(-3.75f + ((avgBlockLength + 0.5f) / 2), 3.75f - ((avgBlockLength + 0.5f) / 2)), nextYSpawn, 0);
				GameObject newBlock = Instantiate(Block, newBlockPosition, Quaternion.identity);

				Vector3 newBlockScale = new Vector3(Random.Range(avgBlockLength - 0.5f, avgBlockLength + 0.5f), 1, 1);
				newBlock.transform.localScale = newBlockScale;

				nextYSpawn += spawnDistance;

				// 10% chance to reduce avg block length by 0.25
				if (Random.Range(0f, 1f) <= 0.1f) {
					avgBlockLength = Mathf.Max(avgBlockLength - 0.25f, minBlockLength);
				}
			}
		}
    }
}
