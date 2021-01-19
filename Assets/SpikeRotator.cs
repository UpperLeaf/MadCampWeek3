using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeRotator : MonoBehaviour
{
    public float rotationSpeed;

    private int dir;

    private void Start()
    {
        dir = transform.parent.GetComponent<SpikeController>().direction;
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, dir * rotationSpeed * Time.deltaTime);
    }
}
