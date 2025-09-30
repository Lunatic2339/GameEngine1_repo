using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0.0f;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        // 이동 벡터 초기화
        Vector3 movement = Vector3.zero;

        // 입력에 따른 이동 방향 설정
        // W, A, S, D 키 입력 처리
        if (Input.GetKey(KeyCode.W))
        {
            movement += Vector3.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            // 앉아있는 상태가 아니면 이동 가능
            if (!animator.GetBool("isCrouching"))
            {
                movement += Vector3.left;
            }
            // 왼쪽으로 이동 시 캐릭터 반전
            transform.localScale = new Vector3(-1, 1, 1); 
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (animator != null && !animator.GetBool("isCrouching"))
            {
                movement += Vector3.down;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (animator != null && !animator.GetBool("isCrouching"))
            {
                movement += Vector3.right;
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
            animator?.SetFloat("MoveSpeed", moveSpeed); // Animator에 속도 전달
            Debug.Log("Current Move Speed: " + moveSpeed);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            moveSpeed -= 0.1f;
            if (moveSpeed < 0f) moveSpeed = 0f; // 음수 방지
            animator?.SetFloat("MoveSpeed", moveSpeed); // Animator에 속도 전달
            Debug.Log("Current Move Speed: " + moveSpeed);
        }

        // 속도 계산: 이동 중이면 moveSpeed, 아니면 0
        float currentSpeed = movement != Vector3.zero ? moveSpeed : 0f;

        // 실제 이동 적용
        if (movement != Vector3.zero)
        {
            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }

        // Animator에 속도 전달
        animator?.SetFloat("MoveSpeed", currentSpeed);
    }
}
