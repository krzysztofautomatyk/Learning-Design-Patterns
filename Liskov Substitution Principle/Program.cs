using static System.Console;

namespace DesignPatterns
{
    public class Rectangle
    {
        /*
         
        The virtual keyword allows the Width property to be overridden in a derived class. 

         */
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {

        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle
    {
        /*
         
        In C#, the new keyword in this context is used to hide 
        the Width property from the base class. This means 
        that the derived class introduces a new implementation
        of the Width property, which shadows the one inherited
        from the base class.

        */
        //public new int Width
        //{
        //    set { base.Width = base.Height = value; }
        //}
        //public new int Width
        //{
        //    set
        //    {
        //        base.Width = value;
        //        base.Height = value;
        //    }
        //}

        public override int Width
        {
            set { base.Width = base.Height = value; }
        }

        /*
        
        The override keyword means this property is replacing
        or modifying a virtual or abstract property from the base class.

        */
        public override int Height
        {
            set { base.Height = base.Width = value; }
        }
    }

    public class Program
    {
        static public int Area(Rectangle r) => r.Width * r.Height;
        //public static int Area(Rectangle r)
        //{
        //    int width = r.Width;
        //    int height = r.Height;
        //    int area = width * height;
        //    return area;
        //}
         
        public static void Main(string[] args)
        {
            Rectangle ractangle = new Rectangle(2, 3);
            WriteLine($"{ractangle} has area {Area(ractangle)}");

            Rectangle square = new Square();
            square.Width = 4;
            WriteLine($"{square} has area {Area(square)}");
        }
    }
}