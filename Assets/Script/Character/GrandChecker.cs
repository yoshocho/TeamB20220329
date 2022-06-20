using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ڒn������R���|�[�l���g
/// </summary>
public class GrandChecker : MonoBehaviour
{
    /// <summary>
    /// �ڒn����
    /// </summary>
    public bool IsGrand { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Grand") return;
        IsGrand = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Grand") return;
        IsGrand = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Grand") return;
        IsGrand = true;
    }
}
