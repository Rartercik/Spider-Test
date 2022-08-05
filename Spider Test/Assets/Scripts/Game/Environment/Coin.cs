using UnityEngine;
using Zenject;
using Game.Tools;
using Game.Economics;

namespace Game.Environment
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private LayerMask _player;

        [Inject] private MoneyVault _vault;

        private bool _got;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (LayerTool.EqualLayers(_player, other.gameObject.layer))
            {
                if (_got == false)
                {
                    _vault.AddCoin();
                    _got = true;
                }

                gameObject.SetActive(false);
            }
        }
    }
}