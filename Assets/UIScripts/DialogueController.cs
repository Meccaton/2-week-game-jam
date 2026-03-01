using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class DialogueController : MonoBehaviour
{
    public UIDocument document;          // assign in inspector
    public DialogueData dialogueData;    // assign dialogue lines
    public Animator npcAnimator;         // optional
    public GameObject inventoryUI;       // assign InventoryUI GameObject

    private Label dialogueLabel;
    private int currentLine = 0;
    private bool isTyping = false;
    private bool dialogueFinished = false;
    private Coroutine typingCoroutine;
    private bool inventoryOpen = false;

    void Start()
    {
        if(document == null)
        {
            Debug.LogError("UIDocument not assigned in DialogueController!");
            return;
        }

        var root = document.rootVisualElement;
        dialogueLabel = root.Q<Label>("dialogueLabel");

        if(dialogueLabel == null)
            Debug.LogError("dialogueLabel not found in UIDocument!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !dialogueFinished && dialogueLabel != null)
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
                    dialogueFinished = true;
                    if(npcAnimator != null)
                    {
                        Debug.Log("Triggering NPC leave animation");
                        npcAnimator.SetTrigger("LeaveTrigger");
                    }
                }
            }
            else
            {
                // Finish typing instantly
                if (typingCoroutine != null)
                    StopCoroutine(typingCoroutine);

                dialogueLabel.text = dialogueData.lines[currentLine - 1];
                isTyping = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Toggling inventory");
            VisualElement inventoryPanel = document.rootVisualElement.Q<VisualElement>("InventoryPanel");

            Debug.Log(inventoryPanel);

            bool open = inventoryPanel.style.display != DisplayStyle.Flex;

            inventoryPanel.style.display =
                open ? DisplayStyle.Flex : DisplayStyle.None;

            var blur = document.rootVisualElement.Q<VisualElement>("BlurOverlay");

            if(blur != null)
                blur.style.display =
                    open ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }

    private IEnumerator TypeText(string text)
    {
        if(dialogueLabel == null) yield break;

        isTyping = true;
        dialogueLabel.text = "";

        foreach (char letter in text)
        {
            dialogueLabel.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        isTyping = false;
    }

    void ToggleInventory()
    {
        inventoryOpen = !inventoryOpen;

        inventoryUI.SetActive(inventoryOpen);

        if (inventoryOpen)
        {
            Time.timeScale = 0f; // Pause game
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}