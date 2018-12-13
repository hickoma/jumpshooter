using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [SerializeField] private PlayerControl _playerControl;
    public static PlayerControl PlayerControl
    {
        get { return _instance._playerControl; }
    }

    [SerializeField] private float _moveSpeed;
    public static float MoveSpeed
    {
        get { return _instance._moveSpeed; }
        private set { _instance._moveSpeed = value; }
    }

    [SerializeField] private float _fireSpeed = 10f;
    public static float FireSpeed
    {
        get { return _instance._fireSpeed; }
        set { _instance._fireSpeed = value; }
    }

    [SerializeField] private float _fireDamage = 10f;
    public static float FireDamage
    {
        get { return _instance._fireDamage; }
        set { _instance._fireDamage = value; }
    }

    [SerializeField] private float _fireRate = 2f;
    public static float FireRate
    {
        get { return _instance._fireRate; }
        set { _instance._fireRate = value; }
    }

    [SerializeField] private float _enemyHealth = 20f;
    public static float EnemyHealth
    {
        get { return _instance._enemyHealth; }
        set { _instance._enemyHealth = value; }
    }

    private void Awake()
    {
        if (_instance == null) _instance = this;

        var leftScreenBorderXCoord = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.1f, 0, 0)).x;
        var rightScreenBorderXCoord = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.9f, 0, 0)).x;
        Debug.Log(leftScreenBorderXCoord + " " + rightScreenBorderXCoord);
    }
}
