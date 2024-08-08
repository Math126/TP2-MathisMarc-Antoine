using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class MainPlayerMission : MonoBehaviour
{
    public GameObject CardMission;
    public GameObject PrefabCard;
    public TextMeshPro QteMoney;
    private int Money = 0;
    private bool havingAMission = false;

    public void ChangeCardMission(List<Material> goalObjects)
    {
        RefreshCard();

        for (int i = 0; i < goalObjects.Count; i++)
        {
            GameObject card = Instantiate(PrefabCard);

            card.transform.SetParent(CardMission.transform);
            card.GetComponent<RectTransform>().localPosition = new Vector3(4.7f, -5f + (i * -5.5f), 0);
            card.GetComponent<RectTransform>().localScale = Vector3.one;
            card.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0,0,0);

            card.GetNamedChild("Image").GetComponent<Renderer>().material = goalObjects[i];
        }
    }

    public void RefreshCard()
    {
        foreach (Transform child in CardMission.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddMoney(int Money)
    {
        this.Money += Money;
        QteMoney.text = "Score: " + this.Money;
    }

    public void SetHavingAMission(bool state)
    {
        havingAMission = state;
    }

    public bool GetHavingAMission()
    {
        return havingAMission;
    }
}