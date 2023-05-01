using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2Int currentPosition; // ������� ������� ������
    private const int gridSize = 3; // ������ �����
    private const float moveTime = 0.2f; // ����� ����������� ������

    private bool isMoving; // ����, ������������, �������� �� ����� � ������ ������

    private void Start()
    {
        currentPosition = Vector2Int.zero; // ��������� ������� ������
        transform.position = new Vector3(currentPosition.x, currentPosition.y, 0); // ���������� ������ � ��������� �������
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
        Vector2Int targetPosition = currentPosition + direction; // ��������� ������� ������� ������

        // ���������, ��� ������� ������� ��������� � �������� �����
        if (targetPosition.x >= 0 && targetPosition.x < gridSize &&
            targetPosition.y >= 0 && targetPosition.y < gridSize)
        {
            // ��������� �������� ��� ����������� ������
            StartCoroutine(MoveCoroutine(targetPosition));
        }
    }

    private IEnumerator MoveCoroutine(Vector2Int targetPosition)
    {
        isMoving = true; // ������������� ����, ��� ����� ��������

        float t = 0; // ������ ��� ������������
        Vector3 startPosition = transform.position; // ��������� ������� ������
        Vector3 endPosition = new Vector3(targetPosition.x, targetPosition.y, 0); // �������� ������� ������

        while (t < moveTime)
        {
            t += Time.deltaTime; // ����������� ������
            transform.position = Vector3.Lerp(startPosition, endPosition, t / moveTime); // ������������� ������� ������
            yield return null; // ���� ���������� �����
        }

        // ������������� ������� ������ � �������� �����
        transform.position = endPosition;

        currentPosition = targetPosition; // ��������� ������� ������� ������
        isMoving = false; // ������������� ����, ��� ����� �������� ���������
    }
}
