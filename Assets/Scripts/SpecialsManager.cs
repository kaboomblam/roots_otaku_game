using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtakuGameJam
{
    public class SpecialsManager : MonoBehaviour
    {
        [Range(3, 5)]
        public float _specialTimeout;

        [SerializeField]
        private GameObject stickySapPrefab;

        private Rigidbody2D rb;

        NPCBehaviour NPC;


        public void TriggerSpecial(string specialType)
        {
            NPC = GetComponent<NPCBehaviour>();
            // try
            // {
            // }
            // catch (Exception e)
            // {
            //     Debug.Log(e.Message);
            // }

            if (NPC.specialsCount > 0)
                switch (specialType)
                {
                    case "Nitrous":
                        {
                            if (NPC.specialsCount >= 2)
                            {
                                Debug.Log("Using Nitro");

                                NPC.speed = 10;

                            }
                            break;
                        }

                    case "Stick Sap":
                        {
                            Debug.Log("Using StickSap");

                            InstantiateSpecial();
                            NPC.specialsCount--;

                            break;
                        }

                }
        }

        private void InstantiateSpecial()
        {

            GameObject newSpecial = Instantiate(
                stickySapPrefab, transform.position, transform.rotation);

            BoxCollider2D bc = newSpecial.GetComponent<BoxCollider2D>();

            Destroy(newSpecial, _specialTimeout);

        }

        public void AddToSpecial()
        {
            if (NPC.specialsCount < 5)
            {

                NPC.specialsCount++;

            }
        }
    }
}
