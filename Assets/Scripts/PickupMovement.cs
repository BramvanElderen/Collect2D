using UnityEngine;
using System.Collections;
using System;

public class PickupMovement : MonoBehaviour {

    public float rs;
    public float mv;
    private Vector3 destination;
    private Vector3 direction;
    private Vector3 rotation;
    private Rigidbody2D rb;
    private Boolean isColliding;

	// Use this for initialization
	void Start () {
        isColliding = false;
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        System.Random rnd = new System.Random();

        direction = (new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0.0f, UnityEngine.Random.Range(-1.0f, 1.0f))).normalized;
        destination = new Vector3(rnd.Next(-46, 46), rnd.Next(-46, 46));
        Debug.Log("X: " + destination.x.ToString() + " Y: " + destination.y.ToString());
        rotation = new Vector3(0, 0, rnd.Next(0, 360));
        transform.Rotate(rotation);

    }
	
	// Update is called once per frame
	void Update () {
        isColliding = false;
        //this.transform.Rotate(new Vector3(0, 0, 1 * rs) * Time.deltaTime);
        //transform.forward.Set(1, 1, 0);
        //Vector3 position = transform.position;
        //float step = mv * Time.deltaTime;
        //Vector3 newPosition = Vector3.MoveTowards(position, destination, step);
        //if (newPosition != transform.position)
        //{
        //    transform.position = newPosition;
        //}
        //else
        //{
        //    System.Random rnd = new System.Random();
        //    destination = new Vector3(rnd.Next(-46, 46), rnd.Next(-46, 46));
        //}

        transform.Translate(new Vector3(0, 1 * mv * Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isColliding) return;
        isColliding = true;
        if (other.gameObject.CompareTag("WallCollider"))
        {
            GameObject collider = other.gameObject;
            float z = collider.transform.eulerAngles.z;
            float rotation = transform.eulerAngles.z;


            //Debug.Log("X: " + rotation.ToString() + " Z: " + z.ToString() + " Result: " + (-rotation).ToString());
            //transform.Rotate(new Vector3(0, 0, result));
            float result = 0;
            if ((int)z == 0 || (int)z == 180)
            {
                result = 180 - rotation;
            } else
            {
                result = -rotation;
            }
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, result));
            //(gameObject.GetComponent(typeof(Collider2D)) as Collider2D).isTrigger = true;
        }
    }
}
