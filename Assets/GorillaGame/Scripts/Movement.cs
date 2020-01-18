using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    [SerializeField]
    public float horizontalspeed = 80;
    public float verticalspeed = 40;
    public bool lockedOn = false;
    public Rigidbody rb;
    [SerializeField]
    private Transform target;

    public bool on_ground;
    public int jumpForce;
    public int num_jumps = 0;
    public bool dashing = false;
    public float extraForce;
    public float distToGround;
    public Animator bosscontroller;
    Collider m_collider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Boss").GetComponent<Transform>();
        m_collider = this.GetComponent<Collider>();
        bosscontroller = GameObject.FindWithTag("Fire").GetComponent<Animator>();

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "bosscollide") 
        {
            on_ground = true;
            num_jumps = 0;
        }
        if(collision.gameObject.name == "key_JNT") {
            bosscontroller.SetBool("hasWon", true);


            Debug.Log(bosscontroller.GetBool("hasWon"));
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "bosscollide")
        {
            on_ground = false;
        }
    }

    public bool is_grounded () {
        return Physics.Raycast(transform.position, -1 * Vector3.up, (float)(distToGround + 0.1));
        //return Physics.CheckBox(m_collider.bounds.center, new Vector3(m_collider.bounds.center.x, m_collider.bounds.min.y - 0.1f,
                                                              // m_collider.bounds.center.z));
        
    }
    void FixedUpdate()
    {



        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (lockedOn)
            {
                lockedOn = false;
            }
            else
            {
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                lockedOn = true;
            }

        }

        if (lockedOn)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

            if (Input.GetKey(KeyCode.A))
            {
                if (horizontalspeed < 0)
                {
                    horizontalspeed *= -1;
                }
                transform.RotateAround(target.position, Vector3.up, horizontalspeed * Time.deltaTime);

            }
            if (Input.GetKey(KeyCode.D))
            {
                if (horizontalspeed > 0)
                {
                    horizontalspeed *= -1;
                }
                transform.RotateAround(target.position, Vector3.up, horizontalspeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.W))
            {
                // v = Input.GetAxis("Vertical");
                //Vector3 movement = new Vector3(0, 0, v);


                rb.AddForce(transform.forward * (verticalspeed * extraForce));
            }
            if (Input.GetKey(KeyCode.S))
            {
                //Debug.Log(transform.forward * (-1 * (speed * extraForce)));
                rb.AddForce(transform.forward * (-1 * (verticalspeed * extraForce)));
            }

        }
        else {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            rb.AddForce(movement * horizontalspeed * extraForce);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            distToGround = m_collider.bounds.extents.y;
            on_ground = is_grounded();
            Debug.Log(on_ground);
            if (on_ground)
            {
                num_jumps = 0;
            }
            if (num_jumps < 2)
            {
                
                  rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                Vector3 extraGravityForce = (Physics.gravity * 20f) - Physics.gravity;
                rb.AddForce(-extraGravityForce);
                num_jumps++;
            }
        }

    }
}
