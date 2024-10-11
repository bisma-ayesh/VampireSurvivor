using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float MoveSpeed;
    [SerializeField] public int MaxHealth;
    [SerializeField] public Transform PlayerPos;
    [SerializeField] public GameObject HeartPrefab;
    [SerializeField] public Animator Animator;
    [SerializeField] public HealthBar healthBar;

    protected int Health;
    protected GameObject instantiatedHeart;
    protected Vector3 heartOffset = new Vector3(0, 1.0f, 0);
    private GameStateManager gameStateManager;
  

    public virtual void Start()
    {
        gameStateManager = FindAnyObjectByType<GameStateManager>();
        Health = MaxHealth;
        Animator = GetComponentInChildren<Animator>();

        HeartInstantiate();
        UpdateHealthBar();
    }

    protected virtual void Update()
    {
        if (instantiatedHeart != null)
        {
            instantiatedHeart.transform.position = PlayerPos.position + heartOffset;
        }
    }

    protected void HeartInstantiate()
    {
        instantiatedHeart = Instantiate(HeartPrefab, PlayerPos.position + heartOffset, Quaternion.identity);
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
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
        float healthPercentage = (float)Health / MaxHealth;
        healthBar.SetHealth(healthPercentage);
    }

    public void IncreaseHealth(int amount)
    {
        Health += amount;
        Debug.Log($"Player health increased by {amount}. New health: {Health}");
    }

    public void IncreaseSpeed(float amount)
    {
        MoveSpeed += amount;
        Debug.Log($"Player Speed increased by {amount}. New speed: {MoveSpeed}");
    }

    public void Death()
    {
        if (gameObject != null)
        {
            gameStateManager.Invoke("ResetGame", 0.1f); // Delay the reset by 0.1 seconds
            gameStateManager.GameOver();
        }

        if (instantiatedHeart != null)
        {
            Destroy(instantiatedHeart);
        }
       

    }

}




