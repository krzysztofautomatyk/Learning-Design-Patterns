using static System.Console;

namespace DesignPatterns
{
    public class Document
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    public interface IFax
    {
        void Fax(Document d);
    }


    public interface IMachine : IPrinter, IScanner, IFax
    {
         
    }


    public class OldPrinter : IPrinter
    {
        public void Print(Document d)
        {
            WriteLine("Printing");
        }
    }

    public class Printer : IPrinter, IScanner
    {
        public void Print(Document d)
        {
            WriteLine("Printing");
        }
        
        public void Scan(Document d)
        {
            WriteLine("Scanning");
        }
    }

    public class MultiFunctionPrinter : IMachine
    {
        public void Print(Document d)
        {
            WriteLine("Printing");
        }
        public void Fax(Document d)
        {
            WriteLine("Faxing");
        }
        public void Scan(Document d)
        {
            WriteLine("Scanning");
        }
    }

    /*
     
    The CloneMuliFunctionPrinter class demonstrates composition
    by combining multiple devices (printer and scanner) into 
    one multi-functional machine. Instead of implementing print 
    and scan functionality directly, it delegates these tasks 
    to objects passed via the constructor.

    Key Points:
        Constructor: Accepts IPrinter and IScanner objects. 
        If either is null, an exception (ArgumentNullException)
        is thrown to ensure valid inputs.

    Delegation:
        Print and Scan methods call the corresponding methods
        on the provided objects (printer.Print(d), scanner.Scan(d)).
        
        The Fax method is implemented directly.

    */

    public class CloneMuliFunctionPrinter : IMachine
    {
        private IPrinter printer;
        private IScanner scanner;
        public CloneMuliFunctionPrinter(IPrinter printer, IScanner scanner)
        {
            if(printer == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(printer));
            }
            if (scanner == null)
            {
                throw new System.ArgumentNullException(paramName: nameof(scanner));
            }

            this.printer = printer;
            this.scanner = scanner;
        }
        public void Print(Document d)
        {
            printer.Print(d);
        }
        public void Scan(Document d)
        {
            scanner.Scan(d);
        }
        public void Fax(Document d)
        {
            WriteLine("Faxing");
        }
    }



    public class Program
    {
         
        public static void Main(string[] args)
        {
             
        }
    }
}