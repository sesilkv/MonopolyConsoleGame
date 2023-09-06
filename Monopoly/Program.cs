using Monopoly;

class Program
{
	static void Main(string[] args)
	{
		Game monopoly = new Game();
		monopoly.StartGame();

		// Dice dice = new Dice(6);
		// int result1 = dice.Roll();
		// Console.WriteLine(result1);
		// int result2 = dice.Roll();
		// Console.WriteLine(result2);
	}
}

public class Game
{
	private GameController _gameController;
	private List<IPlayer> players = new List<IPlayer>();
	private bool _switchTurn;
	private List<Menu> _menuDesc;

	public Game()
	{
		Board board = new Board();
		_gameController = new GameController(board);
		_gameController.AddDice(6);
		_gameController.AddDice(6);
	}

	public void StartGame()
	{
		Console.Clear();
		Console.WriteLine("======= Welcome to Monopoly Game! =======");

		// PLAYER
		int totalPlayer = 0;
		bool inputValid = false;
		while (!inputValid || totalPlayer < 2 || totalPlayer > 4)
		{
			Console.WriteLine("How many players? (2-4)");
			inputValid = int.TryParse(Console.ReadLine(), out totalPlayer);

			if (!inputValid)
			{
				Console.WriteLine("Please input a valid number!\n");
			}
			else if (totalPlayer < 2 || totalPlayer > 4)
			{
				Console.WriteLine("Please input number of players between 2 and 4!\n");
			}

		}

		Console.Clear();
		for (int x = 1; x <= totalPlayer; x++)
		{
			Console.WriteLine($"Username player {x}: ");
			string playerName = Console.ReadLine();

			while (string.IsNullOrEmpty(playerName))
			{
				Console.WriteLine("Invalid input! Player name cannot be empty.");
				Console.WriteLine($"Username player {x}: ");
				playerName = Console.ReadLine();
			}

			_gameController.AddPlayer(playerName);
			Console.WriteLine("The player was successfully added.\n");
		}

		Console.WriteLine("Press Enter to start!");
		Console.ReadKey();

		Console.Clear();

		// DICE
		// _gameController.SetInitialState();
		Console.WriteLine("The game is starting!");

		while (_gameController.GetGameState() == GameState.IN_PROGRESS)
		{
			Player activePlayer = _gameController.ActivePlayer();
			Console.WriteLine($"{activePlayer.GetName()}'s Turn");
			Console.WriteLine("Press Enter to Roll the Dice!");

			for(int rollCount = 0; rollCount < 2; rollCount++)
			{
				int total = _gameController.Roll();

				Console.WriteLine($"Total Dice: {total}");
			}
			_gameController.Move();

			// while (totalDice[0] == totalDice[1])
			// {
			// 	Console.WriteLine("Rolled double. Press Enter to rolling again!");
			// 	Console.ReadKey();
			// 	_gameController.Roll();
			// 	totalDice = _gameController.GetTotalDice();
			// 	total = _gameController.TotalDice();
			// 	for (int x = 1; x <= totalDice.Count; x++)
			// 	{
			// 		Console.WriteLine("Dice {0}: {1}", x + 1, totalDice[x]);
			// 	}
			// 	Console.WriteLine($"Total Dice: {total}");
			// 	_gameController.Move();
			// }

			// Console.WriteLine($"{activePlayer.GetName()}'s position: {_gameController.GetPlayerPosition()}");
			// Console.WriteLine($"Tile name: {_gameController.TileName()}");

			// Console.ReadKey();
			// _switchTurn = false;
			// while (!_switchTurn)
			// {

			// }

			// List<int> totalDice = _gameController.GetDice();
			// _gameController.DiceRolled += (player, rollResults) =>
			// {
			// 	Console.WriteLine($"{player.GetName()} rolled: {string.Join(", ", rollResults)}");
			// };
		}

	}

	private List<Menu> GetMenu()
	{
		List<Property> playerProp = _gameController.PlayerProperty();
		return _menuDesc;
	}

	private void FinishTurn()
	{
		_switchTurn = true;
		_gameController.SwitchTurn();
	}
}

public class Menu
{
	private string? _menu;
	public string? MenuDesc { get { return _menu; } }
}


