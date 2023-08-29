using System;
using System.Collections.Generic;

//Rule https://www.notion.so/Game-TYpe-3-Dice-46d84fe6e09c4f188dd8a6218d12c099?pvs=4
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
            Console.WriteLine($"Press any touch to roll the {this.Name} dice");
            Console.ReadKey();
            Random random = new Random();
            number = random.Next(1, 7);// En gros le random est mathmatiquement egal a ceci [1,7[ vue que c'est des entiers t'as capter
            result(number, name);
            return number;
        }

        public void result(int number, string name)
        {
            Console.WriteLine($"The {name} dice roll and give \u001b[47m\u001b[30m{number}\u001b[40m\u001b[37m");
        }
    }

    class Player
    {
        private string name;
        private int counterluck;
        private int hp, attackBluePoint, attackRedPoint, defenseBluePoint, defenseRedPoint;
        public Dice dice_Red, dice_Blue, dice_White;

        public int HP { get { return hp;} set { hp = value; } }
        public int AttackBlue { get { return attackBluePoint; } set { attackBluePoint = value; } }
        public int DefenseBlue { get { return defenseBluePoint; } set { defenseBluePoint = value; } }
        public int AttackRed { get { return attackRedPoint; } set { attackRedPoint = value; } }
        public int DefenseRed { get { return defenseRedPoint; } set { defenseRedPoint = value; } }

        public string Name { get { return name; } set { name = value;} }

        public Player(string name, Dice red, Dice blue, Dice white)
        {
            this.Name = name;
            this.hp = 10;
            this.dice_Red = red;
            this.dice_Blue = blue;
            this.dice_White = white;
            this.AttackBlue = 0;
            this.DefenseBlue = 0;
            this.AttackRed = 0;
            this.DefenseRed = 0;
            this.counterluck = 0;
        }

        public int Start_turn() //Determine si tu attack ou defend en debut de tour 1 sttack 0 defense
        {
            int status;
            if(counterluck < 2)
            {
                int result = this.dice_White.roll();
                if (result % 2 == 0)
                {
                    Console.WriteLine("You roll an even number, you're roll will be your defense point");
                    status = 0;
                }
                else
                {
                    Console.WriteLine("You roll an odd number, you'll attack this turn");
                    counterluck = 0;
                    status = 1;
                }
                counterluck++;
            }
            else
            {
                    Console.WriteLine("You defend too much go attack now");
                status = 1;
                counterluck = 0;
            }
            return status;
        }

        public void attack_turn(Player playerAttacked)
        {
            int bluePoint = this.dice_Blue.roll();
            int redPoint = this.dice_Red.roll();
            if (redPoint > bluePoint)
            {
                Console.WriteLine("The Red Dice have a greater values so it's a \u001b[31mphysical attack\u001b[37m");
                this.AttackRed += redPoint;
                attackRed(playerAttacked);
            }
            else if (redPoint < bluePoint) 
            {
                Console.WriteLine("The Blue Dice have a greater values so it's a \u001b[34mmagical attack\u001b[37m");
                this.AttackBlue += bluePoint;
                attackBlue(playerAttacked);
            }
            else
            {
                Console.WriteLine("OMG A DOUBLEE you can roll the dice again");
                this.AttackBlue += bluePoint;
                this.AttackRed += redPoint;
                attack_turn(playerAttacked);
            }

        }

        public void attackBlue(Player playerAttacked)
        {
            playerAttacked.DefenseBlue -= this.AttackBlue;
            if (playerAttacked.DefenseBlue < 0)
            {
                playerAttacked.HP += playerAttacked.DefenseBlue;
                if (playerAttacked.alive())
                {
                    playerAttacked.DefenseBlue = 0;
                    this.AttackBlue = 0;
                    Thread.Sleep(2000);
                    Console.WriteLine($"Ouch {this.Name} did damage to {playerAttacked.Name} who now have \u001b[47m\u001b[30m{playerAttacked.HP} HP\u001b[40m\u001b[37m");
                }
                else
                {
                    Thread.Sleep(5000);
                    Console.WriteLine($"End of the game {this.Name} won the match");

                }
            }
            else
            {
                this.AttackBlue = 0;
                Thread.Sleep(2000);
                Console.WriteLine($"Wouah {playerAttacked.Name} tanked it SUGOI");
            }
        }

        public void attackRed(Player playerAttacked)
        {
            playerAttacked.DefenseRed -= this.AttackRed;
             if (playerAttacked.DefenseRed < 0)
            {
                playerAttacked.HP += playerAttacked.DefenseRed;
                if (playerAttacked.alive())
                {
                    playerAttacked.DefenseRed = 0;
                    this.AttackRed = 0;
                    Thread.Sleep(2000);
                    Console.WriteLine($"Ouch {this.Name} did damage to {playerAttacked.Name} who now have \u001b[47m\u001b[30m{playerAttacked.HP} HP\u001b[40m\u001b[37m");
                }
                else
                {
                    Thread.Sleep(2000);
                    Console.WriteLine($"End of the game {this.Name} won the match");
                    
                }
            }
            else
            {
                this.AttackRed = 0;
                Thread.Sleep(2000);
                Console.WriteLine($"Wouah {playerAttacked.Name} tanked it SUGOI");
            }
        }

        public void defend_turn()
        {
            int bluePoint = this.dice_Blue.roll();
            int redPoint = this.dice_Red.roll();
            if (redPoint > bluePoint)
            {
                Console.WriteLine("The Red Dice have a greater values so it's a \u001b[41mphysical defense\u001b[40m");
                DefenseRed += redPoint;
                
            }
            else if (redPoint < bluePoint)
            {
                Console.WriteLine("The Blue Dice have a greater values so it's a \u001b[44mmagical defense\u001b[40m");
                DefenseBlue += bluePoint;
            }
            else
            {
                Console.WriteLine("OMG A DOUBLEE you can roll the dice again");
                DefenseBlue += bluePoint;
                DefenseRed += redPoint;
                defend_turn();
            }
        }

        public bool alive()
        {
            if (this.HP > 0) { return true; }
            else { return false; }
        }

        public void stat()
        {
            Console.WriteLine($"{this.Name} have {this.HP} HP");
        }
        
    }

  

    class Program
    {
        static void Main(string[] args)
        {
            //Initialisation
            Console.WindowWidth = 800; 
            Console.WindowHeight = 600;
            
            Dice redDice = new Dice("Red");
            Dice blueDice = new Dice("Blue");
            Dice whiteDice = new Dice("White");

            Player player1 = new Player("Silver",redDice,blueDice,whiteDice);
            Player player2 = new Player("Silvio", redDice, blueDice, whiteDice);

            while (player1.HP > 0 && player2.HP > 0) //Boucle du jeu
            {
                Console.WriteLine($"Player 1 : {player1.Name}    │\t\t\t\t\t\t\t\t\t│ Player 2 : {player2.Name}");
                Console.WriteLine($"   HP    : {player1.HP}        │\t\t\t\t\t\t\t\t\t│     HP   : {player2.HP}");
                Console.WriteLine($"AttRed {player1.AttackRed} │ AttBlue {player1.AttackBlue} │\t\t\t\t\t\t\t\t\t│ AttRed {player2.AttackRed} │ AttBlue {player2.AttackBlue}");
                Console.WriteLine($"DefRed {player1.DefenseRed} │ DefBlue {player1.DefenseBlue} │\t\t\t\t\t\t\t\t\t│ DefRed {player2.DefenseRed} │ DefBlue {player2.DefenseBlue}");
                int repeatCount = 22; // Nombre de répétitions
                char characterToRepeat = '▔'; // Caractère à répéter

                string repeatedCharacters = new string(characterToRepeat, repeatCount);
                Console.Write(repeatedCharacters);
                Console.Write("\t\t\t\t\t\t\t\t\t");
                Console.WriteLine(repeatedCharacters);
                int white;
                if (player1.alive())
                {

                    Console.WriteLine($"\u001b[42m{player1.Name} turn\u001b[40m\n");
                    Thread.Sleep(1000);
                    white = player1.Start_turn();
                    if (white == 0)
                    {
                        player1.defend_turn();
                    }
                    else
                    {
                        player1.attack_turn(player2);
                    }
                }
                if(player2.alive())
                {
                    Console.WriteLine($"\u001b[43m{player2.Name} turn\u001b[40m\n");
                    Thread.Sleep(1000);
                    white = player2.Start_turn();
                    if (white == 0)
                    {
                        player2.defend_turn();
                    }
                    else { player2.attack_turn(player1); }
                
                }
                Console.ReadKey();
                for (int i = 0; i < 28; i++)
                {
                    Console.WriteLine(' ');
                }

            }
        }

       

    }
}