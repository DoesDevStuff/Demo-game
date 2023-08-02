using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBullets : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text = null;
    [SerializeField]
    private TextMeshProUGUI _flaretext = null;

    public void UpdateBulletsText(int bulletCount)
    {
        if(bulletCount == 0)
        {
            _text.color = Color.grey; // alternatively red will see which shows up better
        }
        else
        {
            _text.color = Color.white;
        }
        _text.SetText(bulletCount.ToString());
    }

    public void UpdateFlareText(int flareCount)
    {
        if (flareCount == 0)
        {
            _flaretext.color = Color.red;
        }
        else
        {
            _flaretext.color = Color.white;
        }
        _flaretext.SetText(flareCount.ToString());
    }
}
