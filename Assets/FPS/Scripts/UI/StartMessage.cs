using System.Collections;
using TMPro;
using UnityEngine;

public class StartMessage : MonoBehaviour
{
    public TextMeshProUGUI startText;
    //public HeartRateService heartRateService;
    //public float delay = 0.3f;  // Delay in seconds between each dot

    //public string baseText = "Connecting";

    //public bool isReady;

    void Start()
    {
        startText.gameObject.SetActive(true);
        //isReady = heartRateService.isSubscribed;
        //StartCoroutine(AnimateDots());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startText.gameObject.SetActive(false);
         }
    }

    //IEnumerator AnimateDots()
    //{
    //    while (true)
    //    {
    //        for (int i = 0; i <= 3; i++)
    //        {
    //            startText.text = baseText + new string('.', i);
    //            yield return new WaitForSeconds(delay);
    //        }
    //    }
    //}

}
