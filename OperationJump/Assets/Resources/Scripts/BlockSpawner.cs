using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
	public GameObject Block;

	private float avgBlockLength = 4;
	private float minBlockLength = 1.5f;

	private float nextYSpawn;
	private float floorY = -8f;
	private float spawnDistance = 2.4f;

	private float nextDifficultyIncrease;
	private float difficultyIncreaseDistance;

	private int blockSpawnCount;

	private CameraController game;

	void Start() {
		game = GetComponent<CameraController>();
		nextYSpawn = floorY + spawnDistance;
		blockSpawnCount = 0;

		//20 blocks to increase difficulty
		difficultyIncreaseDistance = 20 * spawnDistance;
		nextDifficultyIncrease = nextYSpawn + difficultyIncreaseDistance;
	}

	void Update() {
        if (game.isGame) {
			if (transform.position.y - floorY > nextYSpawn) {
				// Spawn a new block
				if (blockSpawnCount % 3 != 0) {
					Vector3 newBlockPosition = new Vector3(Random.Range(-3.75f + ((avgBlockLength + 0.5f) / 2), 3.75f - ((avgBlockLength + 0.5f) / 2)), nextYSpawn, 0);
					GameObject newBlock = Instantiate(Block, newBlockPosition, Quaternion.identity);
					Vector3 newBlockScale = new Vector3(Random.Range(avgBlockLength - 0.5f, avgBlockLength + 0.5f), 1, 1);
					newBlock.transform.localScale = newBlockScale;
				} else {
					Vector3 newBlockPosition = new Vector3(Random.Range(-1.75f, 1.75f), nextYSpawn, 0);
					GameObject newBlock = Instantiate(Block, newBlockPosition, Quaternion.identity);
					Vector3 newBlockScale = new Vector3(4, 1, 1);
					newBlock.transform.localScale = newBlockScale;
				}

				nextYSpawn += spawnDistance;
				blockSpawnCount++;

				// 10% chance to reduce avg block length by 0.25
				//if (Random.Range(0f, 1f) <= 0.1f) {
				//	avgBlockLength = Mathf.Max(avgBlockLength - 0.25f, minBlockLength);
				//}
				if (nextDifficultyIncrease <= nextYSpawn) {
					avgBlockLength = Mathf.Max(avgBlockLength - 0.25f, minBlockLength);
					nextDifficultyIncrease += difficultyIncreaseDistance;
				}
			}
		}
    }
}
