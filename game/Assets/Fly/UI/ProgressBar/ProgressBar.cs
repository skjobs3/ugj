using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fly
{
    namespace UI
    {
        public class ProgressBar : MonoBehaviour
        {
            public GameObject Map;
            public GameObject Ship;
            public GameObject Enemy;

            public float enemyPosition;
            public float shipPosition;

            void Update()
            {
                float mapWidth = Map.GetComponent<RectTransform>().rect.width;
                var pos = Ship.transform.position;
                pos.x = mapWidth * shipPosition + Map.transform.position.x;

                Ship.transform.position = pos;

                pos.x = mapWidth * enemyPosition + Map.transform.position.x;
                Enemy.transform.position = pos;
            }
        }
    }
}