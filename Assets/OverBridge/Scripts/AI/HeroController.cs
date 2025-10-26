using UnityEngine;

public class HeroController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public bool isMoving = false;
    public bool isFalling = false;

    [Header("References")]
    public BridgeController bridge;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isMoving)
        {
            MoveAlongBridge();
        }

        UpdateAnimations();
    }

    public void StartMoving()
    {
        isMoving = true;
        isFalling = false;
    }

    void MoveAlongBridge()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;

        // Проверка достижения конца моста
        if (transform.position.x >= bridge.GetEndPosition().x)
        {
            CheckLanding();
        }
    }

    void CheckLanding()
    {
        isMoving = false;

        // Проверка, есть ли остров под концом моста
        if (!IslandGenerator.Instance.IsIslandAtPosition(bridge.GetEndPosition()))
        {
            StartFalling();
        }
        else
        {
            // Успешное перемещение на следующий остров
            GameManager.Instance.AddScore();
            // Переместить героя на следующий остров
            IslandGenerator.Instance.MoveToNextIsland();
        }
    }

    void StartFalling()
    {
        isFalling = true;
        rb.gravityScale = 1f;
        GameManager.Instance.GameOver();
    }

    void UpdateAnimations()
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", isMoving);
            animator.SetBool("IsFalling", isFalling);
        }
    }
}
