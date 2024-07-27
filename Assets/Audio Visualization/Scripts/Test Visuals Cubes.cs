using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVisualsCubes : MonoBehaviour {

    [SerializeField]
    private GameObject _cubePrefab;

    [SerializeField]
    private float _maxHeight = 20f;

    private GameObject[] _cubes = new GameObject[256];

    private void Start() {
        for (int i = 0; i < 256; i++) {
            GameObject cube = Instantiate(_cubePrefab, gameObject.transform);
            cube.name = "Cube " + i;

            Vector3 position = transform.position + new Vector3(i, 0, 0);

            cube.transform.position = position;

            _cubes[i] = cube;
        }
    }

    private void Update() {
        for (int i = 0; i < 256; i++) {
            _cubes[i].transform.localScale = new Vector3(1, AudioPeer._spectrum[i] * _maxHeight + 1, 1);
        }
    }
}
