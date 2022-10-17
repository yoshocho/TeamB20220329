using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの分身クラス
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Clone : MonoBehaviour,IRBCtrl
{
    [SerializeField]
    float _waitTimer = 1.0f;
    Rigidbody2D _rb;
    public bool CanCatch { get; private set; } = false;

    public Rigidbody2D RB => _rb;

    private void Start()
    {
        StartCoroutine(WaitTimer());
    }
    public void SetUp()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector2 dir)
    {
        _rb.AddForce(dir,ForceMode2D.Impulse);
    }

    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(_waitTimer);
        CanCatch = true;
        yield return null;
    }
}
