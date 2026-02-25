using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class DialogueController : MonoBehaviour
{
    public UIDocument document;

    // Assign the dialogue asset in the Inspector
    public DialogueData dialogueData;

    private UnityEngine.UIElements.Label dialogueLabel;

    private int currentLine = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        var root = document.rootVisualElement;
        dialogueLabel = root.Q<UnityEngine.UIElements.Label>("dialogueLabel");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueData == null || dialogueData.lines.Length == 0)
                return;

            if (!isTyping)
            {
                if (currentLine < dialogueData.lines.Length)
                {
                    typingCoroutine = StartCoroutine(TypeText(dialogueData.lines[currentLine]));
                    currentLine++;
                }
                else
                {
                    dialogueLabel.text = "";
                    currentLine = 0; // reset to allow replay
                }
            }
            else
            {
                // Finish typing instantly
                StopCoroutine(typingCoroutine);
                dialogueLabel.text = dialogueData.lines[currentLine - 1];
                isTyping = false;
            }
        }
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