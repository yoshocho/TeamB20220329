using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class CollisionEvent : MonoBehaviour
{
    public delegate void CollisionEv(Collision2D collision);
    
    CollisionEv _OnEnterEv;
    
    CollisionEv _OnExitEv;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _OnEnterEv?.Invoke(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _OnExitEv?.Invoke(collision);
    }

    public CollisionEvent SetCollisionEnterEv(CollisionEv onColliEnter)
    {
        _OnEnterEv += onColliEnter;
        return this;
    }
    public CollisionEvent SetCollisionExitEv(CollisionEv onColliExit)
    {
        _OnExitEv += onColliExit;
        return this;
    }
}
