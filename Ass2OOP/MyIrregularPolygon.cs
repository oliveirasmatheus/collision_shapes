using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ass2OOP
{
    public class MyIrregularPolygon : Shape
    {
        private List<Point> vertices;
        private bool changeColor = false;
        private Color polygonColor = Color.PaleGoldenrod;
        public List<Shape> shapeList;
        public Rectangle polygonBox;

        public override int xVelocity { get; set; }
        public override int yVelocity { get; set; }


        public MyIrregularPolygon(List<Point> polygonVertices, List<Shape> shapes)
        {
            vertices = polygonVertices;
            // You might want to add more error handling here to ensure a valid polygon is provided

            Random random = new Random();

            //set the x and y velocity
            while (xVelocity > -3 && xVelocity < 3)
                xVelocity = random.Next(-15, 15);

            while (yVelocity > -3 && yVelocity < 3)
                yVelocity = random.Next(-15, 15);

            shapeList = shapes;

            // Find minimum and maximum X and Y coordinates among the points
            int minX = polygonVertices.Min(point => point.X);
            int minY = polygonVertices.Min(point => point.Y);
            int maxX = polygonVertices.Max(point => point.X);
            int maxY = polygonVertices.Max(point => point.Y);

            // Calculate the width and height of the bounding rectangle
            int width = maxX - minX;
            int height = maxY - minY;

            // Create the bounding rectangle based on the min/max coordinates and width/height
            polygonBox = new Rectangle(minX, minY, width, height);
        }

        public override void Draw(PaintEventArgs e, Form form)
        {
            e.Graphics.FillPolygon(new SolidBrush(polygonColor), vertices.ToArray());
            MovePolygon(form.ClientSize.Width, form.ClientSize.Height);
            CheckWallCollision(form.ClientSize.Width, form.ClientSize.Height);

            if (changeColor)
            {
                // Change the polygonColor color here
                polygonColor = (polygonColor == Color.PaleGoldenrod) ? Color.SkyBlue : Color.PaleGoldenrod;
                changeColor = false; // Reset flag
            }

            CheckCollision();
        }

        private void MovePolygon(int formWidth, int formHeight)
        {
            // Update the position of the vertices based on velocity
            for (int i = 0; i < vertices.Count; i++)
            {
                vertices[i] = new Point(vertices[i].X + xVelocity, vertices[i].Y + yVelocity);
            }
            UpdatePolygonBox(); // Update the bounding box after moving the vertices
        }

        private void UpdatePolygonBox()
        {
            // Recalculate the bounding box based on the updated vertices
            int minX = vertices.Min(point => point.X);
            int minY = vertices.Min(point => point.Y);
            int maxX = vertices.Max(point => point.X);
            int maxY = vertices.Max(point => point.Y);

            int width = maxX - minX;
            int height = maxY - minY;

            // Update the bounding rectangle based on the new min/max coordinates and width/height
            polygonBox = new Rectangle(minX, minY, width, height);
        }

        private void CheckWallCollision(int formWidth, int formHeight)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].X <= 0 || vertices[i].X >= formWidth)
                {
                    xVelocity *= -1; // Reverse x direction
                    changeColor = true;
                    break;
                }

                if (vertices[i].Y <= 0 || vertices[i].Y >= formHeight)
                {
                    yVelocity *= -1; // Reverse y direction
                    changeColor = true;
                    break;
                }
            }
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

                    if (this.polygonBox.IntersectsWith(circle.circleBox))
                    {
                        ChangeVelocities(circle);
                    }
                }
                // Check collision with MyDogImage objects
                else if (shapeList[i] is MyDogImage && shapeList[i] != this)
                {
                    MyDogImage dog = (MyDogImage)shapeList[i];

                    if (this.polygonBox.IntersectsWith(dog.dogBox.Bounds))
                    {
                        ChangeVelocities(dog);
                    }
                }
                // Check collision with MyPolyTriangle objects
                else if (shapeList[i] is MyPolyTriangle && shapeList[i] != this)
                {
                    MyPolyTriangle triangle = (MyPolyTriangle)shapeList[i];

                    if (this.polygonBox.IntersectsWith(triangle.triangleBox))
                    {
                        ChangeVelocities(triangle);
                    }
                }
                // Check collision with MyIrregularPolygon objects
                else if (shapeList[i] is MyIrregularPolygon && shapeList[i] != this)
                {
                    MyIrregularPolygon polygon = (MyIrregularPolygon)shapeList[i];

                    if (this.polygonBox.IntersectsWith(polygon.polygonBox))
                    {
                        ChangeVelocities(polygon);
                    }
                }
            }
        }
    }
}
