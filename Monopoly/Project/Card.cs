using MonopolyEnum;

namespace Monopoly 
{
	public abstract class Card : Tile
	{
		private TypeCard _typeCard; // enum
		private string? _cardDesc;
		
		public TypeCard TypeCard{ get => _typeCard; set => typeCard = value; }
		public string? CardDesc{ get => _cardDesc; set => cardDesc = value; }
		
		public Card()
		{
			
		}
	}
	
	
}