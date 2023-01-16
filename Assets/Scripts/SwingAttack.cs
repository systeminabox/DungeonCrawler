using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingAttack : MonoBehaviour
{
    public GameObject target;
    public float age = 0;
    public float initAngle = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        //initAngle = transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D body;
        age += 1 * Time.deltaTime;
        body = target.GetComponent<Rigidbody2D>();
        body.rotation += 1;
        if (body.transform.rotation.z > 90)
        {
            Destroy(target);
        }
    }
}
