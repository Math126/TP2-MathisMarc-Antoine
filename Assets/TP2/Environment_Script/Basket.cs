using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Basket : MonoBehaviour
{
    public List<Collider> colliders;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LeftHand") && other.CompareTag("RightHand"))
        {
            foreach(Collider c in colliders)
            {
                c.isTrigger = true;
            }


            Destroy(gameObject.GetComponent<Rigidbody>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LeftHand") && other.CompareTag("RightHand"))
        {
            foreach (Collider c in colliders)
            {
                c.isTrigger = false;
            }

            gameObject.AddComponent<Rigidbody>();
        }
    }
}
