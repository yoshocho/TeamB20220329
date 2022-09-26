using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtility.Sound;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class Player : CharacterBase,IRBCtrl
{
    public enum SEType
    {
        Jump,
        Shoot,
    }
    [System.Serializable]
    public class PlayerSE 
    {
        [SerializeField]
        public SEType Type;
        [SerializeField]
        public SEData SeData;
    }
    [SerializeField]
    float _moveSpeed = 10f;
    [SerializeField]
    float _jumpPower = 10f;
    [SerializeField]
    GrandChecker _grandChecker;
    [SerializeField]
    float _shootSpeed = 5.0f;

    [SerializeField]
    List<PlayerSE> _playerSEs = new List<PlayerSE>();

    public bool CanPush { get; private set; } = true;

    public bool CanMove { get; set; } = true;
    public Rigidbody2D RB { get => _rb; }

    bool _playerDir = false;
    Animator _anim;
    Rigidbody2D _rb;
    SpriteRenderer _charaImage;
    SizeCtrl _sizeCtrl;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (!_grandChecker) _grandChecker = GetComponentInChildren<GrandChecker>();
        _charaImage = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponentInChildren<Animator>();
        _sizeCtrl = GetComponent<SizeCtrl>();
        _sizeCtrl.SetUp();
        _status.SetUp(_status.hp);
        
    }

    void Update()
    {
        if (!CanMove) return;
        ApplyMove();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("shoot");
            _sizeCtrl.CreateClone(_playerDir,_shootSpeed,
                () => SoundManager.PlaySE(_playerSEs.First(s => s.Type == SEType.Shoot).SeData));
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

        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        //Debug.Log(horizontalAxis);
        if (horizontalAxis == -1.0f)
        {
            _charaImage.flipX = true;
            _playerDir = _charaImage.flipX;
            if (IsGrand()) _anim.SetBool("Move", true);
            else _anim.SetBool("Move", false);
            _rb.velocity = new Vector2(-_moveSpeed, _rb.velocity.y);
        }
        else if (horizontalAxis == 1.0f)
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
        _sizeCtrl.OnCollisionEvent(collision);
    }
    void Jump() 
    {
        _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        PlayerSE data = _playerSEs.FirstOrDefault(s => s.Type == SEType.Jump);
        SoundManager.PlaySE(data.SeData);
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
