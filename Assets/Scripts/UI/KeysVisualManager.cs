using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class KeysVisualManager : MonoBehaviour
    {
        [SerializeField] private GameObject KeyPillPrefab;

        private List<GameObject> keys = new();

        public void UpdateKeyVisuals(List<bool> data)
        {
            int currentSize = keys.Count;
            int newSize = data.Count;

            RegenerateVisuals(currentSize, newSize);

            StartCoroutine(SetVisuals(data));
        }

        private IEnumerator SetVisuals(List<bool> data)
        {
            yield return 1f;
            ThemeManager.Instance?.UpdateThemePalette(null, false);
            
            for (int i = 0; i < data.Count; i++)
            {
                keys[i].GetComponent<PillHighlighter>().IsHighlighted = data[i];
            }

        }
        
        private void RegenerateVisuals(int currentSize, int newSize)
        {
            if (newSize == 0 && newSize != currentSize)
            {
                foreach (Transform child in transform) Destroy(child.gameObject);
                keys.Clear();
            }
        
            if (currentSize == newSize)
            {
                return;
            }

            if (currentSize < newSize)
            {
                for (int i = currentSize; i < newSize; i++)
                {
                    GameObject instance = Instantiate(KeyPillPrefab, transform);

                    KeyPill? keyPill = instance.GetComponent<KeyPill>();
                
                    keyPill.UpdateText((i+1).ToString());
                
                    keys.Add(instance);
                }

                return;
            }

            if (currentSize > newSize)
            {
                for (int i = newSize; i > currentSize; i++)
                {
                    Destroy(keys[i - 1]);
                    keys.RemoveAt(i-1);
                }
            }
        }
    }
}
