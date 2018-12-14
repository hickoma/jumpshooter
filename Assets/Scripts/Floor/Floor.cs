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

    [SerializeField] private GameObject _enemyPrefab;

    private void Awake()
    {
        var holesCount = Random.Range(0, 3);
        var holes = new int[holesCount];
        for (int i = 0; i < holesCount; i++)
        {
            holes[i] = Random.Range(0, BoxColliders2D.Length);
            BoxColliders2D[i].GetComponent<SpriteRenderer>().enabled = false;
        }
        
        foreach (var col in BoxColliders2D)
        {
            col.isTrigger = true;
        }
    }

    private void Update()
    {
        if (transform.position.y < GameManager.PlayerControl.transform.position.y - GameManager.PlayerControl.PlayerCollider.size.y/2 && !_becameGround)
        {
            var jumpInHole = false;
            foreach (var boxCollider2D in BoxColliders2D)
            {
                if (GameManager.PlayerControl.transform.position.x > boxCollider2D.transform.position.x - boxCollider2D.size.x/2 &&
                    GameManager.PlayerControl.transform.position.x < boxCollider2D.transform.position.x + boxCollider2D.size.x/2)
                {
                    if (!boxCollider2D.GetComponent<SpriteRenderer>().enabled)
                    {
                        jumpInHole = true;
                        break;
                    }
                }
            }
            if (!jumpInHole)
            {
                _becameGround = true;
                foreach (var col in BoxColliders2D)
                {
                    col.isTrigger = false;
                    col.GetComponent<SpriteRenderer>().enabled = true;
                }
                ScoreManager.Score += 1;
            }
        }
        else if (transform.position.y > GameManager.PlayerControl.transform.position.y + GameManager.PlayerControl.PlayerCollider.size.y / 2 && _becameGround)
        {
            _becameGround = false;
            foreach (var col in BoxColliders2D)
            {
                col.isTrigger = true;
            }
        }

        transform.position += GameManager.FloorsSpeed * Vector3.down * Time.deltaTime;

        CanBeDestroyed = transform.position.y < -5;
    }

    public void GenerateEnemies()
    {
        var enemiesCount = Random.Range(1, 3);
        var occupiedFloorParts = new List<int>();
        for (int i = 0; i < enemiesCount; i++)
        {
            var randomFloorPartIndex = Random.Range(0, BoxColliders2D.Length);
            while (!BoxColliders2D[randomFloorPartIndex].GetComponent<SpriteRenderer>().enabled
                || occupiedFloorParts.Contains(randomFloorPartIndex))
            {
                randomFloorPartIndex++;
                if (randomFloorPartIndex >= BoxColliders2D.Length)
                {
                    randomFloorPartIndex = 0;
                }
            }
            var enemy = Instantiate(_enemyPrefab,
                new Vector3(BoxColliders2D[randomFloorPartIndex].transform.position.x, transform.position.y),
                Quaternion.identity);
            enemy.transform.position += Vector3.up * enemy.GetComponent<Enemy>().EnemyCollider.size.y / 2;
            enemy.transform.parent = transform;
            occupiedFloorParts.Add(randomFloorPartIndex);
        }
    }
}
