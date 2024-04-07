using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
 
	private Rigidbody rb; 
	private int count;
	private int jumpcount = 2;
	private float movementX;
	private float movementY;
	private float movementZ;
	public float speed = 10;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;

 // Start is called before the first frame update.
void Start()
{
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
}
 
// This function is called when a move input is detected.
void OnMove(InputValue movementValue)
{
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y; 
}

 // FixedUpdate is called once per fixed frame-rate frame.
private void FixedUpdate() 
{
	// Checks if player is touching the ground and instantiates number of jumps
	if (GetComponent<Rigidbody>().transform.position.y <= 0.5f)
	{
		jumpcount = 2;
	}
	// If space pressed and there are jumps available, jump
	if (Input.GetKeyDown ("space") && jumpcount > 0)
	{
		movementZ = 20.0f;
		jumpcount = jumpcount - 1;
	}

        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

	// Adding this to rb for the jump
	Vector3 jump = new Vector3 (0.0f, movementZ, 0.0f);

        rb.AddForce(movement * speed); 
	rb.AddForce(jump);
	
	// Decrement movementZ so the player falls
	if (movementZ > 0.0f) 
	{
		movementZ = movementZ - 0.4f;
	}
}

void OnTriggerEnter(Collider other) 
{

	if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
}

void SetCountText() 
{
	countText.text = "Count: " + count.ToString();

	if (count >= 8)
	{
            winTextObject.SetActive(true);
        }
}

}
