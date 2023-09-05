using Monopoly;

public class Dice : IDice 
{
	private int _numOfSide;
	
	public Dice(int numOfSide)
	{
		_numOfSide = numOfSide;
	}
	
	public int Roll()
	{
		Random randRoll = new Random();
		int resultDice = randRoll.Next(1,7);
		return resultDice;
	}
}