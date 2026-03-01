using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public UIDocument uiDocument;
    public InventoryManager inventoryManager;

    private VisualElement inventoryPanel;
    private VisualElement coinGrid;
    private Label[] equippedSlots = new Label[3];

    void OnEnable()
    {
        if(uiDocument == null)
        {
            Debug.LogError("UIDocument not assigned!");
            return;
        }

        if(inventoryManager == null)
        {
            Debug.LogError("InventoryManager not assigned!");
            return;
        }

        var root = uiDocument.rootVisualElement;

        inventoryPanel = root.Q<VisualElement>("InventoryPanel");
        coinGrid = root.Q<VisualElement>("CoinGrid");

        equippedSlots[0] = root.Q<Label>("EquippedSlot0");
        equippedSlots[1] = root.Q<Label>("EquippedSlot1");
        equippedSlots[2] = root.Q<Label>("EquippedSlot2");

        if(inventoryPanel == null)
            Debug.LogError("InventoryPanel NOT FOUND");

        if(coinGrid == null)
            Debug.LogError("CoinGrid NOT FOUND");

        PopulateInventory();
        RefreshEquipped();

        // Start hidden
        inventoryPanel.style.display = DisplayStyle.None;
    }

    void PopulateInventory()
    {
        if(coinGrid == null) return;
        coinGrid.Clear();

        foreach(var coin in inventoryManager.allCoins)
        {
            Button btn = new Button();
            btn.text = coin.name;
            btn.style.width = 100;
            btn.style.height = 100;

            btn.clicked += () =>
            {
                bool equipped = inventoryManager.EquipCoin(coin);
                RefreshEquipped();
            };

            coinGrid.Add(btn);
        }
    }

    void RefreshEquipped()
    {
        for(int i=0; i<3; i++)
        {
            if(i < inventoryManager.equippedCoins.Count)
            {
                equippedSlots[i].text = inventoryManager.equippedCoins[i].name;
                equippedSlots[i].style.backgroundColor = new StyleColor(new Color(0.7f,0.7f,0.7f,0.8f));
            }
            else
            {
                equippedSlots[i].text = "Empty";
                equippedSlots[i].style.backgroundColor = new StyleColor(new Color(0.4f,0.4f,0.4f,0.5f));
            }
        }
    }
}