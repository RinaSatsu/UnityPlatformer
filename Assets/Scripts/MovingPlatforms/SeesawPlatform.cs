using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesawPlatform : MonoBehaviour
{
    private float startAngle = 90;
    private bool isWaiting = false;
    private enum RotationDir { clock, counterclock };
    private RotationDir rotationDir = RotationDir.clock;
    private Rigidbody2D rotatingRB;


    [SerializeField] private float speed = 20;
    [SerializeField] private float lowerAngle = -90;
    [SerializeField] private float upperAngle = 90;
    [SerializeField] private float waitTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        rotatingRB = gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>();
        startAngle = gameObject.transform.GetChild(0).transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// Rotates platform
    /// </summary>
    private void Move()
    {
        if (isWaiting)
        {
            rotatingRB.angularVelocity = 0;
            return;
        }
        if (gameObject.transform.GetChild(0).transform.rotation.z <= Quaternion.Euler(0, 0, startAngle + lowerAngle).z)
        {
            rotationDir = RotationDir.counterclock;
            isWaiting = true;
            Invoke("Wait", waitTime);
        }
        if (gameObject.transform.GetChild(0).transform.rotation.z >= Quaternion.Euler(0, 0, startAngle + upperAngle).z)
        {
            rotationDir = RotationDir.clock;
            isWaiting = true;
            Invoke("Wait", waitTime);
        }
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

    /// <summary>
    /// Waits in one of end positions
    /// </summary>
    private void Wait()
    {
        isWaiting = false;
    }
}
