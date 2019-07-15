using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineSpinner : MonoBehaviour
{
    public GameObject outline;
    public int horizontalRotationSpeed = 10;
    public int verticalRotationSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        outline.transform.Rotate(verticalRotationSpeed, horizontalRotationSpeed, 0, Space.Self);
    }
}
