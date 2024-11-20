using UnityEngine;

public class StorePanel : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private UIBoosterUpgradeSelection shieldUpgrade;
    [SerializeField] private UIBoosterUpgradeSelection magnetUpgrade;
    [SerializeField] private UIBoosterUpgradeSelection x2Upgrade;
    [SerializeField] private UIBoosterUpgradeSelection dashUpgrade;
    [SerializeField] private UIExitSelection exitSelection;
    [SerializeField] private UIAddHealthSelection healthSelection;

    private bool isStoreNearby;

    private UICoord currentPos;
    public UICoord CurrentPos => currentPos;

    private const int columns = 4;
    private const int rows = 2;

    private UISelection[,] selections;
    
    private void Awake()
    {
        currentPos = new UICoord(3, 0);
        selections = new UISelection[rows, columns];

        selections[0, 0] = shieldUpgrade;
        selections[0, 1] = magnetUpgrade;
        selections[0, 2] = x2Upgrade;
        selections[0, 3] = exitSelection;
        
        selections[1, 0] = healthSelection;
        selections[1, 2] = dashUpgrade;
    }

    private void OnEnable()
    {
        input.DisableGameplayInput();
        input.InteractEvent += Press;
        input.UIXMoveEvent += XMove;
        input.UIYMoveEvent += YMove;
    }

    private void OnDisable()
    {
        input.EnableGameplayInput();
        input.InteractEvent -= Press;
        input.UIXMoveEvent -= XMove;
        input.UIYMoveEvent -= YMove;
    }

    private void Press()
    {
        Debug.Log(selections[currentPos.y, currentPos.x].name);
        selections[currentPos.y, currentPos.x].Press();
        return;
        Debug.Log(isStoreNearby);
        Debug.Log(gameObject.activeInHierarchy);
        
        if (!gameObject.activeInHierarchy && isStoreNearby)
        {
            gameObject.SetActive(true);
        }
        else
        {
            
        }
    }

    public void OnStoreApproaching()
    {
        isStoreNearby = true;
    }

    public void OnStoreLeave()
    {
        isStoreNearby = false;
    }
    
    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void XMove(int value)
    {
        var newPos =  new UICoord(currentPos.x + value, currentPos.y);
        Debug.Log($"x {newPos.x} {newPos.y}");
        
        
        if (IsValid(newPos))
        {
            if (newPos is {y: 1, x: 1})
            {
                newPos = new UICoord(newPos.x + value, newPos.y);
            }
            else if (newPos is {y: 1, x: 3})
            {
                newPos = new UICoord(newPos.x, newPos.y - 1);
            }
            
            selections[currentPos.y, currentPos.x].Select(false);
            currentPos = newPos;
            selections[currentPos.y, currentPos.x].Select(true);
        }
    }

    private void YMove(int value)
    {
        var newPos =  new UICoord(currentPos.x, currentPos.y + value);
        Debug.Log($"y {newPos.x} {newPos.y}");

        
        if (IsValid(newPos))
        {
            if (newPos is {y: 1, x: 1 or 3})
            {
                newPos = new UICoord(newPos.x - 1, newPos.y);
            }
            
            selections[currentPos.y, currentPos.x].Select(false);
            currentPos = newPos;
            selections[currentPos.y, currentPos.x].Select(true);
        }
    }

    private bool IsValid(UICoord coord)
    {
        if (coord.x < 0 || coord.y < 0) return false;
        if (coord.x > 3 || coord.y > 1) return false;
        
        return true;
    }
}
