using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{
    public GameObject greyOut;
    public GameObject swordMask;
    public GameObject Player;

    public float shakeAmp = 0;
    public float shakeTime  =0;
    float xOffset;
    // Start is called before the first frame update
    void Start()
    {
        xOffset = greyOut.transform.position.x;
    }
    public void UpdateSword()
    {
      
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 pos;
        
        //Debug.Log("Desired position : " + (Player.GetComponent<Move>().charge/6.25f) * 100);
        if (Player.GetComponent<Move>().hasSword == true)
        {
            //Debug.Log("has sword");
            greyOut.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        if (Player.GetComponent<Move>().hasSword == false)
        {
            //Debug.Log("lost sword :(");
            greyOut.GetComponent<Image>().color = new Color32(140, 140, 140, 255);
        }
        if (Player.GetComponent<Move>().charge >= Player.GetComponent<Move>().chargeMax)
        {
            if (shakeAmp < 10)
            {
                shakeAmp += 3.3f * Time.deltaTime;
            }
            shakeTime += 0.1f;
            greyOut.transform.position = new Vector3((Mathf.Sin(shakeTime) * shakeAmp) + xOffset, greyOut.transform.position.y, 0.0f);
                
        }
        if (Player.GetComponent<Move>().charge == 0)
        {
            shakeAmp = 0;
            shakeTime = 0;
            greyOut.transform.position = new Vector3(xOffset, greyOut.transform.position.y, 0.0f);
        }
        pos = greyOut.transform.position;
        swordMask.transform.position = new Vector3(pos.x, greyOut.transform.position.y + (Mathf.Min(((Player.GetComponent<Move>().charge / Player.GetComponent<Move>().chargeMax) * 50.0f), 50.0f) - 50.0f), 0.0f);
    }
}
