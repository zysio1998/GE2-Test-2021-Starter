using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    float throwForce = 900;   
    Vector3 objectsPosition;
    public bool canHold = true;
    public bool isHolding = false;
    public GameObject ball;
    public GameObject temp;   
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isHolding = true;
            ball.GetComponent<Rigidbody>().useGravity = false;
        }

        if (isHolding ==true)
        {            
            ball.transform.SetParent(temp.transform);

            if(Input.GetKey(KeyCode.Space))
            {
                ball.GetComponent<Rigidbody>().AddForce(temp.transform.forward * throwForce);
                isHolding = false;
            }
        }
        else
        {
            objectsPosition = ball.transform.position;
            ball.transform.SetParent(null);
            ball.GetComponent<Rigidbody>().useGravity = true;
            ball.transform.position = objectsPosition;
        }
    }   
}




