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
        int currentMonsterIndex = 1;

        //monsters
        Monster wompus;
        Monster thwompas;
        Monster backupwompus;
        Monster UnclePhil;

        public void Run()
        {
            Start();
            

            while (!gameOver)
            {
                Update();
            }

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

            //Set starting fight
            currentMonster1 = GetMonster(currentMonsterIndex);
            currentMonsterIndex++;
            currentMonster2 = GetMonster(currentMonsterIndex);
        }

        void Update()
        {
            Battle();
            UpdateCurrentMonsters();
            Console.ReadKey(true);
            Console.Clear();
        }

        Monster GetMonster(int monsterIndex)
        {
            Monster monster;
            monster.name = "None";
            monster.attack = 1;
            monster.defense = 1;
            monster.health = 1;
            if (monsterIndex == 1)
            {
                monster = UnclePhil;
            }
            else if(monsterIndex == 2)
            {
                monster = backupwompus;
            }
            else if ( monsterIndex == 3)
            {
                monster = wompus;
            }
            else if(monsterIndex == 4)
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
                currentMonster1 = GetMonster(currentMonsterIndex);

            }
            //If monster 2 has died...
            if (currentMonster2.health <= 0)
            {
                //...increment the current monster index and swap out the monster
                currentMonsterIndex++;
                currentMonster2 = GetMonster(currentMonsterIndex);
            }
            //If either monster is set to "None" and the last monster has been set...
            if (currentMonster2.name == "None" || currentMonster1.name == "None" && currentMonsterIndex >= 4)
            {
                //...end the game
                Console.WriteLine("Simulation Over");
                gameOver = true;
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
