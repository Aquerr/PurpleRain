using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PurpleRain
{
    class RainDrop
    {
        private static readonly Random random = new Random();

        private readonly int height;
        private readonly int width;
        private readonly int zIndex;
        private readonly int fallSpeed;
        private readonly Rectangle rectangle;

        public RainDrop()
        {
            //Z-index is used for handling size and speed of raindrops
            this.zIndex = random.Next(0, 4);
            if(zIndex == 0 || zIndex == 1)
            {
                this.width = random.Next(2, 4);
                this.fallSpeed = random.Next(6, 11);
            }
            else
            {
                this.width = random.Next(1, 2);
                this.fallSpeed = random.Next(3, 6);
            }
            this.height = random.Next(15, 20);
            this.rectangle = new Rectangle();
            this.rectangle.Height = this.height;
            this.rectangle.Width = this.width;
            this.rectangle.Fill = Brushes.Purple;
        }

        public int GetHeight()
        {
            return this.height;
        }

        public int GetWidth()
        {
            return this.width;
        }

        public int GetFallSpeed()
        {
            return this.fallSpeed;
        }

        public Shape GetRectangle()
        {
            return this.rectangle;
        }
    }
}
