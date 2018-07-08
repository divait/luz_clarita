using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovePhysic : MonoBehaviour {

    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    private bool grounded = false;
    Rigidbody rigid;

    public Player player;
    public Rotate rotate;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.freezeRotation = true;
        rigid.useGravity = false;
    }

    void Start() 
    {
        player = GetComponentsInChildren<Player>()[0];
        rotate = GetComponentsInChildren<Rotate>()[0];
    }

    void Update() {
        if(!player.isAlive()) {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if(!player.isAlive()) {
            return;
        }

        if (grounded && (rotate.isStateName("run") || rotate.isStateName("idle2")))
        {
            Vector2 input = new Vector2(
                InputMan.getAxisRaw(InputMan.AXIS.H, player.id), 
                InputMan.getAxisRaw(InputMan.AXIS.V, player.id)
            );
            Vector2 inputDir = input.normalized;

            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(
                InputMan.getAxisRaw(InputMan.AXIS.H, player.id),
                0,
                InputMan.getAxisRaw(InputMan.AXIS.V, player.id)
            );
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;
            
            Vector3 velocity = rigid.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);

            rigid.AddForce(velocityChange, ForceMode.VelocityChange);
        } else {
            rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
        }

        // We apply gravity manually for more tuning control
        rigid.AddForce(new Vector3(0, -gravity * rigid.mass, 0));

        grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }
}
