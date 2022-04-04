using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField] float jumpForce = 5f;
    private bool isGrounded;
    private bool isJumping;

    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform foot;
    [SerializeField] public Animator playerAnimator;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(foot.position, 0.1f, ground);
        playerAnimator.SetBool("ground", isGrounded);
    }
}
