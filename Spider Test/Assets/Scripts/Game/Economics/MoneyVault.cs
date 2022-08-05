using UnityEngine;
using UnityEngine.Events;

namespace Game.Economics
{
    public class MoneyVault : MonoBehaviour
    {
        [SerializeField] private UnityEvent<int> _onMoneyCountChanged;
        
        private int _money;

        public void AddCoin()
        {
            _money++;
            _onMoneyCountChanged?.Invoke(_money);
        }
    }
}