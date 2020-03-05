using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb = default;
    [SerializeField] Transform tr = default;
    [SerializeField] SpriteRenderer sprite = default;
    [SerializeField] private float spellSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        //transform.LookAt(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y));

        switch (SpellManager.instance.actualElement)
        {
            case SpellManager.SpellElement.Fire:
                sprite.color = Color.red;
                break;
            case SpellManager.SpellElement.Water:
                sprite.color = Color.blue;
                break;
            case SpellManager.SpellElement.Plant:
                sprite.color = Color.green;
                break;
            default:
                break;
        }

        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 dir = (mousePos - pos);
        rb.AddForce(dir * spellSpeed * 100);
    }

    void FixedUpdate()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.sharedMaterial != null)
        {
            rb.sharedMaterial = null;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
