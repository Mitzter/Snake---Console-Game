﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects.Foods
{
    public class FoodHash : Food
    {
        private const int foodPoints = 3;
        private const char foodSymbol = '#';
        public FoodHash(Field field) : base(field, foodPoints, foodSymbol)
        {

        }
    }
}
