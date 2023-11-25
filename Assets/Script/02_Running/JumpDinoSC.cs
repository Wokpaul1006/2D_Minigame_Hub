using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpDinoSC : MonoBehaviour
{
    [SerializeField] JumpManager manager;
    [HideInInspector] Rigidbody2D rb;
    private Vector3 originPos;
    public bool allowJump;
    private float jumpSpd;
    private void Start()
    {
        originPos = transform.position;
        jumpSpd = 5f;
        allowJump = false;
        manager = GameObject.Find("RunningManager").GetComponent<JumpManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (allowJump == true || Input.GetKeyDown(KeyCode.Space)) { Jump(); }
    }
    private void Jump()
    {
        //transform.position = new Vector3(originPos.x, Time.deltaTime * 10f, 0);
        rb.AddForce(transform.up * jumpSpd, ForceMode2D.Impulse);
        allowJump = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemies")
        {
            manager.ShowPause();
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
