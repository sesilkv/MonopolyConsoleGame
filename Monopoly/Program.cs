using Monopoly;

class Program
{
	static void Main(string[] args)
	{
		Game monopoly = new Game();
		monopoly.StartGame();
		// Player player = new Player("Sesil");
		// Console.WriteLine(player.GetName());

		// Dice dice = new Dice(6);
		// int result = dice.Roll();
		// Console.WriteLine(result);
	}
}

public class Game
{
	private GameController _gameController;
	private List<IPlayer> players = new List<IPlayer>();

	public Game()
	{
		_gameController = new GameController();
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
		
		Console.WriteLine("Press enter to start!");
		
		Console.Clear();
		
		// DICE
		
	}
}

