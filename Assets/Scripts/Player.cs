using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private int MaxHeath;
    [SerializeField] private Transform PlayerPos;

    public GameObject HeartPrefab;
    public Animator Animator;




    private int Health;
    private GameObject instantiatedHeart;




    void Start()
    {
        Health = MaxHeath;
        Animator = GetComponentInChildren<Animator>();
        FindObjectOfType<EnemyManager>().OnEnemyDestroyed.AddListener(HandleEnemyDestroyed);

        //XPManager.Instance.OnLevelUp += HandleLevelUp;

        HeartInsantiate();
    }

    public void Update()
    {
        PlayerMovement();

        if (instantiatedHeart != null)
        {
            instantiatedHeart.transform.position = PlayerPos.position; // Follow player position
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

    public void HeartInsantiate()
    {
        instantiatedHeart = Instantiate(HeartPrefab, PlayerPos.position, Quaternion.identity);
    }

    private void HandleEnemyDestroyed(Vector3 enemyPosition, int xpGained)
    {
        XPManager.Instance.AddXP(xpGained);
        Debug.Log($"Player gained {xpGained} XP from enemy destroyed at {enemyPosition}");
    }


    public void HandleLevelUp(int newLevel) 
    {
        Debug.Log($"Player leveled up{newLevel}");
    }


    public void TakeDamage(int someDamage)
    {
            Health -= someDamage; // same thing as Health= Health-someDamage;
            if (Health < 0) 
                Death();
    }


    public void Death()
    {
            Destroy(gameObject);
    }

}


