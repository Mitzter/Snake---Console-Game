using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects.Foods
{
    public class FoodDollar : Food
    {
        private const int foodPoints = 2;
        private const char foodSymbol = '$';
        public FoodDollar(Field field) : base(field, foodPoints, foodSymbol)
        {
        }
    }
}
