using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBehavior : MonoBehaviour
{
    public bool canMove;
    public SpellManager.SpellElement element;

    [SerializeField]
    private SpriteRenderer sprRenderer;

    private List<Transform> spawners;

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
        ChangeSkin();
        PlaceOnSpot();
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

    public void DestroySelf()
    {
        Destroy(this);
    }
}
