using UnityEngine;
using UnityEngine.Events;

namespace Test
{
    public class BoolToggler : MonoBehaviour
    {
        #region Parameters

        [SerializeField] private UnityEvent<bool> OnBoolToggled;

        #endregion   

        #region Private Members

        private bool _isTrue = true;

        #endregion

        public void ToggleBool()
        {
            _isTrue = !_isTrue;
        
            OnBoolToggled.Invoke(_isTrue);
        }
    
    }
}
