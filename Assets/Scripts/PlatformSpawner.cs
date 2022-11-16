using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _basePlatform;

    private List<GameObject> _platformArray = new();
    private float _nextSpawn = 0.0f;

    private float _minX = -20f;
    private float _maxX = 20f;

    public float fallSpeed = -0.1f;
    public float spawnRate = 5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (!(Time.time > _nextSpawn)) return;
        _nextSpawn = Time.time + spawnRate;
        GameObject platform = Instantiate(_basePlatform,
            new Vector3(Random.Range(_minX, _maxX), transform.position.y, 0), Quaternion.identity);
        _platformArray.Add(platform);
       

        // Call every 100 platforms speedup event
        if (_platformArray.Count % 2 == 0)
        {
            fallSpeed -= 0.1f;
            platform.GetComponent<Renderer>().material.color =  new Color(255, 0, 0, 255);
           
        }
    }
}