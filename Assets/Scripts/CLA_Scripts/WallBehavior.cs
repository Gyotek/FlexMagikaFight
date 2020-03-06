using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    public bool canMove;

    private List<Transform> spawners;

    private void Awake()
    {
        spawners = new List<Transform>();

        spawners.Add(GameObject.Find("Spawner").transform);
        spawners.Add(GameObject.Find("Spawner1").transform);
        spawners.Add(GameObject.Find("Spawner2").transform);
    }


    private void Start()
    {
        PlaceOnSpot();
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
        Destroy(this.gameObject);
    }


    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Spell>())
        {
            if(collision.gameObject.GetComponentInChildren<SpriteRenderer>().color == Color.red)
            {
                -> fire
            }
        }
    }*/


}
