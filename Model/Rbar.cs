using netDxf;
using netDxf.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KreslarzWPF.Model
{
    public class Rbar
    {
        #region Initial Values
        public int NumberOfRbars { get; set; }
        public double Diameter { get; set; }
        public double X0 { get; set; }
        public double Y0 { get; set; }
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }
        public double X3 { get; set; }
        public double Y3 { get; set; }
        #endregion

        #region Fixed Values
        protected double _Offset = -70;
        #endregion

        #region Constructors
        public Rbar(int numberOfRbars, double diameter, 
            double x0, double y0, double x1, double y1,
            double x2,double y2, double x3, double y3)
        {
            this.NumberOfRbars = numberOfRbars;
            this.Diameter = diameter;
            this.X0 = x0;
            this.Y0 = y0;
            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x2;
            this.Y2 = y2;
            this.X3 = x3;
            this.Y3 = y3;
        }
        public Rbar(int numberOfRbars, double diameter,
            double x0, double y0, double x1, double y1)
        {
            this.NumberOfRbars = numberOfRbars;
            this.Diameter = diameter;
            this.X0 = x0;
            this.Y0 = y0;
            this.X1 = x1;
            this.Y1 = y1;
        }
        #endregion

        #region Methods
        public DxfDocument DrawRbar(int id, bool inside, double offset)
        {
            //decides where to put the rbar to avoid overlaping
            double factor, shift = Math.Max(Math.Abs(this.Y1 - this.Y0), Math.Abs(this.Y3 - this.Y2)) + 10;
            if (inside == true)
            {
                factor = 0;
            }
            else
            {
                factor = -Convert.ToInt32(id) % 2;
            }

            //checks if there are any hooks
            double shiftLeft = 0, bulgeLeft = 0, shiftRight = 0, bulgeRight = 0,
                flipLeft = 1, flipRight = 1;
            if (this.Y1 - this.Y0 != 0)
            {
                shiftLeft = 2 * this.Diameter / 10;
                bulgeLeft = 0.4;
            }
            if (this.Y3 - this.Y2 != 0)
            {
                shiftRight = 2 * this.Diameter / 10;
                bulgeRight = 0.4;
            }
            if (this.Y0 < this.Y1)
            {
                flipLeft = -1;
            }
            if (this.Y3 < this.Y2)
            {
                flipRight = -1;
            }

            //find rbar boundary to draw
            List<netDxf.Entities.LwPolylineVertex> vertexes = new List<netDxf.Entities.LwPolylineVertex>() 
            {
                new netDxf.Entities.LwPolylineVertex(this.X0, this.Y0+offset-shift*factor),
                new netDxf.Entities.LwPolylineVertex(this.X1, this.Y1+offset+flipLeft*shiftLeft-shift*factor, flipLeft*bulgeLeft),
                new netDxf.Entities.LwPolylineVertex(this.X1+shiftLeft, this.Y1+offset-shift*factor),
                new netDxf.Entities.LwPolylineVertex(this.X2-shiftRight, this.Y2+offset-shift*factor, flipRight*bulgeRight),
                new netDxf.Entities.LwPolylineVertex(this.X2, this.Y2+offset+flipRight*shiftRight-shift*factor),
                new netDxf.Entities.LwPolylineVertex(this.X3, this.Y3+offset-shift*factor),
            };

            //draw rbar
            LwPolyline rbar = new LwPolyline(vertexes.GetRange(0, 6));
            rbar.SetConstantWidth(this.Diameter / 10);
            Beam.I.Dxf.AddEntity(rbar);

            //if inside of the beam, return
            if (inside)
            {
                return Beam.I.Dxf;
            }

            //find rbar points to annotate
            Vector2[] points =
            {
                new Vector2(this.X0, this.Y0+offset-shift*factor),
                new Vector2(this.X1, this.Y1+offset-shift*factor),
                new Vector2(this.X2, this.Y2+offset-shift*factor),
                new Vector2(this.X3, this.Y3+offset-shift*factor),
            };

            //generate description text           
            string descriptionText = "(" + id + ") " + this.NumberOfRbars.ToString() + "#" + this.Diameter.ToString() + " L=";
            descriptionText += (Math.Abs(this.Y0 - this.Y1) + Math.Abs(this.X0 - this.X2) + Math.Abs(this.Y3 - this.Y2)).ToString();

            //add description of a rbar
            LinearDimension dim = new LinearDimension(
                    points[1],
                    points[2],
                    1.8,
                    0,
                    Beam.I.DimensionStyles.Annotation);
            dim.UserText = descriptionText;
            Beam.I.Dxf.AddEntity(dim);

            //check if there's a left hook and dim
            if (this.Y0 != this.Y1)
            {
                LinearDimension leftHookDim = new LinearDimension(
                    points[0],
                    points[1],
                    -5.4,
                    90,
                    Beam.I.DimensionStyles.Rbars);
                Beam.I.Dxf.AddEntity(leftHookDim);
            }

            //draw dimension of the main part of a rbar
            LinearDimension rbarDim = new LinearDimension(
                    points[1],
                    points[2],
                    -5.4,
                    0,
                    Beam.I.DimensionStyles.Rbars);
            Beam.I.Dxf.AddEntity(rbarDim);

            //check if there's a right hook and dim
            if (this.Y2 != this.Y3)
            {
                LinearDimension rightHookDim = new LinearDimension(
                    points[2],
                    points[3],
                    -5.4,
                    90,
                    Beam.I.DimensionStyles.Rbars);
                Beam.I.Dxf.AddEntity(rightHookDim);
            }
            return Beam.I.Dxf;
        }
        #endregion
    }
}
