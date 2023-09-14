namespace Monopoly;

public class CardSkill
{
	public static bool CommunityPayTax(GameController game)
	{
		IPlayer activePlayer = game.ActivePlayer();

		int taxComm = 300;
		if (game.GetPlayerCash().ContainsKey(activePlayer) && game.GetPlayerCash()[activePlayer] >= taxComm)
		{
			game.GetPlayerCash()[activePlayer] -= taxComm;
			game.NotifyPlayer("Community Chest Card: you have successfully paid the tax $300", activePlayer.GetName());
			return true;
		}
		return false;
	}

	public static bool ChanceBirthday(GameController game)
	{
		IPlayer activePlayer = game.ActivePlayer();

		int birthdayAmount = 500;
		if (game.GetPlayerCash().ContainsKey(activePlayer))
		{
			game.GetPlayerCash()[activePlayer] += birthdayAmount;
			game.NotifyPlayer("Chance Card: happy birthday, you got $500", activePlayer.GetName());
			return true;
		}
		return false;
	}
}