using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePerlin : GenerateGrid {
    [SerializeField] private float _perlinDetail = 0f;
    [SerializeField] private float _height;
    [Header("Item Spawning")]
    [SerializeField] private int _numOfItems = 20;
    [SerializeField] private GameObject _spawnObject;
    private ProceduralPlacement _proceduralPlacement;
    
    /// Works the same as the Grid Generation, but has the add Noise modification for height map 
    protected override void Generate() {
        if(_proceduralPlacement == null)
            _proceduralPlacement = gameObject.AddComponent<ProceduralPlacement>();

        _proceduralPlacement.blockPlacementInformation = new List<Vector3>();
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
                _proceduralPlacement.blockPlacementInformation.Add(cube.transform.position);
                cube.transform.SetParent(transform);
            }
        }
        _proceduralPlacement.SpawnItems(_numOfItems,_spawnObject);
    }

    /*
     * Gets the x and y location of the grid and corresponds the location with an perlin map
     * based on its detail level.
     * In a very simplified why. 
     */
    private float NoiseGen(int xLocation, int yLocation, float detail) {
        float xNoise = (xLocation + this.transform.position.x) / detail;
        float yNoise = (yLocation + this.transform.position.y) / detail;
        return Mathf.PerlinNoise(xNoise, yNoise);
    }

    protected override void OnGUI() {
        // base.OnGUI();
    }
}