using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorsManager : MonoBehaviour
{
    [SerializeField] private GameObject _floorPrefab;
    [SerializeField] private Floor _firstFloor;

    private List<Floor> _floors = new List<Floor>();

    private void Awake()
    {
        _floors.Add(_firstFloor);

        var prevFloorPosition = _firstFloor.transform.position;

        while (_floors.Count < 5)
        {
            var newFloor = Instantiate(_floorPrefab, prevFloorPosition + Vector3.up * 2.5f, Quaternion.identity).GetComponent<Floor>();
            _floors.Add(newFloor);
            prevFloorPosition = newFloor.transform.position;
        }
    }

    private void Update()
    {
        for (int i = 0; i < _floors.Count; i++)
        {
            if (_floors[i].CanBeDestroyed)
            {
                Destroy(_floors[i]);
                _floors.RemoveAt(i);
                break;
            }
        }

        var prevFloorPosition = _floors[_floors.Count - 1].transform.position;
        while (_floors.Count < 5)
        {
            var newFloor = Instantiate(_floorPrefab, prevFloorPosition + Vector3.up * 2.5f, Quaternion.identity).GetComponent<Floor>();
            _floors.Add(newFloor);
            prevFloorPosition = newFloor.transform.position;
        }
    }
}
