using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class PlayerAction : MonoBehaviour
{
    public GameObject JeuxCouteau;
    public TextMeshPro PositionHead, RotationHead, SpeedHead, PositionLeftHand, LeftGripperValue, LeftTriggerValue, LeftThumbstickValue, PositionRightHand, RightGripperValue, RightTriggerValue, RightThumbstickClickState;
    public List<Transform> SpawnPoint;
    private int indexSpawn = 0;
    private float TimeLive = 0, delaiSpawn = 2;
    private bool SonDejaEffectuer = false, PeutSpawn = true;
    private bool PeutAgir;
    private TableJeux table;
    private Knife knifeLeft, knifeRight;
    private XRInputManager manager;

    void Start()
    {
        table = JeuxCouteau.GetComponent<TableJeux>();
        manager = GameObject.FindWithTag("Player").GetComponent<XRInputManager>();
        Invoke("Delai", 0.1f);

        XRInputManager.Hand.onPrimaryChange += PrimaryChange;
        XRInputManager.Hand.onSecondaryChange += SecondaryChange;
        XRInputManager.Hand.onGripperStateChange += GripperChange;
        XRInputManager.Hand.onTriggerStateChange += TriggerChange;
        XRInputManager.Hand.onThumbstickClickStateChange += ThumbstickClickChange;
        XRInputManager.Hand.onThumbstickTouchStateChange += ThumbstickTouchChange;
        XRInputManager.Hand.onGripperValueChange += GripperValueChange;
        XRInputManager.Hand.onTriggerValueChange += TriggerValueChange;
        XRInputManager.Hand.onThumbstickValueChange += ThumbstickValueChange;
        XRInputManager.Hand.onHandPositionChange += HandPositionChange;
        XRInputManager.Hand.onHandRotationChange += HandRotationChange;
        XRInputManager.Hand.onHandSpeedChange += HandSpeedChange;

        XRInputManager.Head.onHeadPositionChange += HeadPositionChange;
        XRInputManager.Head.onHeadRotationChange += HeadRotationChange;
        XRInputManager.Head.onHeadSpeedChange += HeadSpeedChange;

        Knife.onCouteauTenuDroite += KnifeDansMainDroit;
        Knife.onCouteauTenuGauche += KnifeDansMainGauche;
    }

    private void Update()
    {
        TimeLive += Time.deltaTime;

        if (TimeLive >= delaiSpawn)
        {
            PeutSpawn = true;
        }

        //fonction Right
        if (manager.LeftDeviceFind && PeutAgir)
        {
            PrimaryChange("RightHand", manager.GetPrimaryBouton("RightHand"));
            SecondaryChange("RightHand", manager.GetSecondaryBouton("RightHand"));
            GripperChange("RightHand", manager.GetGripperState("RightHand"));
            TriggerChange("RightHand", manager.GetTriggerState("RightHand"));
            ThumbstickClickChange("RightHand", manager.GetThumbstickClick("RightHand"));
            ThumbstickTouchChange("RightHand", manager.GetThumbstickTouch("RightHand"));
            GripperValueChange("RightHand", manager.GetGripperValue("RightHand"));
            TriggerValueChange("RightHand", manager.GetTriggerValue("RightHand"));
            ThumbstickValueChange("RightHand", manager.GetThumbstickValue("RightHand"));
            HandPositionChange("RightHand", manager.GetPositionValue("RightHand"));
            HandRotationChange("RightHand", manager.GetRotationValue("RightHand"));
            HandSpeedChange("RightHand", manager.GetSpeedValue("RightHand"));
        }

        //Fonction Gauche
        if (manager.RightDeviceFind && PeutAgir)
        {
            PrimaryChange("LeftHand", manager.GetPrimaryBouton("LeftHand"));
            SecondaryChange("LeftHand", manager.GetSecondaryBouton("LeftHand"));
            GripperChange("LeftHand", manager.GetGripperState("LeftHand"));
            TriggerChange("LeftHand", manager.GetTriggerState("LeftHand"));
            ThumbstickClickChange("LeftHand", manager.GetThumbstickClick("LeftHand"));
            ThumbstickTouchChange("LeftHand", manager.GetThumbstickTouch("LeftHand"));
            GripperValueChange("LeftHand", manager.GetGripperValue("LeftHand"));
            TriggerValueChange("LeftHand", manager.GetTriggerValue("LeftHand"));
            ThumbstickValueChange("LeftHand", manager.GetThumbstickValue("LeftHand"));
            HandPositionChange("LeftHand", manager.GetPositionValue("LeftHand"));
            HandRotationChange("LeftHand", manager.GetRotationValue("LeftHand"));
            HandSpeedChange("LeftHand", manager.GetSpeedValue("LeftHand"));
        }
    }

    //Event
    public void PrimaryChange(string main, bool state) //Démarrer Jeux Couteau
    {
        if (state)
        {
           table.StartAGame();
        }
    }

    public void SecondaryChange(string main, bool state) //Mettre Fin à la partie
    {
        if (state)
        {
            table.FinirPartie();
        }
    }

    public void GripperChange(string main, bool state) //Si attrape couteau, faire un son
    {
        if (main == "LeftHand" && knifeLeft != null && !SonDejaEffectuer)
        {
            SonDejaEffectuer = true;
            knifeLeft.MakeSound();
        }
        else if(main == "RightHand" && knifeRight != null && !SonDejaEffectuer)
        {
            SonDejaEffectuer = true;
            knifeRight.MakeSound();
        }
    }

    public void TriggerChange(string main, bool state) //Montre les boutons pour la game
    {
        table.afficherBtn();
    }

    public void ThumbstickClickChange(string main, bool state) //Spawn 1 Couteau
    {
        if(main == "LeftHand")
        {
            table.SpawnKnife();
        }
        else 
        {
            RightThumbstickClickState.text = "Thumbstick Click : " + state;
        }
    }

    public void ThumbstickTouchChange(string main, bool state) // afficher score
    {
        table.afficherScore();
    }

    public void GripperValueChange(string main, float value) //Afficher
    {
        if (main == "LeftHand")
        {
            LeftGripperValue.text = "Gripper Value : " + value;
        }
        else
        {
            RightGripperValue.text = "Gripper Value : " + value;
        }
    }

    public void TriggerValueChange(string main, float value) //Afficher
    {
        if (main == "LeftHand")
        {
            LeftTriggerValue.text = "Trigger Value : " + value;
        }
        else
        {
            RightTriggerValue.text = "Trigger Value : " + value;
        }
    }

    public void ThumbstickValueChange(string main, Vector2 value)
    {
        if (main == "LeftHand")
        {
            LeftThumbstickValue.text = "Thumbstick Value : " + value;
        }
        else if (main == "RightHand" && PeutSpawn && value != new Vector2(0, 0))
        {
            indexSpawn++;

            if (indexSpawn == 3)
            {
                indexSpawn = 0;
            }

            transform.position = SpawnPoint[indexSpawn].position;
            PeutSpawn = false;
            TimeLive = 0;
        }
    }

    public void HandPositionChange(string main, Vector3 value) //Afficher
    {
        if (main == "LeftHand")
        {
            PositionLeftHand.text = "Position : " + (Math.Round(value.x, 2), Math.Round(value.y, 2), Math.Round(value.z, 2));
        }
        else if (main == "RightHand")
        {
            PositionRightHand.text = "Position : " + (Math.Round(value.x, 2), Math.Round(value.y, 2), Math.Round(value.z, 2));
        }
    }

    public void HandRotationChange(string main, Vector3 value) //change la rotation du couteau au lancé
    {
        if (main == "LeftHand" && knifeLeft != null)
        {
            knifeLeft.ChangerRotationObjet(value);
        }
        else if (main == "RightHand" && knifeRight != null)
        {
            knifeRight.ChangerRotationObjet(value);
        }
    }

    public void HandSpeedChange(string main, Vector3 value) //change puissance appliquer sur couteau
    {
        if (main == "LeftHand" && knifeLeft != null)
        {
            knifeLeft.ChangeSpeedObjet(value);
        }
        else if (main == "RightHand" && knifeRight != null)
        {
            knifeRight.ChangeSpeedObjet(value);
        }
    }

    public void HeadPositionChange(string head, Vector3 value) //Afficher
    {
        PositionHead.text = "Position : " + (Math.Round(value.x, 2), Math.Round(value.y, 2), Math.Round(value.z, 2));
    }

    public void HeadRotationChange(string head, Vector3 value) //Afficher
    {
        RotationHead.text = "Rotation : " + (Math.Round(value.x, 2), Math.Round(value.y, 2), Math.Round(value.z, 2));
    }

    public void HeadSpeedChange(string head, Vector3 value) //Afficher
    {
        SpeedHead.text = "Vitesse : " + (Math.Round(value.x, 2), Math.Round(value.y, 2), Math.Round(value.z, 2));
    }

    public void KnifeDansMainDroit(Knife CouteauTenu)
    {
        knifeRight = CouteauTenu;
        SonDejaEffectuer = false;
    }

    public void KnifeDansMainGauche(Knife CouteauTenu)
    {
        knifeLeft = CouteauTenu;
        SonDejaEffectuer = false;
    }

    private void Delai()
    {
        PeutAgir = true;
    }
}