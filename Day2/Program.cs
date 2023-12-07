/*
 * Created by SharpDevelop.
 * User: skalmar
 * Date: 12/7/2023
 * Time: 9:46 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace Day2
{
	class Program
	{
		
		private const int RED = 0;
		private const int GREEN = 1;
		private const int BLUE = 2;

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
				int sum = 0;
				String line;
				int[] maxPossibleColors = {12, 13, 14};
				
				while((line = reader.ReadLine()) != null) {
					int[] maxColors = new int[3];
					String[] splitLine = line.Split(':');
					int gameID = int.Parse(splitLine[0].Substring(splitLine[0].IndexOf(' ') + 1));
					
					splitLine = splitLine[1].Split(';');
					foreach(var game in splitLine) {
						String[] colors = game.Split(',');
						foreach(var color in colors) {
							String tcolor = color.Trim();
							int index = tcolor.IndexOf(' ');
							int num = int.Parse(tcolor.Substring(0,index));
							String colorStr = tcolor.Substring(index + 1);
							if(colorStr.Equals("red")) {
								if (num > maxColors[RED])
									maxColors[RED] = num;
							} else if(colorStr.Equals("green")) {
								if(num > maxColors[GREEN])
									maxColors[GREEN] = num;
							} else if(colorStr.Equals("blue")) {
								if(num > maxColors[BLUE])
									maxColors[BLUE] = num;
							} else
								Console.Out.WriteLine("Something when wrong on line: " + line);
						}
					}
					
					if(maxColors[RED] <= maxPossibleColors[RED] && maxColors[GREEN] <= maxPossibleColors[GREEN] && maxColors[BLUE] <= maxPossibleColors[BLUE]) {
						sum += gameID;
					}
				}

				Console.WriteLine("The solution to Part 1 with inputfile: {0} is: {1}", filename, sum);
			}
		}

		static void Part2(string filename) {
			using(StringReader reader = new StringReader(File.ReadAllText(filename))){
				int sum = 0;
				String line;
				
				while((line = reader.ReadLine()) != null) {
					int[] maxColors = new int[3];
					String[] splitLine = line.Split(':');
					int gameID = int.Parse(splitLine[0].Substring(splitLine[0].IndexOf(' ') + 1));
					
					splitLine = splitLine[1].Split(';');
					foreach(var game in splitLine) {
						String[] colors = game.Split(',');
						foreach(var color in colors) {
							String tcolor = color.Trim();
							int index = tcolor.IndexOf(' ');
							int num = int.Parse(tcolor.Substring(0,index));
							String colorStr = tcolor.Substring(index + 1);
							if(colorStr.Equals("red")) {
								if (num > maxColors[RED])
									maxColors[RED] = num;
							} else if(colorStr.Equals("green")) {
								if(num > maxColors[GREEN])
									maxColors[GREEN] = num;
							} else if(colorStr.Equals("blue")) {
								if(num > maxColors[BLUE])
									maxColors[BLUE] = num;
							} else
								Console.Out.WriteLine("Something when wrong on line: " + line);
						}
					}
					
					sum += (maxColors[RED] * maxColors[GREEN] * maxColors[BLUE]);
				}


				Console.WriteLine("The solution to Part 2 with inputfile: {0} is: {1}", filename, sum);
			}
		}
	}
}