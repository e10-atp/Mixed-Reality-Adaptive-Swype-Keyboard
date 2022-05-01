using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorWhileCollid : MonoBehaviour
{
    [SerializeField] Material[] materials = new Material[4];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {

        //GameObject childObject = this.transform.parent.transform.Find("Key").gameObject.transform.Find("Geometry").gameObject;
        //this.GetComponent<MeshRenderer>().material = materials[3];
        col.gameObject.transform.parent.transform.Find("Key").gameObject.transform.Find("Geometry").gameObject.GetComponent<MeshRenderer>().material = materials[1];
        this.GetComponent<MeshRenderer>().material = materials[3];
     
       
    }

    void OnTriggerExit(Collider col)
    {
        col.gameObject.transform.parent.transform.Find("Key").gameObject.transform.Find("Geometry").gameObject.GetComponent<MeshRenderer>().material = materials[0];
        this.GetComponent<MeshRenderer>().material = materials[2];
}
}
