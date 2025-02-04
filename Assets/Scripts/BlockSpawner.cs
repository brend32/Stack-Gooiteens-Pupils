using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public Block blockPrefab; // Префаб блоку
    public Transform spawnPoint;   // Точка спавну блоку
    public Camera mainCamera;      // Камера для зміни фону
    public Gradient blockGradient;
    public float colorStep = 0.01f;

    private float blockCurrentProgress;
    private Color currentBlockColor;
    private Color currentBackgroundColor;

    private void Start()
    {
        UpdateColors();
    }

    [ContextMenu("Spawn Block")]
    public Block SpawnBlock()
    {
        // Створюємо новий блок і змінюємо його колір
        Block newBlock = Instantiate(blockPrefab, spawnPoint.position, Quaternion.identity);
        newBlock.SetColor(currentBlockColor);
        
        UpdateColors();
        return newBlock;
    }

    private void UpdateColors()
    {
        // Оновлюємо кольори для наступного блоку і фону
        blockCurrentProgress += colorStep;
        blockCurrentProgress %= 1;
        
        currentBlockColor = blockGradient.Evaluate(blockCurrentProgress);
        currentBackgroundColor = GetRandomColor();
        mainCamera.backgroundColor = currentBackgroundColor;
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}