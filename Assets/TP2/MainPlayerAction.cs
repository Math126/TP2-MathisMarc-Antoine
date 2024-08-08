using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;
using UnityEngine.XR.Interaction.Toolkit;

public class MainPlayerAction : MonoBehaviour
{
    public GameObject ListeMission;
    public GameObject card;
    public Transform SpawnPos;
    public AudioClip siffle;
    public AudioSource source;

    private Seller marchant;
    private MainPlayerMission mplayer;
    private ContinuousMoveProviderBase moveProvider;
    private XRInputManager manager;
    private bool playingSiffle = false, AlreadyTalk = false, peutAgir = false;

    private void Start()
    {
        mplayer = GetComponent<MainPlayerMission>();
        manager = GameObject.FindWithTag("Player").GetComponent<XRInputManager>();
        moveProvider = GetComponent<ContinuousMoveProviderBase>();

        XRInputManager.Hand.onPrimaryChange += spawnMissionCard;
        XRInputManager.Hand.onSecondaryChange += acceptMission;
        XRInputManager.Hand.onGripperStateChange += NotUse;
        XRInputManager.Hand.onTriggerStateChange += siffler;
        XRInputManager.Hand.onThumbstickClickStateChange += NotUse;
        XRInputManager.Hand.onThumbstickTouchStateChange += NotUse;
        XRInputManager.Hand.onGripperValueChange += NotUse;
        XRInputManager.Hand.onTriggerValueChange += NotUse;
        XRInputManager.Hand.onThumbstickValueChange += NotUse;
        XRInputManager.Hand.onHandPositionChange += NotUse;
        XRInputManager.Hand.onHandRotationChange += NotUse;
        XRInputManager.Hand.onHandSpeedChange += NotUse;
        XRInputManager.Head.onHeadSpeedChange += NotUse;
        XRInputManager.Head.onHeadPositionChange += NotUse;
        XRInputManager.Head.onHeadRotationChange += NotUse;

        Invoke("Delai", 2);
    }

    private void Update()
    {
        if (manager.RightDeviceFind && peutAgir)
        {
            sprint("RightHand", manager.GetThumbstickClick("RightHand"));
        }

        if (manager.LeftDeviceFind && peutAgir)
        {
            sprint("LeftHand", manager.GetThumbstickClick("LeftHand"));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vendeur"))
        {
            marchant = other.transform.GetComponent<Seller>();

            if (!AlreadyTalk)
            {
                other.gameObject.GetComponent<Animator>().SetBool("Talking", true);
                AlreadyTalk = true;
            }
        }
        else if (other.CompareTag("GoldenFish"))
        {
            other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            other.gameObject.GetComponent<Fish>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Vendeur"))
        {
            marchant = null;
            other.gameObject.GetComponent<Animator>().SetBool("Talking", false);
        }
    }

    private void spawnMissionCard(string main, bool state)
    {
        if(state)
        {
            card.transform.position = SpawnPos.position;
        }
    }

    private void acceptMission(string main, bool state)
    {
        if(marchant != null && state && !mplayer.GetHavingAMission())
        {
            marchant.StartMission();
        }
    }

    private void siffler(string main, bool state)
    {
        if (state && !playingSiffle)
        {
            source.PlayOneShot(siffle);
            playingSiffle = true;
            StartCoroutine(DelaiAnim());
        }
    }

    private void sprint(string main, bool state)
    {
        if (state)
        {
            moveProvider.moveSpeed = 4;
        }
        else
        {
            moveProvider.moveSpeed = 1;
        }
    }

    private IEnumerator DelaiAnim()
    {
        yield return new WaitForSeconds(3);
        playingSiffle = false;
    }

    private void NotUse(string main, bool state) { }
    private void NotUse(string main, float value) { }
    private void NotUse(string main, Vector2 value) { }
    private void NotUse(string main, Vector3 value) { }

    private void Delai()
    {
        peutAgir = true;
    }
}