using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class OneWayPlatform : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision enter " + collision.gameObject.name);
            
            if (collision.gameObject.CompareTag("player"))
            {
                Debug.Log("Player collision enter");
                collision.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collision enter " + other.gameObject.name);
            
            if (other.gameObject.CompareTag("player"))
            {
                Debug.Log("Player collision enter");
                other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            Debug.Log("Collision enter " + other.gameObject.name);
            
            if (other.gameObject.CompareTag("player"))
            {
                Debug.Log("Player collision enter");
                other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            Debug.Log("Collision exit " + collision.gameObject.name);
            
            if (collision.gameObject.CompareTag("player"))
            {
                Debug.Log("Player collision exit");
                collision.gameObject.GetComponent<CapsuleCollider>().enabled = true;
            }
        }
    }
}