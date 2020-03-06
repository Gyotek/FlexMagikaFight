using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb = default;
    [SerializeField] Transform tr = default;
    [SerializeField] SpriteRenderer sprite = default;
    [SerializeField] private float spellSpeed = 4f;
    [SerializeField] private float destroyDelay = 1f;
    [SerializeField] private float CircleScalling = 10f;
    private Vector3 newScale;

    public bool isCircle = false;
    public bool isCircleing = false;
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
                transform.GetComponent<Collider2D>().isTrigger = false;
                rb.sharedMaterial = bounce;
                break;
            case SpellManager.SpellType.Impact:
                //transform.localScale = new Vector3(transform.localScale.x * 1.5f, transform.localScale.y * 1.5f, transform.localScale.z);
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

    private void Update()
    {
        if (isCircleing)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, newScale, 0.1f);
            StartCoroutine(DestroyCoroutine());
        }
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
        if (cible.GetComponent<BossBehavior>())
            cible.GetComponent<BossBehavior>().TakeDamage(damage, SpellManager.instance.actualElement);
        else if (cible.GetComponent<AddBehavior>())
            cible.GetComponent<AddBehavior>().TakeDamage(damage, SpellManager.instance.actualElement);
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            rb.sharedMaterial = null;
            transform.GetComponent<Collider2D>().isTrigger = true;

            if (collision.gameObject.GetComponent<BossBehavior>() || collision.gameObject.GetComponent<AddBehavior>())
            {
                Transform cible = collision.transform;
                MakeDamage(cible);
            }

            isBouncing = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<BossBehavior>() || collision.gameObject.GetComponent<AddBehavior>() || collision.gameObject.GetComponent<WallBehavior>())
        {
            Transform cible = collision.transform;
            if (isPerforant)
            {
                isPerforant = false;
                MakeDamage(cible);
                return;
            }
            else if (isCircle)
            {
                rb.velocity = Vector3.zero;
                newScale = transform.localScale * 10;
                MakeDamage(cible);
                isCircle = false;
                isCircleing = true;
                //circle.SetActive(true);
            }
            else
            {
                rb.velocity = Vector3.zero;
                MakeDamage(cible);
                StartCoroutine(DestroyCoroutine());
            }
        }

        if (isCircle)
        {
            rb.velocity = Vector3.zero;
            newScale = transform.localScale * 10;
            isCircle = false;
            isCircleing = true;
            //circle.SetActive(true);
        }
        else
        {
            rb.velocity = Vector3.zero;
            StartCoroutine(DestroyCoroutine());
        }
    }
}
/*
NullReferenceException: Object reference not set to an instance of an object
SliderPhase+<SliderDisplay>d__7.MoveNext() (at Assets/Scripts/AD_Script/SliderPhase.cs:33)
UnityEngine.SetupCoroutine.InvokeMoveNext(System.Collections.IEnumerator enumerator, System.IntPtr returnValueAddress) (at<f38c71c86aa64e299d4cea9fb7c715e1>:0)
UnityEngine.MonoBehaviour:StartCoroutine(IEnumerator)
SliderPhase:SliderTime() (at Assets/Scripts/AD_Script/SliderPhase.cs:25)
UnityEngine.EventSystems.EventSystem:Update() (at C:/Program Files/Unity/Hub/Editor/2019.3.0f6/Editor/Data/Resources/PackageManager/BuiltInPackages/com.unity.ugui/Runtime/EventSystem/EventSystem.cs:377)
*/