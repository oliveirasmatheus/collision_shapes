using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Ass2OOP
{
    public class MyCircle : Shape
    {
        private int centerX;
        private int centerY;
        private int radius;
        public List<Shape> shapeList;
        private bool hasGradient = false;
        public Rectangle circleBox;

        public override int xVelocity { get; set; }
        public override int yVelocity { get; set; }

        // circle
        public MyCircle(int x, int y, int r, List<Shape> shapes)
        {
            centerX = x;
            centerY = y;
            radius = r;
            shapeList = shapes;
            Random random = new Random();

            //set the x and y velocity
            while (xVelocity > -3 && xVelocity < 3)
                xVelocity = random.Next(-15, 15);

            while (yVelocity > -3 && yVelocity < 3)
                yVelocity = random.Next(-15, 15);

            // Calculate the bounding box for the circle
            circleBox = new Rectangle(centerX - radius, centerY - radius, 2 * radius, 2 * radius);

        }

        public override void Draw(PaintEventArgs e, Form form)
        {
            using (Bitmap bmp = new Bitmap(form.ClientSize.Width, form.ClientSize.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // Update circle's position and properties...
                    centerX += xVelocity;
                    centerY += yVelocity;

                    // Check boundaries for x-axis (left and right)
                    if (centerX - radius <= 0 || centerX + radius >= form.ClientSize.Width)
                    {
                        xVelocity *= -1;
                        hasGradient = !hasGradient; // Toggle the gradient status
                    }

                    // Check boundaries for y-axis (top and bottom)
                    if (centerY - radius <= 0 || centerY + radius >= form.ClientSize.Height)
                    {
                        yVelocity *= -1;
                        hasGradient = !hasGradient; // Toggle the gradient status
                    }

                    // Apply drawing based on gradient status
                    if (hasGradient)
                    {
                        using (var brush = new LinearGradientBrush(circleBox, Color.Red, Color.Blue, LinearGradientMode.Vertical))
                        {
                            g.FillEllipse(brush, circleBox);
                        }
                    }
                    else
                    {
                        g.FillEllipse(Brushes.Red, circleBox);
                    }
                }

                // Draw the buffer to the form to prevent flickering
                e.Graphics.DrawImage(bmp, 0, 0);

                // Update circle's position and properties
                centerX += xVelocity;
                centerY += yVelocity;

                // Update circleBox to match the new position of the circle
                circleBox.X = centerX - radius;
                circleBox.Y = centerY - radius;

                CheckCollision();
            }
        }

        public override void CheckCollision()
        {
            for (int i = 0; i < shapeList.Count; i++)
            {
                if (shapeList[i] is MyCircle && shapeList[i] != this)
                {
                    MyCircle otherCircle = (MyCircle)shapeList[i];

                    if (this.circleBox.IntersectsWith(otherCircle.circleBox))
                    {
                        ChangeVelocities(otherCircle);
                    }
                }
                else if (shapeList[i] is MyDogImage && shapeList[i] != this)
                {
                    MyDogImage dog = (MyDogImage)shapeList[i];

                    if (this.circleBox.IntersectsWith(dog.dogBox.Bounds))
                    {
                        ChangeVelocities(dog);
                    }
                }
                else if (shapeList[i] is MyPolyTriangle && shapeList[i] != this)
                {
                    MyPolyTriangle triangle = (MyPolyTriangle)shapeList[i];

                    if (this.circleBox.IntersectsWith(triangle.triangleBox))
                    {
                        ChangeVelocities(triangle);
                    }
                }
                else if (shapeList[i] is MyIrregularPolygon && shapeList[i] != this)
                {
                    MyIrregularPolygon polygon = (MyIrregularPolygon)shapeList[i];

                    if (this.circleBox.IntersectsWith(polygon.polygonBox))
                    {
                        ChangeVelocities(polygon);
                    }
                }
            }
        }

    }
}
