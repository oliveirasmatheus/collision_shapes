using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ass2OOP
{
    // Abstract class that serves as a blueprint for various shapes
    public abstract class Shape
    {
        // Abstract properties representing the velocity along x and y axes
        public abstract int xVelocity { get; set; }
        public abstract int yVelocity { get; set; }
        
        // Abstract method to draw the shape on a form using graphical context
        public abstract void Draw(PaintEventArgs e, Form form);

        // Abstract method to check for collisions with other shapes
        public abstract void CheckCollision();

        // Virtual method to change velocities with another shape when a collision occurs
        public virtual void ChangeVelocities(Shape other)
        {
            // Temporary variables to store velocities
            int tempXVelocity = xVelocity;
            int tempYVelocity = yVelocity;

            // Exchanging velocities with the 'other' shape
            xVelocity = other.xVelocity;
            yVelocity = other.yVelocity;

            // Restoring the 'other' shape's velocities from temporary variables
            other.xVelocity = tempXVelocity;
            other.yVelocity = tempYVelocity;
        }
    }
}
