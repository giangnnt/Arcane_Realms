using TMPro;
using UnityEngine;

public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text coinText;
    private int currentCoin = 0;

    public void UpdateCurrentCoin()
    {
        currentCoin += 1;

        if (coinText == null)
        {
            coinText = GameObject.Find("Gold Amount Text").GetComponent<TMP_Text>();
        }
        coinText.text = currentCoin.ToString("D3");
    }
}
