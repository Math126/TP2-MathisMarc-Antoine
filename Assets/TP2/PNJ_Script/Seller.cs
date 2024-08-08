using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Seller : MonoBehaviour
{
    public int PieceObtenu;
    public List<Material> materialsMission;
    public GameObject activeMission;
    public MainPlayerMission playerMission;
    public List<string> tagObjToGet;
    public GameObject card;
    public Transform CardSpawn;

    private Animator animator;
    private int nbGot;
    private bool GotMission = false, AlreadyDone = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartMission()
    {
        if (!AlreadyDone)
        {
            playerMission.ChangeCardMission(materialsMission);
            playerMission.SetHavingAMission(true);
            activeMission.SetActive(false);
            GotMission = true;
            card.transform.position = CardSpawn.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GotMission)
        {
            for (int i = 0; i < tagObjToGet.Count; i++)
            {
                if (other.CompareTag(tagObjToGet[i]))
                {
                    Destroy(other.gameObject);
                    nbGot++;
                }
            }
        }
    }

    private void Update()
    {
        if(nbGot >= tagObjToGet.Count && GotMission) //Mission Reussi
        {
            animator.SetBool("Clap", true);
            playerMission.AddMoney(PieceObtenu);
            playerMission.RefreshCard();
            playerMission.SetHavingAMission(false);
            AlreadyDone = true;
            GotMission = false;
        }
    }
}