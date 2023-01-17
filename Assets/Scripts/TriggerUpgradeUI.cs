using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Localization
{
    public class TriggerUpgradeUI : MonoBehaviour
    {
        public UIUpdater ui;
        // Start is called before the first frame update
        void Start()
        {
        
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
                ui.SpawnUpgrades();
                Destroy(this.gameObject);
            }
        }
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
