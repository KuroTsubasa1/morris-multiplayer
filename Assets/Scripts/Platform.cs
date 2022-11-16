using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Rigidbody _rb;
    private float _fallSpeed = -1;

    private PlatformSpawner _platformSpawner;

    private void Awake()
    {
        // register _platformSpawner
        _platformSpawner = GameObject.Find("PlatformSpawner").GetComponent<PlatformSpawner>();

        // read current speed
        _fallSpeed = _platformSpawner.fallSpeed;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(0, _fallSpeed, 0);
        //_rb.AddForce(0, _fallSpeed, 0, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("platformDestroyer"))
        {
            Destroy(gameObject);
        }
    }
}