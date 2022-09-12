using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SizeCtrl : MonoBehaviour
{

    [SerializeField, Header("�����A����{��")]
    float _magnification = 0.1f;
    [SerializeField, Header("�v���C���[���傫���Ȃ��ő�l")]
    float _maxMagnification = 3.0f;
    [SerializeField, Header("�v���C���[���������Ȃ�ŏ��l")]
    float _minimumMagnification = 0.5f;
    [SerializeField, Header("���g�I�u�W�F�N�g")]
    GameObject _cloneObj;
    [SerializeField, Header("�{�^����������傫��")]
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
        Debug.Log("Clone��Hit");
        Clone clone = col.collider.GetComponent<Clone>();
        if (!clone.CanCatch) return;

        if (MaxSize.x <= transform.localScale.x || MaxSize.y <= transform.localScale.y)
        {
            transform.localScale = MaxSize;
            if (_canPushSize.x <= transform.localScale.x || _canPushSize.y <= transform.localScale.y) CanPush = true;
            Debug.Log("�ő�Size");
            return;
        }
        Debug.Log("�傫���Ȃ�");
        transform.localScale += new Vector3(_magnification, _magnification, 0.0f);
        if (_canPushSize.x <= transform.localScale.x || _canPushSize.y <= transform.localScale.y) CanPush = true;
        Destroy(col.gameObject);
    }

    /// <summary>
    /// ���g�𐶐�����
    /// </summary>
    public void CreateClone(bool playerDir,float shootSpeed = 5.0f,Action onShoot = null)
    {
        Debug.Log("Create");
        Vector2 dir = new Vector2(1, 0);
        if (MinimumSize.x >= transform.localScale.x || MinimumSize.y >= transform.localScale.y)
        {
            transform.localScale = MinimumSize;
            if (_canPushSize.x >= transform.localScale.x || _canPushSize.y >= transform.localScale.y) CanPush = false;
            Debug.Log("�ŏ�Size");
            return;
        }
        Debug.Log("�������Ȃ�");
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
