using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Door : MonoBehaviour
{
    public bool ouvre;

    private Vector3 ClosedDoor, OpennedDoor;

    private void Start()
    {
        ClosedDoor = transform.rotation.eulerAngles;
        OpennedDoor = new Vector3(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y -90,transform.rotation.eulerAngles.z);
    }

    private void Update()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        Quaternion target;

        if (ouvre && rotation != OpennedDoor)
        {
            target = Quaternion.Euler(OpennedDoor);
        }
        else
        {
            target = Quaternion.Euler(ClosedDoor);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        ouvre = true;
    }

    private void OnTriggerExit(Collider other)
    {
        ouvre = false;
    }
}
