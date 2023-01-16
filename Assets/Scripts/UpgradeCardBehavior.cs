using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class UpgradeCardBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject card;
    private bool mouseOver = false;
    public int tier = 0;
    public int upgradeID = 0;
    public string upgrade = "Blade";

    string[] listUpgradeNames = new string[] {"Blade", "Speed", "Haste", "Health", "Evasion"};

    GameObject desc;
    GameObject cardname;

    float scaleUp = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        upgradeID = UnityEngine.Random.Range(0, 4);
        
        desc = card.transform.Find("Description").gameObject;
        desc.GetComponent<TextMeshProUGUI>().text = "upgrade" + upgradeID + "_desc";
        cardname = card.transform.Find("CardName").gameObject;
        cardname.GetComponent<TextMeshProUGUI>().text = "upgrade" + upgradeID;
        card.GetComponent<UpgradeCardBehavior>().tier = UnityEngine.Random.Range(0, 2);
        cardname.GetComponent<LocalizeStringEvent>().StringReference.TableEntryReference = cardname.GetComponent<TextMeshProUGUI>().text;
        desc.GetComponent<LocalizeStringEvent>().StringReference.TableEntryReference = desc.GetComponent<TextMeshProUGUI>().text;
        cardname.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
        desc.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
        //myStringReference.StringReference.TableEntryReference = "TEST";
        // public LocalizedString localizedString = new LocalizedString  TableReference = "My String Table Collection", TableEntryReference = "My Text 1";
    }

    // Update is called once per frame
    void Update()
    {
        if (card.GetComponent<UpgradeCardBehavior>().mouseOver == true)
        {
            if (scaleUp < 0.2f)
            {
                card.transform.localScale = new Vector3(1 * (1 + scaleUp), 2 * (1 + scaleUp), 1.0f);
                scaleUp += 1f * Time.deltaTime;
            }
        }
        if (card.GetComponent<UpgradeCardBehavior>().mouseOver == false)
        {
            if (scaleUp > 0f)
            {
                card.transform.localScale = new Vector3(1 * (1 + scaleUp), 2 *(1 + scaleUp), 1.0f);
                scaleUp -= 1f * Time.deltaTime;
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        Debug.Log("Mouse exit");
    }
    //when the player clicks on an upgrade, use sin to increase the selected boon slightly, and shrink the unselected
}
