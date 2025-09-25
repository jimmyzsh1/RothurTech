//using System.Collections.Generic;

//All the answers should be written inside Program.cs.
//●	Short-answer (theory) questions → should be written as comments (// or /* ... */).

//●	oop questions → all classes should be placed in the same Program.cs

/*
Questions
1.	Describe the problem generics address.

    Generics allow you to define a class, method, or data structure with a placeholder for the type of data it will operate on. 
    This enables code reusability and type safety, as you can create a single implementation that works with different data types without sacrificing performance or risking runtime errors due to type mismatches.

2.	How would you create a list of strings, using the generic List class?
    List<string> stringList = new List<string>();

3.	How many generic type parameters does the Dictionary class have?
    The Dictionary class has two generic type parameters: TKey and TValue.

4.	True/False. When a generic class has multiple type parameters, they must all match.
    False. When a generic class has multiple type parameters, they can be different types and do not need to match.

5.	What method is used to add items to a List object?
    The method used to add items to a List object is the Add() method.

    Example:
    ```
    List<string> names = new List<string>();
    names.Add("Alice");
    ```

6.	Name two methods that cause items to be removed from a List.
    Two methods that can be used to remove items from a List are:
    - Remove(item): Removes the first occurrence of a specific object from the List.
    - RemoveAt(index): Removes the item at the specified index in the List.

7.	How do you indicate that a class has a generic type parameter?
    You indicate that a class has a generic type parameter by using angle brackets <> after the class name, followed by the type parameter(s).
    Example:
    ```
    public class MyStack<T>
    {
        private List<T> items = new List<T>();

        public void Push(T item)
        {
            items.Add(item);
        }

        public T Pop()
        {
            T top = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            return top;
        }
    }

    ```

8.	True/False. Generic classes can only have one generic type parameter.
    False. Generic classes can have multiple generic type parameters.
    Example:
    ```
    public class MyPair<T1, T2>
    {
        public T1 First;
        public T2 Second;
    }
    ```

9.	True/False. Generic type constraints limit what can be used for the generic type.
    True. Generic type constraints allow you to specify requirements for the types that can be used as arguments for a generic type parameter, 
    such as requiring that the type must implement a specific interface or inherit from a particular base class.
    
    Example:
    ```
    public class Repository<T> where T : class
    ```

10.	True/False. Constraints let you use the methods of the thing you are constraining to.
    True. Constraints allow you to use the methods and properties of the constrained type within the generic class or method.
    Example:
    ```
    public class Printer<T> where T : IPrintable
    {
        public void Print(T item)
        {
            item.Print();  // 因为 T 实现了 IPrintable 接口，所以可以安全调用 Print()
        }
    }
    ```
*/



/*
Practice Exercise
Task 1:
 Define a generic class called MyStack<T> with the following requirements:
1.Use Stack<T> internally to store the data.

2.	Implement a Count() method that returns the number of elements in the stack.

3.	Implement a Pop() method that returns and removes the top element of the stack.

4.	Implement a Push(T obj) method that adds an element to the stack.

Finally, create an instance of MyStack<int>, push two integers into it, and print out the current number of elements in the stack.

*/

using System;
using System.Collections.Generic;

namespace HW03
{
    public class  MyStack<T> 
    {
        private Stack<T> mystack = new Stack<T>();

        public void Push(T obj)
        {
            mystack.Push(obj);
        }

        public T Pop()
        {
            return mystack.Pop();
        }

        public int Count()
        {
            return mystack.Count;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyStack<int> stack = new MyStack<int>();
            stack.Push(10);
            stack.Push(20);
            Console.WriteLine("The current number of elements in the stack: " + stack.Count());
        }
    }
}

/*
Task 2:
 Create a generic repository pattern in C# with the following requirements:
1.	Define a generic interface IGenericRepository<T> where T : class.

○	The interface should declare the following methods:

■	Add(T item)

■	Remove(T item)

■	Save()

■	IEnumerable<T> GetAll()

■	T GetById(int id)

*/

namespace HW03
{
    public interface IGenericRepository<T> where T : class 
    {
        void Add(T item);
        void Remove(T item);
        void Save();
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}

/*
2.Implement a class GenericRepository<T> that inherits from IGenericRepository<T>.

○	Use a private List<T> field to store the data.
○	In the constructor, initialize the list as a new empty List<T>.
○	Provide method implementations for Add, Remove, GetAll, GetById.No actual implementation is needed for Save.
*/

namespace HW03
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private List<T> items;
        public GenericRepository()
        {
            items = new List<T>();
        }
        public void Add(T item)
        {
            items.Add(item);
        }
        public void Remove(T item)
        {
            items.Remove(item);
        }
        public IEnumerable<T> GetAll()
        {
            return items;
        }
        public void Save()
        {
            // No actual implementation needed
        }
        public T GetById(int id)
        {
            // Use reflection to find property "Id"
            return items.FirstOrDefault(item =>
            {
                var prop = item.GetType().GetProperty("Id");
                if (prop != null)
                {
                    var value = prop.GetValue(item);
                    return value is int && (int)value == id;
                }
                return false;
            });
        }
    }
}