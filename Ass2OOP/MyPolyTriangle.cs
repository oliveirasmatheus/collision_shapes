using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ass2OOP
{
    public class MyPolyTriangle : Shape
    {
        private const int triangleSize = 60;
        private int x;
        private int y;
        private int topX;
        private int topY;
        private int leftX;
        private int leftY;
        private int rightX;
        private int rightY;
        public List<Shape> shapeList;
        private int regularSpeed = 2;
        private int increasedSpeed = 6;
        public Rectangle triangleBox; // Rectangle surrounding the triangle

        public override int xVelocity { get; set; }
        public override int yVelocity { get; set; }

        // Constructor for the MyPolyTriangle class, initializing its properties
        public MyPolyTriangle(int x, int y, List<Shape> shapes)
        {
            this.x = x;
            this.y = y;
            topX = x;
            topY = y;
            leftX = x - triangleSize / 2;
            leftY = y + triangleSize;
            rightX = x + triangleSize / 2;
            rightY = y + triangleSize;
            shapeList = shapes;
            Random random = new Random();

            //set the x and y velocity
            while (xVelocity > -3 && xVelocity < 3)
                xVelocity = random.Next(-15, 15);

            while (yVelocity > -3 && yVelocity < 3)
                yVelocity = random.Next(-15, 15);

            // Update triangleBox based on the GetBoundingBox method
            triangleBox = GetBoundingBox();
        }

        public override void Draw(PaintEventArgs e, Form form)
        {
            MoveTriangle(form.ClientSize.Width, form.ClientSize.Height);
            e.Graphics.FillPolygon(Brushes.Aquamarine, new Point[]
            {
                new Point(topX, topY),
                new Point(leftX, leftY),
                new Point(rightX, rightY)
            });
            // Update triangleBox whenever you draw the triangle after position change
            triangleBox = GetBoundingBox();

            CheckCollision();
        }
        private void MoveTriangle(int formWidth, int formHeight)
        {
            // Calculate the next position based on current position and velocities
            int nextX = x + xVelocity;
            int nextY = y + yVelocity;

            // Check if the next position hits the horizontal walls (left and right)
            if (nextX - triangleSize / 2 <= 0 || nextX + triangleSize / 2 >= formWidth) // hits the x wall
            {
                xVelocity = -xVelocity; // Reverse direction

                // Adjust speed if not already at increased speed
                if (Math.Abs(xVelocity) != increasedSpeed)
                {
                    xVelocity = (xVelocity < 0) ? -increasedSpeed : increasedSpeed;
                }
                else
                {
                    xVelocity = (xVelocity < 0) ? -regularSpeed : regularSpeed;
                }
            }

            // Check if the next position hits the vertical walls (top and bottom)
            if (nextY <= 0 || nextY + triangleSize >= formHeight)
            {
                yVelocity = -yVelocity; // Reverse direction

                // Adjust speed if not already at increased speed
                if (Math.Abs(yVelocity) != increasedSpeed)
                {
                    yVelocity = (yVelocity < 0) ? -increasedSpeed : increasedSpeed;
                }
                else
                {
                    yVelocity = (yVelocity < 0) ? -regularSpeed : regularSpeed;
                }
            }

            // Update the current position based on the velocities
            x += xVelocity;
            y += yVelocity;

            // Update the coordinates of the triangle vertices based on the new position
            topX = x;
            topY = y;
            leftX = x - triangleSize / 2;
            leftY = y + triangleSize;
            rightX = x + triangleSize / 2;
            rightY = y + triangleSize;

        }

        public override void CheckCollision()
        {
            // Iterate through the list of shapes to check for collisions
            for (int i = 0; i < shapeList.Count; i++)
            {
                // Check collision with MyCircle objects
                if (shapeList[i] is MyCircle && shapeList[i] != this)
                {
                    MyCircle circle = (MyCircle)shapeList[i];

                    if (this.triangleBox.IntersectsWith(circle.circleBox))
                    {
                        ChangeVelocities(circle);
                    }
                }
                // Check collision with MyDogImage objects
                else if (shapeList[i] is MyDogImage && shapeList[i] != this)
                {
                    MyDogImage dog = (MyDogImage)shapeList[i];

                    if (this.triangleBox.IntersectsWith(dog.dogBox.Bounds))
                    {
                        ChangeVelocities(dog);
                    }
                }
                // Check collision with MyPolyTriangle objects
                else if (shapeList[i] is MyPolyTriangle && shapeList[i] != this)
                {
                    MyPolyTriangle triangle = (MyPolyTriangle)shapeList[i];

                    if (this.triangleBox.IntersectsWith(triangle.triangleBox))
                    {
                        ChangeVelocities(triangle);
                    }
                }
                // Check collision with MyIrregularPolygon objects
                else if (shapeList[i] is MyIrregularPolygon && shapeList[i] != this)
                {
                    MyIrregularPolygon polygon = (MyIrregularPolygon)shapeList[i];

                    if (this.triangleBox.IntersectsWith(polygon.polygonBox))
                    {
                        ChangeVelocities(polygon);
                    }
                }
            }
        }

        public Rectangle GetBoundingBox()
        {
            // Calculate the minimum and maximum coordinates of the triangle vertices
            int minX = Math.Min(topX, Math.Min(leftX, rightX));
            int minY = Math.Min(topY, Math.Min(leftY, rightY));
            int maxX = Math.Max(topX, Math.Max(leftX, rightX));
            int maxY = Math.Max(topY, Math.Max(leftY, rightY));

            // Calculate the width and height of the bounding box
            int width = maxX - minX;
            int height = maxY - minY;

            // Create and return a rectangle representing the bounding box of the triangle
            return new Rectangle(minX, minY, width, height);
        }

    }
}
