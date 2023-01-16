using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnSword : MonoBehaviour
{
    GameObject plr;
    Rigidbody2D body;
    CapsuleCollider2D hitbox;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        plr = GameObject.Find("Player");
        //bring the sword back to the player
        //this functionality is kinda cool.
        float rot = Mathf.Atan2(plr.transform.position.x - pos.x, plr.transform.position.y - pos.y) * Mathf.Rad2Deg;
        body.rotation = -rot;
        body.transform.position += transform.up * (20.0f * Time.deltaTime);
    }
}
