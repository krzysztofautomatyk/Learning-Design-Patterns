using System.Text;
using static System.Console;

namespace DesignPatterns
{
    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int indentSize = 2;

        public HtmlElement()
        {

        }

        public HtmlElement(string name, string text)
        {
            /*
            nameof in C# returns the name of a variable, parameter, property,
            or method as a string. It helps avoid hardcoding strings, reducing
            errors and improving maintainability. If the referenced item is renamed,
            nameof automatically reflects the change, preventing issues during refactoring.
            */
            //if (name == null)
            //{ throw new ArgumentNullException(paramName: nameof(name)); }
            //if (text == null)
            //{ throw new ArgumentNullException(paramName: nameof(text)); }

            Name = name ?? throw new ArgumentNullException(paramName: nameof(name)); ;
            Text = text ?? throw new ArgumentNullException(paramName: nameof(text)); ;

        }


        private string ToStringImpl(int indent)
        {
            var stringBuilder = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            stringBuilder.Append($"{i}<{Name}>\n");

            if (!string.IsNullOrWhiteSpace(Text))
            {
                stringBuilder.Append(new string(' ', indentSize * (indent + 1)));
                stringBuilder.Append(Text);
                stringBuilder.Append("\n");
            }

            foreach (var element in Elements)
            {
                stringBuilder.Append(element.ToStringImpl(indent + 1));
            }

            stringBuilder.Append($"{i}</{Name}>\n");
            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }

    }

    public class HtmlBuilder
    {
        private readonly string _rootName;
        HtmlElement root = new HtmlElement();

        public HtmlBuilder(string rootName)
        {
            _rootName = rootName;
            root.Name = rootName;
        }

        public HtmlBuilder AddChild(string childName, string childText)
        {
            var element = new HtmlElement(childName, childText);
            root.Elements.Add(element);
            /*
            
            Fluent Interface
            
            In C#, the return this; statement in the AddChild method means
            that the method returns a reference to the current object (this). 
            This allows for method chaining, a technique where multiple methods
            are called in a single line, one after the other.

            */
            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            root = new HtmlElement { Name = _rootName };
        }
    }


    public class Program
    {

        public static void Main(string[] args)
        {
            var hello = "Hello";
            var sb = new System.Text.StringBuilder();
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");
            WriteLine(sb);

            var words = new[] { "Hello", "world" };
            sb.Clear();
            sb.Append("<ul>");
            foreach (var word in words)
            {
                sb.Append($"<li>{word}</li>");
            }
            sb.Append("</ul>");
            WriteLine(sb);

            /*
             
            A Fluent Interface is a design pattern used to create more readable 
            and expressive code by chaining method calls together in a way 
            that resembles natural language. This approach is often used to 
            configure objects, build queries, or construct complex structures step by step.

            */
            var builder = new HtmlBuilder("ul")
                    .AddChild("li", "hello")
                    .AddChild("li", "world");
            WriteLine(builder.ToString());

        }
    }
}