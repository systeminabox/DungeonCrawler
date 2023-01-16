using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwingAttack : MonoBehaviour
{
    public GameObject target;
    public GameObject player;
    public float age = 0;
    public float initAngle = 0.0f;

    Quaternion pos;
    // Start is called before the first frame update
    void Start()
    {
        //initAngle = transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        //Rigidbody2D body;
        age += 1 * Time.deltaTime;
        //body = target.GetComponent<Rigidbody2D>();
        pos = target.transform.rotation;
        target.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Min((target.transform.rotation.z + 720 * age),90) + initAngle));
        if (age > 0.25)
        {
            Destroy(target);
            player.GetComponent<Move>().isSwingin = false;
        }
    }
}
