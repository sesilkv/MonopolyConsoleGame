using Monopoly;

class Program 
{
	static void Main(string[] args)
	{
		Console.WriteLine("==== Welcome to Monopoly Game! ====");
		
		Player player = new Player(115, "Sesil");
		Console.WriteLine(player.GetId());
		Console.WriteLine(player.GetName());
		
		Dice dice = new Dice(6);
		int result = dice.Roll();
		Console.WriteLine(result);
	}
}