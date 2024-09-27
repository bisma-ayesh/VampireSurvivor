using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float MoveSpeed; // Base move speed
    [SerializeField] private float SpeedIncreasePerLevel = 5.0f; // Amount to increase speed per level
    [SerializeField] private int MaxHealth;
    [SerializeField] private Transform PlayerPos;
    [SerializeField] private GameObject HeartPrefab; // Heart prefab to instantiate
    [SerializeField] private Animator Animator;

    private int Health;
    private GameObject instantiatedHeart;
    private Vector3 heartOffset = new Vector3(0, 1.0f, 0); // Offset to place the heart above the player

    void Start()
    {
        Health = MaxHealth;
        Animator = GetComponentInChildren<Animator>();
        FindObjectOfType<EnemyManager>().OnEnemyDestroyed.AddListener(HandleEnemyDestroyed);

        // Subscribe to level up event
        XPManager.Instance.OnLevelUp += HandleLevelUp; // Ensure this matches the delegate

        // Instantiate the heart only once
        HeartInstantiate();
    }

    void Update()
    {
        PlayerMovement();

        if (instantiatedHeart != null)
        {
            // Update the heart position based on player's position
            instantiatedHeart.transform.position = PlayerPos.position + heartOffset; // Follow player position with offset
        }

        Debug.Log($"Current XP: {XPManager.Instance.CurrentXP}, Level: {XPManager.Instance.Level}");
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
        Debug.Log($"Player gained {xpGained} XP from enemy destroyed at {enemyPosition}");
    }

    public void HandleLevelUp(int newLevel) // Changed from float to int
    {
        Debug.Log($"Player leveled up to level {newLevel}");
        IncreasePlayerSpeed(newLevel); // Increase speed on level up
    }

    private void IncreasePlayerSpeed(int newLevel)
    {
        // Increase speed based on level
        MoveSpeed += SpeedIncreasePerLevel; // Increase speed, can modify this logic if needed
        Debug.Log($"New Move Speed: {MoveSpeed}");
    }

    public void TakeDamage(int someDamage)
    {
        Health -= someDamage;
        if (Health < 0)
            Death();
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}


