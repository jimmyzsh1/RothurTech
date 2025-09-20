//All the answers should be written inside Program.cs.
//●	Short-answer (theory) questions → should be written as comments (// or /* ... */).

//●	oop questions → all classes should be placed in the same Program.cs

/*
OOP Q&A
1.	What are the six combinations of access modifier keywords and what do they do?

    The six C# access modifiers are public, private, protected, internal, protected internal, and private protected. They control the accessibility of classes, 
    methods, and other members by defining where they can be accessed, ranging from anywhere (public) to only within the containing class (private), 
    or specific combinations such as within the current assembly and derived classes (protected internal). 

    1. **public**
       - Accessible from anywhere in the project or from other projects that reference it.
       - Example: `public class Person { }`

    2. **private**
       - Accessible only within the same class or struct (most restrictive).
       - This is the default access level for class members.
       - Example: `private int salary;`

    3. **protected**
       - Accessible within the same class and by derived (child) classes.
       - Example: `protected void SetName(string name) { }`

    4. **internal**
       - Accessible only within the same assembly (project).
       - Cannot be accessed from another project, even if referenced.
       - Example: `internal class Helper { }`

    5. **protected internal**
       - Accessible within the same assembly, or in derived classes (even if the derived class is in another assembly).
       - This is a combination of `protected` and `internal`.
       - Example: `protected internal int id;`

    6. **private protected**
       - Accessible only within the same class or derived classes, but *only* if they are in the same assembly.
       - This is more restrictive than `protected internal`.
       - Example: `private protected void Configure() { }`


2.	What is the difference between the static, const, and readonly keywords when applied to a type member?

    1. **static**
       - Indicates that the member belongs to the **type itself**, not to any specific instance.
       - Shared across all instances of the class.
       - Can be assigned and changed (unless combined with const or readonly).
       - Example:
         ```csharp
         public static int Counter = 0;
         ```

    2. **const**
       - The value is **constant and must be assigned at the time of declaration**.
       - It is **implicitly static** and cannot be changed afterwards.
       - The value is **compiled into the code**, so changing it requires recompilation of all dependent code.
       - Only **primitive types and strings** can be const.
       - Example:
         ```csharp
         public const double Pi = 3.14159;
         ```

    3. **readonly**
       - The value can be assigned **either at declaration or in the constructor**.
       - Once set, it **cannot be changed**.
       - Unlike `const`, `readonly` is **evaluated at runtime**, not compile time.
       - Can be used with reference types and even user-defined objects.
       - Example:
         ```csharp
         public readonly DateTime CreatedAt;
     
         public MyClass()
         {
             CreatedAt = DateTime.Now;
         }
         ```

3.	What does a constructor do?

    A constructor is a special method in a class that is automatically called when an object of that class is created.
    It is used to initialize the object’s fields or execute any setup code when the object is instantiated.

    ✍️ Example:
    ```
    public class Person
    {
        public string Name;

        // Constructor
        public Person(string name)
        {
            Name = name;
        }
    }
    ```

4.	Why is the partial keyword useful?

    The partial keyword allows a class, struct, or interface to be split across multiple files.
    It is useful for separating auto-generated code (e.g., by a designer or scaffolding tool) from user-written code.
    This makes the code easier to maintain, reduces merge conflicts, and keeps custom code safe from being overwritten.

    ✍️ Example:
    ```
    // File 1
    public partial class MyClass
    {
        public void MethodA() { }
    }

    // File 2
    public partial class MyClass
    {
        public void MethodB() { }
    }
    ```

5.	What is a tuple?

    A tuple is a lightweight data structure that can store multiple values of different types in a single object.
    Tuples are useful when you want to return multiple values from a method without creating a custom class.
    ✍️ Example:

    ```
    (string, int) person = ("Alice", 25);
    Console.WriteLine(person.Item1); // "Alice"
    Console.WriteLine(person.Item2); // 25

    // With named fields:
    (string Name, int Age) person2 = ("Bob", 30);
    Console.WriteLine(person2.Name); // "Bob"
    Console.WriteLine(person2.Age);  // 30
    ```

6.	What does the C# record keyword do?

    The record keyword in C# defines a reference type that is intended to be immutable and primarily used for storing data.
    Records automatically generate value-based equality, ToString(), Equals(), and GetHashCode() methods.

    Records are ideal for scenarios like Data Transfer Objects (DTOs), where data immutability and comparison by value (not by reference) are important.

    ✍️ Example:
    ```
    public record Person(string Name, int Age);

    // Usage
    var p1 = new Person("Alice", 25);
    var p2 = new Person("Alice", 25);
    Console.WriteLine(p1 == p2);  // True (value-based equality)

    ```

7.	What does overloading and overriding mean?

    Overloading means defining multiple methods in the same class with the same name but different parameters (type or number).

    Overriding means providing a new implementation of a method in a subclass, replacing the base class version using the override keyword.

    ✍️ Overloading Example:
    ```
    public class MathHelper {
        public int Add(int a, int b) => a + b;
        public double Add(double a, double b) => a + b;
        public int Add(int a, int b, int c) => a + b + c;
    }
    ```
    ✍️ Overriding Example:
    ```
    public class Animal {
        public virtual void Speak() {
            Console.WriteLine("Animal speaks");
        }
    }

    public class Dog : Animal {
        public override void Speak() {
            Console.WriteLine("Dog barks");
        }
    }
    ```

8.	What is the difference between a field and a property?

    A field is a variable that is declared directly in a class or struct and holds data.
    A property provides a controlled way to access or modify the value of a field, often using get and set accessors.

    Fields are typically private, and properties are used to encapsulate them, allowing validation, logging, or lazy loading.

    ✍️ Example:
    ```
    // Field
    private int _age;

    // Property
    public int Age
    {
        get { return _age; }
        set { _age = value; }
    }
    ```

9.	How do you make a method parameter optional?

    To make a method parameter optional in C#, you assign it a default value in the method signature.
    If the caller omits the parameter, the default value will be used.

    ✍️ Example:
    ```
    public void Greet(string name = "Guest")
    {
        Console.WriteLine($"Hello, {name}!");
    }

    Greet();        // Output: Hello, Guest!
    Greet("Alice"); // Output: Hello, Alice!
    ```

    name is an optional parameter with default value "Guest".

10.	What is an interface and how is it different from an abstract class?

    An interface in C# defines a contract that a class or struct must follow. It contains only declarations of methods, properties, events, or indexers — but no implementation.

    An abstract class can contain both abstract members (which must be implemented by derived classes) and concrete members (which already have implementation). Abstract classes can also have fields and constructors, while interfaces cannot.

    ✍️ Summary of Key Differences:
    Feature	        Interface	                        Abstract Class
    Implementation	No implementation	                Can have partial implementation
    Constructors	Not allowed	                        Allowed
    Fields	        Not allowed	                        Allowed
    Multiple Inheritance	A class can implement many	A class can inherit only one

11.	What accessibility level are members of an interface by default?

    By default, all members of an interface are public, and they must be public. You cannot assign a different access modifier to interface members.

    ✍️ Example:
    ```
    public interface IShape
    {
        void Draw(); // implicitly public
    }
    ```

    You don't write public inside the interface, but Draw() is still public by default and must be implemented as public.

12.	True/False: Polymorphism allows derived classes to provide different implementations of the same method.

    True. Polymorphism allows methods to do different things based on the object that it is acting upon, even if they share the same name.

13.	True/False: The override keyword is used to indicate that a method in a derived class is providing its own implementation.

   True. The override keyword is used in a derived class to provide a new implementation of a method that is declared as virtual or abstract in the base class.

14.	True/False: The new keyword is used to indicate that a method in a derived class is providing its own implementation.

   True. The new keyword is used in a derived class to hide a method in the base class with the same name, effectively providing a new implementation that does not override the base method.

15.	True/False: Abstract methods can be used in a normal (non-abstract) class.

    False. Abstract methods can only be declared in abstract classes and must be implemented by derived classes.

16.	True/False: Normal (non-abstract) methods can be used in an abstract class.

   True. An abstract class can include normal methods with implementations as well as abstract methods. It's one of the key features of abstract classes.

17.	True/False: Derived classes can override methods that were virtual in the base class.

    True. If a method is marked as virtual in the base class, it can be overridden in a derived class using the override keyword.

18.	True/False: Derived classes can override methods that were abstract in the base class.

    True. This is required behavior. Abstract methods have no implementation in the base class, so derived classes must override them and provide an implementation.

19.	True/False: Derived classes must override the abstract methods from the base class.

    True. Abstract methods have no implementation and must be overridden in derived classes. Otherwise, the derived class must also be marked as abstract.

20.	True/False: In a derived class, you can override a method that was neither virtual nor abstract in the base class.

    False. Only methods marked as virtual, abstract, or override in the base class can be overridden. Non-virtual methods cannot be overridden.

21.	True/False: A class that implements an interface does not have to provide an implementation for all of the members of the interface.

    False. A class must implement all members defined in an interface. Otherwise, the class must be declared as abstract.

22.	True/False: A class that implements an interface is allowed to have other members in addition to the interface members.

    True. A class can implement the required interface members and have additional fields, properties, methods, or events. Interfaces define only a contract, not a limitation.

23.	True/False: A class can inherit from more than one base class.

    False. C# does not support multiple inheritance for classes. A class can only inherit from one base class. However, a class can implement multiple interfaces.

24.	True/False: A class can implement more than one interface.

    True. C# allows a class to implement multiple interfaces, which is often used to achieve polymorphism and separation of concerns in design.

 * */

/*
Create 3 classes in Program.cs:
a. Person class
●	Create an abstract class Person with the following members:

○	An Id property (int).

○	A private field salary with a public property Salary that only accepts positive values; throw an exception if a negative value is assigned.

○	A DateOfBirth property (DateTime).

○	An Address property (List of strings).

*/
using System;
using System.Collections.Generic;

namespace HW02
{
    public abstract class Person
    {
        public int Id { get; set; }
        private decimal salary;
        public decimal Salary
        {
            get { return salary; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Salary must be a positive value.");
                }
                salary = value;
            }

        }
        public DateTime DateOfBirth { get; set; }
        public List<string> Address { get; set; }
    }
}


/*
________________________________________
b. Instructor class
●	Create a class Instructor that inherits from Person.

○	Add a DepartmentId property (int).

*/
namespace HW02
{
    public class Instructor : Person
    {
        public int DepartmentId { get; set; }
    }
}

/*
________________________________________
c. Student class
●	Create a class Student that inherits from Person.

○	Add a property SelectedCourses, which is a list of Course objects.
*/

namespace HW02
{
    public class Student : Person
    {
        public List<Course> SelectedCourses { get; set; }
    }
}