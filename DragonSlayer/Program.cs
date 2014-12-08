using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpongebobVSPatrick
{
    class Program
    {
        static void Main(string[] args)
        {
            //game dialogue
            Console.WriteLine("WELCOME TO SPONGEBOB VS PATRICK");
            Console.WriteLine("You and patrick have suddenly become worst enemys and get into a fight.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            //call our functions by using all of our classes
            Game game = new Game();

            game.PlayGame();

            //keep console open
            Console.ReadKey();
        }
        class Enemy
        {
            //create properties
            //one for the enemy name
            public string Name { get; set; }
            //one fot the enemy starting HP
            public int PatrickHP { get; set; }
            //read-only boolean that returns true if HP>0
            public bool IsAlive
            {
                get { return this.PatrickHP > 0; }
            }

            //create the constructor that takes in the Name and starting HP
            public Enemy(string name, int startingHP)
            {
                this.Name = name;
                this.PatrickHP = startingHP;
            }

            //create methods now
            //create function for enemy attacking player
            public int enemyDoAttack()
            {
                //create random number generator
                Random rng = new Random();
                //create damage variable
                int damage = 0;

                //print to user
                Console.WriteLine("Patrick Attacks.");
                
                int patrickChance = rng.Next(0, 101);
                if (patrickChance >= 20)
                {
                    int patrickAttack = rng.Next(5, 16);
                    Console.WriteLine("Patrick hits you with his bad breath for a total of " + patrickAttack + " damage!\n");
                    damage += patrickAttack;
                    return damage;
                    
                }
                else
                {
                    Console.WriteLine("Paticks bad breath didnt even faze you.\n");
                }
                return damage;
            }
            //create function for taking damage from player
            public void TakeDamage(int damage)
            {
                PatrickHP -= damage;
                //print to user
                Console.WriteLine("Patrick takes " + damage + " damage from spongebob.");
            }
        }
        public enum AttackType { Bubbles = 1, FlyingPatty, EatKrabbyPatty, }

        class Player
        {
            //create properties for player
            //one for player's name
            public string Name { get; set; }
            //one for player's starting hp
            public int SpongebobHP { get; set; }
            //one for if they are alive where startingHP > 0
            public bool IsAlive
            {
                get { return this.SpongebobHP > 0; }
            }

            //create constructor for the player
            public Player(string name, int startingHP)
            {
                this.Name = name;
                this.SpongebobHP = startingHP;
            }

            //create methods
            //create a random number generator for the different attacks
            Random rng = new Random();

            //create player choice for attack
            private AttackType ChooseAttack()
            {
                //ask user to choose an attack
                Console.WriteLine("\n\n==============Attacks==============\n\nChoose an attack!\n\n1.  Bubbles- attacks with ploom of bubbles for 20-35 damage with 70% hit chance\n2.  Flying Krabby Patty - attacks with patty that always hits at 10-15 damage\n3.  Eat Krabby Patty - heal yourself for 10-20 HP\n\n");
                Console.WriteLine("Please enter either 1, 2, or 3 and hit ENTER\n");
                //create variable for users choice and turn into integer
                string userChoice = Console.ReadLine();
                if(userChoice=="")
                {
                    Console.WriteLine("Not a valid answer choose again.\n");
                    userChoice=Console.ReadLine();
                }
                int numberedChoice = int.Parse(userChoice);
                //returns the enum depending on the users choice to choose the attack
                return (AttackType)numberedChoice;
            }

            //create a function for player attack
            public int playerDoAttack()
            {
                //create damage variable
                int damage = 0;
                //run switch conditionals depending on result of ChooseAttack()
                switch (ChooseAttack())
                {
                    case AttackType.Bubbles:
                        //run bubbles hit chance
                        int bubbleChance = rng.Next(0, 101);
                        //bubble only attacks 70% of the time
                        if (bubbleChance >= 30)
                        {
                            //bubble attacks anywhere from 20-35 hp
                            int bubbleDMG = rng.Next(20, 36);
                            //print to user
                            Console.WriteLine("\n\nYou attack with your bubbles and cause " + bubbleDMG + " damage.\n");
                            damage += bubbleDMG;
                            return damage;
                        }
                        //if bubbles hitchance is below 30%, then it misses the dragon
                        else
                        {
                            Console.WriteLine("\n\nSorry you missed with your bubbles!\n");
                        }
                        break;
                    case AttackType.FlyingPatty:
                        //run flying patty attack at anywhere between 10 and 15 damage and take away from dragon's health
                        //flying patty attack hits 100% of the time
                        int flyingPattyDMG = rng.Next(10, 16);
                        //print to user
                        
                        Console.WriteLine("\n\nYour patty slaps patrick in the face for a total of " + flyingPattyDMG + " damage.\n");
                        damage += flyingPattyDMG;
                        return damage;
                        break;
                    case AttackType.EatKrabbyPatty:
                        //run heal to heal player anywhere between 10 and 20 health points
                        //add heal back to spongebobs health
                        int heal = rng.Next(10, 21);
                        //print to user
                        Console.WriteLine("\n\nYou heal yourself for " + heal + " health points!");
                        SpongebobHP += heal;
                        break;
                    default:
                        break;
                }
                return damage;
            }
            //create function for taking damage from enemy
            public void TakeDamage(int damage)
            {
                SpongebobHP -= damage;
                //print to user
                Console.WriteLine("\nYou take " + damage + " damage from patrick!\n");
            }
        }
        class Game
        {
            //create properties for player and enemy
            public Player player { get; set; }
            public Enemy enemy { get; set; }

            //create a constructor that intializes the player and enemy
            public Game()
            {
                //creating the Player and Enemy
                this.player = new Player("Spongebob", 100);
                this.enemy = new Enemy("Patrick", 200);
            }

            //create methods
            //create function for displaying current hp of player and enemy
            public void DisplayCombatInfo()
            {
                //display current hp of both player and enemy
                Console.WriteLine("==========Current Health==========\n");
                Console.WriteLine("Spongebob HP:  " + player.SpongebobHP);
                Console.WriteLine("Patrick HP:  " + enemy.PatrickHP);
            }
            //create function to run the game
            public void PlayGame()
            {
                //create round counter
                int roundCount = 1;

                while (this.player.IsAlive && this.enemy.IsAlive)
                {

                    Console.WriteLine("=============Spongebob VS Patrick=============\n\n");
                    //show user what round
                    Console.WriteLine("==============ROUND " + roundCount + "==============\n\n");
                    //display HPs
                    DisplayCombatInfo();
                    //player attacks enemy
                    this.enemy.TakeDamage(this.player.playerDoAttack());
                    //enemy attacks player
                    this.player.TakeDamage(this.enemy.enemyDoAttack());
                    //add one to the round counter
                    roundCount++;
                    Console.WriteLine("\nPress any key to continue to the next round of fun.");
                    Console.ReadKey();
                    Console.Clear();
                }
                //game ends
                if (!this.player.IsAlive)
                {
                    Console.WriteLine("You died from lack of oxygen.");
                }
                else
                {
                    Console.WriteLine("YOU WON. PATRICKS FACE IS COVERED IN GREASE\nAND YOUR FRIENSHIP IS COMPLETELY RUINED BUT HE MOST LIKELY WONT REMEMBER SO ITS ALL GOOD.");
                }
               
            }

           

            
            
            
        }
    }
}
