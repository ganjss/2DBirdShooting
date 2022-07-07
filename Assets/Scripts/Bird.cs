using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _xSpeed;
    [SerializeField] float _yMinSpeed;
    [SerializeField] float _yMaxSpeed;

    Rigidbody2D _rb2d;
    bool _isMoveLeft;

    [SerializeField] GameObject _vfxDeath;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GetDirectionMoving();
    }

    private void FixedUpdate()
    {
        //short_hand cua if, neu _isMoveLeft = true thi -xSpeed, neu khong thi xSpeed
        _rb2d.velocity = _isMoveLeft ?
            new Vector2(-_xSpeed, Random.Range(_yMinSpeed, _yMaxSpeed))
            : new Vector2(_xSpeed, Random.Range(_yMinSpeed, _yMaxSpeed));

        Flip();
    }

    public void GetDirectionMoving() {
        if (transform.position.x >= 0)
        {
            _isMoveLeft = true;
        }
        else
        {
            _isMoveLeft = false;
        }
    }

    void Flip() {
        if (_isMoveLeft)
        {
            if (transform.localScale.x < 0) return;

            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else 
        {
            if (transform.localScale.x > 0) return;

            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    public void Die() {

        GameManager.Ins.BirdKilled++;

        Destroy(gameObject);

        if (_vfxDeath)
        {
            Instantiate(_vfxDeath, transform.position, Quaternion.identity);
        }

        GameGUIManager.Ins.UpdateKilledCounting(GameManager.Ins.BirdKilled);
    }

}
