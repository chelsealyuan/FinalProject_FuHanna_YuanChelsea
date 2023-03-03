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

            Debug.Log("player postition: " + player.transform.position);

            PlayerPrefs.SetInt("TimeToLoad", 0);

            PlayerPrefs.Save();
        }
        
    }

    public void PlayerPositionSave()
    {
        PlayerPrefs.SetFloat("p_x", player.transform.position.x);
        PlayerPrefs.SetFloat("p_y", player.transform.position.y);
        PlayerPrefs.SetInt("Saved", 1);
        Debug.Log("Saving player postition at: " + player.transform.position);
        PlayerPrefs.Save();
    }

    public void PlayerPositionLoad()
    {
        Debug.Log("Loading player postition at: " + player.transform.position);
        PlayerPrefs.SetInt("timeToLoad", 1);
        PlayerPrefs.Save();
    }
}
