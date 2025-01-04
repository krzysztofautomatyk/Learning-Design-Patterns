using static System.Console;

namespace DesignPatterns
{
      
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
        }
    }
}