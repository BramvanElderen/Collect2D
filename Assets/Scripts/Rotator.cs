using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public float speed;

	void Update () {
        this.transform.Rotate(new Vector3(0, 0 , 1 * speed) * Time.deltaTime);
	}
}
