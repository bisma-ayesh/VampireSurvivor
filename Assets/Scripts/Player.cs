using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float MoveSpeed; 
    [SerializeField] private float SpeedIncreasePerLevel = 1.0f; 
    [SerializeField] private int MaxHealth;
    [SerializeField] private Transform PlayerPos;
    [SerializeField] private GameObject HeartPrefab; 
    [SerializeField] private Animator Animator;
    [SerializeField] private HealthBar healthBar;
    private int Health;
    private GameObject instantiatedHeart;
    private Vector3 heartOffset = new Vector3(0, 1.0f, 0); 
    void Start()
    {
        GameStateManager.Instance.ChangeState(new PlayingState(this));
        Health = MaxHealth;
        Animator = GetComponentInChildren<Animator>();
        FindObjectOfType<EnemyManager>().OnEnemyDestroyed.AddListener(HandleEnemyDestroyed);
        XPManager.Instance.OnLevelUp += HandleLevelUp;
        HeartInstantiate();
        UpdateHealthBar();
    }

    void Update()
    {
        PlayerMovement();

        if (instantiatedHeart != null)
        {
            
            instantiatedHeart.transform.position = PlayerPos.position + heartOffset; 
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        //Debug.Log($"Current XP: {XPManager.Instance.CurrentXP}, Level: {XPManager.Instance.Level}");
    }
    private void TogglePause()
    {
        {
            if (GameStateManager.Instance.CurrentState is PlayingState)
            {
                GameStateManager.Instance.ChangeState(new PausedState());
                Time.timeScale = 0; 
                Animator.enabled = false; 
                Debug.Log("Game Paused");
            }
            else
            {
                GameStateManager.Instance.ChangeState(new PlayingState(this));
                Time.timeScale = 1; 
                Animator.enabled = true; 
                Debug.Log("Game Resumed");
            }
        }
    }

    public void PlayerMovement()
    {
         Vector3 moveDirection = Vector3.zero;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                moveDirection = Vector3.right;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                moveDirection = Vector3.left;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                moveDirection = Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                moveDirection = Vector3.back;
            }

            PlayerPos.position += moveDirection.normalized * MoveSpeed * Time.deltaTime;

            float speed = moveDirection.magnitude;

            if (speed > 0)
            {
                Animator.enabled = true;
                Animator.SetFloat("Speed", speed);
            }
            else
            {
                Animator.enabled = false;
            }

            Animator.SetFloat("Speed", speed);
    }

     public void HeartInstantiate()
        {
            instantiatedHeart = Instantiate(HeartPrefab, PlayerPos.position + heartOffset, Quaternion.identity);
        }

     private void HandleEnemyDestroyed(Vector3 enemyPosition, int xpGained)
        {
            XPManager.Instance.AddXP(xpGained);
        }

     public void HandleLevelUp(int newLevel) 
        {
            IncreasePlayerSpeed(newLevel); 
        }

      private void IncreasePlayerSpeed(int newLevel)
        {
            
            MoveSpeed += SpeedIncreasePerLevel; 
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

    public void Death()
    {
        Destroy(gameObject);
    }
}


