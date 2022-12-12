using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    private enum RotationDir { clock, counterclock };
    private Rigidbody2D rotatingRB;


    [SerializeField] private float speed = 20;
    [SerializeField] private RotationDir rotationDir = RotationDir.clock;

    // Start is called before the first frame update
    void Start()
    {
        rotatingRB = gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (rotationDir)
        {
            case RotationDir.clock:
            {
                rotatingRB.angularVelocity = -speed;
                break;
            }
            case RotationDir.counterclock:
            {
                rotatingRB.angularVelocity = speed;
                break;
            }
        }
    }
}
