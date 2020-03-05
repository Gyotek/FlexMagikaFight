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

    private void Update()
    {
        if(Input.GetKeyDown("t"))
        { SwitchPos(); }
    }

    public void SwitchPos()
    {
        if (canMove)
        {
            var spawner = 0;
            while (!spawners[spawner].gameObject.GetComponent<Spawner>().taken)
            {
                spawner = Random.Range(0, spawners.Count);
                transform.position = spawners[spawner].position;
                transform.rotation = spawners[spawner].rotation;
            }
        }
    }

    public void DestroySelf()
    {
        Destroy(this);
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
