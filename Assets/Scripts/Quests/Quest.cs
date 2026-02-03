using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class QuestList
{
    public List<Quest> quests;
}
public class Quest
{
    public string name;
    public List<QuestType> types;
}

public class QuestType
{
    public string name;
}
