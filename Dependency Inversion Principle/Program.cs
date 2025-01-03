using static System.Console;

namespace DesignPatterns
{
    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    public class Person
    {
        public string Name;
        //public int DateOfBirth;
    }

    // low-level module
    public class Relationships : IRelationshipBrowser
    {
        private List<(Person, Relationship, Person)> relations
          = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        // public List<(Person, Relationship,Person)> Relation => relations;
        //public List<(Person, Relationship, Person)> Relation
        //{
        //    get
        //    {
        //        return relations;
        //    }
        //}
        //public List<(Person, Relationship, Person)> Relation
        //{
        //    get => relations;
        //}

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            //foreach (var rel in relations.Where(
            //    x => x.Item1.Name == name &&
            //         x.Item2 == Relationship.Parent
            //     ))
            //{
            //    yield return rel.Item3;
            //}
            
            return relations
              .Where(x => x.Item1.Name == name &&
                          x.Item2 == Relationship.Parent)
              .Select(r => r.Item3);
        }
    }



    public class Program
    {
        //public Program(Relationships relationship)
        //{
        //    var relations = relationship.Relation;
        //    foreach (var rel in relations.Where(
        //        x => x.Item1.Name == "John" &&
        //             x.Item2 == Relationship.Parent
        //             ))
        //    {
        //        WriteLine($"John has a child called {rel.Item3.Name}");
        //    }

        //}

        public Program(IRelationshipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOf("John"))
            {
                WriteLine($"John has a child called {p.Name}");
            }
        }


        public static void Main(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Matt" };

            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new Program(relationships);
            
        }

    }
}