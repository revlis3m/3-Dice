using System;

namespace Dice3
{
    class Dice
    {
        //Attributs
        private string name;
        private int number = 0;

        public int Number { get { return number;} }
        public string Name { get { return name; } }

        //Constructeur
        public Dice(string name)
        {
            this.name = name;
        }

        //Methodes
        public int roll() //Cette methode va nous permettre de faire rouler notre de et renvoyer le resultat
        {
            Random random = new Random();
            number = random.Next(1, 7);// En gros le random est mathmatiquement egal a ceci [1,7[ vue que c'est des entiers t'as capter
            return number;
        }
    }

    class Player
    {
        private string name;
        private int hp;
        enum action
        {
            Roll,
            Attack,
            Defend
        }
        public string Name { get { return name; } set { name = value;} }

        public Player(string name)
        {
            this.Name = name;
            this.hp = 100;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Dice red = new Dice("Rouge");
            int numero;
            for (int i = 0; i < 100; i++)
            {
                numero = red.roll();
                Console.WriteLine(numero);
            }
        }
    }
}