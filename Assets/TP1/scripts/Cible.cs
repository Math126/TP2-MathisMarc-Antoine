using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cible : MonoBehaviour
{
    public int point;
    public GameObject TableJeux;
    public GameObject panneau;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Couteau"))
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<Rigidbody>().freezeRotation = true;
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Collider>().enabled = false;

            TableJeux.GetComponent<TableJeux>().ModifScore(point);
        }
    }
}
