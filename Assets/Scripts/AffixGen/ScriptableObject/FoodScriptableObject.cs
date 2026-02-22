using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Food")]
public class FoodScriptableObject : ScriptableObject
{
    public enum FoodRarities
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    [Tooltip("Describes what is the rarity of the Food.")]
    public FoodRarities FoodRarity;

    [Tooltip("Determines how common the Food itself is.")]
    [Range(0f, 1f)]
    public float Weight;

    [Tooltip("The name of the Food.")]
    public string FoodName;

    [Tooltip("The sprite that is associated to represent the Food. At the moment this will be empty")]
    public Sprite FoodSprite;
}
