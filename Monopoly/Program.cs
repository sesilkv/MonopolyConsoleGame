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
	// private List<IPlayer> players = new List<IPlayer>();
	private bool _finishTurn;
	private Player _activePlayer;
	private List<Menu> _menuDesc;

	public Game()
	{
		Board board = new Board();
		_gameController = new GameController(board);
		_activePlayer = _gameController.ActivePlayer();
		_gameController.AddDice(6);
		_gameController.AddDice(6);
	}

	public void StartGame()
	{
		Console.Clear();
		
		_gameController.PlayerNotified += HandleNotification;
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
			string inputName = Console.ReadLine();

			while (string.IsNullOrEmpty(inputName))
			{
				Console.WriteLine("Invalid input! Player name cannot be empty.");
				Console.WriteLine($"Username player {x}: ");
				inputName = Console.ReadLine();
			}
			
			inputName = char.ToUpper(inputName[0]) + inputName.Substring(1);
			_gameController.AddPlayer(inputName);
			Console.WriteLine("The player was successfully added.\n");
		}

		Console.WriteLine("Press Enter to start!");
		Console.ReadKey();

		Console.Clear();

		// DICE
		// _gameController.SetInitialState();
		Console.WriteLine("The game is starting!\n");
		Console.ReadKey();

		while (_gameController.GetGameState() == GameState.IN_PROGRESS || _gameController.GetGameState() == GameState.NOT_STARTED)
		{
			Player activePlayer = _gameController.ActivePlayer();
			Console.WriteLine($"=== {activePlayer.GetName()}'s Turn ===");
			Console.WriteLine("Press Any Key to Roll the Dice!\n");
			Console.ReadKey();

			_gameController.Roll();
			List<int> totalDice = _gameController.GetTotalDice();
			int total = _gameController.TotalDice();

			for (int x = 0; x < totalDice.Count; x++)
			{
				Console.WriteLine($"Dice {x+1}: {totalDice[x]}");
			}
			Console.WriteLine($"Total: {total} \n");
			Console.ReadKey();
	
			_gameController.Move();

			// twin dice
			while (totalDice[0] == totalDice[1])
			{
				Console.WriteLine("Rolled double. Press Enter to rolling again!");
				Console.ReadKey();
				_gameController.Roll();
				totalDice = _gameController.GetTotalDice();
				total = _gameController.TotalDice();
				for (int x = 0; x < totalDice.Count; x++)
				{
					Console.WriteLine($"Dice {x+1}: {totalDice[x]}");
				}
				Console.WriteLine($"Total Dice: {total} \n");
				_gameController.Move();
				Console.ReadKey();
			}

			Console.WriteLine($"{activePlayer.GetName()}'s position: {_gameController.GetPlayerPosition()}");
			Console.WriteLine($"Tile name: {_gameController.TileName()} \n");
			Console.WriteLine($"{_gameController.TileDescription()}, {activePlayer.GetName()}!\n");

			Console.ReadKey();
			_finishTurn = false;
			while (!_finishTurn)
			{
				int option = 0;
				bool isValidOption = false;

				if (_gameController.Jailed().Contains(activePlayer))
				{
					Console.WriteLine("You are in jail.");
					Console.WriteLine("Do you want to:");
					Console.WriteLine("1. Pay to get out of jail");
					Console.WriteLine("2. Roll the dice again");

					while (!isValidOption)
					{
						Console.Write("Select an option: ");
						if (int.TryParse(Console.ReadLine(), out option))
						{
							isValidOption = true;
						}
						else
						{
							Console.WriteLine("Please enter a valid option number.");
						}
					}

					switch (option)
					{
						case 1:
							if (_gameController.PayToGetOutOfJail())
							{
								Console.WriteLine("You paid to get out of jail.");
								_gameController.Move();
							}
							else
							{
								Console.WriteLine("You don't have enough money to pay and remain in jail.");
								_finishTurn = true;
								_gameController.SwitchTurn();
							}
							break;
						case 2:
							int failedRoll = 0;

							for (int x = 0; x < 3; x++)
							{
								Console.WriteLine($"Turn {x+1} in jail.");

								Console.WriteLine("Press enter to Roll the Dice");
								Console.ReadKey();
								_gameController.Roll();
								Console.WriteLine($"Dice 1: {totalDice[0]}");
								Console.WriteLine($"Dice 2: {totalDice[1]}");

								if (_gameController.GetOutOfJail())
								{
									Console.WriteLine("Congrats! You rolled doubles and you can get out from jail.");
									_gameController.Move();
									break;
								}
								else
								{
									failedRoll++;
									if (failedRoll == 3)
									{
										Console.WriteLine("You failed to roll doubles for 3 turns. You remain in jail.");
										_finishTurn = true;
										_gameController.SwitchTurn();
									}
								}
							}
							break;
					}
				}
				_menuDesc = Menu();
				ShowMenu();

				while (!isValidOption)
				{
					Console.Write("Choose an option: ");
					if (int.TryParse(Console.ReadLine(), out option) && option >= 1 && option <= _menuDesc.Count)
					{
						isValidOption = true;
					}
					else
					{
						Console.WriteLine("Please enter a valid number.");
					}
				}
				Console.Clear();

				Menu selectedMenu = _menuDesc[option - 1];
				selectedMenu.Action.Invoke();
			}
		}
	}

	public List<Menu> Menu()
	{
		List<Property> playerProp = _gameController.PlayerProperty();
		_menuDesc = new List<Menu>()
		{
			new Menu { NameMenu = "Your Dashboard", Action = Dashboard },
			new Menu { NameMenu = "Purchase Property", Action = PurchaseProperty },
			new Menu { NameMenu = "Finish Turn", Action = FinishTurn }
		};

		if (playerProp.Count > 0)
		{
			_menuDesc.Add(new Menu { NameMenu = "Sell Property", Action = StateSellProperty });
			_menuDesc.Add(new Menu { NameMenu = "Buy House", Action = StateBuyHouse });
		}

		_menuDesc.Add(new Menu { NameMenu = "Quit Game", Action = () => QuitGame() });
		return _menuDesc;
	}

	public void ShowMenu()
	{
		Console.WriteLine("\n======= M E N U =======");
		for (int x = 0; x < _menuDesc.Count; x++)
		{
			if (!_gameController.Jailed().Contains(_activePlayer) || x == 0)
			{
				Console.WriteLine($"{x + 1}. {_menuDesc[x].NameMenu}");
			}
		}
		Console.WriteLine("========================\n");
	}
	
	public void Dashboard()
	{
		Console.Clear();
		List<Property> playerProp = _gameController.PlayerProperty();
		Player activePlayer = _gameController.ActivePlayer();
		Console.WriteLine($"{activePlayer.GetName()}'s position: {_gameController.TileName()}");
		Console.WriteLine($"{activePlayer.GetName()}'s Money: {_gameController.MoneyPlayer()}");
		Console.WriteLine($"{activePlayer.GetName()}'s properties are: ");
		if (playerProp.Count > 0)
		{
			foreach (var prop in playerProp)
			{
				Console.WriteLine($"- {prop.TileName}");
				Console.WriteLine($"Total House: {_gameController.PlayerHouse()}");
			}
		}
		else
		{
			Console.WriteLine("You don't have any properties.");
		}
		Console.ReadKey();
	}
	
	public void PurchaseProperty()
	{
		PropState propState = _gameController.BuyProperty();
		
		switch (propState)
		{
			case PropState.SUCCESS:
				Console.WriteLine("Successfully purchased!");
				break;
			case PropState.ALREADY_OWNED:
				Console.WriteLine("Sorry, the property is already owned by another player.");
				break;
			case PropState.NOT_ENOUGH_MONEY:
				Console.WriteLine("Sorry, you don't have enough money.");
				break;
			default:
				Console.WriteLine("Error. You cannot purchasing the property.");
				break;
		}
	}
	
	public void HandleNotification(string message, string playerName)
	{
		Console.WriteLine($"{message}, {playerName}");
	}

	public void StateSellProperty()
	{
		bool sellProp = _gameController.SellProperty();
		if (sellProp)
		{
			Console.WriteLine("You have successfully sold the property.");
		}
		else
		{
			Console.WriteLine("You failed to sell the property.");
		}
	}
	
	public void StateBuyHouse()
	{
		bool buyHouse = _gameController.BuyHouse();
		if (buyHouse)
		{
			Console.WriteLine("Success purchased!");
		}
		else
		{
			Console.WriteLine("You failed to purchase the house.");
		}
	}

	public void FinishTurn()
	{
		_finishTurn = true;
		_gameController.SwitchTurn();
	}

	public async Task QuitGame()
	{
		// _gameController.SetGameState(GameState.FINISHED);
		Console.Clear();
		await Task.Delay(1000);
		Console.WriteLine("......Exiting the game......");
		Console.WriteLine("Thanks for playing Monopoly!\n");
		Environment.Exit(0);
	}
}


