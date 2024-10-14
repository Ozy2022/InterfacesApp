namespace InterfacesApp
{

    /*What is an Interface?
         An interface in C# is like a blueprint
         that defines methods and
         properties a class must have, but it
         doesn't provide the actual code for
         them. It's used to ensure different
         classes follow the same rules.

    - Why use an Interface?
 
        1. Abstraction:
                Defines what methods a class should
                implement without specifying how.
                Interfaces tell classes what they need
                to do, but not how to do it.

        2. Polymorphism:
                Allows different classes to be treated
                as instances of the interface type.
                Interfaces let different classes be
                used in the same way.

        3. Decoupling:
                Reduces dependencies between
                classes, making the code more
                modular and easier to maintain.
                Interfaces help keep classes separate
                so the code is easier to manage and update.
             
        4. Reusability:
                Ensures that different classes can
                use common methods, enhancing
                code reusability.
                Interfaces make it easy to reuse code
                across different classes.

        5. Testability:
                Facilitates unit testing by allowing
                mock implementations of interfaces.
                Interfaces make testing easier by
                allowing fake versions of classes for
                testing purposes.

        ===============================================================
        Why Polymorphic Interfaces?
 
        1. Where:
             When you need different classes to
             implement the same set of methods
             or properties.
             This ensures consistency and allows
             for flexible implementations.

        2. Why:
             Promotes code reusability and
             flexibility.
             Different classes can implement the
             same interface in various ways,
             allowing for diverse behavior while
             maintaining a common contract.

        3. When:
             Use interfaces when you have multiple
             classes that should provide the same
             functionality but might implement it
             differently.
             Real-World Example: Payment
             Processing System
        ===============================================================
        Interfaces: Decoupling and testability are useful?
            - Where is it useful?
                 In large applications where components
                 need to interact with each other in a
                 loosely coupled manner.
            
            - Why is it useful?    
                  Interfaces help decouple code by reducing
                  dependencies between classes. This
                  makes the code easier to test and
                  maintain.

            - When is it useful?
                 Use interfaces when you want to decouple
                 the implementation details from the
                 interface contract, especially useful in
                 large and complex systems.

        ==================================================================
        #Ex6:
        Dependency Injection (DI)
             - What is Dependency Injection?
                     Dependency injection is a programming
                     technique that makes a class independent
                     of its dependencies.
                     This helps you to follow SOLID's
                     dependency inversion and single
                     responsibility principles.

        Types of D.I.
            1. Constructor Injection:
                    Dependencies are provided through a class
                    constructor, ensuring that the class
                    receives all its dependencies at the time of
                    instantiation.

            EX:
                public class MyClass {
                    private readonly IDependency _dependency;
                    public MyClass (IDependency dependency) {
                        _dependency = d dependency;
                    }
                }

        ------------------------

            2. Setter Injection:
                    Dependencies are assigned to public setter
                    methods, allowing for the injection of
                    dependencies after the object is created.

            EX:
                public class MyClass {
                    public IDependency Dependency { private get; set; }
                }
        
        ------------------------
            
            3. Interface Injection:
                    Dependencies are provided through an
                    interface, requiring the class to implement
                    an interface that exposes a method for
                    injecting the dependency.
        
            EX:
                public interface IDependencyInjector
                    void SetDependency (IDependency dependency);
                }
                public class MyClass : IDependencyInjector
                    private IDependency _dependency;
                    public void SetDependency (IDependency dependency) {
                        _dependency = d dependency;
                }

     */

    //Ex1 -> how interfaces works.
    //First interface
    public interface IAnimal
    {

        void MakeSound();
        void Eat(string food);


    }

    public class Dog : IAnimal
    {
        public void Eat(string food)
        {
            Console.WriteLine($"Dog ate {food}");
        }

        public void MakeSound()
        {
            Console.WriteLine("Bark");
        }
    }

    public class Cat : IAnimal
    {
        public void Eat(string food)
        {
            Console.WriteLine($"Cat ate {food}");
        }

        public void MakeSound()
        {
            Console.WriteLine("Mewoo");
        }
    }

    //===============================================
    //Ex3 -> Interface Reusability while using Polymorphism
    //Second interface

    public interface IPaymentProcesser
    {
        void ProcessPayment(decimal amount);
    }

    //Start point of the Program
    internal class Program
    {

        //Ex2 -> Polymorphism

        public class Animal
        {
            public virtual void MakeSound()
            {
                Console.WriteLine("Some generic animal sound");
            }
        }

        public class Dog2 : Animal
        {
            public override void MakeSound()
            {
                Console.WriteLine("Bark");
            }
        }

        public class Cat2 : Animal
        {
            public override void MakeSound()
            {
                Console.WriteLine("Meow");
            }
        }

        //=======================================
        //Ex3
        public class CreditCardProcess : IPaymentProcesser
        {
            public void ProcessPayment(decimal amount)
            {
                Console.WriteLine($"Processing credit card payment of {amount}");
                // Implement credit card payment logic ...
            }
        }

        public class PaypalProcessor : IPaymentProcesser
        {
            public void ProcessPayment(decimal amount)
            {
                Console.WriteLine($"Processing Paypal payment of {amount}");
                // Implement Paypal payment logic ...
            }
        }

        public class PaymentService
        {
            //set the inteface to read only in this class
            private readonly IPaymentProcesser _processor;
            public PaymentService(IPaymentProcesser processor)
            {
                _processor = processor;
            }
            public void ProcessOrderPayment(decimal amount)
            {
                _processor.ProcessPayment(amount);
            }
        }
        //================================================
        //Ex5
        /*Decoupling: The Application class depends on the
        ILogger interface rather than specific implementations
        like FileLogger or DatabaseLogger.
        This means you can easily switch the logging mechanism
        without changing the Application class.
        */

        public interface ILogger
        {
            void Log(string message);
        }

        //1_note that after we made the base class and would inhernt
        //from the interface it must implement the interface on it 
        public class FileLogger : ILogger
        {
            //1_note
            public void Log(string message)
            {
                string directoryPath = @"C:\Logs";
                string filePath = Path.Combine(directoryPath, "log.txt");


                //if directoryPath is not Exist?
                //then create directory
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                File.AppendAllText(filePath, message + "\n");
            }
        }

        public class DatabaseLogger : ILogger
        {
            public void Log(string message)
            {
                //Implement the logic to log a message to a database
                Console.WriteLine($"Logging to database. {message}");
            }

        }

        public class Application
        {

            private readonly ILogger _logger;

            public Application(ILogger logger)
            {
                _logger = logger;
            }

            public void DoWork()
            {
                _logger.Log("Work Started!");

                //Do all the work!

                _logger.Log("WORK IS DONE, GOOD JOB.");
            }
        }

        //===============================================
        //Ex6, 7 Understanding Dependency in Dependency Injection

        /*The dependency is that the builder depends on the hammer and 
         * the saw.
         * So this is where now the saw and the hammer are dependencies 
         * of the builder.
         */

        //#Ex6 is wothout Injection
        /*public class Hammer
        {
            public void Use()
            {
                Console.WriteLine("Hammering Nails!");
            }
        }

        public class Builder
        {
            //so would not be prop
            private Hammer _hammer;
            private Saw _saw;
            public Builder()
            {
                //Bulider is responsible for creating its dependencies
                _hammer = new Hammer();
                _saw = new Saw();
            }

            public void BuildHouse()
            {
                _hammer.Use();
                _saw.Use();
                Console.WriteLine("House built");
            }
        }

        public class Saw
        {
            public void Use()
            {
                Console.WriteLine("Saing wood");
            }
        }*/

        //===============================================
        //#Ex7 -> Constructor Dependency Injection type
        /*public class Hammer
        {
            public void Use()
            {
                Console.WriteLine("Hammering Nails!");
            }
        }

        public class Builder
        {
            //so would not be prop
            private Hammer _hammer;
            private Saw _saw;

            //This is a Constructor Dependency Injection (DI)
            public Builder(Hammer hammer, Saw saw)
            {
                
                _hammer = hammer;
                _saw = saw;
            }

            public void BuildHouse()
            {
                _hammer.Use();
                _saw.Use();
                Console.WriteLine("House built");
            }
        }

        public class Saw
        {
            public void Use()
            {
                Console.WriteLine("Sawing wood");
            }
        }*/


        //================================================
        //#Ex8 ->  Setter Dependency Injection Type
        /*public class Hammer
        {
            public void Use()
            {
                Console.WriteLine("Hammering Nails!");
            }
        }

        public class Builder
        {

            public Hammer Hammer { get; set; }
            public Saw Saw { get; set; }

            //This is a Setter DI
           

            public void BuildHouse()
            {
                Hammer.Use();
                Saw.Use();
                Console.WriteLine("House built");
            }
        }

        public class Saw
        {
            public void Use()
            {
                Console.WriteLine("Sawing wood");
            }
        }*/
        //=======================================================
        //Ex9 -> Interface Dependency Injection and Comparison
        public interface IToolUser
        {
            void SetHammer(Hammer hammer);
            void SetSaw(Saw saw);
        }

        public class Hammer
        {
            public void Use()
            {
                Console.WriteLine("Hammering Nails!");
            }
        }

        public class Builder : IToolUser
        {

            private Hammer _hammer;
            private Saw _saw;

            //This is a Setter DI


            public void BuildHouse()
            {
                _hammer.Use();
                _saw.Use();
                Console.WriteLine("House built");
            }

            public void SetHammer(Hammer hammer)
            {
                _hammer = hammer;
            }

            public void SetSaw(Saw saw)
            {
                _saw = saw;
            }
        }

        public class Saw
        {
            public void Use()
            {
                Console.WriteLine("Sawing wood");
            }
        }
        //=======================================================
        //#Ex10 -> Mutliple Inheritance interfaces in csharp
        public interface IPrintable
        {
            void Print();
        }

        public interface IScannable
        {
            void Scan();
        }

        public class MultiFunctionPrinter : IPrintable, IScannable
        {
            public void Print()
            {
                Console.WriteLine("Printing document");
            }

            public void Scan()
            {
                Console.WriteLine("Scanning document");
            }

        }
        //==================================
        static void Main(string[] args)
        {
            //Ex1 interfaces

            /*Dog dog = new Dog();
            dog.MakeSound();
            dog.Eat("Treat");

            Cat cat = new Cat();
            cat.MakeSound();
            cat.Eat("Tona");*/

            //==================================
            //Ex2 Polymorphism

            //here we used 2 objects becasue
            //Dog2 is inhertence from Animal
            //so it has the same capabilities that Animal does 
            /*Animal myDog = new Dog2();
            myDog.MakeSound();*/

            //====================================
            //Ex3 -> Interface Reusability while using Polymorphism

            /*IPaymentProcesser creditCardProcessor = new CreditCardProcess();
            PaymentService paymentService = new PaymentService(creditCardProcessor);

            paymentService.ProcessOrderPayment(100.00m);

            IPaymentProcesser paypalProcesser = new PaypalProcessor();
            PaymentService paymentService1 = new PaymentService(paypalProcesser);

            paymentService1.ProcessOrderPayment(150.00m);*/

            //==================================================================
            //#Ex4 -> Storing a Log Text File on your PC with your own text
            //Creating a Folder on Your PC and Logging text there
            //The @ sign in C# is used to denote a verbatim string literal

            /*string directoryPath = @"C:\Logs";
            string filePath = Path.Combine(directoryPath, "log.txt");
            string message = "This is a log entry";

            //if directoryPath is not Exist?
            //then create directory
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.AppendAllText(filePath, message + "\n");*/


            //================================================================
            //#Ex5 -> Decoupling Code and Enhancing Testability
            //in this example it gives how you can test your
            //Application with differnts logging implementation
            //Or Mock Objects
            /* ILogger fileLogger = new FileLogger();
             Application app = new Application(fileLogger);
             app.DoWork();

             ILogger dbLogger = new DatabaseLogger();
             app = new Application(dbLogger);
             app.DoWork();*/

            //=================================================================
            //#Ex6 and 7 -> Understanding Dependency in Dependency Injection
            //Constructor Dependency Injection (DI) 
            /*Hammer hammer = new Hammer();
            Saw saw = new Saw();   
            Builder builder = new Builder(hammer, saw);

            builder.BuildHouse();*/

            //===================
            //#Ex8 Setter DI
            /*Hammer hammer = new Hammer();
            Saw saw = new Saw();
            Builder builder = new Builder();

            //Injct dependencies via Setters 
            builder.Hammer = hammer;
            builder.Saw = saw;

            builder.BuildHouse();*/
            //==============================
            //Ex9 Interface Dependency Injection and Comparison
            
            /*Hammer hammer = new Hammer();
            Saw saw = new Saw();
            Builder builder = new Builder();

            builder.SetHammer(hammer);
            builder.SetSaw(saw);

            builder.BuildHouse();*/


            //=================================
            //#Ex10 -> Mutliple Inheritance interfaces in csharp
            MultiFunctionPrinter printer = new MultiFunctionPrinter();
            printer.Print();
            printer.Scan();

            Console.ReadKey();

        }

    }

    
}
