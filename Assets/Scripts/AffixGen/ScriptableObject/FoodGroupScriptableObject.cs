using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Scriptable Objects/FoodGroup")]
public class FoodGroupScriptableObject : ScriptableObject
{
    public enum FoodGroups
    {
        Unclassified,
        Grains,
        Vegetables,
        Fruits,
        Protein,
        Dairy
    }

    public FoodGroups FoodGroup;

    public List<FoodScriptableObject> FoodNames;
}
