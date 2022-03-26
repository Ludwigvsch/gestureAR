using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    /*
      Moving an object closer in the z over a period of time
    */
    public IEnumerator Move(float fadeDuration, GameObject gObject, float distance)
    {
        Vector3 position = gObject.transform.position;

        Vector3 target = new Vector3(position.x, position.y, position.z - distance);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            gObject.transform.position = Vector3.Lerp(position, target, elapsedTime / fadeDuration);
            yield return null;
        }
    }
}
