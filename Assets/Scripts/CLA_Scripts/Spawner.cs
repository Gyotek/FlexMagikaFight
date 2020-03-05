using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool taken;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        taken = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        taken = false;
    }
}
