using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float jumpSpeed;

    private bool isJumping = false;

    private Rigidbody rb;
    private int count;

    public Text countText;
    public Text winText;

	void Start()
	{
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
	}

	void FixedUpdate()
	{
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * moveSpeed);

        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            rb.velocity = Vector3.up * jumpSpeed;
            isJumping = true;
        }
	}

	void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Super Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 2;
            SetCountText();
        }
	}

    // 空中ジャンプをさせないための処理
    void OnCollisionEnter(Collision other)
    {
        // 床に触れたら
        if (other.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 20)
        {
            winText.text = "You Win!";
        }
    }
}
