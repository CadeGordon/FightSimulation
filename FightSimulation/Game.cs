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
     
        
        public void Run()
        {
            // monster 1 stats
            Monster monster1;
            monster1.name = "Wompus";
            monster1.attack = 10.0f;
            monster1.defense = 5.0f;
            monster1.health = 20.0f;



            //Monster 2 stats
            Monster monster2;
            monster2.name = "Thwompas";
            monster2.attack = 15.0f;
            monster2.defense = 10.0f;
            monster2.health = 15.0f;

            //monster 1 stats
            PrintStats(monster1);
            //monster 2 stats
            PrintStats(monster2);
            Console.ReadKey();
            

            //monster 1 attacks monster 2 
            float damagetaken = Fight(monster1, monster2);
            Console.WriteLine(monster2.name + " has taken " + damagetaken);

            //monster 2 attacks monster 1
            damagetaken = Fight(monster2, monster1);
            Console.WriteLine(monster1.name + " has taken " + damagetaken);

            Console.ReadKey();
            Console.Clear();

            PrintStats(monster1);

            PrintStats(monster2);

        }

        void PrintStats(Monster monster)
        {
            Console.WriteLine("Name: " + monster.name);
            Console.WriteLine("Health: " + monster.health);
            Console.WriteLine("Attack:" + monster.attack);
            Console.WriteLine("Defense:" + monster.defense);
            
        }

        float Fight(Monster attacker, Monster defender)
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
