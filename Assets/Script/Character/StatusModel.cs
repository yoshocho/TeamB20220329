using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusModel
{
    [SerializeField, Tooltip("�̗͒l")]
    public int hp = 0;
    public int MaxHp { get; private set; }
    public void SetUp(int maxHp)
    {
        MaxHp = maxHp;
    }
}
