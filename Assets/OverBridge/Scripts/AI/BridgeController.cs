using UnityEngine;

public class BridgeController : MonoBehaviour
{
    [Header("Bridge Settings")]
    public float growSpeed = 2f;
    public float fallSpeed = 5f;
    public float maxLength = 10f;
    
    [Header("Bridge State")]
    public bool isGrowing = false;
    public bool isFalling = false;
    public bool isPlaced = false;
    
    [Header("References")]
    public Transform startPoint;
    public Transform bridgeVisual;
    private float currentLength = 0f;
    private float targetRotation = 0f;
    
    void Update()
    {
        if (isGrowing)
        {
            GrowBridge();
        }
        else if (isFalling)
        {
            FallBridge();
            return;
        }
    }
    
    public void StartGrowing()
    {
        if (!isPlaced && GameManager.Instance.isGameActive)
        {
            isGrowing = true;
            isFalling = false;
            currentLength = 0f;
            bridgeVisual.localScale = new Vector3(1f, currentLength, 1f);
        }
    }
    
    public void StopGrowing()
    {
        if (isGrowing)
        {
            isGrowing = false;
            isFalling = true;
            targetRotation = 0f;
        }
    }
    
    void GrowBridge()
    {
        currentLength += growSpeed * Time.deltaTime;
        currentLength = Mathf.Min(currentLength, maxLength);
        bridgeVisual.localScale = new Vector3(1f, currentLength, 1f);
    }
    
    void FallBridge()
    {
        float currentRotation = transform.eulerAngles.z;
        float newRotation = Mathf.LerpAngle(currentRotation, targetRotation, fallSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, newRotation);
        
        if (Mathf.Abs(Mathf.DeltaAngle(currentRotation, targetRotation)) < 1f)
        {
            isFalling = false;
            isPlaced = true;
            OnBridgePlaced();
        }
    }
    
    void OnBridgePlaced()
    {
        GameManager.Instance.hero.StartMoving();
    }
    
    public Vector2 GetEndPosition()
    {
        return bridgeVisual.position + bridgeVisual.up * currentLength;
    }
    
    public void ResetBridge()
    {
        isGrowing = false;
        isFalling = false;
        isPlaced = false;
        currentLength = 0f;
        transform.rotation = Quaternion.identity;
        bridgeVisual.localScale = new Vector3(1f, 0f, 1f);
    }
}