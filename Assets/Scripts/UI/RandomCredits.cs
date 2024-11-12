using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI
{
    public class RandomCredits : MonoBehaviour 
    {
        [SerializeField] private List<TextMeshProUGUI> _texts;
        [SerializeField] private List<string> _devs; 

        private void Start()
        {
            var count = _texts.Count;
            for (int i = 0; i < count; i++)
            {
                var dev = _devs[Random.Range(0, _devs.Count)];
                var text = _texts[Random.Range(0, _texts.Count)];

                text.text = dev;
                _devs.Remove(dev);
                _texts.Remove(text);
            }
        }
    }
}