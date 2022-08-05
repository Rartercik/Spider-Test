using UnityEngine;
using Zenject;
using Game.PlayerComponents;
using Game.Economics;

namespace Game.Installers
{
    public class Gameplay : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private MoneyVault _vault;
        
        public override void InstallBindings()
        {
            Container.Bind<Player>().FromInstance(_player).AsSingle().NonLazy();
            Container.Bind<MoneyVault>().FromInstance(_vault).AsSingle().NonLazy();
        }
    }
}