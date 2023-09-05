using Monopoly;
using System;
public class Player : IPlayer 
{
	private string _name;
	private int _id;
	
	public Player(string? name)
	{
		// _id = id;
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
}