using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CouteauTenuGauche(Knife couteau);
public delegate void CouteauTenuRight(Knife couteau);
public class Knife : MonoBehaviour
{
    public static event CouteauTenuGauche onCouteauTenuGauche;
    public static event CouteauTenuGauche onCouteauTenuDroite;

    public AudioClip KnifeSound;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ChangerRotationObjet(Vector3 valeurRotation)
    {
        rb.AddRelativeTorque(valeurRotation * 0.1f);
    }

    public void ChangeSpeedObjet(Vector3 valeurSpeed)
    {
        rb.AddRelativeForce(new Vector3(valeurSpeed.x * 0.02f, valeurSpeed.y * 0.02f, valeurSpeed.z * 0.3f));
    }

    public void MakeSound()
    {
        GetComponent<AudioSource>().PlayOneShot(KnifeSound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftHand"))
        {
            onCouteauTenuGauche(gameObject.GetComponent<Knife>());
        }
        else if (other.CompareTag("RightHand"))
        {
            onCouteauTenuDroite(gameObject.GetComponent<Knife>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LeftHand"))
        {
            onCouteauTenuGauche(null);
        }
        else if (other.CompareTag("RightHand"))
        {
            onCouteauTenuDroite(null);
        }
    }
}