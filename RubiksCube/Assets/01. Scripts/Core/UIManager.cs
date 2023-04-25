using UnityEngine;

public class UIManager
{
    public static UIManager Instance = null;

    private RectTransform menuPanel = null;
    public RectTransform MenuPanel
    {
        get
        {
            if (menuPanel == null)
                menuPanel = DEFINE.MainCanvas.Find("MenuPanel").GetComponent<RectTransform>();

            return menuPanel;
        }
    }

    private RectTransform inventoryPanel = null;
    public RectTransform InventoryPanel {
        get {
            if (inventoryPanel == null)
                inventoryPanel = DEFINE.MainCanvas.Find("InventoryPanel").GetComponent<RectTransform>();

            return inventoryPanel;
        }
    }
    
    private RectTransform inputPanel = null;
    public RectTransform InputPanel {
        get {
            if (inputPanel == null)
                inputPanel = DEFINE.MainCanvas.Find("InputPanel").GetComponent<RectTransform>();

            return inputPanel;
        }
    }
}
