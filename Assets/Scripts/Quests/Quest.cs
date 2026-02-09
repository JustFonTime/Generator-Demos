using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/QuestGenre")]
public class QuestGenre : ScriptableObject
{
    public enum QuestGenres
    {
        Adventure,
        MMO,
        RPG
    }

    public QuestGenres Genre;

    public List<QuestType> questTypes;
}
