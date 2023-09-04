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
		var _random = new Random();
		int res1 = _random.Next(1, 7);
		int res2 = _random.Next(1, 7);
		int total = res1 + res2;
		return total;
	}
}