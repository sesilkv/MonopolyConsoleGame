using MonopolyInterface;

class Program 
{
	static void Main(string[] args)
	{
		Player player = new Player(115, "Sesil");
		Console.WriteLine(player.GetId());
		Console.WriteLine(player.GetName());
		
		Dice dice = new Dice(6);
		int result = dice.Roll();
		Console.WriteLine(result);
	}
}