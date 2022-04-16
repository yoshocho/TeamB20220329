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

    bool _isGrand = true;

    [SerializeField]
    SEData Jampse = new SEData();
    Animator _anim;
    Rigidbody2D _rb;
    SpriteRenderer _charaImage;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _charaImage = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponentInChildren<Animator>();
        _status.SetUp(_status.hp);
    }

    void Update()
    {

        ApplyMove();

        if (Input.GetKeyDown(KeyCode.Space) && _isGrand)
        {
            _anim.SetBool("Jump", true);
            Jump();
            SoundManager.PlaySE(Jampse);
        }
    }


    void ApplyMove()
    {
        _rb.velocity *= new Vector2(0, 1);
        if (Input.GetKey(KeyCode.A))
        {
            _charaImage.flipX = true;
            if (_isGrand) _anim.SetBool("Move", true);
            else _anim.SetBool("Move", false);
            _rb.velocity = new Vector2(-_moveSpeed, _rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _charaImage.flipX = false;
            if (_isGrand) _anim.SetBool("Move", true);
            else _anim.SetBool("Move", false);
            _rb.velocity = new Vector2(_moveSpeed, _rb.velocity.y);
        }
        else
        {
            _anim.SetBool("Move", false);
        }
    }

    void Jump() => _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Grand") return;
        _isGrand = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Grand") return;
        _isGrand = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Grand") return;
        _isGrand = true;
    }

    public override void GetDamage(int damage)
    {
        _status.hp -= damage;
    }
}
