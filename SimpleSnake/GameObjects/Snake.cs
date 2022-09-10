using SimpleSnake.GameObjects.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects
{
    public class Snake
    {
        private const char snakeSymbol = '\u25CF';
        private const char emptySpace = ' ';

        private readonly Queue<Point> snakeElements;
        private readonly IList<Food> food;
        private Field field;

        private int nextLeftX;
        private int nextTopY;
        private int foodIndex;

        private Snake()
        {
            this.snakeElements = new Queue<Point>();
            this.food = new List<Food>();
            this.foodIndex = this.RandomFoodNumber;
        }
        public Snake(Field field) : this()
        {
            this.field = field;
           
            this.GetFoods();
            this.CreateSnake();

        }

        private int RandomFoodNumber => new Random().Next(0, this.food.Count);
        public bool CanMove(Point direction)
        {
            Point currentSnakeHead = this.snakeElements.Last();
            this.GetNextPoint(direction, currentSnakeHead);

            bool isNextPointOfSnake = this.snakeElements.Any(p => p.LeftX == this.nextLeftX && p.TopY == this.nextTopY);

            if (isNextPointOfSnake)
            {
                return false;
            }

            Point newHead = new Point(this.nextLeftX, this.nextTopY);
            if (this.field.IsPointOfWall(newHead))
            {
                return false;
            }

            this.snakeElements.Enqueue(newHead);
            newHead.Draw(snakeSymbol);

            if (this.food[foodIndex].IsFoodPoint(newHead))
            {
                this.Eat(direction, currentSnakeHead);
            }
            Point snakeTail = this.snakeElements.Dequeue();
            snakeTail.Draw(emptySpace);

            return true;
        }
        private void CreateSnake()
        {
            for (int topY = 2; topY <= 6; topY++)
            {
                Point newPoint = new Point(2, topY);
                this.snakeElements.Enqueue(newPoint);
                
            }
            this.foodIndex = this.RandomFoodNumber;
            this.food[this.foodIndex].SetRandomPosition(this.snakeElements);
        }

        private void GetFoods()
        {
            Type[] foodTypes = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Name.ToLower().StartsWith("food") && !t.IsAbstract)
                .ToArray();

            foreach (Type foodType in foodTypes)
            {
                Food food = (Food)Activator
                    .CreateInstance(foodType, new object[] { this.field });
                this.food.Add(food);
            }
        }

        private void GetNextPoint(Point direction, Point snakeHead)
        {
            this.nextLeftX = snakeHead.LeftX + direction.LeftX;
            this.nextTopY = snakeHead.TopY + direction.TopY;
        }


        private void Eat(Point direction, Point snakeHead)
        {
            int points = this.food[this.foodIndex].FoodPoints;

            for (int i = 0; i < points; i++)
            {
                Point newPoint = new Point(this.nextLeftX, this.nextTopY);
                this.snakeElements.Enqueue(newPoint);

                this.GetNextPoint(direction, snakeHead);
            }

            this.foodIndex = this.RandomFoodNumber;
            this.food[this.foodIndex].SetRandomPosition(this.snakeElements);

            
        }
    }
}
