using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
     public float moveSpeed = 0.0f;

    [Header("Jump Settings")]
    public float jumpForce = 3.0f;
    public int score = 0; // 플레이어의 점수

    private Vector3 respawnPoint;
    private bool isGrounded = false;
    private bool infrontofDoor = false;
    private Animator animator;
    private Rigidbody2D rb;

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        // 바닥에 닿았을 때 isGrounded를 true로 설정
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            score--;  // 점수 감소
            Debug.Log("해로운 아이템 접촉! 현재 점수: " + score);
            transform.position = respawnPoint; // 플레이어를 시작 위치로 리스폰
            Debug.Log("Respawn to last checkpoint!");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥에서 떨어졌을 때 isGrounded를 false로 설정
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }


    }

    // 아이템 수집 감지 (Trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            score++;  // 점수 증가
            Debug.Log("코인 획득! 현재 점수: " + score);
            Destroy(other.gameObject);  // 코인 제거
        }
        else if (other.CompareTag("Goal"))
        {
            infrontofDoor = true;
            Debug.Log("문 앞에 도착! 점수: " + score);
        }

    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        respawnPoint = transform.position; // 시작 위치를 리스폰 포인트로 설정
        moveSpeed = 3.0f; // 초기 이동 속도 설정
        jumpForce = 10.0f; // 초기 점프 힘 설정
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        float moveX = 0f;

        // 입력에 따른 이동 방향 설정
        // W, A, S, D 키 입력 처리
        if (Input.GetKey(KeyCode.W))
        {
            if (animator != null && infrontofDoor)
            {
                Debug.Log("Level Complete! Final Score: " + score);
                enabled = false; // 컨트롤러 비활성화
            }
        }
        // A키 입력 처리 앉아있지 않을 때 왼쪽이동
        if (Input.GetKey(KeyCode.A))
        {
            // 앉아있는 상태가 아니면 이동 가능
            if (!animator.GetBool("isCrouching"))
            {
                moveX -= moveSpeed;
            }
            // 왼쪽으로 이동 시 캐릭터 반전
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // S 키는 앉아있는 상태가 아닐 때만 이동 가능
        if (Input.GetKey(KeyCode.S))
        {
            if (animator != null && !animator.GetBool("isCrouching"))
            {
                // Upcomming feature: 앞으로 앉기 기능 추가 가능
            }
        }
        // D키 입력 처리 앉아있지 않을 때 오른쪽이동
        if (Input.GetKey(KeyCode.D))
        {
            if (animator != null && !animator.GetBool("isCrouching"))
            {
                moveX += moveSpeed;
            }
            // 오른쪽으로 이동 시 캐릭터 원래 방향
            transform.localScale = new Vector3(1, 1, 1); 
        }

        // 점프 입력 처리
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (animator != null)
            {
                animator.SetTrigger("JumpTrigger");
                Debug.Log("Jump triggered");
            }
            if (rb != null && isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                Debug.Log("Applied jump force: " + jumpForce);
            }
        }
        // 앉기 입력 처리
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (animator != null)
            {
                if (animator.GetBool("isCrouching"))
                {
                    animator.SetBool("isCrouching", false);
                }
                else
                {
                    animator.SetBool("isCrouching", true);
                }
            }
        }

        // 마우스 휠 입력 처리로 속도 조절
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            moveSpeed += 0.1f;
            if (moveSpeed > 5.0f) moveSpeed = 5.0f; // 최대 속도 제한
            Debug.Log("Current Move Speed: " + moveSpeed);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            moveSpeed -= 0.1f;
            if (moveSpeed < 0f) moveSpeed = 0f; // 음수 방지
            Debug.Log("Current Move Speed: " + moveSpeed);
        }


        // 속도 계산: 이동 중이면 moveSpeed, 아니면 0
        float currentSpeed = Mathf.Abs(rb.linearVelocity.x);

        // Rigidbody2D를 사용하여 이동
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        // Animator에 속도 전달
        animator?.SetFloat("MoveSpeed", currentSpeed);
    }
}

