using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour {
    
    private Rigidbody2D rb;
    public float ms;
    public float rs;
    public float mp;
    private int count;
    public float volume;

    private bool nitroPlayed;
    private float nitroMp;

    public AudioClip collisionSound;
    public AudioClip driveSound;
    public AudioClip nitroSound;

    private AudioSource[] sources;
    private AudioSource pickupSource;
    private AudioSource driveSource;
    private AudioSource nitroSource;


    // Use this for initialization
    void Start () {
        nitroPlayed = false;
        nitroMp = 2.5F;
        sources = GetComponents<AudioSource>();
        pickupSource = sources[0];
        driveSource = sources[1];
        nitroSource = sources[2];

        rb = GetComponent<Rigidbody2D>();
        count = 0;
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 1 * ms * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.A))
        {
            //transform.Translate(new Vector3(-1 * speed * Time.deltaTime, 0));
            transform.Rotate(new Vector3(0, 0, 1 * rs * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, -1 * ms * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D))
        {
            //transform.Translate(new Vector3(1 * speed * Time.deltaTime, 0));
            transform.Rotate(new Vector3(0, 0, -1 * rs * Time.deltaTime));
        }
        float multiplier = 1;

        if (Input.GetButton("A"))
        {
            multiplier = mp;
        }

        if (Input.GetAxis("RT") != 0)
        {
            driveSource.enabled = true;
            driveSource.loop = true;
            driveSource.volume = Input.GetAxis("RT") / nitroMp;
        } else
        {
            driveSource.enabled = false;
            driveSource.loop = false;
        }

        if (Input.GetButton("A"))
        {
            if (Input.GetAxis("RT") > 0.75)
            {
                if (!nitroSource.isPlaying && !nitroPlayed)
                {
                    nitroSource.Play();
                    nitroPlayed = true;
                    nitroMp = 2F;
                }
            }
            multiplier = mp;
        }
        if (Input.GetButtonUp("A"))
        {
            nitroPlayed = false;
            nitroMp = 2.5F;
        }

        transform.Translate(new Vector3(0, multiplier * ms * Input.GetAxis("RT") * Time.deltaTime));
        transform.Translate(new Vector3(0, -ms * Input.GetAxis("LT") * Time.deltaTime));

        float deadzone = 0.75f;

        Vector2 stickInput = new Vector2(Input.GetAxis("LSX"), Input.GetAxis("LSY"));
        if (stickInput.magnitude < deadzone)
            stickInput = Vector2.zero;

        transform.Rotate(new Vector3(0, 0, rs * stickInput.x * Time.deltaTime));

    }

    public static explicit operator PlayerController(GameObject v)
    {
        throw new NotImplementedException();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            pickupSource.PlayOneShot(collisionSound, volume);
            other.gameObject.SetActive(false);
            other.gameObject.transform.parent.gameObject.SetActive(false);
            count++;
        }
    }

    public int getCount()
    {
        return count;
    }

    
}
