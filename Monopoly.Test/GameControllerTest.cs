using System.Reflection.Metadata;
using System.Reflection;
namespace Monopoly.Test;

[TestFixture]
public class GameControllerTests
{
	public GameController game;
	public Board board;

	[SetUp]
	public void Setup()
	{
		board = new Board();
		game = new GameController(board);
	}

	[Test]
	public void AddPlayer_PlayerAddedSuccessfully()
	{
		// arrange=> set up the test
		string playerName = "Caca";

		// act=> call the method to add a player to the game
		game.AddPlayer(playerName);

		// assert=> to check if the player is added correctly
		List<IPlayer> players = game.GetPlayers();

		Assert.IsNotNull(players);
		Assert.IsTrue(players.Any(p => p.GetName() == playerName)); //LINQ returns a boolean value
	}

	[Test]
	public void ActivePlayer_ReturnsActivePlayer()
	{
		// arrange
		string playerName = "Cici";
		game.AddPlayer(playerName);

		// act
		IPlayer activePlayer = game.ActivePlayer();

		// assert
		Assert.IsNotNull(activePlayer);
		Assert.AreEqual(playerName, activePlayer.GetName());
	}

	[Test]
	public void GetPlayerIndex_ReturnsPlayerAtIndex()
	{
		// arrange
		string playerName = "Caca";
		game.AddPlayer(playerName);

		// act
		IPlayer playerAtIndex = game.GetPlayerIndex(0);

		// assert
		Assert.IsNotNull(playerAtIndex);
		Assert.AreEqual(playerName, playerAtIndex.GetName());
	}

	[Test]
	public void GetPlayerCash_ReturnsPlayerCash()
	{
		// arrange
		string playerName = "Cika";
		game.AddPlayer(playerName);

		// act
		Dictionary<IPlayer, int> playerCash = game.GetPlayerCash();

		// assert
		Assert.IsTrue(playerCash.ContainsKey(game.GetPlayerIndex(0)));
		Assert.AreEqual(2000, playerCash[game.GetPlayerIndex(0)]);
	}
}