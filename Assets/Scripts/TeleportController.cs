using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public Transform SnakeHead;
    public TeleportController TeleportTo;
    private float Cooldown = 0;
    private Vector3 CellOffset = new Vector3(0, -0.83f, 0);

    private void Update()
    {
        Cooldown -= Time.deltaTime;
        if (Cooldown < 0)
            Cooldown = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Snake Head" && Cooldown == 0)
        {
            TeleportTo.Cooldown = 5;
            SnakeHead.position = TeleportTo.gameObject.transform.position - CellOffset;
        }
    }
}
