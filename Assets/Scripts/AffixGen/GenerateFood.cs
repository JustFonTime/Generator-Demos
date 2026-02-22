using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GenerateFood : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI targetText;

    [SerializeField] private List<FoodGroupScriptableObject> foodList;

    public void Generate()
    {
        foreach (var food in foodList)
        {
            for(int i = 0; i < food.FoodNames.Count; i++)
            {
                print("Food Name is: " + food.FoodNames[i].FoodName);
            }
        }
    }
}
