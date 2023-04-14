using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables 
{
    public static int money { get; set; }

    public static string currentEnemy;

    public static GameObject currentObstacle;

    public static List<string> objectsDestroyed = new List<string>();
    


    public static int SetEnemy(string enemyName)
    {
        currentEnemy = enemyName;
        return 0;
    }
}

