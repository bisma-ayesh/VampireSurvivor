using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] public int maxHealth;
    [SerializeField] public Transform playerPos;
    [SerializeField] public GameObject heartPrefab;
    [SerializeField] public Animator animator;
    [SerializeField] public HealthBar healthBar;

    protected int Health;
    protected GameObject instantiatedHeart;
    protected Vector3 heartOffset = new Vector3(0, 1.0f, 0);
    private GameStateManager _gameStateManager;
  

    public virtual void Start()
    {
        _gameStateManager = FindAnyObjectByType<GameStateManager>();
        Health = maxHealth;
        animator = GetComponentInChildren<Animator>();

        HeartInstantiate();
        UpdateHealthBar();
    }

    protected virtual void Update()
    {
        if (instantiatedHeart != null)
        {
            instantiatedHeart.transform.position = playerPos.position + heartOffset;
        }
    }

    protected void HeartInstantiate()
    {
        instantiatedHeart = Instantiate(heartPrefab, playerPos.position + heartOffset, Quaternion.identity);
    }

    public void OnTriggerEnter(Collider _collision)
    {
        if (_collision.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int someDamage)
    {
        Health -= someDamage;
        if (Health < 0)
            Death();
        else
            UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float healthPercentage = (float)Health / maxHealth;
        healthBar.SetHealth(healthPercentage);
    }

    public void IncreaseHealth(int amount)
    {
        Health += amount;
        Debug.Log($"Player health increased by {amount}. New health: {Health}");
    }

    public void IncreaseSpeed(float amount)
    {
        moveSpeed += amount;
        Debug.Log($"Player Speed increased by {amount}. New speed: {moveSpeed}");
    }

    public void Death()
    {
        if (gameObject != null)
        {
            _gameStateManager.Invoke("ResetGame", 0.1f); 
            _gameStateManager.GameOver();
        }

        if (instantiatedHeart != null)
        {
            Destroy(instantiatedHeart);
        }
       

    }

}




