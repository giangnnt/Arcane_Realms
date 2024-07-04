using System.Collections.Generic;
using UnityEngine;

public class HeartbarSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject heartPrefab;


    private List<HealthHeart> hearts = new();

    public void DrawHeart(int maxHealth, int currentHealth)
    {
        ClearHeats();
        int maxHeartToFill = (maxHealth / 4) + (maxHealth % 4 == 0 ? 0 : 1);
        for (int i = 0; i < maxHeartToFill; i++)
        {
            CreateEmptyHeart();
        }
        for (int i = 0; i < maxHeartToFill; i++)
        {
            if (currentHealth > 4)
            {
                hearts[i].SetHeartImage(HeartStatus.Full);
                currentHealth -= 4;
            }
            else
            {
                hearts[i].SetHeartImage((HeartStatus)currentHealth);
                currentHealth = 0;
            }
        }
    }
    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }
    public void ClearHeats()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new();
    }
}
