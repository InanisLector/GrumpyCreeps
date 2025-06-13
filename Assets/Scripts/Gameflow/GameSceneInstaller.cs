using GameFlow;
using HexGridSystem;
using ScriptableObjects.GameState;
using UnityEngine;
using Zenject;

namespace Gameflow
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject gridGameObject;
        [SerializeField] private GameState initialGameState;

        public override void InstallBindings()
        {
            var playerControls = new PlayerControls();
            playerControls.Enable();

            Container.BindInterfacesAndSelfTo<PlayerControls>().FromInstance(playerControls);


            var hexManager = Container.InstantiatePrefabForComponent<HexManager>(gridGameObject);
            Container.BindInterfacesAndSelfTo<IHexGrid>().FromInstance(hexManager);


            var GameManager = new GameManager(initialGameState);
            Container.BindInterfacesAndSelfTo<GameManager>().FromInstance(GameManager);
        }
    }
}
