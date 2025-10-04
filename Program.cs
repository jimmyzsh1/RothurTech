
/******************************************************
 * Name: Shihua Zhang (JimmyZsh)
  * Assignment: HW01 – Introduction to C# and Data Types
 * Course: C# Programming (.NET)
 * Date: 2025-09-15
 ******************************************************/


// All the answers should be written inside Program.cs.
// ●	Short-answer (theory) questions → should be written as comments (// or /* ... */).

// ●	Coding questions → should each be placed in a separate class, with the solution implemented as a function.

/*
01 Introduction to C# and Data Types – Questions
1.	What type would you choose for the following “numbers”?



○	A person’s telephone number
	string 

○	A person’s height
    double

○	A person’s age
    int

○	A person’s gender (Male, Female, Prefer Not To Answer)
    string

○	A person’s salary
    decimal

○	A book’s ISBN
    string

○	A book’s price
    decimal

○	A book’s shipping weight
    double

○	A country’s population
    long

○	The number of stars in the universe
    long

○	The number of employees in each of the small or medium businesses in the United Kingdom (up to about 50,000 employees per business)
    int
*/

/*
2.	What are the differences between value type and reference type variables?
 What is boxing and unboxing?

Value types store data directly, examples: int, double, char, bool, struct, enum. Reference types store data to a reference (memory address), examples: string, class, object, array, List<T>.
Boxing is converting a value type to a reference type, unboxing is converting a reference type back to a value type.


3.	What is meant by the terms managed resource and unmanaged resource in .NET?

Managed resources are those that are handled by the .NET runtime (Common Language Runtime - CLR), such as memory for objects created in managed code. Unmanaged resources are those that the developer must handle manually, such as file handles, database connections, and network sockets.

4.	What is the purpose of the Garbage Collector in .NET?

The Garbage Collector (GC) in .NET automatically manages memory allocation and deallocation for managed objects. It helps to reclaim memory occupied by objects that are no longer in use, thus preventing memory leaks and optimizing application performance.
*/

/*
Controlling Flow and Converting Types – Questions
1.	What happens when you divide an int variable by 0?
Dividing an int by 0 throws a DivideByZeroException at runtime.

2.	What happens when you divide a double variable by 0?
Dividing a double by 0 results in positive infinity (Infinity) or negative infinity (-Infinity) depending on the sign of the numerator. If the numerator is zero, the result is NaN (Not a Number).

3.	What happens when you overflow an int variable (assign a value beyond its range)?

When an int variable overflows, it wraps around to the other end of the range.
For example:
int max = int.MaxValue; // 2,147,483,647
max += 1; // Results in -2,147,483,648
However, if you use the checked keyword or enable overflow checking in your project settings, an OverflowException will be thrown when an overflow occurs.

4.	What is the difference between x = y++; and x = ++y;?

- x = y++ → Post-increment: Assigns y to x, then increments y
- x = ++y → Pre-increment: Increments y first, then assigns y to x
Example:
int y = 5;
x = y++; // x = 5, y = 6
x = ++y; // y = 6 → x = 6, then y = 6


5.	What is the difference between break, continue, and return when used inside a loop statement?

 break: Immediately exits the loop and continues with the next statement after the loop.
 continue: Skips the rest of the current loop iteration and proceeds with the next iteration.
 return: Exits the entire method immediately and returns a value (if any) to the caller.


6.	What are the three parts of a for statement and which of them are required?

The three parts of a for loop are:
1. Initialization
2. Condition
3. Iterator (increment/decrement)

None of them are strictly required. You can write: for ( ; ; ) which creates an infinite loop.


7.	What is the difference between the = and == operators?

 = is the assignment operator: assigns a value to a variable.
 == is the equality operator: checks if two values are equal.

8.	Does the following statement compile? for ( ; true; ) ;
yes, it compiles and creates an infinite loop.

9.	What interface must an object implement to be enumerated by the foreach statement?
To be enumerated by the foreach statement, an object must implement the IEnumerable interface(or IEnumerable<T> for generics).
*/

/*
Coding：
1. How can we find the minimum and maximum values, as well as the number of bytes, for the following data types: sbyte, byte, short, ushort, int, uint, long, ulong, float, double, and decimal?
*/

