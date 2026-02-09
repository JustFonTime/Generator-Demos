using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/QuestType")]
public class QuestType : ScriptableObject
{
    public string questTypeName;

    [Range(0f, 1f)]
    public float weight;

    [TextArea] public List<string> possibleDescriptions;
}
