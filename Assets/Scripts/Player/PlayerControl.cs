using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;

    private float _shootTimerCurrentValue = 0f;
    private bool _shootLocked = false;

    private Rigidbody2D _rigidbody2D;
    public Rigidbody2D RigidBody2D
    {
        get
        {
            if (!_rigidbody2D) _rigidbody2D = GetComponent<Rigidbody2D>();
            return _rigidbody2D;
        }
    }

    private void OnEnable()
    {
        InputManager.MoveLeft += MoveLeft;
        InputManager.MoveRight += MoveRight;
        InputManager.Shoot += Shoot;
        InputManager.Jump += Jump;
    }

    private void OnDisable()
    {
        InputManager.MoveLeft -= MoveLeft;
        InputManager.MoveRight -= MoveRight;
        InputManager.Shoot -= Shoot;
        InputManager.Jump -= Jump;
    }

    private void Update()
    {
        if (_shootLocked)
        {
            if (_shootTimerCurrentValue <= 1f / GameManager.FireRate)
            {
                _shootTimerCurrentValue += Time.deltaTime;
            }
            else
            {
                _shootLocked = false;
                _shootTimerCurrentValue = 0;
            }
        }

    }

    private void MoveRight(float value)
    {
        var playerPosition = transform.position;
        playerPosition.x += value * GameManager.MoveSpeed * Time.deltaTime;
        var leftScreenBorderXCoord = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        var rightScreenBorderXCoord = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        playerPosition.x = Mathf.Clamp(playerPosition.x, leftScreenBorderXCoord, rightScreenBorderXCoord);
        transform.position = playerPosition;
    }

    private void MoveLeft(float value)
    {
        var playerPosition = transform.position;
        playerPosition.x -= value * GameManager.MoveSpeed * Time.deltaTime;
        var leftScreenBorderXCoord = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        var rightScreenBorderXCoord = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        playerPosition.x = Mathf.Clamp(playerPosition.x, leftScreenBorderXCoord, rightScreenBorderXCoord);
        transform.position = playerPosition;
    }

    private void Shoot()
    {
        if (_shootLocked) return;
        _shootLocked = true;

        var instRocket = Instantiate(_bulletPrefab, transform);
        instRocket.transform.localPosition = _bulletSpawnPoint.localPosition;
        instRocket.transform.localRotation = _bulletSpawnPoint.localRotation;
        instRocket.transform.parent = null;

        var rocketComponent = instRocket.GetComponent<Bullet>();
        rocketComponent.Launch();
    }

    private void Jump()
    {
        RigidBody2D.AddForce(Vector2.up * 400);
    }

    public void Die()
    {
        SceneManager.LoadScene(0);
    }
}
