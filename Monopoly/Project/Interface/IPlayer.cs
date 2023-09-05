namespace Monopoly;

public interface IPlayer 
{
	string? GetName();
	bool SetName(string? name);
	int GetId();
}