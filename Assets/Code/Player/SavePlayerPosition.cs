using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerPosition : MonoBehaviour
{

    public GameObject player;

    private void Start()
    {

        if(PlayerPrefs.GetInt("Saved") == 1 && PlayerPrefs.GetInt("TimeToLoad") == 1)
        {
            float pX = player.transform.position.x;
            float pY = player.transform.position.y;

            pX = PlayerPrefs.GetFloat("p_x");
            pY = PlayerPrefs.GetFloat("p_y");

            player.transform.position = new Vector2(pX, pY);

            PlayerPrefs.SetInt("TimeToLoad", 0);

            PlayerPrefs.Save();
        }
        
    }

    public void PlayerPositionSave()
    {
        float adjusted_pX;
        float adjusted_pY;

        /*
         TODO: Come up with a method to better to make sure character
        does not accidentally touch fight trigger again upon resetting
        into the exploration scene

        //NOTE THE CURRENT CALCULATION BELOW WILL BUG IF YOU APPROACH THE
        OBSTACLE FROM THE LOWER LEFT
        */

        adjusted_pX = player.transform.position.x + 1;
        adjusted_pY = player.transform.position.y + 1;

        PlayerPrefs.SetFloat("p_x", adjusted_pX);
        PlayerPrefs.SetFloat("p_y", adjusted_pY);
        PlayerPrefs.SetInt("Saved", 1);
        Debug.Log(player.transform.position.x + " and " + player.transform.position.y);
        //Debug.Log(adjusted_pX + " and " + adjusted_pY);
        PlayerPrefs.Save();
    }

    public void PlayerPositionLoad()
    {
        //Debug.Log("Loading player postition at: " + player.transform.position);
        PlayerPrefs.SetInt("TimeToLoad", 1);
        PlayerPrefs.Save();
    }
}
