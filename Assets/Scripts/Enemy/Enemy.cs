using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private BoxCollider2D _enemyCollider;
    public BoxCollider2D EnemyCollider
    {
        get
        {
            if (!_enemyCollider) _enemyCollider = GetComponent<BoxCollider2D>();
            return _enemyCollider;
        }
    }
    
    public float EnemyCurrentHealth
    {
        get; private set;
    }

    [SerializeField] private Transform _healthBar;
    public Transform HealthBar
    {
        get
        {
            return _healthBar;
        }
    }

    private void Awake()
    {
        EnemyCurrentHealth = GameManager.EnemyHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.PlayerControl.Die();
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            EnemyCurrentHealth -= GameManager.FireDamage;
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        var healthBarScale = HealthBar.localScale;
        healthBarScale.x = 0.4f * EnemyCurrentHealth / GameManager.EnemyHealth;
        HealthBar.localScale = healthBarScale;

        if (EnemyCurrentHealth <= 0)
        {
            ScoreManager.Score += 5;
            Destroy(gameObject);
        }
    }
}
