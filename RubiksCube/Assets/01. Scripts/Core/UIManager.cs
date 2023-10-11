using UnityEngine;

public class UIManager
{
    public static UIManager Instance { get; private set; } = null;

    static UIManager() {
        if (Instance != null)
            return; // 실행되면 무언가 잘못된거임

        Instance = new UIManager();
    }

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

    private KarmaPanel karmaPanel = null;
    public KarmaPanel KarmaPanel {
        get {
            if(karmaPanel == null)
                karmaPanel = DEFINE.MainCanvas.Find("KarmaPanel").GetComponent<KarmaPanel>();
            
            return karmaPanel;
        }
    }

    private ItemInfoPanel itemInfoPanel = null;
    public ItemInfoPanel ItemInfoPanel {
        get {
            if(itemInfoPanel == null)
                itemInfoPanel = DEFINE.MainCanvas.Find("ItemInfoPanel").GetComponent<ItemInfoPanel>();
        
            return itemInfoPanel;
        }
    }
}
