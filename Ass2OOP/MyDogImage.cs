using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ass2OOP
{
    public class MyDogImage : Shape
    {
        private int x;
        private int y;
        private Image dogImage;
        private int imageSize = 80;
        private int collisionSize = 120;
        private bool isReduced = false;
        public List<Shape> shapeList;
        public PictureBox dogBox;

        public override int xVelocity { get; set; }
        public override int yVelocity { get; set; }

        public MyDogImage(int x, int y, List<Shape> shapes)
        {
            this.x = x;
            this.y = y;
            shapeList = shapes;

            Image originalImage = Image.FromFile("images/toia.png");
            dogImage = ResizeImage(originalImage, imageSize);

            Random random = new Random();

            //set the x and y velocity
            while (xVelocity > -3 && xVelocity < 3)
                xVelocity = random.Next(-15, 15);

            while (yVelocity > -3 && yVelocity < 3)
                yVelocity = random.Next(-15, 15);


            // Initialize the dogBox
            dogBox = new PictureBox();
            dogBox.Image = dogImage;
            dogBox.Width = imageSize;
            dogBox.Height = imageSize;
        }

        public override void Draw(PaintEventArgs e, Form form)
        {
            x += xVelocity;
            y += yVelocity;

            if (x <= 0 || x >= form.ClientSize.Width - dogImage.Width)
            {
                xVelocity *= -1;
                if (!isReduced)
                {
                    dogImage = ResizeImage(dogImage, collisionSize);
                    isReduced = true;
                }
                else
                {
                    dogImage = ResizeImage(dogImage, imageSize);
                    isReduced = false;
                }
            }

            if (y <= 0 || y >= form.ClientSize.Height - dogImage.Height)
            {
                yVelocity *= -1;
                if (!isReduced)
                {
                    dogImage = ResizeImage(dogImage, collisionSize);
                    isReduced = true;
                }
                else
                {
                    dogImage = ResizeImage(dogImage, imageSize);
                    isReduced = false;
                }
            }

            // Adjust the position if the image exceeds the form boundaries
            if (x < 0)
            {
                x = 0;
            }
            else if (x > form.ClientSize.Width - dogImage.Width)
            {
                x = form.ClientSize.Width - dogImage.Width;
            }

            if (y < 0)
            {
                y = 0;
            }
            else if (y > form.ClientSize.Height - dogImage.Height)
            {
                y = form.ClientSize.Height - dogImage.Height;
            }

            PointF ulCorner = new PointF(x, y);
            e.Graphics.DrawImage(dogImage, ulCorner);

            // Update the dogBox with the new position and size
            dogBox.Location = new Point(x, y);

            dogBox.Width = dogImage.Width;
            dogBox.Height = dogImage.Height;

            CheckCollision();
        }

        private Image ResizeImage(Image image, int size)
        {
            Bitmap newImage = new Bitmap(size, size);

            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, new Rectangle(0, 0, size, size));
            }

            return newImage;
        }

        public override void CheckCollision()
        {
            for (int i = 0; i < shapeList.Count; i++)
            {
                if (shapeList[i] is MyCircle && shapeList[i] != this)
                {
                    MyCircle circle = (MyCircle)shapeList[i];

                    if (this.dogBox.Bounds.IntersectsWith(circle.circleBox))
                    {
                        ChangeVelocities(circle);
                    }
                }
                else if (shapeList[i] is MyDogImage && shapeList[i] != this)
                {
                    MyDogImage dog = (MyDogImage)shapeList[i];

                    if (this.dogBox.Bounds.IntersectsWith(dog.dogBox.Bounds))
                    {
                        ChangeVelocities(dog);
                    }
                }
                else if (shapeList[i] is MyPolyTriangle && shapeList[i] != this)
                {
                    MyPolyTriangle triangle = (MyPolyTriangle)shapeList[i];

                    if (this.dogBox.Bounds.IntersectsWith(triangle.triangleBox))
                    {
                        ChangeVelocities(triangle);
                    }
                }
                else if (shapeList[i] is MyIrregularPolygon && shapeList[i] != this)
                {
                    MyIrregularPolygon polygon = (MyIrregularPolygon)shapeList[i];

                    if (this.dogBox.Bounds.IntersectsWith(polygon.polygonBox))
                    {
                        ChangeVelocities(polygon);
                    }
                }
            }
        }
    }
}