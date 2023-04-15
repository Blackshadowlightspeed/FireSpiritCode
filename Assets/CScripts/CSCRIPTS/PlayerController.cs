using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardspeed;
    public float maxspeed;

    public bool Gamestarted;

    private int desiredLane = 0;
    public float laneDistance = 1;

    public bool isGrounded;

    public float jumpForce;
    public float Gravity = -20f;

    public Animator animator;
    public bool isSliding = false;

    public GameObject activeSound;
    public GameObject inactiveSound;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        direction.z = forwardspeed;

        if (forwardspeed < maxspeed)
            forwardspeed += 0.1f * Time.deltaTime;


        if (controller.isGrounded)
        {
            direction.y = -1;
            if (SwipeManager.swipeUp)
            {
                Jump();
            }
        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
        }

        if (SwipeManager.swipeDown)
        {
            StartCoroutine(Slide());
        }

        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            animator.SetBool("StepRight", true);

            if (desiredLane == 2)
            {
                desiredLane = 1;
                animator.SetBool("StepRight", false);
            }

        }

        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            animator.SetBool("StepLeft", true);

            if (desiredLane == -2)
            {
                desiredLane = -1;
                animator.SetBool("StepLeft", false);
            }
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == -1)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 1)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 10f * Time.deltaTime);
        if (transform.position == targetPosition)
            return;
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 movedir = diff * 10 * Time.deltaTime;
            if (movedir.sqrMagnitude < diff.sqrMagnitude)
                controller.Move(movedir);
            else
                controller.Move(diff);
        }

    }

    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
        animator.SetBool("Jumping", false);
        animator.SetBool("StepLeft", false);
        animator.SetBool("StepRight", false);
    }

    public void Jump()
    {
        direction.y = jumpForce;
        animator.SetBool("Jumping", true);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            Debug.Log("SYNCHRONIZED");
            activeSound.SetActive(false);
            inactiveSound.SetActive(true);
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        controller.height = 0.7f;

        yield return new WaitForSeconds(0.7f);

        controller.height = 1.8f;
        isSliding = false;
        animator.SetBool("isSliding", false);
    }
}
