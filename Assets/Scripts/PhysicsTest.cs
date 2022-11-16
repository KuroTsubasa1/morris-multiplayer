using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PhysicsTest : MonoBehaviour
    {
        private void Start()
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            
            rb.AddForce(0,-5,0, ForceMode.VelocityChange);
        }
    }
}