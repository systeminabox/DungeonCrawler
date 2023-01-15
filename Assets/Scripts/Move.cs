using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Move : MonoBehaviour
{
    //player movespeed
    public float speed = 10.0f;
    //is the player charging their sword throw? used for stopping movement
    private bool isChargingUp = false;
    //how fast the sword is readied
    public float chargeRate = 2.0f;
    //the sword's curent charge
    public float charge = 0.0f;
    //number required before the launch event is fired
    public float chargeMax = 1.25f;
    Rigidbody2D plr;
    public GameObject Projectile;
    public GameObject Player;
    public UIUpdater updater;
    public float mouseX = 0.0f;
    public float mouseY = 0.0f;
    private Vector2 worldPosition = Vector2.zero;

    float horizontal;
    float vertical;

    public bool hasSword = true;

    private void Start()
    {
        plr = GetComponent<Rigidbody2D>();
    }
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
        hitbox.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -rot));
        ProjectileBehavior pb = clone.GetComponent<ProjectileBehavior>();
        //min(max(minpower, (power*2) * 40.0f), maxpower)
        pb.speed = Mathf.Min(Mathf.Max(15.0f,(power*2.0f) * 40.0f), 120.0f);
        clone.transform.parent = null;
        updater.UpdateSword();

        Debug.Log("created clone @ " +transform.position + "with rotation " +rot + " with power " +power);
    }

    //throwing the sword stops movement, but has like no wind up
    void Update()
    {
        //Movement
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        //Aim
        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        
        if (isChargingUp == true)
        {
            //Cancles the input
            if (Input.GetKeyDown("w") || Input.GetKeyDown("s") || Input.GetKeyDown("a") || Input.GetKeyDown("d") || Input.GetKeyUp(KeyCode.Mouse1) && charge < chargeMax)
            {
                isChargingUp = false;
                charge = 0;
                //this should make the player stop charging the throw.
            }
            //Throws the sword
            if (Input.GetKeyUp(KeyCode.Mouse1) && charge >= chargeMax)
            {
                isChargingUp = false;
                Launch(worldPosition.x, worldPosition.y, charge);
                hasSword = false;
                charge = 0;
            }
            
        }
        if (hasSword == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                isChargingUp = true;
            }
            if (Input.GetKey(KeyCode.Mouse1) && isChargingUp == true)
            {
                //if (charge <= chargeMax)
                //{
                charge += chargeRate * Time.deltaTime;
                //Debug.Log("charge is " + charge);
               // }
                if (charge > chargeMax + 3)
                {
                Launch(worldPosition.x, worldPosition.y, charge);
                    hasSword = false;
                    charge = 0;
                //Debug.Log(+worldPosition.x + ", " + worldPosition.y);
                //charge = 0;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (isChargingUp == false)
        {
            if (horizontal != 0 && vertical != 0) //reduces movement speed by 30% when moving diagonally
            {
                horizontal *= 0.7f;
                vertical *= 0.7f;
            }
            plr.velocity = new Vector2(horizontal * speed, vertical * speed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit trigger " + collision.name);
        if (collision.name == "ReturnSword(Clone)")
        {
            hasSword = true;
            Destroy(collision.gameObject);
            updater.UpdateSword();
        }
    }
}