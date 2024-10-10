using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float MoveSpeed;
    //[SerializeField] public float SpeedIncreasePerLevel = 5.0f;
    [SerializeField] public int MaxHealth;
    [SerializeField] public Transform PlayerPos;
    [SerializeField] public GameObject HeartPrefab;
    [SerializeField] public Animator Animator;
    [SerializeField] public HealthBar healthBar;

    protected int Health;
    protected GameObject instantiatedHeart;
    protected Vector3 heartOffset = new Vector3(0, 1.0f, 0);
    private GameStateManager gameStateManager;
    //private bool canMove = true; // Movement state

    public virtual void Start()
    {
        gameStateManager=FindAnyObjectByType<GameStateManager>();
        Health = MaxHealth;
        Animator = GetComponentInChildren<Animator>();
        //FindObjectOfType<EnemyManager>().OnEnemyDestroyed.AddListener(HandleEnemyDestroyed);
        //XPManager.Instance.OnLevelUp += HandleLevelUp;
        HeartInstantiate();
        UpdateHealthBar();
    }

    protected virtual void Update()
    {

        /*if (gameStateManager.CurrentState is PlayingState)
        {
            PlayerMovement();
        }*/


        if (instantiatedHeart != null)
        {
            instantiatedHeart.transform.position = PlayerPos.position + heartOffset;
        }
    }
    //public abstract void PlayerMovement();

    /*public void PlayerMovement()
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

        Animator.SetFloat("Speed", speed);
        Animator.enabled = speed > 0; // Only enable if speed is greater than zero
    }*/

    protected void HeartInstantiate()
    {
        instantiatedHeart = Instantiate(HeartPrefab, PlayerPos.position + heartOffset, Quaternion.identity);
    }

    /*private void HandleEnemyDestroyed(Vector3 enemyPosition, int xpGained)
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
    }*/

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
        Debug.Log($"Player Speed increased by {amount}. New health: {MoveSpeed}");
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    // Public method to set the movement state
    /*public void SetMovementState(bool state)
    {
        canMove = state; // Set the movement state
    }*/
}



