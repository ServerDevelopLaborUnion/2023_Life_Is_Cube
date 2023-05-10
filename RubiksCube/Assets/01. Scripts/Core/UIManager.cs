using UnityEngine;

public class UIManager
{
    public static UIManager Instance = null;

    private MenuBar menuPanel = null;
    public MenuBar MenuPanel
    {
        get {
            if (menuPanel == null)
                menuPanel = DEFINE.MainCanvas.Find("MenuPanel").GetComponent<MenuBar>();

            return menuPanel;
        }
    }

    private InventoryPanel inventoryPanel = null;
    public InventoryPanel InventoryPanel {
        get {
            if (inventoryPanel == null)
                inventoryPanel = DEFINE.MainCanvas.Find("InventoryPanel").GetComponent<InventoryPanel>();

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

    private HPPanel hpPanel = null;
    public HPPanel HPPanel {
        get {
            if(hpPanel == null)
                hpPanel = DEFINE.MainCanvas.Find("HPPanel").GetComponent<HPPanel>();
            
            return hpPanel;
        }
    }
    
    private MapPanel mapPanel = null;
    public MapPanel MapPanel {
        get {
            if(mapPanel == null)
                mapPanel = DEFINE.MainCanvas.Find("MapPanel").GetComponent<MapPanel>();
            
            return mapPanel;
        }
    }

    private StatPanel statPanel = null;
    public StatPanel StatPanel {
        get {
            if(statPanel == null)
                statPanel = DEFINE.MainCanvas.Find("StatPanel").GetComponent<StatPanel>();
            
            return statPanel;
        }
    }
}
