using Monopoly;

public class Player : IPlayer 
{
	private string _name;
	private int _id;
	
	public Player(int id, string? name)
	{
		_id = id;
		_name = name;
	}
	
	public string? GetName()
	{
		return _name;
	}
	
	public bool SetName(string? name)
	{
		if(name.Length > 0)
		{
			_name = name;
			return true;
		}
		return false;
	}
	
	public int GetId()
	{
		return _id;
	}
	
	public bool SetId(int id)
	{
		if(id > 0)
		{
			_id = id;
			return true;
		}
		return false;
	}
	
}