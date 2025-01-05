using System.Security.Cryptography.X509Certificates;
using System.Text;
using static System.Console;

namespace DesignPatterns
{
    public class Person
    {
        public string Name;
        public string Position;

        public class Builder : PersonJobBuilder<Builder>
        {
        }

        public static Builder New => new Builder();

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    /*
      
    The abstract keyword in C# is used to define a class or a method 
    that is incomplete and must be implemented by derived (inheriting) classes.

    */
    public abstract class PersonBuilder
    {
        /*

        The protected keyword in C# is one of the access modifiers 
        that restricts the visibility of a class member (e.g., field, property, method)
        to the class itself and any derived (inheriting) classes.

        */
        protected Person person = new Person();
        public Person Build()
        {
            return person;
        }
    }

    /*
     
    <SELF> acts as a placeholder for the specific type (e.g., EmployeeBuilder)
    provided when inheriting.
    This ensures derived classes return their own type instead of the base class type, 
    which is crucial for fluent chaining.

    
    The where keyword in C# is used to apply constraints to a generic type parameter. 
    It ensures that the type provided for the generic parameter meets certain requirements. 
    In your example, the where clause ensures that the SELF type used with 
    the PersonInforBuilder class must inherit from PersonInforBuilder<SELF>.
    
    */
    public class PersonInforBuilder<SELF>
        :PersonBuilder
        where SELF : PersonInforBuilder<SELF>
    {   
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF)this;
        }
    }

    public class PersonJobBuilder<SELF>
        : PersonInforBuilder<PersonJobBuilder<SELF>>
        where SELF : PersonJobBuilder<SELF>
    {
        public SELF WorksAsA(string position)
        {
            person.Position = position;
            return (SELF)this;
        }
    }
     

    public class Program
    {

        public static void Main(string[] args)
        { 
            var me = new Person.Builder()
                .Called("Krzysztof")
                .WorksAsA("Programmer")
                .Build();

            WriteLine(me);
        }
    }
}