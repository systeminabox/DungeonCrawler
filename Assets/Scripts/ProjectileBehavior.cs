using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ProjectileBehavior : MonoBehaviour
{
    Rigidbody2D hitbox;
    public float speed = 0.0f;
    public float lifeSpan = 0.0f;
    public float maxLife = 1.0f;
    public GameObject projectile;
    public int rotation = 0;
    public bool real = false;
    // Start is called before the first frame update
    void Start()
    {
        //set angle to look at cursor
        hitbox = GetComponent<Rigidbody2D>();
        //Set the speed of the GameObject
        //speed = 40.0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Wall")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Hit a wall!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (projectile.activeSelf == true)
        {
            //Vector3 pos = projectile.transform.position;
            //float rot = projectile.transform.rotation.z;
            lifeSpan += 1.0f * Time.deltaTime;
            //hitbox.velocity = transform.up * speed;
            //pos = projectile.transform.forward;
            if (lifeSpan >= maxLife)
            {
                Destroy(projectile);
            }
            projectile.transform.position += transform.up * (speed*Time.deltaTime);
            
        }
    }
}
