using UnityEngine;

public class PlayerController_v2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator animator;
    public Rigidbody2D rb;
    public float jumpForce = 7.0f;  
    private bool isGrounded = true; // 바닥에 닿았는지 여부를 추적하는 변수


 void OnCollisionEnter2D(Collision2D collision)
    {
        
        // 바닥에 닿았을 때 isGrounded를 true로 설정
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
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

    void Start()
    {

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (animator != null)
            {
                animator.SetTrigger("Jump");
            }
            if (rb != null && isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                Debug.Log("Applied jump force: " + jumpForce);
            }
        }
    }
}
