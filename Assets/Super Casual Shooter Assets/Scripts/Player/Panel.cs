using UnityEngine;
using System.Collections;

public class Panel : MonoBehaviour
{
    // Speed of movement
    public GameObject _instructions;

    public IEnumerator panel()
    {
        _instructions.SetActive(true);
        yield return new WaitForSeconds(5);

        Vector3 startPos = _instructions.transform.position;
        Vector3 targetPos = startPos + Vector3.up; // Move up by 1 unit

        float duration = 1.0f; // Duration of the movement

        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float normalizedTime = (Time.time - startTime) / duration;
            _instructions.transform.position = Vector3.Lerp(startPos, targetPos, normalizedTime);
            yield return null;
        }

        // Ensure it reaches exactly to the target position
        _instructions.transform.position = targetPos;

        // Optionally, deactivate the instructions after movement
        //_instructions.SetActive(false);
    }
}
