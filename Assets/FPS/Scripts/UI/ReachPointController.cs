using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachPointController : MonoBehaviour
{
    public GameObject reachPoint;  // Drag your reach point GameObject here in the Inspector

    private void Start()
    {
        // Ensure the reach point is initially inactive
        reachPoint.SetActive(false);

        // Start the coroutine to show the reach point after 1 minute
        StartCoroutine(ShowReachPointAfterDelay());
    }

    private IEnumerator ShowReachPointAfterDelay()
    {
        yield return new WaitForSeconds(60);  // Wait for 1 minute
        reachPoint.SetActive(true);  // Show the reach point
    }
}
