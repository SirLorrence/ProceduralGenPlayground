using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour {
    /* RULES:
     * 1. Create grid base of determined size (n x n)
     * 2. Create position to place objects
     * 3. Spawn and Execute
     */

    [SerializeField] private int _gridX;
    [SerializeField] private int _gridY;        // in this case, this is represent 'z'
    [SerializeField] private float _gridOffset; // space between

    private const float CAMERA_BASE_HEIGHT = 20;

    void Start() {
        Generate();
    }

    void Generate() {
        for (int x = 0; x < _gridX; x++) {
            for (int z = 0; z < _gridY; z++) {
                Vector3 spawnPosition = new Vector3(x * _gridOffset, 0, z * _gridOffset);
                GameObject cube = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), spawnPosition,
                    Quaternion.identity);
                cube.transform.SetParent(this.transform);
            }
        }
    }

    void Regenerate() {
        try {
            _gridX = Int32.Parse(_xInputString);
        }
        catch (FormatException) {
        }

        try {
            _gridY = Int32.Parse(_yInputString);
        }
        catch (FormatException) {
        }

        if (transform.childCount > 1) {
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }

            Generate();
        }
        else
            Generate();

        // Camera re-position
        if (Camera.main != null) {
            var camTransform = Camera.main.transform;
            if (_gridX > 10 && _gridY > 10)
                camTransform.position = new Vector3(_gridX, _gridX + _gridY, _gridY);
            else if (_gridX <= 10 && _gridY >= 10)
                camTransform.position = new Vector3(_gridX, CAMERA_BASE_HEIGHT + _gridY + 2, _gridY);
            else if (_gridX >= 10 && _gridY <= 10)
                camTransform.position = new Vector3(_gridX, CAMERA_BASE_HEIGHT + _gridX + 2, _gridY);
        }
    }

    #region GUI

    private string _xInputString = "Enter X";
    private string _yInputString = "Enter Y";


    private void OnGUI() {
        GUI.Box(new Rect(10, 10, 100, 125), "Map Settings");
        if (GUI.Button(new Rect(20, 40, 80, 25), "Regenerate"))
            Regenerate();
        GUI.Label(new Rect(20, 70, 100, 30), "X:");
        GUI.Label(new Rect(20, 100, 100, 30), "Y:");
        _xInputString = GUI.TextField(new Rect(35, 70, 65, 20), _xInputString);
        _yInputString = GUI.TextField(new Rect(35, 100, 65, 20), _yInputString);

        // if(GUILayout.Button("Regenerate"))
        //     Regenerate();
    }

    #endregion
}