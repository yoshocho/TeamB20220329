using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtility.Sound;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class Player : CharacterBase
{
    [SerializeField]
    float _moveSpeed = 10f;
    [SerializeField]
    float _jumpPower = 10f;
    [SerializeField]
    GrandChecker _grandChecker;
    [SerializeField,Header("減少、増大倍率")]
    float _magnification = 0.1f;
    [SerializeField, Header("プレイヤーが大きくなれる最大値")]
    float _maxMagnification = 3.0f;
    [SerializeField, Header("プレイヤーが小さくなる最小値")]
    float _minimumMagnification = 0.5f;
    [SerializeField, Header("分身オブジェクト")]
    GameObject _cloneObj;
    [SerializeField, Header("ボタンを押せる大きさ")]
    float _canPushMagnification = 0.7f;
    [SerializeField]
    float _shootSpeed = 5.0f;

    public bool CanPush { get; private set; } = true;

    [SerializeField]
    SEData[] _seDatas;

    bool _playerDir = false;
    Vector3 _maxSize;
    Vector3 _minimumSize;
    Vector3 _canPushSize;
    Animator _anim;
    Rigidbody2D _rb;
    SpriteRenderer _charaImage;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (!_grandChecker) _grandChecker = GetComponentInChildren<GrandChecker>();
        _charaImage = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponentInChildren<Animator>();
        _status.SetUp(_status.hp);
        _maxSize = new Vector3(_maxMagnification, _maxMagnification, 1.0f);
        _minimumSize = new Vector3(_minimumMagnification, _minimumMagnification, 1.0f);
        _canPushSize = new Vector3(_canPushMagnification, _canPushMagnification, 1.0f);
        if (_canPushSize.x >= transform.localScale.x || _canPushSize.y >= transform.localScale.y) CanPush = false;

    }

    void Update()
    {

        ApplyMove();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("shoot");
            CreateClone();
        }


        if (Input.GetKeyDown(KeyCode.Space) && IsGrand())
        {
            _anim.SetBool("Jump", true);
            Jump();
        }
    }


    void ApplyMove()
    {
        _rb.velocity *= new Vector2(0, 1);
        if (Input.GetKey(KeyCode.A))
        {
            _charaImage.flipX = true;
            _playerDir = _charaImage.flipX;
            if (IsGrand()) _anim.SetBool("Move", true);
            else _anim.SetBool("Move", false);
            _rb.velocity = new Vector2(-_moveSpeed, _rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _charaImage.flipX = false;
            _playerDir = _charaImage.flipX;
            if (IsGrand()) _anim.SetBool("Move", true);
            else _anim.SetBool("Move", false);
            _rb.velocity = new Vector2(_moveSpeed, _rb.velocity.y);
        }
        else
        {
            _anim.SetBool("Move", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit");
        if (!collision.collider.CompareTag("Clone")) return;
        Debug.Log("CloneにHit");
        Clone clone = collision.collider.GetComponent<Clone>();
        if (!clone.CanCatch) return;

        if (_maxSize.x <= transform.localScale.x || _maxSize.y <= transform.localScale.y)
        {
            transform.localScale = _maxSize;
            if (_canPushSize.x <= transform.localScale.x || _canPushSize.y <= transform.localScale.y) CanPush = true;
            Debug.Log("最大Size");
            return;
        }
        Debug.Log("大きくなる");
        transform.localScale += new Vector3(_magnification, _magnification, 0.0f);
        if (_canPushSize.x <= transform.localScale.x || _canPushSize.y <= transform.localScale.y) CanPush = true;
        Destroy(collision.gameObject);
    }
    void Jump() => _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);

    /// <summary>
    /// 分身を生成する
    /// </summary>
    void CreateClone()
    {
        Debug.Log("Create");
        Vector2 dir = new Vector2(1, 0);
        if (_minimumSize.x >= transform.localScale.x || _minimumSize.y >= transform.localScale.y)
        {
            transform.localScale = _minimumSize;
            if (_canPushSize.x >= transform.localScale.x || _canPushSize.y >= transform.localScale.y) CanPush = false;
            Debug.Log("最小Size");
            return;
        }
        Debug.Log("小さくなる");
        transform.localScale -= new Vector3(_magnification, _magnification, 0.0f);
        if (_canPushSize.x >= transform.localScale.x || _canPushSize.y >= transform.localScale.y) CanPush = false;
        if (_playerDir)
        {
            var cloneobj = Instantiate(_cloneObj, new Vector3(transform.position.x - 0.5f, transform.position.y
                , transform.position.z), Quaternion.identity);
            var clone = cloneobj.GetComponent<Clone>();
            clone.SetUp();
            clone.Shoot(-dir * _shootSpeed);
        }
        else
        {
            var cloneobj = Instantiate(_cloneObj, new Vector3(transform.position.x + 0.5f, transform.position.y
               , transform.position.z), Quaternion.identity);
            var clone = cloneobj.GetComponent<Clone>();
            clone.SetUp();
            clone.Shoot(dir * _shootSpeed);
        }
    }

    public bool IsGrand()
    {
        return _grandChecker.IsGrand;
    }

    public override void GetDamage(int damage)
    {
        _status.hp -= damage;
    }
}
