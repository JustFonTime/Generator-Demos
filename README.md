# What is This?

This tool was designed for developers who seek to implement a quest system into their game but are unsure of where to get started. Generated outputs of this tool provide a simple visualization of a quest structure.

# How to Use:

1. Run the tool
2. Edit parameters for generation as you see fit for the current generation
3. Press the Generate button
4. Click on any of the quest nodes to see more detailed information
5. Press [R] if you want to regenerate using the same parameters
6. Press [ESC] if you want to go back to the prompt screen and change your parameters

# Example Outputs:
<img width="2100" height="1180" alt="Screenshot 2026-02-11 184244" src="https://github.com/user-attachments/assets/5f5eef07-01e2-44ea-88eb-499400f1de96" />
<img width="2100" height="1180" alt="Screenshot 2026-02-11 184323" src="https://github.com/user-attachments/assets/c5a308d5-8ccf-4871-983a-6b9646c8f775" />
<img width="2100" height="1180" alt="Screenshot 2026-02-11 184431" src="https://github.com/user-attachments/assets/ac2ccb5a-e4df-40e5-b32c-5118df62d1c3" />
<img width="2100" height="1180" alt="Screenshot 2026-02-11 184449" src="https://github.com/user-attachments/assets/002ec3de-5635-433e-a385-2cc704a3cc08" />


# Installation Instructions:
> Developed in Unity 6000.3.4f1

1. Clone the Repository
2. Open and run the scene 'QuestGen`


# Adding Your Own Data?

> In ```../Scripts/Quests/ScriptableObjects``` you will find two folders ```QuestGenre``` and ```QuestType```.

> This tool applies data stored inside ScriptableObjects, to create one simply right-click then ```Create -> Quest -> REQUIREDTYPE```

### New Genre:
1. Locate the folder ```QuestGenre``` and create a new ScriptableObject of ```QuestGenre```. Label it in the following format ```QuestGenre_YourGenre```
2. In the script ```Quest.cs``` you will need to edit the enum to contain your genre
3. Now, back to the ScriptableObject, you need to set the genre and the type of quests that can show up
4. Locate in the scene hierarchy the Game Object called _InfluenceOption_ (it is a UI Dropdown) and add a new option, and label it according to your genre
5. Locate in the scene hierarchy the Game Object called _Event System_, you will need to scroll down to the script called ```Assign Quest Data``` and add your new genre

### New Type:
1. Locate the folder ```QuestType``` and create a new ScriptableObject of ```QuestType```. Label it in the following format ```QuestType_YourQuestType```
2. Inside the ScriptableObject in the inspector, you need to set the weight of the quest type (how likely the quest will appear) as well as the possible descriptions (generation pulls randomly from this list of descriptions). You also need to set the name of the Quest Type (ex: Deliver)
3. Locate the ```QuestGenre``` ScriptableObject that you feel will best fit your new ```QuestType``` and add it 
