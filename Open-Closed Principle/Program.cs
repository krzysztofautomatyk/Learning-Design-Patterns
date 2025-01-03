
using System.Diagnostics;
using static System.Console;

namespace DesignPatterns
{
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Yuge
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if (name == null)
                throw new ArgumentNullException(paramName: nameof(name));

            Name = name;
            Color = color;
            Size = size;
        }
    }

    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var p in products)
                if (p.Size == size)
                    yield return p;
        }

        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
                if (p.Color == color)
                    yield return p;
        }

        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
        {
            foreach (var p in products)
                if (p.Size == size && p.Color == color)
                    yield return p;
        }

    }

    public interface ISpecification<T>
    {
        bool IsSatisfied(T item);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size _size;
        public SizeSpecification(Size size)
        {
            _size = size;
        }
        public bool IsSatisfied(Product p)
        {
            return p.Size == _size;
        }

    }

    public class ColorSpecification : ISpecification<Product>
    {
        private Color _color;
        public ColorSpecification(Color color)
        {
            _color = color;
        }
        public bool IsSatisfied(Product p)
        {
            return p.Color == _color;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> _first, _second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            if (first == null)
                throw new ArgumentNullException(paramName: nameof(first));
            if (second == null)
                throw new ArgumentNullException(paramName: nameof(second));
            _first = first;
            _second = second;
        }

        public bool IsSatisfied(T item)
        {
            return _first.IsSatisfied(item) && _second.IsSatisfied(item);
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
                if (spec.IsSatisfied(i))
                    yield return i;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };

            var productFilter = new ProductFilter();
            WriteLine("Green products (old):");
            foreach (var p in productFilter.FilterByColor(products, Color.Green))
                WriteLine($" - {p.Name} is green");

            var betterFilter = new BetterFilter();
            WriteLine("Green products (new):");
            foreach (var p in betterFilter.Filter(products, new ColorSpecification(Color.Green)))
                WriteLine($" - {p.Name} is green");

            WriteLine("Large blue items");
            foreach (var p in betterFilter.Filter(
                products,
                new AndSpecification<Product>(
                        new ColorSpecification(Color.Blue),
                        new SizeSpecification(Size.Large)
                    )
                ))
                WriteLine($" - {p.Name} is {p.Color}");
        }
    }
}