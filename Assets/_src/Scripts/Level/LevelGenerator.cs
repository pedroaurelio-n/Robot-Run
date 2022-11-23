using System.Collections.Generic;
using UnityEngine;

namespace PedroAurelio.RobotRun
{
    public class LevelGenerator : MonoBehaviour
    {
        public delegate void CalculateDistanceRemaining(float distanceRemaining);
        public static event CalculateDistanceRemaining onDistanceCalculated;

        [Header("Dependencies")]
        [SerializeField] private LevelArea startArea;
        [SerializeField] private LevelArea emptyAreaPrefab;
        [SerializeField] private LevelArea finalAreaPrefab;
        [SerializeField] private List<LevelArea> prefabAreas;

        [Header("Dimensions Settings")]
        [SerializeField] private float cameraOffsetX = 6f;
        [SerializeField] private float areaWidth = 24f;

        [Header("Area Settings")]
        [SerializeField] private int startActiveCount = 3;
        [SerializeField] private int maxActiveAreas = 5;
        [SerializeField] private int areaRepositionLimit = 10;

        [Header("Generation Settings")]
        [SerializeField] private int generateEmptyAreaUntil = 3;
        [SerializeField] private int generateEndAreaAt = 30;

        private List<LevelArea> _areaPool;
        private List<LevelArea> _activeAreas = new List<LevelArea>();
        private int _areaPositionIndex;
        private int _totalAreas;

        private void OnValidate()
        {
            if (maxActiveAreas < 3)
                maxActiveAreas = 3;

            if (areaRepositionLimit < maxActiveAreas)
                areaRepositionLimit = maxActiveAreas;
        }

        private void Awake()
        {
            OnValidate();

            _activeAreas.Add(startArea);

            InitializePool();

            for (int i = _activeAreas.Count; i < startActiveCount; i++)
                GenerateNewArea();
        }

        private void InitializePool()
        {
            _areaPool = new List<LevelArea>();

            for (int i = 0; i < prefabAreas.Count; i++)
            {
                var area = CreateNewArea(i);
                area.Id = i;
                _areaPool.Add(area);
                area.gameObject.SetActive(false);
            }
        }

        private LevelArea CreateNewArea(int id)
        {
            var area = Instantiate(prefabAreas[id], transform);
            return area;
        }

        private LevelArea CreateNewArea(LevelArea areaToCreate)
        {
            var area = Instantiate(areaToCreate, transform);
            return area;
        }

        private LevelArea TryToGetAreaFromPool(int id)
        {
            foreach (LevelArea area in _areaPool)
            {
                if (area.Id == id)
                    return area;
            }

            var newArea = CreateNewArea(id);
            newArea.Id = id;
            return newArea;
        }

        private void Start()
        {
            var distanceRemaining = (generateEndAreaAt * areaWidth);
            onDistanceCalculated?.Invoke(distanceRemaining);
        }

        private void GenerateNewArea()
        {
            _areaPositionIndex++;
            _totalAreas++;

            var areaPositionX = (_areaPositionIndex * areaWidth);
            var areaPosition = new Vector2(areaPositionX + cameraOffsetX, 0f);

            LevelArea newArea;

            if (_totalAreas < generateEmptyAreaUntil)
            {
                newArea = CreateNewArea(emptyAreaPrefab);
                newArea.Id = -1;
            }
            else if (_totalAreas >= generateEmptyAreaUntil && _totalAreas < generateEndAreaAt)
            {
                var r = Random.Range(0, prefabAreas.Count);
                newArea = TryToGetAreaFromPool(r);
                newArea.gameObject.SetActive(true);
                _areaPool.Remove(newArea);
            }
            else
                newArea = CreateNewArea(finalAreaPrefab);

            newArea.Initialize(areaPosition);
            _activeAreas.Add(newArea);
        }

        private void DestroyOldestArea()
        {
            if (_activeAreas.Count >= maxActiveAreas)
            {
                var oldestArea = _activeAreas[0];

                if (oldestArea != startArea && oldestArea != emptyAreaPrefab)
                    _areaPool.Add(oldestArea);

                _activeAreas.RemoveAt(0);
                oldestArea.ReleaseFromPool(false);
            }

            if (_areaPositionIndex >= areaRepositionLimit)
                RepositionAreas();

            GenerateNewArea();
        }

        private void RepositionAreas()
        {
            _areaPositionIndex = 0;

            var areasBehindPlayer = maxActiveAreas - 2;
            var resetIndex = -areasBehindPlayer;

            foreach (LevelArea area in _activeAreas)
            {
                var areaPositionX = (resetIndex * areaWidth);
                area.transform.position = new Vector2(areaPositionX + cameraOffsetX, 0f);
                resetIndex++;
            }
        }

        private void OnEnable() => CheckForNewAreaStart.onNewAreaStart += DestroyOldestArea;
        private void OnDisable() => CheckForNewAreaStart.onNewAreaStart -= DestroyOldestArea;
    }
}
