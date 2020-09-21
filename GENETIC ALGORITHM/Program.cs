using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Channels;

namespace GENETIC_ALGORITHM
{
    class Program
    {
        static Random r = new Random();
        static RandomLetter rndL = new RandomLetter();

        static void Main(string[] args)
        {
            Console.WriteLine("Írjon be egy szó ékezet nélkül: ");
            string word = Console.ReadLine();
            Start(generateFirstPopulation(200, word),word);
        }
        
        public static void Start(List<string> first_population,string word)
        {
            List<string> pop = first_population;
            while (true)
            {
                pop = mutatePopulation(createChildren(selectFromPop(computePerfPopulation(pop, word), 50, 30), 6), 20);
                string best = computePerfPopulation(pop, word)[0].Word;
                Console.WriteLine(best);
                System.Threading.Thread.Sleep(500);
            }
            
            
        }

        public static string mutateWord(string word)
        {
            StringBuilder sb = new StringBuilder(word);

            int index_modification = r.Next(0,word.Length);
            sb[index_modification]= rndL.GetLetter();

            return sb.ToString();
        }

        public static List<string> mutatePopulation(List<string> population, int chance_of_mutation)
        {
            
            for (int i = 0; i < population.Count(); i++)
            {
                if (r.Next(0,101)<chance_of_mutation)
                {
                    population[i] = mutateWord(population[i]);
                }
            }
            return population;
        }

        public static string createChild(string individual1, string individual2)
        {
            string child = "";
            for (int i = 0; i < individual1.Length; i++)
            {
                if (r.Next(0,101)<50)
                {
                    child += individual1[i];
                }
                else
                {
                    child += individual2[i];
                }
            }
            return child;
        }

        public static List<string> createChildren(List<Individual> breeders,int numberOfChild)
        {
            List<string> nextPopulation = new List<string>();
            for (int i = 0; i < breeders.Count()/2; i++)
            {
                for (int j = 0; j < numberOfChild; j++)
                {
                    nextPopulation.Add(createChild(breeders[i].Word, breeders[breeders.Count() - 1 - i].Word));
                }
            }
            return nextPopulation;
        }

        public static List<Individual> selectFromPop(List<Individual> populationSorted, int best_sample, int lucky_few)
        {
            List<Individual> nextGen = new List<Individual>();
            for (int i = 0; i < best_sample; i++)
            {
                nextGen.Add(populationSorted[i]);
            }
            for (int i = 0; i < lucky_few; i++)
            {
                nextGen.Add(populationSorted[r.Next(populationSorted.Count())]);
            }

            return nextGen.OrderBy(o => Guid.NewGuid()).ToList();
        }


        public static List<Individual> computePerfPopulation(List<string> population, string password)
        {
            List<Individual> populationPerf = new List<Individual>();
            Individual individual;
            foreach (var word in population)
            {
                individual = new Individual();
                individual.Word = word;
                individual.Fitness = fitness(password, word);
                populationPerf.Add(individual);

            }
            return populationPerf.OrderByDescending(o => o.Fitness).ToList();
        }

        public static List<string> generateFirstPopulation(int size,string password)
        {
            List<string> population = new List<string>();

            for (int i = 0; i < size; i++)
            {
                population.Add(generateWord(password.Length));
            }           
            return population;
        }

        public static string generateWord(int lenght)
        {
            string result="";
            for (int i = 0; i < lenght; i++)
            {
                char letter = rndL.GetLetter();
                result += letter;
            }
            return result;
        }

        public static double fitness(string password, string testWord) 
        {
            if (testWord.Length != password.Length)
            {
                return -1;
            }
            else 
            {
                double score = 0;
                for (int i = 0; i < password.Length; i++)
                {
                    if (password[i]==testWord[i])
                    {
                        score += 1;
                    }
                }
                return score * 100 / password.Length;
            }
        }
    }

}
