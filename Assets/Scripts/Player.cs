using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //get object ong ngam
    [SerializeField] GameObject _viewFinder;
    //clone lai ong ngam de dung
    GameObject _mViewFinderClone;

    [SerializeField] float _reloadSpeed;
    float _curReloadSpeed;
    bool _isFire;

    private void Awake()
    {
        _curReloadSpeed = _reloadSpeed;
    }


    private void Start()
    {
        if (_viewFinder)
        {
            _mViewFinderClone = Instantiate(_viewFinder, Vector3.zero, Quaternion.identity);
        }
    }


    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && !_isFire)
        {
            Shot(mousePos);
        }

        if (_isFire)
        {
            _curReloadSpeed -= Time.deltaTime;

            if (_curReloadSpeed <= 0)
            {
                _isFire = false;
                _curReloadSpeed = _reloadSpeed;
            }

            GameGUIManager.Ins.UpdateFireRate(_curReloadSpeed / _reloadSpeed);
        }

        if (_mViewFinderClone)
        {
            _mViewFinderClone.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        }

    }

    void Shot(Vector3 mousePos) {
        _isFire = true;

        

        Vector3 shootDir = Camera.main.transform.position - mousePos;
        shootDir.Normalize();

        //lay tat ca thong tin khi Raycast cham vao object: RaycastAll(position bat dau, huong cua raycast)
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, shootDir);

        if (hits != null && hits.Length > 0) {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                if (hit.collider && Vector3.Distance((Vector2)hit.collider.transform.position, (Vector2)mousePos) <= 1.5f )
                {
                    Debug.Log(hit.collider.name);
                    Bird bird = hit.collider.GetComponent<Bird>();
                    if (bird)
                    {
                        bird.Die();
                    }
                }
            }
        }

        AudioController.Ins.PlaySound(AudioController.Ins.shooting);
    }
}
