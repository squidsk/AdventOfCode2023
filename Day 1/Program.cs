/*
 * Created by SharpDevelop.
 * User: skalmar
 * Date: 12/4/2023
 * Time: 11:39 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Day_1
{
	class Program
	{
		public static void Main(string[] args) {
			Part1("test.txt");
			Part1("input.txt");
			//Part2("test.txt");
			Part2("test2.txt");
			Part2("input.txt");

			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}

		static void Part1(string filename) {
			using(StringReader reader = new StringReader(File.ReadAllText(filename))){
				String line;
				Match match;
				int sum = 0;
				const String pattern = @"[^0-9]*([0-9]).*([0-9])[^0-9]*";
				const String pattern2 = @"[^0-9]*([0-9])";
				while((line = reader.ReadLine()) != null) {
					match = Regex.Match(line, pattern);
					if(match.Success) {
						sum += int.Parse(match.Groups[1].Value + match.Groups[2].Value);
					} else {
						match = Regex.Match(line, pattern2);
						sum += int.Parse(match.Groups[1].Value + match.Groups[1].Value);
					}
				}
				Console.WriteLine("The solution to Part 1 with inputfile: {0} is: {1}", filename, sum);
			}
		}

		static void Part2(string filename) {
			using(StringReader reader = new StringReader(File.ReadAllText(filename))){
				String line;
				Match match;
				int sum = 0;
				const String pattern = @"[^0-9]*([0-9]).*([0-9])[^0-9]*";
				const String pattern2 = @"[^0-9]*([0-9])";
				while((line = reader.ReadLine()) != null) {
					var new_line = replaceStringWithDigits(line);
					match = Regex.Match(new_line, pattern);
					if(match.Success) {
						sum += int.Parse(match.Groups[1].Value + match.Groups[2].Value);
					} else {
						match = Regex.Match(new_line, pattern2);
						sum += int.Parse(match.Groups[1].Value + match.Groups[1].Value);
					}
				}
				Console.WriteLine("The solution to Part 2 with inputfile: {0} is: {1}", filename, sum);
			}
		}
		
		static String convertToDigit(string value) {
			switch (value) {
					case "one": return "1e";
					case "two":	return "t2o";
					case "three": return "t3e";
					case "four":  return "4";
					case "five": return "5e";
					case "six": return "6";
					case "seven": return "7n";
					case "eight": return "e8t";
					case "nine": return "n9e";
					default: return value;
			}
		}
		
		static String replaceStringWithDigits(String line){
			int index = line.Length;
			String numStr = "";
			String[] numbers = {"one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};
			
			do {
				index = line.Length;
				numStr = "";
				foreach(var num in numbers) {
					var curIndex = line.IndexOf(num);
					if(curIndex != -1 && curIndex < index){
						index = curIndex;
						numStr = num;
					}
				}
				if(index != line.Length) {
					if(index == 0){
						line = convertToDigit(numStr) + line.Substring(numStr.Length);
					} else {
						line = line.Substring(0,index) + convertToDigit(numStr) + line.Substring(index + numStr.Length);
					}
				}
			} while(index != line.Length);
			
			return line;
		}
	}
}