using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2Int currentPosition; // текущая позиция игрока
    private const int gridSize = 3; // размер сетки
    private const float moveTime = 0.2f; // время перемещения игрока

    private bool isMoving; // флаг, показывающий, движется ли игрок в данный момент

    private void Start()
    {
        currentPosition = Vector2Int.zero; // начальная позиция игрока
        transform.position = new Vector3(currentPosition.x, currentPosition.y, 0); // перемещаем игрока в начальную позицию
    }

    private void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.W))
            {
                MovePlayer(new Vector2Int(-1, 1));
            }
            else if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.S))
            {
                MovePlayer(new Vector2Int(-1, -1));
            }
            else if (Input.GetKeyDown(KeyCode.D) && Input.GetKeyDown(KeyCode.W))
            {
                MovePlayer(new Vector2Int(1, 1));
            }
            else if (Input.GetKeyDown(KeyCode.D) && Input.GetKeyDown(KeyCode.S))
            {
                MovePlayer(new Vector2Int(1, -1));
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                MovePlayer(Vector2Int.up);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                MovePlayer(Vector2Int.down);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                MovePlayer(Vector2Int.left);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                MovePlayer(Vector2Int.right);
            }

        }
    }

    private void MovePlayer(Vector2Int direction)
    {
        Vector2Int targetPosition = currentPosition + direction; // вычисляем целевую позицию игрока

        // проверяем, что целевая позиция находится в пределах сетки
        if (targetPosition.x >= 0 && targetPosition.x < gridSize &&
            targetPosition.y >= 0 && targetPosition.y < gridSize)
        {
            // запускаем корутину для перемещения игрока
            StartCoroutine(MoveCoroutine(targetPosition));
        }
    }

    private IEnumerator MoveCoroutine(Vector2Int targetPosition)
    {
        isMoving = true; // устанавливаем флаг, что игрок движется

        float t = 0; // таймер для интерполяции
        Vector3 startPosition = transform.position; // начальная позиция игрока
        Vector3 endPosition = new Vector3(targetPosition.x, targetPosition.y, 0); // конечная позиция игрока

        while (t < moveTime)
        {
            t += Time.deltaTime; // увеличиваем таймер
            transform.position = Vector3.Lerp(startPosition, endPosition, t / moveTime); // интерполируем позицию игрока
            yield return null; // ждем следующего кадра
        }

        // устанавливаем позицию игрока в конечную точку
        transform.position = endPosition;

        currentPosition = targetPosition; // обновляем текущую позицию игрока
        isMoving = false; // устанавливаем флаг, что игрок перестал двигаться
    }
}
