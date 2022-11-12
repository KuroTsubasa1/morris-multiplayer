using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // https://coolors.co/201e1f-ff4000-faaa8d-feefdd-50b2c0

    private Rigidbody _rb;
    private float _speed = 5f;
    private float _maxSpeed = 5f;
    private float _jumpPower = 20;

    private bool _jumpState = true;

    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Use last device which provided input.
        var inputDevice = InputManager.ActiveDevice;

        // map movement on joystickaxis 
        _rb.AddForce(0, 0, inputDevice.LeftStickY * _speed, ForceMode.VelocityChange);


        // movement for keyboard
        if (Input.GetKey(KeyCode.D))
        {
            _rb.AddForce(_speed, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _rb.AddForce(-_speed, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _jumpState)
        {
            _jumpState = false;
            _rb.AddForce(0, _jumpPower, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }

        // check x  velocity is positive
        if (_rb.velocity.x >= 0)
        {
            // check if x velocity has reached max velocity
            if (_rb.velocity.x > _maxSpeed)
            {
                _rb.velocity = new Vector3(_maxSpeed, _rb.velocity.y, 0);
            }
        }
        else
        {
            // check if x velocity has reached min velocity
            if (_rb.velocity.x < -_maxSpeed)
            {
                _rb.velocity = new Vector3(-_maxSpeed, _rb.velocity.y, 0);
            }
        }

        _rb.velocity = new Vector3(0, _rb.velocity.y, 0);

        // check y velocity is positive
        if (_rb.velocity.y > 0)
        {
            // _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
        }
        else
        {
            // apply more force when falling 
            _rb.velocity = new Vector3(0, _rb.velocity.y * 1.005f, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            _jumpState = true;
        }
    }
}