using Monopoly;
using System.Linq;

class Program
{
	static void Main(string[] args)
	{
		Game monopoly = new Game();
		monopoly.StartGame();
	}
}

public class Game
{
	private GameController _gameController;
	private bool _finishTurn;
	private IPlayer _activePlayer;
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
		_gameController.PlayerNotifiedJail += JailNotification;
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.WriteLine("======= Welcome to Monopoly Game! =======");
		Console.ReadKey();

		// PLAYER
		int totalPlayer = 0;
		bool inputValid = false;
		while (!inputValid || totalPlayer < 2 || totalPlayer > 4)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("\nHow many players? (2-4)");
			inputValid = int.TryParse(Console.ReadLine(), out totalPlayer);

			Console.ForegroundColor = ConsoleColor.Red;
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
		
		HashSet<string> usedPlayerName = new HashSet<string>();
		for (int x = 1; x <= totalPlayer; x++)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"Username player {x}: ");
			string inputName = Console.ReadLine();

			while (string.IsNullOrEmpty(inputName) || usedPlayerName.Contains(inputName))
			{
				Console.ForegroundColor = ConsoleColor.Red;
				if(string.IsNullOrEmpty(inputName))
				{
					Console.WriteLine("Invalid input! Player name cannot be empty.\n");
				}
				else 
				{
					Console.WriteLine("Invalid input! Player name already exists.\n");
				}
				
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine($"Username player {x}: ");
				inputName = Console.ReadLine();
			}
			
			usedPlayerName.Add(inputName);
			inputName = char.ToUpper(inputName[0]) + inputName.Substring(1);
			_gameController.AddPlayer(inputName);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("The player was successfully added.\n");
		}

		Console.ForegroundColor = ConsoleColor.Blue;
		Console.WriteLine("Press Enter to start!");
		Console.ReadKey();

		Console.Clear();

		// DICE
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.WriteLine("The game is starting!\n");
		Console.ReadKey();

		while (_gameController.GetGameState() == GameState.InProgress || _gameController.GetGameState() == GameState.NotStarted)
		{
			IPlayer activePlayer = _gameController.ActivePlayer();
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"========= {activePlayer.GetName()}'s Turn =========");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("Press Any Key to Roll the Dice!\n");
			Console.ReadKey();

			_gameController.Roll();
			List<int> totalDice = _gameController.GetTotalDice();
			int total = _gameController.TotalDice();
			
			Console.ForegroundColor = ConsoleColor.Yellow;
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
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine("Rolled double. Press Enter to rolling again!");
				Console.ReadKey();
				_gameController.Roll();
				totalDice = _gameController.GetTotalDice();
				total = _gameController.TotalDice();
				
				Console.ForegroundColor = ConsoleColor.Yellow;
				for (int x = 0; x < totalDice.Count; x++)
				{
					Console.WriteLine($"Dice {x+1}: {totalDice[x]}");
				}
				Console.WriteLine($"Total Dice: {total} \n");
				
				Console.ForegroundColor = ConsoleColor.Red;
				if (_gameController.JailedPlayer().Contains(activePlayer))
				{
					Console.WriteLine("You are still in jail. Cannot move.");
					break;
				}
				
				_gameController.Move();
				Console.ReadKey();
			}
			
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"\n{activePlayer.GetName()}'s position: {_gameController.GetPlayerPosition()}");
			Console.WriteLine($"Tile name: {_gameController.TileName()} \n");

			Tile currTile = _gameController.GetTile();
			if (_gameController.IsNotChanceOrCommunityChest(currTile))
			{
				Console.WriteLine($"{_gameController.TileDescription()}, {activePlayer.GetName()}!\n");
			}

			Console.ReadKey();
			_finishTurn = false;
			while (!_finishTurn)
			{
				int option = 0;
				bool isValidOption = false;

				if (_gameController.JailedPlayer().Contains(activePlayer))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("You are in jail.\n");
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine("Choose option:");
					Console.WriteLine("1. Pay to get out from jail");
					Console.WriteLine("2. Roll the dice again");

					while (!isValidOption)
					{
						Console.Write("\nSelect an option: ");
						if (int.TryParse(Console.ReadLine(), out option))
						{
							isValidOption = true;
						}
						else
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine("Please enter a valid option number.");
						}
					}

					switch (option)
					{
						case 1:
							if (_gameController.PayToGetOutOfJail())
							{
								Console.Clear();
								Console.ForegroundColor = ConsoleColor.Green;
								Console.WriteLine("You paid to get out of jail.");
								Console.ReadKey();
								Console.ForegroundColor = ConsoleColor.Blue;
								Console.WriteLine("Press Any Key to Roll the Dice!\n");
								_gameController.Roll();
								Console.ForegroundColor = ConsoleColor.Yellow;
								for (int x = 0; x < totalDice.Count; x++)
								{
									Console.WriteLine($"Dice {x + 1}: {totalDice[x]}");
								}
								Console.WriteLine($"Total: {total} \n");
								Console.ReadKey();
								_gameController.Move();
							}
							else
							{
								Console.ForegroundColor = ConsoleColor.Red;
								Console.WriteLine("Sorry, you don't have enough money to pay and remain in jail.");
								_finishTurn = true;
								_gameController.SwitchTurn();
							}
							break;
						case 2:
							int failedRoll = 0;

							for (int x = 0; x < 3; x++)
							{
								Console.Clear();
								Console.ForegroundColor = ConsoleColor.Blue;
								Console.WriteLine($"Turn {x+1} in jail.");
								Console.WriteLine("Roll the Dice!\n");
								Console.ReadKey();
								_gameController.Roll();
								Console.ForegroundColor = ConsoleColor.Yellow;
								Console.WriteLine($"Dice 1: {totalDice[0]}");
								Console.WriteLine($"Dice 2: {totalDice[1]}");
								
								Console.ReadKey();

								if (_gameController.GetOutOfJailByDice())
								{
									Console.Clear();
									Console.ForegroundColor = ConsoleColor.Green;
									Console.WriteLine("Congrats! You rolled doubles and you can get out from jail.");
									_gameController.Move();
									break;
								}
								else
								{
									failedRoll++;
									if (failedRoll == 3)
									{
										Console.ForegroundColor = ConsoleColor.Red;
										Console.WriteLine("You failed to roll doubles for 3 turns. You remain in jail.");
										_finishTurn = true;
										_gameController.SwitchTurn();
									}
								}
							}
							break;
						default:
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine("There's no option like that.");
							break;
					}
				}
				_menuDesc = Menu();
				DisplayMenu();

				while (!isValidOption)
				{
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write("Choose an option: ");
					if (int.TryParse(Console.ReadLine(), out option) && option >= 1 && option <= _menuDesc.Count)
					{
						isValidOption = true;
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Red;
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
		Console.ForegroundColor = ConsoleColor.White;
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
			if (_gameController.GetNumberOfHouses() == 3)
			{
				_menuDesc.Add(new Menu { NameMenu = "Buy Hotel", Action = StateBuyHotel });
			}
		}

		_menuDesc.Add(new Menu { NameMenu = "Quit Game", Action = () => QuitGame() });
		return _menuDesc;
	}

	public void DisplayMenu()
	{
		// Console.Clear();
		Console.WriteLine("\n======= M E N U =======");
		for (int x = 0; x < _menuDesc.Count; x++)
		{
			if (!_gameController.JailedPlayer().Contains(_activePlayer) || x == 0)
			{
				Console.WriteLine($"{x + 1}. {_menuDesc[x].NameMenu}");
			}
		}
		Console.WriteLine("========================\n");
	}
	
	public void Dashboard()
	{
		Console.Clear();
		Console.ForegroundColor = ConsoleColor.Yellow;
		List<Property> playerProp = _gameController.PlayerProperty();
		IPlayer activePlayer = _gameController.ActivePlayer();
		Console.WriteLine($"{activePlayer.GetName()}'s position: {_gameController.TileName()}");
		Console.WriteLine($"{activePlayer.GetName()}'s Money: {_gameController.MoneyPlayer()}");
		Console.WriteLine($"{activePlayer.GetName()}'s properties are: ");
	
		if (playerProp.Count > 0)
		{
			foreach (var prop in playerProp)
			{
				Console.ForegroundColor = ConsoleColor.Magenta;
				Console.WriteLine($"- {prop.TileName}");
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine($"Total House: {prop.NumberOfHouse}");
				if (prop.NumberOfHouse == 3)
				{
					Console.WriteLine($"Total Hotel: {prop.NumberOfHotel}");
				}
			}
		}
		else
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("You don't have any properties.");
		}
		Console.ReadKey();
	}
	
	public void PurchaseProperty()
	{
		PropState propState = _gameController.BuyProperty();
		
		switch (propState)
		{
			case PropState.Success:
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Successfully purchased!");
				break;
			case PropState.AlreadyOwned:
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Sorry, the property is already owned by another player.");
				break;
			case PropState.NotEnoughMoney:
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Sorry, you don't have enough money.");
				break;
			case PropState.NotProperty:
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Sorry, this is not a property you can buy.");
				break;
			default:
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Error. You cannot purchasing the property.");
				break;
		}
	}
	
	public void HandleNotification(string message, string playerName)
	{
		Console.ForegroundColor = ConsoleColor.White;
		Console.WriteLine($"{message}, {playerName}\n");
	}
	
	public void JailNotification(IPlayer player)
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine($"\n{player.GetName()} has been sent to jail.");
	}

	public void StateSellProperty()
	{
		bool sellProp = _gameController.SellProperty();
		if (sellProp)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("You have successfully sold the property.");
		}
		else
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("You failed to sell the property.");
		}
	}
	
	public void StateBuyHouse()
	{
		bool buyHouse = _gameController.BuyHouse();
		if (buyHouse)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("The house was successfully purchased!");
		}
		else
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("You failed to purchase the house.");
		}
	}
	
	public void StateBuyHotel()
	{
		bool buyHotel = _gameController.BuyHotel();
		if (buyHotel)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("The hotel was successfully purchased!");
		}
		else
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("You failed to purchase the hotel.");
		}
	}

	public void FinishTurn()
	{
		_finishTurn = true;
		_gameController.SwitchTurn();
		
		Dictionary<IPlayer, int> playerCash = _gameController.GetPlayerCash();
		List<IPlayer> activePlayers = new List<IPlayer>();
		List<IPlayer> playersToRemove = new List<IPlayer>();
		
		foreach (var kvp in playerCash)
		{
			IPlayer player = kvp.Key;
			int cash = kvp.Value;

			if (cash > 0)
			{
				activePlayers.Add(player);
			}
			else if (cash <= 0)
			{
				activePlayers.Remove(player);
				playersToRemove.Add(player);
			}
		}

		// remove bankrupt player from active player
		foreach (var playerToRemove in playersToRemove)
		{
			activePlayers.Remove(playerToRemove);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"{playerToRemove.GetName()} is bankrupt. Sorry, you cannot continue this game.\n");
		}

		if (activePlayers.Count == 1)
		{
			IPlayer potentialWinner = activePlayers[0];
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"{potentialWinner.GetName()} has won the game!\n");
			_gameController.SetGameState(GameState.Finished);
		}
		else if (activePlayers.Count == 0)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("No active players left. The game is a draw.\n");
			_gameController.SetGameState(GameState.Finished);
		}
		else 
		{
			// cek currPlayer is bankrupt
			int currPlayerIndex = _gameController.CurrentPlayer();
			IPlayer currPlayer = _gameController.GetPlayerIndex(currPlayerIndex);
			if (!activePlayers.Contains(currPlayer))
			{
				// curr player is bankrupt, skip their turn
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"{currPlayer.GetName()} is bankrupt and cannot roll the dice.\n");
				FinishTurn(); // skip turn, next player
			}
		}
	}

	public async Task QuitGame()
	{
		Console.Clear();
		await Task.Delay(1000);
		Console.ForegroundColor = ConsoleColor.Blue;
		Console.WriteLine("......Exiting the game......");
		Console.WriteLine("Thanks for playing Monopoly!\n");
		Environment.Exit(0);
	}
}


