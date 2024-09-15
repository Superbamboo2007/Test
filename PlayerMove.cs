using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] float speedCurve;
    [SerializeField] [Range(1,5)] private float Speed;
    [SerializeField] [Range(3,5)] private float ForceJump;
    private float MoveX, MoveY;
    private Vector2 vec;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    private bool CanJump;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(); 
    }
    private void Move()
    {
        speedCurve = curve.Evaluate(Time.deltaTime);
        MoveX = Input.GetAxis("Horizontal");
        MoveY = Input.GetAxis("Vertical");
        vec = new Vector2(MoveX,MoveY);
        transform.Translate(vec * Speed * Time.deltaTime * speedCurve);

        if (MoveX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (MoveX < 0)
        {
            spriteRenderer .flipX = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * ForceJump, ForceMode2D.Impulse);
            CanJump = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CanJump = true;
    }
}
