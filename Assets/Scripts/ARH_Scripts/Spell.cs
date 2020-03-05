using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb = default;
    [SerializeField] Transform tr = default;
    [SerializeField] private float spellSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        //rb.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2 (spellSpeed, 0);
        Debug.Log(rb.velocity);

    }
}
