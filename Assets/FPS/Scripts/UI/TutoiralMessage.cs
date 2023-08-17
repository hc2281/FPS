using System.Collections;
using UnityEngine;

public class TutoiralMessage : StartMessage
{
    public float gameStartDelay = 300f; // 5 minutes in seconds

    private new void Start()
    {
        base.Start(); // Calls the Start method from the base class (StartMessage)

        // Start the countdown to begin the game
        StartCoroutine(WaitAndStartGame());
    }

    IEnumerator WaitAndStartGame()
    {
        yield return new WaitForSeconds(gameStartDelay);

        // Your logic to start the game goes here
    }
}
