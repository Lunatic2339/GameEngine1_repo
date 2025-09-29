using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
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
        // 이동 벡터 계산
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {   
            if(!animator.GetBool("isCrouching"))
            {
                movement += Vector3.up;
            }
      
        }
        if (Input.GetKey(KeyCode.A))
        {
            if(!animator.GetBool("isCrouching"))
            {
                movement += Vector3.left;
            }
            transform.localScale = new Vector3(-1, 1, 1); // 왼쪽으로 이동 시 캐릭터 좌우 반전
        }
        if (Input.GetKey(KeyCode.S))
        {
            if(!animator.GetBool("isCrouching"))
            {
                movement += Vector3.down;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if(!animator.GetBool("isCrouching"))
            {
                movement += Vector3.right;
            }
            transform.localScale = new Vector3(1, 1, 1); // 오른쪽으로 이동 시 캐릭터 원래 방향
        }

        // 실제 이동 적용
        if (movement != Vector3.zero)
        {
            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }

        // 속도 계산: 이동 중이면 moveSpeed, 아니면 0
        float currentSpeed = movement != Vector3.zero ? moveSpeed : 0f;

        // Animator에 속도 전달
        if (animator != null)
        {
            animator.SetFloat("Speed", currentSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (animator != null)
            {
                animator.SetTrigger("JumpTrigger");
                Debug.Log("Jump triggered");
            }
        }
        
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (animator != null)
            {
                animator.SetBool("isCrouching", true);
            }
        }
    }
}
