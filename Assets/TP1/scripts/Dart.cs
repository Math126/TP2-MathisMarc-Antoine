using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Dart : MonoBehaviour
{
    Rigidbody rb;
    private GameObject CentreDeTire;
    private List<MeshCollider> Cible;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        CentreDeTire = GameObject.Find("LancerDart");

        foreach (GameObject cible in CentreDeTire.transform.Find("Cible"))
        {
            Cible.Add(cible.GetComponent<MeshCollider>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cible"))
        {
            rb.useGravity = false;
            rb.freezeRotation = true;
            rb.isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cible"))
        {
            rb.useGravity = true;
            rb.freezeRotation = false;
            rb.isKinematic = false;
        }
    }
}
