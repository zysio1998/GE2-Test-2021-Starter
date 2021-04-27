using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    float throwForce = 900;   
    Vector3 objectsPosition;
    public bool canHold = true;
    public bool isHolding = false;
    public GameObject target;
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
            target.GetComponent<Rigidbody>().useGravity = false;
        }

        if (isHolding ==true)
        {
            target.transform.SetParent(temp.transform);

            if(Input.GetKey(KeyCode.Space))
            {
                target.GetComponent<Rigidbody>().AddForce(temp.transform.forward * throwForce);
                isHolding = false;
            }
        }
        else
        {
            objectsPosition = target.transform.position;
            target.transform.SetParent(null);
            target.GetComponent<Rigidbody>().useGravity = true;
            target.transform.position = objectsPosition;
        }
    }   
}




