using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickEditor : MonoBehaviour
{
    [SerializeField] private GameObject _blockPrefab;

    [SerializeBoolMatrix] private Vector3[,] _brickPosition;
    private GameObject[,] _blocks = new GameObject[4, 4];

    private Camera _mainCamera;
    private Plane _boardPlane = new Plane(Vector3.forward, Vector3.zero);
    private Vector3 center2;
    private Vector3 centerBlock;
    private Vector2Int centerPosition;

    private bool[,] boolMatrix = new bool[4, 4];
    private void Awake()
    {
        _mainCamera = Camera.main;
        _brickPosition = new Vector3[4, 4];

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                _brickPosition[x, y] = new Vector3((transform.position.x - 2) + 0.5f + 1f * y, (transform.position.z + 3) - 0.5f - 1f * x, 0);
            }
        }

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                _blocks[x, y] = Instantiate(_blockPrefab, _brickPosition[x, y], Quaternion.identity, transform);
                _blocks[x, y].SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ChangeBlockVisibility();
        }
    }

    private void ChangeBlockVisibility()
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (_boardPlane.Raycast(ray, out float entrer))
        {
            Vector3 position = ray.GetPoint(entrer);

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (Vector3.Distance(position, _brickPosition[x, y]) < 0.5)
                        _blocks[x, y].SetActive(!_blocks[x, y].activeSelf);
                }
            }
        }
        CalculateCenterGizmos();
    }

    private void CalculateCenterGizmos()
    {
        int count = 0;
        Vector3 center = Vector3.zero;
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (_blocks[x, y].activeSelf == true)
                {
                    center += _blocks[x, y].transform.position;
                    count++;
                }
            }
        }
        if (count != 0)
        {
            center2 = center / count;
        }
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (Vector3.Distance(center2, _brickPosition[x, y]) < Vector3.Distance(center2, centerBlock))
                {
                    centerBlock = _brickPosition[x, y];
                    centerPosition.x = x;
                    centerPosition.y = y;
                }
            }
        }

        Debug.Log($"{centerPosition}");
    }

    public void OnSaveButtonClicked()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(center2 - Vector3.forward, 0.2f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(centerBlock - Vector3.forward, 0.2f);
    }
}
