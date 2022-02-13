using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralPlacement : MonoBehaviour {
    public List<Vector3> blockPlacementInformation;
    public void SpawnItems(int amount, GameObject item) {
        for (int i = 0; i < amount; i++) {
           Instantiate(item, GetRandomPosition(), Quaternion.identity);
        }
    }
    private Vector3 GetRandomPosition() {
        int randomIndex = Random.Range(0, blockPlacementInformation.Count);
        Vector3 randomPosition = new Vector3(blockPlacementInformation[randomIndex].x, blockPlacementInformation[randomIndex].y + 0.5f, blockPlacementInformation[randomIndex].z);
        blockPlacementInformation.RemoveAt(randomIndex); // to avoid spawn overlap
        return randomPosition;
    }
}
