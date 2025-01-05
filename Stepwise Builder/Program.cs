using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static System.Console;

namespace DesignPatterns
{
    public enum CarType
    {
        Crossover, Sedan
    }

    public class Car
    {
        public CarType Type;
        public int WheelSize;
        
        
    }

    public interface ISpecificationCarType
    {
        ISpecificationWheelSize OfType(CarType type);
    }

    public interface ISpecificationWheelSize
    {
        ICarBuilder WithWheelSize(int size);
    }

    public interface ICarBuilder
    {
       public Car Build();
    }

    public class CarBuilder
    {
        private class Impl: ISpecificationCarType, ISpecificationWheelSize, ICarBuilder
        {
            private Car car = new Car();
            public Car Build()
            {
                return car;
            }
            public ISpecificationWheelSize OfType(CarType type)
            {
                car.Type = type;
                return this;
            }
            public ICarBuilder WithWheelSize(int size)
            {
                switch(car.Type)
                {
                    case CarType.Crossover when size < 17 || size > 20:
                    case CarType.Sedan when size < 15 || size > 17:
                        throw new System.Exception($"Invalid wheel size for {car.Type}.");
                }
                car.WheelSize = size;
                return this;
            }
            
        }
         
        public static ISpecificationCarType Create()
        {
            return new Impl();
        }
    }

    public class Program
    {

        public static void Main(string[] args)
        {
            var car = CarBuilder.Create()       
                .OfType(CarType.Crossover)      // ISpecificationCarType
                .WithWheelSize(18)         // ISpecificationWheelSize
                .Build();                       // ICarBuilder
        }
    }
}