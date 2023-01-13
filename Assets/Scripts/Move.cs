using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //player movespeed
    public float speed = 10.5f;
    //is the player charging their sword throw? used for stopping movement
    private bool isChargingUp = false;
    //how fast the sword is readied
    public float chargeRate = 1.0f;
    //the sword's curent charge
    private float charge = 0.0f;
    //number required before the launch event is fired
    //private float chargeMax = 6.25f;

    public GameObject Projectile;
    public float mouseX = 0.0f;
    public float mouseY = 0.0f;
    private Vector2 worldPosition = Vector2.zero;
    void Launch(float a, float b, float power)
    {
        GameObject clone;
        Rigidbody2D hitbox;
        Vector3 pos = transform.position;
        clone = Instantiate(Projectile, transform);
        hitbox = clone.GetComponent<Rigidbody2D>();
        clone.SetActive(true);
        clone.transform.position = pos;
        float rot = Mathf.Atan2(a - pos.x,b - pos.y) * Mathf.Rad2Deg;
        clone.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -rot));
        ProjectileBehavior pb = clone.GetComponent<ProjectileBehavior>();
        //min(max(minpower, (power*2) * 40.0f), maxpower)
        pb.speed = Mathf.Min(Mathf.Max(40.0f,(power*2.0f) * 40.0f), 120.0f);
        clone.transform.parent = null;

        Debug.Log("created clone @ " +transform.position + "with rotation " +rot + " with power " +power);
    }

    //throwing the sword stops movement, but has like no wind up
    void Update()
    {
        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 pos = transform.position;
        if (isChargingUp == false){ 
         if (Input.GetKey("w"))
            {
                pos.y += speed * Time.deltaTime;
            }
          if (Input.GetKey("s"))
        {
               pos.y -= speed * Time.deltaTime;
        }
         if (Input.GetKey("d"))
         {
                pos.x += speed * Time.deltaTime;
         }
         if (Input.GetKey("a"))
         {
               pos.x -= speed * Time.deltaTime;
         }
            transform.position = pos;

        }
        if (isChargingUp == true)
        {
            //Cancles the input
            if (Input.GetKeyDown("w") || Input.GetKeyDown("s") || Input.GetKeyDown("a") || Input.GetKeyDown("d") )
            {
                isChargingUp = false;
                charge = 0;
                //this should make the player stop charging the throw.
            }
            //Throws the sword
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                isChargingUp = false;
                Launch(worldPosition.x, worldPosition.y, charge);
                charge = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)) 
        {
            isChargingUp = true;
        }
        if (Input.GetKey(KeyCode.Mouse1) && isChargingUp == true)
        {
            //if (charge < chargeMax)
            //{
                charge += chargeRate * Time.deltaTime;
                //Debug.Log("charge is " + charge);
            //}
            //else
            //{
                //Launch(worldPosition.x, worldPosition.y, charge);
                //Debug.Log(+worldPosition.x + ", " + worldPosition.y);
                //charge = 0;
            //}
        }
        
    }
}