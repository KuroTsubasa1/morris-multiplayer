using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        // find player
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // update camera movement

        var transform1 = this.transform;
        var position = transform1.position;
        position =
            new Vector3(_player.transform.position.x,_player.transform.position.y + 5f, position.z);
        transform1.position = position;
    }
}