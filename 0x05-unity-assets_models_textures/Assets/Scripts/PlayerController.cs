using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that defines the player controller
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Rigibody of the player
    /// </summary>
    Rigidbody rb;
    
    /// <summary>
    /// speed of the player
    /// </summary>
    public float speed;
    
    /// <summary>
    /// jump speed of the player
    /// </summary>
    public float jumpspeed;
    
    /// <summary>
    /// boolean that checks if the player can jump
    /// </summary>
    public bool canjump = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent <Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canjump == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpspeed, ForceMode.Impulse);
            canjump = false;
        }
        Rotation();
    }
    void Rotation()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
    }
    private void FixedUpdate()
    {
        /// Define de movement of the player
        float vHorizontal = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(0,0,0);
        if (Input.GetAxis("Vertical") > 0)
        {
            move += transform.forward;
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            move += -transform.forward;
        }
        if (vHorizontal > 0)
        {
            move += transform.right;
        }
        if (vHorizontal < 0)
        {
            move += -transform.right;
        }
        
        ///Method that normalize de vector, diagonal velocity is the same as the horizontal and vertical
        move.Normalize();

        ///player normal velocity
        if (canjump == true)
        {
           move *= speed;
        }
        /// reduced velocity of the player while is in the air
        else
        {
            move *= speed / 4;
        }
        /// Force of movement
        rb.AddForce(move);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            canjump = true;
        }
        if (other.tag == "Void")
        {
            rb.velocity = new Vector3(0,0,0);
            transform.position = new Vector3(0, 21, -2);
        }
    }
}
