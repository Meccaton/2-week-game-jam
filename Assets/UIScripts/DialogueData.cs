using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/Dialogue Set")]
public class DialogueData : ScriptableObject
{
    [TextArea(2, 10)] // allows multi-line editing in Inspector
    public string[] lines;
}