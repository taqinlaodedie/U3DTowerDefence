using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float upDown = 10 * Input.GetAxis("Mouse ScrollWheel") * speed * Time.deltaTime;

        transform.Translate(moveHorizontal, moveVertical, upDown);
    }
}
