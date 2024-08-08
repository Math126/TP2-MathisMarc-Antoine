using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class TableJeux : MonoBehaviour
{
    public List<Transform> SpawnPrefabLocation;
    public GameObject Prefab;
    public Canvas rules, bouton, Score, startCanvas;

    private float DelaiTime = 5;
    private float TimeLiveBtn = 0, TimeLiveScore = 0, TimeLiveKnife = 0;
    private bool PeutAfficherBouton = true, PeutSpawnCouteau = true, PeutAfficherScore = true;
    private bool PartieEnCours;
    private int score = 0;

    private void Start()
    {
        rules.enabled = false;
        bouton.enabled = false;
        Score.enabled = false;
    }

    private void Update()
    {
        TimeLiveBtn += Time.deltaTime;
        TimeLiveScore += Time.deltaTime;
        TimeLiveKnife += Time.deltaTime;

        if (TimeLiveBtn >= DelaiTime)
            PeutAfficherBouton = true;

        if(TimeLiveKnife >= DelaiTime)
            PeutSpawnCouteau = true;

        if (TimeLiveScore >= DelaiTime)
            PeutAfficherScore = true;

        Score.GetComponentInChildren<TextMeshProUGUI>().text = "Score : " + score.ToString();
    }

    public void ModifScore(int points)
    {
        score += points;
    }

    public void StartAGame()
    {
        if (!PartieEnCours)
        {
            startCanvas.enabled = false;
            score = 0;
            rules.enabled = true;
            foreach (Transform spawn in SpawnPrefabLocation)
            {
                Instantiate(Prefab, spawn.transform.position, Quaternion.identity);
            }
            PartieEnCours = true;
        }
    }

    public void FinirPartie()
    {
        if (PartieEnCours)
        {
            PartieEnCours = false;
            rules.enabled = false;
            Score.enabled = false;
            bouton.enabled = false;

            foreach (GameObject knife in GameObject.FindGameObjectsWithTag("Couteau"))
            {
                Destroy(knife);
            }
        }
    }

    public void SpawnKnife()
    {
        if (PartieEnCours && PeutSpawnCouteau)
        {
            Instantiate(Prefab, SpawnPrefabLocation[1].transform.position, Quaternion.identity);
            PeutSpawnCouteau = false;
            TimeLiveKnife = 0;
        }
    }

    public void afficherBtn()
    {
        if (PartieEnCours && PeutAfficherBouton)
        {
            if (bouton.isActiveAndEnabled)
                bouton.enabled = false;
            else
                bouton.enabled = true;

            PeutAfficherBouton = false;
            TimeLiveBtn = 0;
        }
    }

    public void afficherScore()
    {
        if (PartieEnCours && PeutAfficherScore)
        {
            if (Score.isActiveAndEnabled)
                Score.enabled = false;
            else
                Score.enabled= true;

            PeutAfficherScore = false;
            TimeLiveScore = 0;
        }
    }
}
