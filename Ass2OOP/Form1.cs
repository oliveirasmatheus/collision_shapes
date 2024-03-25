using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Ass2OOP
{
    public partial class Form1 : Form
    {
        // Timer to trigger the refreshing of the form
        System.Timers.Timer timer = new System.Timers.Timer(50);
        List<Shape> shapeList = new List<Shape>(); // List to store various shapes

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true; // Set KeyPreview property to true to capture key events on the form
            this.KeyDown += new KeyEventHandler(Form1_KeyDown); // Subscribe to the KeyDown event

            this.Paint += new PaintEventHandler(Form1_Paint); // Subscribe to the Paint event to draw shapes

            timer.Elapsed += OnTimedEvent;
            timer.Start(); // Start the timer to refresh the form

            this.MouseClick += MouseClickHandler; // Subscribe to the MouseClick event for creating shapes
        }

        // Timer event to refresh the form
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Invalidate(); // Refresh the form to redraw shapes
        }

        // Handles the initial loading of the form
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Event triggered when the form is painted (shapes need to be drawn)
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Shape shape in shapeList)
            {
                shape.Draw(e, this); // Draw each shape in the list
            }
        }

        // Event triggered when the mouse is clicked on the form
        private void MouseClickHandler(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Random random = new Random();
            //int ran = 2;
            int ran = random.Next(0, 4); // Generate a random number between 0 and 3
            int mouseX = e.X; // Get X coordinate of the mouse click
            int mouseY = e.Y; // Get Y coordinate of the mouse click

            // Create different shapes based on the random number generated
            if (ran == 0)
            {
                int radius = 30; // Define the radius of the circle
                shapeList.Add((MyCircle)new MyCircle(mouseX, mouseY, radius, shapeList)); // Add a circle to the list
            }
            else if (ran == 1)
            {
                shapeList.Add((MyPolyTriangle)new MyPolyTriangle(mouseX, mouseY, shapeList)); // Add a polygon (triangle) to the list
            }
            else if (ran == 2)
            {
                shapeList.Add((MyDogImage)new MyDogImage(mouseX, mouseY, shapeList)); // Add an image of a dog to the list
            }
            else if (ran == 3)
            {
                // Create an irregular polygon based on mouse click coordinates
                List<Point> polygonVertices = new List<Point>
                {
                    new Point(mouseX, mouseY - 60), // Top point
                    new Point(mouseX + 30, mouseY - 30), // Upper right
                    new Point(mouseX + 30, mouseY + 30), // Lower right
                    new Point(mouseX, mouseY), // Center
                    new Point(mouseX - 30, mouseY + 30), // Lower left
                    new Point(mouseX - 30, mouseY - 30) // Upper left
                };
                shapeList.Add((MyIrregularPolygon)new MyIrregularPolygon(polygonVertices, shapeList)); // Add the irregular polygon to the list
            }
        }

        // Event triggered when a key is pressed
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Random random = new Random();
            int mouseX = this.Width / 2; // Set initial X coordinate (can be adjusted)
            int mouseY = this.Height / 2; // Set initial Y coordinate (can be adjusted)
            int ran = random.Next(0, 4); // Generate a random number between 0 and 3

            if (e.KeyCode == Keys.N) // Check if the pressed key is 'N'
            {
                // Create different shapes based on the random number generated
                if (ran == 0)
                {
                    int radius = 30; // Define the radius of the circle
                    shapeList.Add((MyCircle)new MyCircle(mouseX, mouseY, radius, shapeList)); // Add a circle to the list
                }
                else if (ran == 1)
                {
                    shapeList.Add((MyPolyTriangle)new MyPolyTriangle(mouseX, mouseY, shapeList)); // Add a polygon (triangle) to the list
                }
                else if (ran == 2)
                {
                    shapeList.Add((MyDogImage)new MyDogImage(mouseX, mouseY, shapeList)); // Add an image of a dog to the list
                }
                else if (ran == 3)
                {
                    // Create an irregular polygon based on the initial coordinates
                    List<Point> polygonVertices = new List<Point>
                    {
                        new Point(mouseX, mouseY - 60), // Top point
                        new Point(mouseX + 30, mouseY - 30), // Upper right
                        new Point(mouseX + 30, mouseY + 30), // Lower right
                        new Point(mouseX, mouseY), // Center
                        new Point(mouseX - 30, mouseY + 30), // Lower left
                        new Point(mouseX - 30, mouseY - 30) // Upper left
                    };
                    shapeList.Add((MyIrregularPolygon)new MyIrregularPolygon(polygonVertices, shapeList)); // Add the irregular polygon to the list
                }

            }
            else if (e.KeyCode == Keys.F1)
            {
                int radius = 30; // Define the radius of the circle
                shapeList.Add((MyCircle)new MyCircle(mouseX, mouseY, radius, shapeList)); // Add a circle to the list
            }
            else if (e.KeyCode == Keys.F2)
            {
                shapeList.Add((MyPolyTriangle)new MyPolyTriangle(mouseX, mouseY, shapeList)); // Add a polygon (triangle) to the list
            }
            else if (e.KeyCode == Keys.F3)
            {
                shapeList.Add((MyDogImage)new MyDogImage(mouseX, mouseY, shapeList)); // Add an image of a dog to the list
            }
            else if (e.KeyCode == Keys.F4)
            {
                // Create an irregular polygon based on the initial coordinates
                List<Point> polygonVertices = new List<Point>
                    {
                        new Point(mouseX, mouseY - 60), // Top point
                        new Point(mouseX + 30, mouseY - 30), // Upper right
                        new Point(mouseX + 30, mouseY + 30), // Lower right
                        new Point(mouseX, mouseY), // Center
                        new Point(mouseX - 30, mouseY + 30), // Lower left
                        new Point(mouseX - 30, mouseY - 30) // Upper left
                    };
                shapeList.Add((MyIrregularPolygon)new MyIrregularPolygon(polygonVertices, shapeList)); // Add the irregular polygon to the list
            }

            Invalidate(); // Refresh the form to display the new shape

        }
    }
}