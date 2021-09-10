using System;
using System.Collections.Generic;
using System.Text;

namespace FightSimulation
{
    struct Monster
    {
        public string name;
        public float health;
        public float attack;
        public float defense;


    }
    class Game
    {
        bool gameOver = false;
        Monster currentMonster1;
        Monster currentMonster2;
        int currentMonsterIndex = 0;
        int currentscene = 0;

        //monsters
        Monster wompus;
        Monster thwompas;
        Monster backupwompus;
        Monster UnclePhil;
        Monster[] monsters;

        public void Run()
        {
            Start();
            

            while (!gameOver)
            {
                Update();
            }
            End();
        }

        void Start()
        {
            


            //Initailze monsters
            wompus.name = "Wompus";
            wompus.attack = 10.0f;
            wompus.defense = 5.0f;
            wompus.health = 20.0f;


            thwompas.name = "Thwompas";
            thwompas.attack = 15.0f;
            thwompas.defense = 10.0f;
            thwompas.health = 15.0f;


            backupwompus.name = "Backup Wompus";
            backupwompus.attack = 25.6f;
            backupwompus.defense = 5.0f;
            backupwompus.health = 3.0f;


            UnclePhil.name = "uncle Phil";
            UnclePhil.attack = 100000000000;
            UnclePhil.defense = 0;
            UnclePhil.health = 1.0f;

            monsters = new Monster[] { wompus, thwompas, backupwompus, UnclePhil };

            ResetCurrentMonsters();
            
        }

        /// <summary>
        /// Called every game loop
        /// </summary>
        void Update()
        {
            UpdateCurrentScene();
            Console.Clear();

        }

        void End()
        {
            Console.WriteLine("Guhbah fren");
        }
        /// <summary>
        /// resets the current fighters to be the first two monsters in the array
        /// </summary>
        void ResetCurrentMonsters()
        {
            currentMonsterIndex = 0;
            //Set starting fight
            currentMonster1 = monsters[currentMonsterIndex];
            currentMonsterIndex++;
            currentMonster2 = monsters[currentMonsterIndex];
        }


        void UpdateCurrentScene()
        {
            switch (currentscene)
            {
                case 0:
                    DisplayStartMenu();
                    break;

                case 1:
                    Battle();
                    UpdateCurrentMonsters();
                    Console.ReadKey();
                    break;

                case 2:
                    DisplayRestartMenu();
                    break;

                default:
                    Console.WriteLine("Invalid Scene index");
                    break;
            }

           
        }


        /// <summary>
        /// Gets an nput from the player based on some decision
        /// </summary>
        /// <param name="description">the context for description</param>
        /// <param name="option1">the first choice for player</param>
        /// <param name="option2">the second choice for the player</param>
        /// <param name="pauseInvalid">If true, the player must press a key to continue afer inputting
        /// an incorrect value</param>
        /// <returns>A number representing which of the two options was chosen. Returns 0 if and invalid input
        /// was recieved</returns>
        int GetInput(string description, string option1, string option2, bool pauseInvalid = false)
        {
            // Print the context and options
            Console.WriteLine(description);
            Console.WriteLine("1. " + option1);
            Console.WriteLine("2. " + option2);

            // Get player input
            string input = Console.ReadLine();
            int choice = 0;

            //If player typed 1...
            if (input == "1" || input != "2")
            {
                //...set the return variable to be 1
                choice = 1;
            }
            // If the player typed 2...
            else if (input == "2")
            {
                //...set the retrun variable to be 2
                choice = 2;
            }
            // if the player did not type 1 or 2...
            else
            {
                //...let them know the input was invalid
                Console.WriteLine("Invalid Input");

                //if we want to pause when an invalid input is recieved...
                if (pauseInvalid)
                {
                    //...make the player press a key to continue
                    Console.ReadKey(true);
                }
            }
            // return the player choice
            return choice;
                
            
        }

        /// <summary>
        /// Display the starting menu. Gives the player the option to start or
        /// exit the simulation
        /// </summary>
        void DisplayStartMenu() 
        {
            //Get player choice
            int choice = GetInput("Welcome To Monster Fight Club and Uncle Phil!", "Start Simulation", "Quit Application");

            //if they chose to start the sim...
            if (choice == 1)
            {
                //...start the battle scene
                currentscene = 1;
            }
            // Otherwise if they chose to exit...
            else if (choice == 2)
            {
                //...end the game
                gameOver = true;
            }
        
        }
        /// <summary>
        ///  Displays the restart menu. Gives player the option the restart or exit the program
        /// </summary>
        void DisplayRestartMenu()
        {
            //Get the player choice
            int choice = GetInput("Simulation Over. Would you like to play again", "Yes", "No");

            //if the player chose to restart...
            if (choice == 1)
            {
                //...set the current scene to be the start menu
                ResetCurrentMonsters();
                currentscene = 0;
            }
            // if the player chose to quit the program...
            else if (choice == 2)
            {
                //...exit the program
                gameOver = true;
            }
        }

        

