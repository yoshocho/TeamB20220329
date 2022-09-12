using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SizeCtrl : MonoBehaviour
{

    [SerializeField, Header("減少、増大倍率")]
    float _magnification = 0.1f;
    [SerializeField, Header("プレイヤーが大きくなれる最大値")]
    float _maxMagnification = 3.0f;
    [SerializeField, Header("プレイヤーが小さくなる最小値")]
    float _minimumMagnification = 0.5f;
    [SerializeField, Header("分身オブジェクト")]
    GameObject _cloneObj;
    [SerializeField, Header("ボタンを押せる大きさ")]
    float _canPushMagnification = 0.7f;

    public Vector3 MaxSize { get; private set; }
    public Vector3 MinimumSize { get; private set; }
    Vector3 _canPushSize;
    public bool CanPush { get; private set; } = true;

    public void SetUp()
    {
        MaxSize = new Vector3(_maxMagnification, _maxMagnification, 1.0f);
        MinimumSize = new Vector3(_minimumMagnification, _minimumMagnification, 1.0f);
        _canPushSize = new Vector3(_canPushMagnification, _canPushMagnification, 1.0f);
        if (_canPushSize.x >= transform.localScale.x || _canPushSize.y >= transform.localScale.y) CanPush = false;
    }

    public void OnCollisionEvent(Collision2D col)
    {
        Debug.Log("Hit");
        if (!col.collider.CompareTag("Clone")) return;
        Debug.Log("CloneにHit");
        Clone clone = col.collider.GetComponent<Clone>();
        if (!clone.CanCatch) return;

        if (MaxSize.x <= transform.localScale.x || MaxSize.y <= transform.localScale.y)
        {
            transform.localScale = MaxSize;
            if (_canPushSize.x <= transform.localScale.x || _canPushSize.y <= transform.localScale.y) CanPush = true;
            Debug.Log("最大Size");
            return;
        }
        Debug.Log("大きくなる");
        transform.localScale += new Vector3(_magnification, _magnification, 0.0f);
        if (_canPushSize.x <= transform.localScale.x || _canPushSize.y <= transform.localScale.y) CanPush = true;
        Destroy(col.gameObject);
    }

    /// <summary>
    /// 分身を生成する
    /// </summary>
    public void CreateClone(bool playerDir,float shootSpeed = 5.0f,Action onShoot = null)
    {
        Debug.Log("Create");
        Vector2 dir = new Vector2(1, 0);
        if (MinimumSize.x >= transform.localScale.x || MinimumSize.y >= transform.localScale.y)
        {
            transform.localScale = MinimumSize;
            if (_canPushSize.x >= transform.localScale.x || _canPushSize.y >= transform.localScale.y) CanPush = false;
            Debug.Log("最小Size");
            return;
        }
        Debug.Log("小さくなる");
        transform.localScale -= new Vector3(_magnification, _magnification, 0.0f);
        if (_canPushSize.x >= transform.localScale.x || _canPushSize.y >= transform.localScale.y) CanPush = false;
        onShoot?.Invoke();
        if (playerDir)
        {
            var cloneobj = Instantiate(_cloneObj, new Vector3(transform.position.x - 0.5f, transform.position.y
                , transform.position.z), Quaternion.identity);
            var clone = cloneobj.GetComponent<Clone>();
            clone.SetUp();
            clone.Shoot(-dir * shootSpeed);
        }
        else
        {
            var cloneobj = Instantiate(_cloneObj, new Vector3(transform.position.x + 0.5f, transform.position.y
               , transform.position.z), Quaternion.identity);
            var clone = cloneobj.GetComponent<Clone>();
            clone.SetUp();
            clone.Shoot(dir * shootSpeed);
        }
    }
}
