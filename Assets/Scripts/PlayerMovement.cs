using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 attackInput;
    public Animator animator;
    public float playerDamage = 30f;
    public PlayerSFX sfxScript;
    public GameObject enemy;

    public bool startPlayerKnockedBackCoroutine = false;
    public bool isBeingKnockedBack = false;
    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }


    // Update is called once per frame
    void Update()
    {
        if (startPlayerKnockedBackCoroutine) StartCoroutine(KnockPlayerBack());
        if(!isBeingKnockedBack)
        {
            rb.velocity = moveInput * moveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            isAttacking = true;
            animator.SetBool("isAttacking", true);
            sfxScript.PlayerPunched();

        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if(!isAttacking)
        {
            animator.SetBool("isWalking", true);

            if (context.canceled)
            {
                animator.SetBool("isWalking", false);
                animator.SetFloat("LastInputX", moveInput.x);
                animator.SetFloat("LastInputY", moveInput.y);

            }
            animator.SetFloat("AttackX", moveInput.x);
            animator.SetFloat("AttackY", moveInput.y);

            moveInput = context.ReadValue<Vector2>();
            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);
        }
    }

    public void EndAttackAnimation()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }

    private IEnumerator KnockPlayerBack()
    {
        startPlayerKnockedBackCoroutine = false;
        isBeingKnockedBack = true;
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector2.zero;
        isBeingKnockedBack = false;
    }
}
