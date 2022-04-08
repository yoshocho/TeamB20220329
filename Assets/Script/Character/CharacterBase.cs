using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour,IDamage
{
    [SerializeField]
    protected StatusModel _status;

    public virtual void GetDamage(int damage) { }

}
