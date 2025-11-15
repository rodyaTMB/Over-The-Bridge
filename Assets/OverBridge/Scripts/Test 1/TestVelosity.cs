using UnityEngine;

public class TestVelosity : MonoBehaviour
{
    float speed = 3f;
    bool velosityDone = false;

    void FixedUpdate()
    {
        if (!velosityDone)
        {
            GetComponent<Rigidbody2D>().linearVelocityX = 3f;
            velosityDone = true;
        }
    }
}
