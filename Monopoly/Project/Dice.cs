using MonopolyInterface;

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
		int result = _random.Next(1, _numOfSide + 1);
		return result;
	}
	
	//should I declare SetDiceSide method?
	public bool SetDiceSide(int numOfSide)
	{
		if(numOfSide > 0)
		{
			_numOfSide = numOfSide;
			return true;
		}
		return false;
	}
}