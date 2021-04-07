using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Joystick joystickMove;
    [SerializeField] Joystick joystickAttack;
    [SerializeField] Transform firePoint;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float speed = 10f;

    float gravity = -9.81f;
    float groundDistance = 0.4f;
    Vector3 velocity;
    [SerializeField] float bulletForce = 20f;
    [SerializeField] float attackSpeed = 0.3f;

    bool canAttack = true;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = joystickMove.Horizontal;
        float vertical = joystickMove.Vertical;

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(joystickMove.Horizontal, 0, joystickMove.Vertical));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            controller.Move(direction * speed * Time.deltaTime);

            velocity.y += gravity * speed * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    void Attack()
    {
        float horizontal = joystickAttack.Horizontal;
        float vertical = joystickAttack.Vertical;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(joystickAttack.Horizontal, 0, joystickAttack.Vertical));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            if (canAttack)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(direction * bulletForce, ForceMode.Impulse);
                StartCoroutine(AttackSpeed());
            }
        }
    }

    IEnumerator AttackSpeed()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }
}