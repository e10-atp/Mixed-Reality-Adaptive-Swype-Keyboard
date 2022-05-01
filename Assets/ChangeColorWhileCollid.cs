using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
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

        // col.gameObject.transform.parent.transform.Find("Key").gameObject.transform.Find("Geometry").gameObject
        //     .GetComponent<MeshRenderer>().material = materials[2];
        // //this.GetComponent<MeshRenderer>().material = materials[3];
        // col.gameObject.transform.parent.transform.Find("Key").gameObject.transform.Find("Geometry").gameObject
        //     .GetComponent<MeshRenderer>().material = materials[0];
        StartCoroutine(ChangeColor(col));
    }

    // void OnTriggerExit(Collider col)
    // {
    //     col.gameObject.transform.parent.transform.Find("Key").gameObject.transform.Find("Geometry").gameObject
    //         .GetComponent<MeshRenderer>().material = materials[0];
    //     this.GetComponent<MeshRenderer>().material = materials[2];
    // }
    // private void OnDisable()
    // {
    //     col.gameObject.transform.parent.transform.Find("Key").gameObject.transform.Find("Geometry").gameObject
    //     .GetComponent<MeshRenderer>().material = materials[0];
    // }
    IEnumerator ChangeColor(Collider col)
    {
        col.gameObject.transform.parent.transform.Find("Key/Geometry")
            .gameObject
            .GetComponent<MeshRenderer>().material = materials[2];
        yield return new WaitForSeconds(0.4f);
        col.gameObject.transform.parent.transform.Find("Key/Geometry")
            .gameObject
            .GetComponent<MeshRenderer>().material = materials[0];
    }
}