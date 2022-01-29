using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    Rigidbody rb;
    Vector2 lastMousePos = Vector2.zero;
    [SerializeField] float thrust = 350f;
    [SerializeField] float wallDistance = 4.5f;
    [SerializeField] float minCamDistance = 4f;
    [SerializeField] float speed = 5f;
    public GameObject winPanel;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        Vector2 deltaPos = Vector2.zero;

        if (Input.GetMouseButton(0))
        {
            Vector2 currentMousePos = Input.mousePosition;
            if(lastMousePos == Vector2.zero)
                lastMousePos = currentMousePos;

            deltaPos = currentMousePos - lastMousePos;

            lastMousePos = currentMousePos;

            Vector3 force = new Vector3(deltaPos.x, 0, deltaPos.y) * thrust;
            rb.AddForce(force);
        }
        else
        {
            lastMousePos = Vector2.zero;
        }

    }

    private void LateUpdate()
    {
        Vector3 pos = transform.position; 
        if (pos.z < Camera.main.transform.position.z + minCamDistance)
        { 
            pos.z = Camera.main.transform.position.z + minCamDistance;
        }

        if(pos.x < -wallDistance)
        {
            pos.x = -wallDistance;
        }
        else if (pos.x > wallDistance)
        {
            pos.x = wallDistance;
        }

        transform.position = pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Evil"))
            StartCoroutine(Die(2));
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hello");
        if (other.gameObject.CompareTag("FinishLine"))
            StartCoroutine(Win(1));


    }

    private void FixedUpdate()
    { 
        rb.MovePosition(rb.position + Vector3.forward * speed * Time.fixedDeltaTime); 
        Camera.main.transform.position += Vector3.forward * speed * Time.fixedDeltaTime;
    }

    IEnumerator Die(float delayTime)
    {
        Debug.Log("You're dead");
        
        speed = 0;
        thrust = 0;

        yield return new WaitForSeconds(delayTime);

        SceneManager.LoadScene(0);
    }

    IEnumerator Win(float delayTime)
    {
        thrust = 0;
        speed = 0;
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(delayTime);

        winPanel.SetActive(true);

    }
}
