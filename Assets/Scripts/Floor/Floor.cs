using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private BoxCollider2D[] _boxColliders2D;
    public BoxCollider2D[] BoxColliders2D
    {
        get
        {
            if (_boxColliders2D == null)
            {
                _boxColliders2D = GetComponentsInChildren<BoxCollider2D>();
            }
            return _boxColliders2D;
        }
    }

    private bool _becameGround = false;
    public bool CanBeDestroyed
    {
        private set; get;
    }

    private void Awake()
    {
        foreach (var col in BoxColliders2D)
        {
            col.isTrigger = true;
        }
    }

    private void Update()
    {
        if (transform.position.y < GameManager.PlayerControl.transform.position.y && !_becameGround)
        {
            foreach (var col in BoxColliders2D)
            {
                col.isTrigger = false;
            }
        }

        transform.position += 0.5f * Vector3.down * Time.deltaTime;

        CanBeDestroyed = transform.position.y < -5;
    }
}
