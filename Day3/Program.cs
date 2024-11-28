/*
 * Created by SharpDevelop.
 * User: skalmar
 * Date: 12/7/2023
 * Time: 11:09 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day3
{
	class Program
	{
		public static void Main(string[] args) {
			Part1("test.txt");
			Part1("input.txt");
//			Part2("test2.txt");
			Part2("test.txt");
			Part2("input.txt");

			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}

		static bool isPart(int starti, int endi, int startj, int endj, String[] lines) {
			bool partCharFound = false;
			if(starti < 0 ) starti = 0;
			if(endi >= lines.Length) endi = lines.Length - 1;
			if(startj < 0) startj = 0;
			if(endj >= lines[0].Length) endj = lines[0].Length - 1;
			for(int i = starti; i <= endi; i += 1) {
				for(int j = startj; j <= endj; j +=1) {
					char charToCheck = lines[i][j];
					if((charToCheck < '0' || charToCheck > '9') && charToCheck != '.') {
						partCharFound = true;
					}
				}
			}
			
			return partCharFound;
		}
		static void Part1(string filename) {
			String[] lines = File.ReadLines(filename).ToArray();
			int lineLength = lines[0].Length;
			int partSum = 0;
			
			for(int i = 0; i < lines.Length; i += 1) {
				String line = lines[i];
				for(int j = 0; j < lineLength; j += 1){
					if(line[j] >= '0' && line[j] <= '9') {
						int num = line[j] - '0';
						int startj = j;
						if(++j < lineLength && line[j] >= '0' && line[j] <= '9') {
							num = num*10 + line[j] - '0';
							if(++j < lineLength && line[j] >= '0' && line[j] <= '9') {
								num = num*10 + line[j] - '0';
							}
						}
						if(isPart(i-1,i+1,startj-1,j+1, lines)){
							partSum += num;
						}
					}
				}
			}
			
			
			Console.WriteLine("The solution to Part 1 with inputfile: {0} is: {1}", filename, partSum);
			
		}

		static unsafe void GetNums(String[] lines, int row, int col, out int num1, out int num2) {
			//int rOffset = -1;
			//int cOffset = -1;
			int localRow;
			int localCol;
			int colLength = lines[0].Length;
			int rowLength = lines.Length;
			int count = 0;
			num1 = -1;
			num2 = -1;
			fixed(int* localN1 = &num1, localN2 = &num2) {
				int *ptr = localN1;
				
				//check to the left
				localCol = col;
				if(--localCol >= 0 && Char.IsDigit(lines[row][localCol])) {
					*ptr = lines[row][localCol] - '0';
					if(--localCol >= 0 && Char.IsDigit(lines[row][localCol])) {
						*ptr = 10*(lines[row][localCol] - '0') + *ptr;
						if(--localCol >= 0 && Char.IsDigit(lines[row][localCol])) {
							*ptr = 100*(lines[row][localCol] - '0') + *ptr;
						}
					}
					ptr = localN2;
					count += 1;
				}

				//check to the right
				localCol = col+1;
				if(localCol < colLength && Char.IsDigit(lines[row][localCol])) {
					*ptr = lines[row][localCol] - '0';
					if(++localCol < colLength && Char.IsDigit(lines[row][localCol])) {
						*ptr = (lines[row][localCol] - '0') + 10*(*ptr);
						if(++localCol < colLength && Char.IsDigit(lines[row][localCol])) {
							*ptr = (lines[row][localCol] - '0') + 10*(*ptr);
						}
					}
					ptr = localN2;
					count += 1;
				}
				
				//check row above
				localRow = row-1;
				localCol = col;
				if(localRow >= 0) {
					if(Char.IsDigit(lines[localRow][localCol])) {
						*ptr = lines[localRow][localCol] - '0';
						if(localCol-1 >= 0 && Char.IsDigit(lines[localRow][localCol-1])) {
							*ptr = 10*(lines[localRow][localCol-1] - '0') + *ptr;
							if(localCol-2 >= 0 && Char.IsDigit(lines[localRow][localCol-2])) {
								*ptr = 100*(lines[localRow][localCol-2] - '0') + *ptr;
							}
						}
						if(localCol+1 <colLength && Char.IsDigit(lines[localRow][localCol+1])) {
							*ptr = (lines[localRow][localCol+1] - '0') + (*ptr)*10;
							if(localCol+2 < colLength && Char.IsDigit(lines[localRow][localCol+2])) {
								*ptr = (lines[localRow][localCol+2] - '0') + (*ptr)*10;
							}
						}
						ptr = localN2;
						count += 1;
					} else {
						//top center is not a digit, check up left and up right
						//check top left
						if(localCol-1 >= 0 && Char.IsDigit(lines[localRow][localCol-1])) {
							*ptr = lines[localRow][localCol-1] - '0';
							if(localCol-2 >= 0 && Char.IsDigit(lines[localRow][localCol-2])) {
								*ptr = 10*(lines[localRow][localCol-2] - '0') + *ptr;
								if(localCol-3 >= 0 && Char.IsDigit(lines[localRow][localCol-3])) {
									*ptr = 100*(lines[localRow][localCol-3] - '0') + *ptr;
								}
							}
							ptr = localN2;
							count += 1;
						}

						//check top right
						if(localCol+1 < colLength && Char.IsDigit(lines[localRow][localCol+1])) {
							*ptr = lines[localRow][localCol+1] - '0';
							if(localCol+2 < colLength && Char.IsDigit(lines[localRow][localCol+2])) {
								*ptr = (lines[localRow][localCol+2] - '0') + (*ptr)*10;
								if(localCol+3 < colLength && Char.IsDigit(lines[localRow][localCol+3])) {
									*ptr = (lines[localRow][localCol+3] - '0') + (*ptr)*10;
								}
							}
							ptr = localN2;
							count += 1;
						}
					}
				}

				//check row below
				localRow = row+1;
				localCol = col;
				if(localRow < rowLength) {
					if(Char.IsDigit(lines[localRow][localCol])) {
						*ptr = lines[localRow][localCol] - '0';
						if(localCol-1 >= 0 && Char.IsDigit(lines[localRow][localCol-1])) {
							*ptr = 10*(lines[localRow][localCol-1] - '0') + *ptr;
							if(localCol-2 >= 0 && Char.IsDigit(lines[localRow][localCol-2])) {
								*ptr = 100*(lines[localRow][localCol-2] - '0') + *ptr;
							}
						}
						if(localCol+1 <colLength && Char.IsDigit(lines[localRow][localCol+1])) {
							*ptr = (lines[localRow][localCol+1] - '0') + (*ptr)*10;
							if(localCol+2 < colLength && Char.IsDigit(lines[localRow][localCol+2])) {
								*ptr = (lines[localRow][localCol+2] - '0') + (*ptr)*10;
							}
						}
						ptr = localN2;
						count += 1;
					} else {
						//bottom center is not a digit, check down left and down right
						//check down left
						if(localCol-1 >= 0 && Char.IsDigit(lines[localRow][localCol-1])) {
							*ptr = lines[localRow][localCol-1] - '0';
							if(localCol-2 >= 0 && Char.IsDigit(lines[localRow][localCol-2])) {
								*ptr = 10*(lines[localRow][localCol-2] - '0') + *ptr;
								if(localCol-3 >= 0 && Char.IsDigit(lines[localRow][localCol-3])) {
									*ptr = 100*(lines[localRow][localCol-3] - '0') + *ptr;
								}
							}
							ptr = localN2;
							count += 1;
						}

						//check top right
						if(localCol+1 < colLength && Char.IsDigit(lines[localRow][localCol+1])) {
							*ptr = lines[localRow][localCol+1] - '0';
							if(localCol+2 < colLength && Char.IsDigit(lines[localRow][localCol+2])) {
								*ptr = (lines[localRow][localCol+2] - '0') + (*ptr)*10;
								if(localCol+3 < colLength && Char.IsDigit(lines[localRow][localCol+3])) {
									*ptr = (lines[localRow][localCol+3] - '0') + (*ptr)*10;
								}
							}
							ptr = localN2;
							count += 1;
						}
					}
				}
			}
			if(count != 2) num2 = -1;
		}
		
		static void Part2(string filename) {
			String[] lines = File.ReadLines(filename).ToArray();
			int lineLength = lines[0].Length;
			int partSum = 0;
			
			for(int i = 0; i < lines.Length; i += 1) {
				String line = lines[i];
				for(int j = 0; j < lineLength; j += 1){
					if(line[j] == '*') {
						int num1, num2;
						GetNums(lines, i, j, out num1, out num2);
						if(num1 != -1 && num2 != -1){
							partSum += num1*num2;
						}
					}
				}
			}
			Console.WriteLine("The solution to Part 2 with inputfile: {0} is: {1}", filename, partSum);
			
		}
	}
}