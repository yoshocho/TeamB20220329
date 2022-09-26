using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAir : GimmickBase
{
    [SerializeField]
    float _upSpeed = 2.0f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        IRBCtrl rb;
        Debug.Log("UpAir");
        if (collision.TryGetComponent(out rb))
        {
            Debug.Log("aru");
            rb.RB.AddForce(Vector2.up * _upSpeed, ForceMode2D.Impulse);
        }
    }
}
