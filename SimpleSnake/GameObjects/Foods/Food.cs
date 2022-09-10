using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects.Foods
{
    public abstract  class Food : Point
    {
        private readonly Random random;
        
        private readonly Field field;
        protected Food(Field field, int foodPoints, char foodSymbol) : base(field.LeftX, field.TopY)
        {
            this.random = new Random();
            this.FoodSymbol = foodSymbol;
            this.FoodPoints = foodPoints;
            this.field = field;
        }

        public int FoodPoints { get; }
        public char FoodSymbol { get; }
        public void SetRandomPosition(Queue<Point> snake)
        {
            do
            {
                this.LeftX = random.Next(2, field.LeftX - 2);
                this.TopY = random.Next(2, field.TopY - 2);
            } while (snake.Any(p=> p.LeftX == this.LeftX && p.TopY == this.TopY));

            Console.BackgroundColor = ConsoleColor.Red;
            this.Draw(FoodSymbol);
            Console.BackgroundColor = ConsoleColor.White;
        }

        public bool IsFoodPoint(Point snakeHead)
        {
            return this.LeftX == snakeHead.LeftX && this.TopY == snakeHead.TopY;
        }
    }
}
