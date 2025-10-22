using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 5.0f;

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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 10.0f;
            animator?.SetFloat("Speed", 1.0f);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 5.0f;
            animator?.SetFloat("Speed", 0.3f);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed = 1.0f;
            animator?.SetFloat("Speed", 0.1f);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            moveSpeed = 5.0f;
            animator?.SetFloat("Speed", 0.3f);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator?.SetFloat("Speed", 1.0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            animator?.SetFloat("Speed", 1.0f);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator?.SetFloat("Speed", 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator?.SetFloat("Speed", 1.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            animator?.SetFloat("Speed", 0.0f);
        }
    }
}
