using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public GameObject player;
    public float mv;
    public AudioClip collisionSound;
    public AudioClip driveSound;
    private AudioSource[] sources;
    private AudioSource collisionSource;
    public float volume;

    public GameObject ExplosionPrefab;
    private GameObject Explosion;

    // Use this for initialization
    void Start () {
        collisionSource = GetComponent<AudioSource>();

        sources = GetComponents<AudioSource>();
        collisionSource = sources[0];
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 vectorToTarget = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5);

        transform.Translate(new Vector3(0, -1 * mv * Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            collisionSource.PlayOneShot(collisionSound, volume);
            Debug.Log("lose");
            TriggerExplosion(other.gameObject);

            other.gameObject.SetActive(false);
            other.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    

    void TriggerExplosion(GameObject parent)
    {
        Explosion = Instantiate(ExplosionPrefab, new Vector3(0,0,0), new Quaternion(0,0,0,0)) as GameObject;
        Explosion.transform.position = new Vector3(parent.transform.parent.position.x, parent.transform.parent.position.y, 0);
    }
}
