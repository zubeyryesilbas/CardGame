public class Card
{
    public string Name { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }

    public Card(string name, int attack, int defense)
    {
        Name = name;
        Attack = attack;
        Defense = defense;
    }

    public void SetAttackValue(int attack)
    {
        Attack += attack;
    }

    public void SetDeffenseValue(int deffense)
    {
        Defense += deffense;
    }
}