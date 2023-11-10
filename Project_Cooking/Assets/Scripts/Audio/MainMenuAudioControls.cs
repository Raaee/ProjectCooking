using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudioControls : MonoBehaviour
{
    private int playerAudioAmt = 0;
    private const int MAX_AUDIO_AMOUNT = 100;
    // Start is called before the first frame update
    void Start()
    {
        try
        {

            ES3.Save("playerAudioAmt", MAX_AUDIO_AMOUNT);
            playerAudioAmt = ES3.Load<int>("myInt", MAX_AUDIO_AMOUNT);

        }
        catch (System.IO.IOException)
        {
            Debug.Log("The file is open elsewhere or there was not enough storage space");
        }
        catch (System.Security.SecurityException)
        {
            Debug.Log("You do not have the required permissions");
        }
    }

   
}