        Monster GetMonster(int monsterIndex)
        {
            Monster monster;
            monster.name = "None";
            monster.attack = 1;
            monster.defense = 1;
            monster.health = 1;
            if (monsterIndex == 0)
            {
                monster = UnclePhil;
            }
            else if(monsterIndex == 1)
            {
                monster = backupwompus;
            }
            else if ( monsterIndex == 2)
            {
                monster = wompus;
            }
            else if(monsterIndex == 3)
            {
                monster = thwompas;
            }

            return monster;
        }
        /// <summary>
        /// Simulate one turn in the current monster fight
        /// </summary>
        void Battle()
        {
            //monster 1 stats
            PrintStats(currentMonster1);
            //monster 2 stats
            PrintStats(currentMonster2);

            //monster 1 attacks monster 2 
            float damagetaken = Fight(currentMonster1, ref currentMonster2);
            Console.WriteLine(currentMonster2.name + " has taken " + damagetaken);

            //monster 2 attacks monster 1
            damagetaken = Fight(currentMonster2, ref currentMonster1);
            Console.WriteLine(currentMonster1.name + " has taken " + damagetaken);
        }

        bool TryEndSimulation()
        {
            bool simulationOver = currentMonsterIndex >= monsters.Length;

            if (simulationOver)
            {
                currentscene = 2;
            }

            return simulationOver; 
        }

        /// <summary>
        /// Changes one of the current fighters to be the next in the list
        /// if it has died. Ends the game if all fighters in the list have been used
        /// </summary>
        void UpdateCurrentMonsters()
        { 
            // If monster 1 has died...
            if (currentMonster1.health <= 0)
            {
                 //...increment the current monster index and swap out the monster
                currentMonsterIndex++;
                if (TryEndSimulation())
                {
                    return;
                }
                currentMonster1 = monsters[currentMonsterIndex];
                
            }
            //If monster 2 has died...
            if (currentMonster2.health <= 0)
            {
                 //...increment the current monster index and swap out the monster
                 currentMonsterIndex++;
                if (TryEndSimulation())
                {
                    return;
                }
                 currentMonster2 = monsters[currentMonsterIndex];
                
            }
            //If either monster is set to "None" and the last monster has been set...
            if (currentMonsterIndex >= monsters.Length)
            {
                //...go to the restart menu
                currentscene = 2;
            }
        }

        void PrintStats(Monster monster)
        {
            Console.WriteLine("Name: " + monster.name);
            Console.WriteLine("Health: " + monster.health);
            Console.WriteLine("Attack:" + monster.attack);
            Console.WriteLine("Defense:" + monster.defense);
            
        }
        string StartBattle(ref Monster monster1, ref Monster monster2)
        {
            string matchResult = "No Contest";
            while (monster1.health > 0 && monster2.health > 0)
            {
                //monster 1 stats
                PrintStats(monster1);
                //monster 2 stats
                PrintStats(monster2);

                //monster 1 attacks monster 2 
                float damagetaken = Fight(monster1, ref monster2);
                Console.WriteLine(monster2.name + " has taken " + damagetaken);

                //monster 2 attacks monster 1
                damagetaken = Fight(monster2, ref monster1);
                Console.WriteLine(monster1.name + " has taken " + damagetaken);

                Console.ReadKey();
                Console.Clear();

                PrintStats(monster1);

                PrintStats(monster2);
                Console.ReadKey(true);
                Console.Clear();
            }
            if (monster1.health < 0 && monster2.health <= 0)
            {
                matchResult = "Draw";
            }
            else if (monster1.health > 0)
            {
                matchResult = monster1.name;

            }
            else if (monster2.health > 0)
            {
                matchResult = monster2.name;
            }
           
            return matchResult;
        }

        float Fight(Monster attacker, ref Monster defender)
        {
            float damagetaken = CalculateDamage(attacker, defender);
            defender.health -= damagetaken;
            Console.WriteLine(defender.name + " has taken " + damagetaken);
            return damagetaken;

        }
       


        float CalculateDamage(float attack, float defense)
        {
            float damage = attack - defense;
            if (damage <= 0)
            {
                damage = 0;
            }

            return damage;
        }

        float CalculateDamage(Monster attacker, Monster defender)
        {
            return attacker.attack - defender.defense;
        }

    }
}
