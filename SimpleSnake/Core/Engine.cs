﻿using SimpleSnake.Enums;
using SimpleSnake.GameObjects;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSnake.Core
{
    public class Engine : IEngine
    {

        private readonly Point[] pointsOfDirection;
        private readonly Snake snake;
        private readonly Field field;

        private Direction direction;

        private double sleepTime;

        private Engine()
        {
            this.sleepTime = 100;
            this.pointsOfDirection = new Point[4];
        }
        public Engine(Field field, Snake snake) : this()
        {
            this.field = field;
            this.snake = snake;
        }
        public void Run()
        {
            this.InitializeDirections();
            
            
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    this.GetNextDirection();
                }
                bool canMove = this.snake.CanMove(this.pointsOfDirection[(int)this.direction]);
                if (!canMove)
                {
                    this.AskUserForRestart();
                }
                this.sleepTime -= 0.01;
                Thread.Sleep((int)this.sleepTime);
            }


            


        }

        private void AskUserForRestart()
        {
            int leftX = this.field.LeftX + 1;
            int topY = 3;

            Console.SetCursorPosition(leftX, topY);

            Console.Write("Would you like to continue? y/n");
            string input = Console.ReadLine();
            if (input == "y")
            {
                Console.Clear();
                StartUp.Main();
            }
            else
            {
                this.StopGame();
            }
        }

        private void StopGame()
        {
            Console.SetCursorPosition(20, 10);
            Console.Write("Game over!");
            Environment.Exit(0);
        }
        private void InitializeDirections()
        {
            this.pointsOfDirection[0] = new Point(1, 0);
            this.pointsOfDirection[1] = new Point(-1, 0);
            this.pointsOfDirection[2] = new Point(0, 1);
            this.pointsOfDirection[3] = new Point(0, -1);
        }

        private void GetNextDirection()
        {
            ConsoleKeyInfo userInput = Console.ReadKey();

            if (userInput.Key == ConsoleKey.LeftArrow)
            {
                if (this.direction != Direction.Right )
                {
                    this.direction = Direction.Left;
                }
            }
            else if (userInput.Key == ConsoleKey.RightArrow)
            {
                if (this.direction != Direction.Left)
                {
                    this.direction = Direction.Right;
                }
            }
            else if (userInput.Key == ConsoleKey.UpArrow)
            {
                if (this.direction != Direction.Down)
                {
                    this.direction = Direction.Up;
                }
            }
            else if (userInput.Key == ConsoleKey.DownArrow)
            {
                if (this.direction != Direction.Up)
                {
                    this.direction = Direction.Down;
                }
            }

            Console.CursorVisible = false;
        }
    }
}
