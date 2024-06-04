using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSize : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 size;
    private Collider collider;

    void Start()
    {
        collider = GetComponent<Collider>();
        size = collider.bounds.size;
        Debug.Log(size);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
