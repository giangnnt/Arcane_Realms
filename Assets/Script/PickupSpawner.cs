using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject coin, heathGlobe, staminaGlobe;

    public void DropItems()
    {
        int randomNum = UnityEngine.Random.Range(1, 5);
        if (randomNum == 1)
        {
            Instantiate(staminaGlobe, transform.position, Quaternion.identity);
        }
        if (randomNum == 2)
        {
            Instantiate(heathGlobe, transform.position, Quaternion.identity);
        }

        if (randomNum == 3)
        {
            int randomAmountOfCoin = UnityEngine.Random.Range(1, 4);
            for (int i = 0; i < randomAmountOfCoin; i++)
            {
                Instantiate(coin, transform.position, Quaternion.identity);
            }
        }

    }
}