using System;
namespace HW01
{
    public class Question01
    {
        public static void ShowTypeInfo()
        {
            Console.WriteLine("Type\tMin Value\tMax Value\tSize(Bytes)");
            Console.WriteLine("-------------------------------------------------------------");

            Console.WriteLine($"sbyte\t{SByte.MinValue}\t\t\t{SByte.MaxValue}\t\t\t{sizeof(sbyte)}");
            Console.WriteLine($"byte\t{Byte.MinValue}\t\t\t{Byte.MaxValue}\t\t\t{sizeof(byte)}");
            Console.WriteLine($"short\t{Int16.MinValue}\t\t\t{Int16.MaxValue}\t\t\t{sizeof(short)}");
            Console.WriteLine($"ushort\t{UInt16.MinValue}\t\t\t{UInt16.MaxValue}\t\t\t{sizeof(ushort)}");
            Console.WriteLine($"int\t{Int32.MinValue}\t{Int32.MaxValue}\t{sizeof(int)}");
            Console.WriteLine($"uint\t{UInt32.MinValue}\t\t{UInt32.MaxValue}\t\t{sizeof(uint)}");
            Console.WriteLine($"long\t{Int64.MinValue}\t{Int64.MaxValue}\t{sizeof(long)}");
            Console.WriteLine($"ulong\t{UInt64.MinValue}\t\t{UInt64.MaxValue}\t\t{sizeof(ulong)}");
            Console.WriteLine($"float\t{Single.MinValue}\t{Single.MaxValue}\t{sizeof(float)}");
            Console.WriteLine($"double\t{Double.MinValue}\t{Double.MaxValue}\t{sizeof(double)}");
            Console.WriteLine($"decimal\t{Decimal.MinValue}\t{Decimal.MaxValue}\t{sizeof(decimal)}");
        }
    }
}

/*
2. Write a method in C# called FizzBuzz that takes an integer num and prints numbers from 1 up to num, but:
●	Print Fizz if the number is divisible by 3.

●	Print Buzz if the number is divisible by 5.

●	Print FizzBuzz if the number is divisible by both 3 and 5.

●	Otherwise, print the number itself.
*/

namespace HW01
{
    public class Question02
    {
        public static void FizzBuzz(int num)
        {
            for (int i = 1; i <= num; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    Console.WriteLine("FizzBuzz");
                }
                else if (i % 3 == 0)
                {
                    Console.WriteLine("Fizz");
                }
                else if (i % 5 == 0)
                {
                    Console.WriteLine("Buzz");
                }
                else
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}


/*
3. What will happen if this code executes?
int max = 500;
for (byte i = 0; i < max; i++)
{
    Console.WriteLine(i);
}
*/

namespace HW01
{
    public class Question03
    {
        public static void ExplainCode()
        {
            /*
            The code will result in an infinite loop. The byte type can only hold values from 0 to 255. 
            When i reaches 255 and increments, it wraps around to 0 due to overflow, causing the loop condition (i < max) to always be true.
            */
            // int max = 500;
            // for (byte i = 0; i < max; i++)
            // {
            //     Console.WriteLine(i);
            // }
        }
    }
}

/*
4. Two Sum
 Given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target.

●	You may assume that each input would have exactly one solution.

●	You may not use the same element twice.

●	You can return the answer in any order.
*/

namespace HW01
{
    public class Question04
    {
        public static int[] TwoSum(int[] nums, int target)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                int compliment = target - nums[i];
                if (dict.ContainsKey(compliment))
                {
                    return new int[] { dict[compliment], i };
                    dict.Add(compliment, i);
                }
                dict.Add(nums[i], i);
            }
            return new int[0];
        }

        public static void TestTwoSum()
        {
            int[] nums = { 2, 7, 11, 15 };
            int target = 9;
            int[] result = TwoSum(nums, target);
            Console.WriteLine($"Indices: {result[0]}, {result[1]}"); // Output: Indices: 0, 1
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        HW01.Question01.ShowTypeInfo();
        HW01.Question02.FizzBuzz(20);
        //HW01.Question03.ExplainCode();
        HW01.Question04.TestTwoSum();
    }
}
