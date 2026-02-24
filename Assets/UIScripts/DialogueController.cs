using UnityEngine   ;
using UnityEngine.UIElements;
using System.Collections;
using System.Reflection.Emit;

public class DialogueController : MonoBehaviour
{
    public UIDocument document;

    private UnityEngine.UIElements.Label dialogueLabel;
    private bool isTyping;

    void Start()
    {
        var root = document.rootVisualElement;
        dialogueLabel = root.Q<UnityEngine.UIElements.Label>("dialogueLabel");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartDialogue("Hello punk. Ima beat you to a pulp");
            StartDialogue("Just wait.");
        }
    }

    public void StartDialogue(string text)
    {
        if (!isTyping)
            StartCoroutine(TypeText(text));
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueLabel.text = "";

        foreach (char letter in text)
        {
            dialogueLabel.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        isTyping = false;
    }
}