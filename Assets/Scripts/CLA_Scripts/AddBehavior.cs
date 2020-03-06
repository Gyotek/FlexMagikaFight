using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBehavior : MonoBehaviour
{
    public bool canMove;
    public SpellManager.SpellElement element;

    public float maxHealth;
    private float realHealth;

    [SerializeField]
    private SpriteRenderer sprRenderer;

    private List<Transform> spawners;

    public int index;

    private void Awake()
    {
        spawners = new List<Transform>();

        spawners.Add(GameObject.Find("AddSpawner").transform);
        spawners.Add(GameObject.Find("AddSpawner1").transform);
        spawners.Add(GameObject.Find("AddSpawner2").transform);

        sprRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        realHealth = maxHealth;
        ChangeSkin();
        PlaceOnSpot();
    }

    private void Update()
    {
        if (realHealth <= 0) DestroySelf();
    }

    private void ChangeSkin()
    {
        switch(element)
        {
            case SpellManager.SpellElement.Fire:
                sprRenderer.color = Color.red;
                break;
            case SpellManager.SpellElement.Plant:
                sprRenderer.color = Color.green;
                break;
            case SpellManager.SpellElement.Water:
                sprRenderer.color = Color.blue;
                break;
        }
    }

    public void SwitchPos()
    {
        if (canMove)
        {
            for (int i = 0; i < spawners.Count; i++)
            {
                spawners[i].gameObject.GetComponent<Spawner>().taken = false;
            }

            var check = true;
            while (check)
            {
                var spawner = Random.Range(0, spawners.Count);
                if (!spawners[spawner].gameObject.GetComponent<Spawner>().taken)
                {
                    check = false;
                    transform.position = spawners[spawner].position;
                    transform.rotation = spawners[spawner].rotation;
                    spawners[spawner].gameObject.GetComponent<Spawner>().taken = true;
                }
            }
        }
    }

    private void PlaceOnSpot()
    {
        for (int i = 0; i < spawners.Count; i++)
        {
            spawners[i].gameObject.GetComponent<Spawner>().taken = false;
        }

        var check = true;
        while (check)
        {
            var spawner = Random.Range(0, spawners.Count);
            if (!spawners[spawner].gameObject.GetComponent<Spawner>().taken)
            {
                check = false;
                transform.position = spawners[spawner].position;
                transform.rotation = spawners[spawner].rotation;
                spawners[spawner].gameObject.GetComponent<Spawner>().taken = true;
            }
        }
    }

    public void TakeDamage(float damage, SpellManager.SpellElement spellElement)
    {
        switch (spellElement)
        {
            case SpellManager.SpellElement.Fire:
                switch (element)
                {
                    case SpellManager.SpellElement.Fire:
                        damage *= 1;
                        break;
                    case SpellManager.SpellElement.Plant:
                        damage *= 2;
                        break;
                    case SpellManager.SpellElement.Water:
                        damage *= 0;
                        break;
                }
                break;
            case SpellManager.SpellElement.Plant:
                switch (element)
                {
                    case SpellManager.SpellElement.Fire:
                        damage *= 0;
                        break;
                    case SpellManager.SpellElement.Plant:
                        damage *= 1;
                        break;
                    case SpellManager.SpellElement.Water:
                        damage *= 2;
                        break;
                }
                break;
            case SpellManager.SpellElement.Water:
                switch (element)
                {
                    case SpellManager.SpellElement.Fire:
                        damage *= 2;
                        break;
                    case SpellManager.SpellElement.Plant:
                        damage *= 0;
                        break;
                    case SpellManager.SpellElement.Water:
                        damage *= 1;
                        break;
                }
                break;
        }
        realHealth -= damage;
    }

   
    public void DestroySelf()
    {
        BossBehavior.Instance.newAdds.RemoveAt(index);
        Destroy(this.gameObject);
    }
}
