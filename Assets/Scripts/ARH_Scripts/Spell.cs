using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb = default;
    [SerializeField] Transform tr = default;
    [SerializeField] SpriteRenderer sprite = default;
    [SerializeField] private float spellSpeed = 4;

    public bool isCircle = false;
    [SerializeField] GameObject circle = default;
    public bool isPerforant = false;
    public bool isBouncing = false;
    public PhysicsMaterial2D bounce = default;


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

        switch (SpellManager.instance.actualZone)
        {
            case SpellManager.SpellZone.Circle:
                isCircle = true;
                break;
            case SpellManager.SpellZone.Line:
                break;
            case SpellManager.SpellZone.Multiple:
                break;
            default:
                break;
        }

        switch (SpellManager.instance.actualType)
        {
            case SpellManager.SpellType.Bounce:
                rb.sharedMaterial = bounce;
                break;
            case SpellManager.SpellType.Impact:
                transform.localScale = new Vector3(transform.localScale.x * 1.5f, transform.localScale.y * 1.5f, transform.localScale.z);
                break;
            case SpellManager.SpellType.Perforant:
                isPerforant = true;
                break;
            default:
                break;
        }

        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 dir = (mousePos - pos);
        rb.AddForce(dir * spellSpeed * 100);
    }

    void MakeDamage(Transform cible)
    {
        float damage = 0;
        switch (SpellManager.instance.actualType)
        {
            case SpellManager.SpellType.Bounce:
                damage = 3;
                break;
            case SpellManager.SpellType.Impact:
                damage = 2;
                break;
            case SpellManager.SpellType.Perforant:
                damage = 4;
                break;
        }
        switch (SpellManager.instance.actualZone)
        {
            case SpellManager.SpellZone.Line:
                damage *= 2;
                break;
            case SpellManager.SpellZone.Multiple:
                damage *= 0.5f;
                break;
        }
        cible.GetComponent<BossBehavior>().TakeDamage(damage, SpellManager.instance.actualElement);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.sharedMaterial != null)
        {
            rb.sharedMaterial = null;
        }

        if (collision.gameObject.GetComponent<BossBehavior>() || collision.gameObject.GetComponent<AddBehavior>())
        {
            Transform cible = collision.transform;
            if (isPerforant)
            {
                isPerforant = false;
                return;
            }
            
            if (isCircle)
            {
                isCircle = false;
                circle.SetActive(true);
            }

            MakeDamage(cible);
            if (!isBouncing)
                Destroy(this.gameObject);
            else
                isBouncing = false;
        }
    }
}
