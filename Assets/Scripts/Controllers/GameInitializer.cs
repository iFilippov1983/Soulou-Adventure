using InputSystem;

namespace Soulou
{
    public class GameInitializer
    {
        public GameInitializer(ControllersDepo controllers, GameData gameData)
        {
            var playerInitializer = new PlayerInitializer(gameData);
            var inputInitializer = new InputInitializer();
            var inputController = new InputController(inputInitializer);
            

            controllers.Add(inputController);
            controllers.Add(new PlayerController(gameData, playerInitializer.PlayerModel, inputInitializer));
        }
    }
}
