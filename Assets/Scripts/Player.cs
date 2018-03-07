using UnityEngine;

[RequireComponent (typeof(CharacterController))]
[RequireComponent (typeof(Animator))]
public class Player : MonoBehaviour {
    CharacterController m_controller = null;
    Animator m_animator = null;

    public float m_speed = 80.0f;
    public float m_pushPower = 2.0f;

    public float m_jumpStrength = 8.0f;
    public float m_gravity = 11.0f;

    public Vector3 moveDirection = Vector3.zero;

    public bool m_crouching = false;
    public bool m_grounded = false;
    public bool m_running = false;

    // Use this for initialization
    void Start () {
        m_controller = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        
        m_controller.SimpleMove(transform.up * Time.deltaTime);
        transform.Rotate(transform.up, horizontal * m_speed * Time.deltaTime);
        m_animator.SetFloat("Speed", vertical * m_speed * Time.deltaTime);

        m_grounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
        m_animator.SetBool("Grounded", m_grounded);

        m_crouching = Input.GetKey(KeyCode.LeftControl);
        m_animator.SetBool("Crouching", m_crouching);

        m_running = Input.GetKey(KeyCode.LeftShift);
        m_animator.SetBool("Running", m_running);

        if (Input.GetButtonDown("Jump") && m_grounded && !m_crouching) {
            moveDirection.y = m_jumpStrength;
        }
        
        if (m_crouching && m_grounded) {
            m_controller.center = new Vector3(0,0.5f,0);
            m_controller.height = 1.0f;
        } else
        {
            m_controller.center = new Vector3(0, 1.0f, 0);
            m_controller.height = 2.0f;
        }

        moveDirection.y -= m_gravity * Time.deltaTime;

        m_controller.Move(moveDirection * Time.deltaTime);
	}

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3F)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * m_pushPower;
    }
}
