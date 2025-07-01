using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    private const string PlayerPosX = "PlayerPosX";
    private const string PlayerPosY = "PlayerPosY";
    private const string PlayerHP = "PlayerHP";
    private const string SceneName = "SceneName";

    public static void SavePlayer(Player playerTransform)
    {
        PlayerPrefs.SetString(SceneName, SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat(PlayerPosX, playerTransform.transform.position.x);
        PlayerPrefs.SetFloat(PlayerPosY, playerTransform.transform.position.y);
        PlayerPrefs.SetInt(PlayerHP, playerTransform.currentHealth);
        PlayerPrefs.Save();
    }

    public static void LoadPlayer(Player player)
    {
        if (PlayerPrefs.HasKey(PlayerPosX) && PlayerPrefs.HasKey(PlayerPosY))
        {
            if (PlayerPrefs.GetString(SceneName) != SceneManager.GetActiveScene().name)
            {
                SceneManager.LoadScene(PlayerPrefs.GetString(SceneName));
            }
            float x = PlayerPrefs.GetFloat(PlayerPosX);
            float y = PlayerPrefs.GetFloat(PlayerPosY);
            player.transform.position = new Vector3(x, y, player.transform.position.z);
        }
    }

    public static void SaveGame()
    {
        PlayerPrefs.Save();
    }
}