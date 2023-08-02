using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

// https://en.wikipedia.org/wiki/Fitness_proportionate_selection

public class ItemDropper : MonoBehaviour
{
    [SerializeField]
    private List<ItemSpawnData> _itemsToDrop = new List<ItemSpawnData>();
    float[] itemWeights;

    [SerializeField]
    [Range(0, 1)]
    private float _dropChance = 0.5f;

    private void Start()
    {
        // Linq allow us to query a list and and get from it all the items that matches a certain condition
        itemWeights = _itemsToDrop.Select(item => item.rate).ToArray();
    }

    public void DropItem()
    {
        var dropVariable = Random.value;
        if (dropVariable < _dropChance)
        {
            int index = GetRandomWeightedIndex(itemWeights);
            Instantiate(_itemsToDrop[index].itemPrefab, transform.position, Quaternion.identity);
        }
    }

    private int GetRandomWeightedIndex(float[] itemWeights)
    {
        float sum = 0f;
        for (int i = 0; i < itemWeights.Length; i++)
        {
            sum += itemWeights[i];
        }
        float randomValue = Random.Range(0, sum);
        float tempSum = 0;

        for (int i = 0; i < _itemsToDrop.Count; i++)
        {
            //0->0+Weight[0] item 1 (0->0.5)
            //Weight[0]-> Weight[0]+Weight[1](0.5 -> 0.7)
            //tempSum -> tempSu + Weights[N]
            if (randomValue >= tempSum && randomValue < tempSum + itemWeights[i])
            {
                return i;
            }
            tempSum += itemWeights[i];
        }
        return 0;
    }
}

[Serializable]
public struct ItemSpawnData
{
    [Range(0, 1)]
    public float rate;
    public GameObject itemPrefab;
}
