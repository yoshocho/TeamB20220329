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
    [SerializeField]
    float _magnification = 0.1f;
    [SerializeField]
    float _maxMagnification = 3.0f;
    [SerializeField]
    float _minimumMagnification = 0.5f;
    [SerializeField]
    GameObject _cloneObj;
    [SerializeField]
    float _shootSpeed = 5.0f;

    bool _playerDir = false;
    Vector3 _maxSize;
    Vector3 _minimumSize;
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
        Debug.Log("CloneÇ…Hit");
        Clone clone = collision.collider.GetComponent<Clone>();
        if (!clone.CanCatch) return;
        if (_maxSize.x <= transform.localScale.x || _maxSize.y <= transform.localScale.y)
        {
            transform.localScale = _maxSize;
            Debug.Log("ç≈ëÂSize");
            return;
        }
        Debug.Log("ëÂÇ´Ç≠Ç»ÇÈ");
        transform.localScale += new Vector3(_magnification, _magnification, 0.0f);
        Destroy(collision.gameObject);
    }
    void Jump() => _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);

    /// <summary>
    /// ï™êgÇê∂ê¨Ç∑ÇÈ
    /// </summary>
    void CreateClone()
    {
        Debug.Log("Create");
        Vector2 dir = new Vector2(1, 0);
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


        if (_minimumSize.x >= transform.localScale.x || _minimumSize.y >= transform.localScale.y)
        {
            transform.localScale = _minimumSize;
            Debug.Log("ç≈è¨Size");
            return;
        }
        Debug.Log("è¨Ç≥Ç≠Ç»ÇÈ");
        transform.localScale -= new Vector3(_magnification, _magnification, 0.0f);
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
