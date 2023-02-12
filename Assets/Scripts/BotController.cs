using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    private float speed = 10.0f;
    private float turnSpeed = 5.0f; 
    public float range = 10f;
    public float angle = 15f;
    public float xRange = 8f;
    public LayerMask obstacleLayer;

    
    


    // Start is called before the first frame update
    void Start()
    {
        int layerMaskInt = (int)Mathf.Log(obstacleLayer.value, 2);
        Debug.Log("The layer mask value is: " + layerMaskInt);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.forward * Time.deltaTime * speed);


        Collider[] colliders = Physics.OverlapSphere(transform.position, range, obstacleLayer);
        if (colliders.Length > 0)
        {
            Vector3 direction = (transform.position - colliders[0].transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Quaternion targetRotation = Quaternion.Euler(0, lookRotation.eulerAngles.y + angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            Debug.Log("an Obstacle has been detected");
        }
        if(transform.position.x < -xRange){
            transform.position = new Vector3(-xRange,transform.position.y,transform.position.z);
        }

        if(transform.position.x > xRange){
            transform.position = new Vector3(xRange,transform.position.y,transform.position.z);
        }
    }
}
