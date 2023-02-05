using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtakuGameJam
{
    public class SpawnManager : MonoBehaviour
    {
        public GameObject powerupPrefab;
        public Transform spawnPositionParent;

        [HideInInspector]
        public Transform[] spawnPositions;

        public float spawnInterval;
        private float maxSpawnInterval;


        // Start is called before the first frame update
        void Start()
        {
            spawnPositions = spawnPositionParent.GetComponentsInChildren<Transform>();
            maxSpawnInterval = spawnInterval;
        }

        // Update is called once per frame
        void Update()
        {
            spawnInterval -= Time.deltaTime;

            if (spawnInterval < 0)
            {
                var rando = Random.Range(1, spawnPositions.Length);

                Instantiate(
                    powerupPrefab,
                    spawnPositions[rando].transform.position,
                    powerupPrefab.transform.rotation);

                spawnInterval = maxSpawnInterval;
            }

        }
    }
}
