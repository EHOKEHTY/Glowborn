using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private void ExitGame()
    {
        SaveManager.SaveGame();
        Application.Quit();
    }
}
