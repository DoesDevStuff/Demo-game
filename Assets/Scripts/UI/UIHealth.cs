using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject _heartPrefab = null;
    [SerializeField]
    private GameObject _healthPanel = null;

    [SerializeField]
    private Sprite _heartFull = null, _heartEmpty = null;

    private int _heartCount = 0;
    private List<Image> _heartsList = new List<Image>();

    public void InitialiseLives(int livesCount)
    {
        _heartCount = livesCount;
        foreach(Transform child in _healthPanel.transform)
        { //loop through and destroy hearts
            Destroy(child.gameObject);
        }

        for(int i = 0; i < livesCount; i++)
        { // add hearts to list
            _heartsList.Add(Instantiate(_heartPrefab, _healthPanel.transform).GetComponent<Image>());
        }
    }

    public void UpdateUI(int health)
    {
        int currentIndex = 0;
        for(int i = 0; i < _heartCount; i++)
        {
            if(currentIndex >= health)
            {
                _heartsList[i].sprite = _heartEmpty;
            }
            else
            {
                _heartsList[i].sprite = _heartFull;
            }
            currentIndex++;
        }
    }
}
