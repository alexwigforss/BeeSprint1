using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousMovement : MonoBehaviour
{
    public float upDownDistance = 1f; // Distance to move up and down
    public float movementSpeed = 0.005f; // Speed of movement
    private bool movingUp = true;

    private void Start()
    {
        StartCoroutine(UpDownCoroutine());
    }

    private IEnumerator UpDownCoroutine()
    {
        while (true)
        {
            float targetY = movingUp ? transform.position.y + upDownDistance : transform.position.y - upDownDistance;
            Vector3 targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);

            float elapsedTime = 0f;
            while (elapsedTime < 0.25f)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime);
                elapsedTime += Time.deltaTime * movementSpeed;
                yield return null;
            }

            // Wait for one second
            yield return new WaitForSeconds(0.01f);

            // Toggle direction
            movingUp = !movingUp;
        }
    }

}
