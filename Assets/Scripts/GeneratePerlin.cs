using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePerlin : GenerateGrid {
    [SerializeField] private float _perlinDetail = 0f;
    [SerializeField] private float _height;

    protected override void Generate() {
        //Hard set for this example
        _gridX = 50;
        _gridY = 50;

        for (int x = 0; x < _gridX; x++) {
            for (int y = 0; y < _gridY; y++) {
                Vector3 spawnPosition = new Vector3(x, NoiseGen(x, y, _perlinDetail) * _height, y);
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                var boxCollider = cube.GetComponent<BoxCollider>();
                if (boxCollider != null)
                    Destroy(boxCollider);
                cube.transform.position = spawnPosition;
                cube.transform.SetParent(transform);
            }
        }
    }

    private float NoiseGen(int xLocation, int yLocation, float detail) {
        float xNoise = (xLocation + this.transform.position.x) / detail;
        float yNoise = (yLocation + this.transform.position.y) / detail;
        return Mathf.PerlinNoise(xNoise, yNoise);
    }

    protected override void OnGUI() {
        // base.OnGUI();
    }
}