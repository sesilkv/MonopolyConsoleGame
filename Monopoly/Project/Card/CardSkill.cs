namespace Monopoly;
using NLog;

public class CardSkill
{
	private static readonly Logger log = LogManager.GetCurrentClassLogger();
	public static bool CommunityPayTax(GameController game)
	{
		IPlayer activePlayer = game.ActivePlayer();

		int taxComm = 300;
		if (game.GetPlayerCash().ContainsKey(activePlayer) && game.GetPlayerCash()[activePlayer] >= taxComm)
		{
			game.GetPlayerCash()[activePlayer] -= taxComm;
			log.Info($"{activePlayer.GetName()} paid ${taxComm} on Community Chest tile.");
			game.NotifyPlayer("Community Chest Card: you have successfully paid the tax $300", activePlayer.GetName());
			return true;
		}
		game.GetPlayerCash()[activePlayer] -= taxComm;
		game.NotifyPlayer("Community Chest Card: you have successfully paid the tax $300, but it seems you are bankrupt", activePlayer.GetName());
		return false;
	}

	public static bool ChanceBirthday(GameController game)
	{
		IPlayer activePlayer = game.ActivePlayer();

		int birthdayAmount = 500;
		if (game.GetPlayerCash().ContainsKey(activePlayer))
		{
			game.GetPlayerCash()[activePlayer] += birthdayAmount;
			log.Info($"{activePlayer.GetName()} get ${birthdayAmount} on Chance tile.");
			game.NotifyPlayer("Chance Card: happy birthday, you got $500", activePlayer.GetName());
			return true;
		}
		return false;
	}
}