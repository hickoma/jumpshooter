using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _selfDestructTimerCurrentValue = 0f;
    private float _selfDestructTimerDefaultValue = 10f;

    private bool _launched = false;

    private void Update()
    {
        if (_selfDestructTimerCurrentValue <= _selfDestructTimerDefaultValue)
            _selfDestructTimerCurrentValue += Time.deltaTime;
        else Destroy(gameObject);

        if (_launched)
        {
            transform.position += transform.up * GameManager.FireSpeed * Time.deltaTime;
        }
    }

    public void Launch()
    {
        _launched = true;
    }
}
