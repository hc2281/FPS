using UnityEngine;
using TMPro;

public class StartMessage : MonoBehaviour
{
    public TextMeshProUGUI startText;

    void Start()
    {
        startText.text = "Press SPACE to start the game";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startText.gameObject.SetActive(false);
        }
    }

}
