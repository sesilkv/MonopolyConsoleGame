namespace MonopolyInterface;

public interface IPlayer 
{
	string? GetName();
	bool SetName(string? name);
	int GetId();
	bool SetId(int id);
}