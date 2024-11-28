/*
 * Created by SharpDevelop.
 * User: skalmar
 * Date: 11/28/2024
 * Time: 11:08 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day4
{
	class Program
	{
		public static void Main(string[] args) {
			Part1("test.txt");
			Part1("input.txt");
			Part2("test.txt");
			Part2("input.txt");

			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}

		static void Part1(string filename) {
			using(StringReader reader = new StringReader(File.ReadAllText(filename))){
				int totalPoints = 0;
				while(reader.Peek() != -1){
					String line = reader.ReadLine();
					String[] parts = line.Split(':')[1].Split('|');
					List<string> winningNumbers = parts[0].Split(new char[]{' '},StringSplitOptions.RemoveEmptyEntries).ToList();
					List<string> myNumbers = parts[1].Split(new char[]{' '},StringSplitOptions.RemoveEmptyEntries).ToList();
					
					int matches = winningNumbers.Intersect(myNumbers).Count();
					if(matches >= 1) {
						totalPoints += 1<<(matches-1);
					}
				}
				Console.WriteLine("The solution to Part 1 with inputfile: {0} is: {1}", filename, totalPoints);
			}
		}

		static void Part2(string filename) {
			List<int> cardCount = new List<int>();
			using(StringReader reader = new StringReader(File.ReadAllText(filename))){
				int currentCard = 0;
				cardCount.Add(1);
				
				while(reader.Peek() != -1){
					String line = reader.ReadLine();
					String[] parts = line.Split(':')[1].Split('|');
					List<string> winningNumbers = parts[0].Split(new char[]{' '},StringSplitOptions.RemoveEmptyEntries).ToList();
					List<string> myNumbers = parts[1].Split(new char[]{' '},StringSplitOptions.RemoveEmptyEntries).ToList();
					int matches = winningNumbers.Intersect(myNumbers).Count();

					if(currentCard >= cardCount.Count) {
						cardCount.Add(1);
					}

					if(matches >= 1) {
						for(int i=1; i<=matches; i+=1) {
							if(currentCard+i >= cardCount.Count) {
								cardCount.Add(1);
							}
							cardCount[currentCard+i] += cardCount[currentCard];
						}
					}
					currentCard += 1;
				}
			}

			Console.WriteLine("The solution to Part 2 with inputfile: {0} is: {1}", filename, cardCount.Sum());
		}
	}
}