using UnityEngine;
using UnityEngine.UI;

public class HealthHeart : MonoBehaviour
{

    public Sprite heartEmpty, heart1_4, heart1_2, heart3_4, heartFull;

    Image heartImage;

    private void Awake()
    {
        heartImage = GetComponent<Image>();
    }
    public void SetHeartImage(HeartStatus heartStatus)
    {
        switch (heartStatus)
        {
            case HeartStatus.Empty:
                heartImage.sprite = heartEmpty;
                break;
            case HeartStatus.AQuater:
                heartImage.sprite = heart1_4;
                break;
            case HeartStatus.Half:
                heartImage.sprite = heart1_2;
                break;
            case HeartStatus.ThirdQuater:
                heartImage.sprite = heart3_4;
                break;
            case HeartStatus.Full:
                heartImage.sprite = heartFull;
                break;
        }
    }
}

public enum HeartStatus
{
    Empty = 0,
    AQuater = 1,
    Half = 2,
    ThirdQuater = 3,
    Full = 4,
}