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
            Console.Clear();

            //monster 1 attacks monster 2 
            float damagetaken = CalculateDamage(monster1.attack, monster2.defense);
            monster2.health -= damagetaken;
            Console.WriteLine(monster2.name + " has taken " + damagetaken);

            //monster 2 attacks monster 1
            damagetaken = CalculateDamage(monster2.attack, monster1.defense);
            monster1.health -= damagetaken;
            Console.WriteLine(monster1.name + " has taken " + damagetaken);

            Console.ReadKey();
            Console.Clear();
        }

        void PrintStats(Monster monster)
        {
            Console.WriteLine("Name: " + monster.name);
            Console.WriteLine("Health: " + monster.health);
            Console.WriteLine("Attack:" + monster.attack);
            Console.WriteLine("Defense:" + monster.defense);
            
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

    }
}
