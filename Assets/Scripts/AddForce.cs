using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddForce : MonoBehaviour
{

    public int blue_score = 0;
    public int red_score = 0;

    public AudioSource hitSound;
    public AudioSource goalSound;
    public AudioSource winSound;

    public GameObject red_p;
    public GameObject blue_p;
    public GameObject puck;

    public static bool blue_w = false;
    public static bool red_w = false;

    public GameObject startGame;

    public TextMeshPro red_p_score;
    public TextMeshPro blue_p_score;

    private Vector3 lastFrameVelocity;
    private Rigidbody rb;

    void Start()
    {
        red_p_score.text = "" + red_score;
        blue_p_score.text = "" + blue_score;

    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        lastFrameVelocity = rb.velocity;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Bounce(collision.contacts[0].normal);
        hitSound.Play();

        if (collision.gameObject.tag == "red_goal")
        {
            goalSound.Play();
            blue_score++;
            blue_p_score.text = "" + blue_score;

            if (blue_score == 10)
            {
                winSound.Play();
                startGame.SetActive(false);
                blue_w = true;
                Debug.Log("Blue Win");
            }



            red_p.transform.localPosition = new Vector3(0, 0, 0);
            blue_p.transform.localPosition = new Vector3(0, 0, 0);
            puck.transform.localPosition = new Vector3(0, 0, 0);
        }

        if (collision.gameObject.tag == "blue_goal")
        {
            goalSound.Play();

            red_score++;
            red_p_score.text = "" + red_score;

            if (red_score == 10)
            {
                winSound.Play();
                startGame.SetActive(false);
                red_w = true;
                Debug.Log("Red Win");
            }

            red_p.transform.localPosition = new Vector3(0, 0, 0);
            blue_p.transform.localPosition = new Vector3(0, 0, 0);
            puck.transform.localPosition = new Vector3(0, 0, 0);    
        }

    }

    private void Bounce(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);
        rb.velocity = direction * Mathf.Min(speed * 0.7f);
    }
}
