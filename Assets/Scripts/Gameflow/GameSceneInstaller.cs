using GameFlow.GameManager;
using HexGridSystem;
using UnityEngine;
using Zenject;

namespace Gameflow
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject gridGameObject;

        public override void InstallBindings()
        {
            var playerControls = new PlayerControls();
            playerControls.Enable();

            Container.BindInterfacesAndSelfTo<PlayerControls>().FromInstance(playerControls);


            var hexManager = Container.InstantiatePrefabForComponent<HexManager>(gridGameObject);
            Container.BindInterfacesAndSelfTo<IHexGrid>().FromInstance(hexManager);


            var GameManager = new GameManager();
            Container.BindInterfacesAndSelfTo<GameManager>().FromInstance(GameManager);
        }
    }
}
