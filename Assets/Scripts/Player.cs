using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private CharacterController controller;
    private Transform myCamera;

    private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    private float forcaY;
    [SerializeField] private float jumpForce;

    [SerializeField] private GameObject cannon;
    [SerializeField] private Animator cannonAnimator;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject ShootOut;
    [SerializeField] private float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cannonAnimator = cannon.GetComponent<Animator>();
        myCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

        movement = myCamera.TransformDirection(movement);
        movement.y = 0;

        controller.Move(movement * Time.deltaTime * speed);

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            forcaY = jumpForce;
        }

        if (forcaY > -9.81f)
        {
            forcaY += -9.81f * Time.deltaTime;
        }

        controller.Move(new Vector3(0, forcaY, 0) * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            cannonAnimator.SetBool("Shoot", true);

            Vector3 shootPosition = ShootOut.transform.position;
            Vector3 shootDirection = ShootOut.transform.forward;

            GameObject newBullet = Instantiate(bulletPrefab, shootPosition, Quaternion.identity);

            Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();

            bulletRigidbody.AddForce(shootDirection * bulletSpeed, ForceMode.Impulse);

            Destroy(newBullet, 5.0f);
        }
        else
        {
            cannonAnimator.SetBool("Shoot", false);
        }
    }
}
