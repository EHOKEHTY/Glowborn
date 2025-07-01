using System.Collections.Generic;
using UnityEngine;

public class UIHealth : MonoBehaviour
{
    public static UIHealth Instance;

    public GameObject heartPrefab;
    public Transform heartsContainer;

    [SerializeField] Player player;

    private List<GameObject> hearts = new List<GameObject>();

    private void Awake()
    {
        Instance = this;

        float spacing = 80f;

        for (int i = 0; i < player.maxHealth; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartsContainer);
            heart.transform.localPosition = new Vector3(i * spacing, 0, 0);
            hearts.Add(heart);
        }
    }

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].SetActive(i < currentHealth);
        }
    }
}