using netDxf;
using netDxf.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace KreslarzWPF.Model
{
    public class Section
    {
        #region Initial Values
        public bool Draw { get; set; }
        double _TopRbarDiameter;
        int _NumberOfTopRbars;
        double _BottomRbarDiameter;
        int _NumberOfBottomRbars;
        string _Name;

        //tmp
        public int lowerRbarsID;
        public int topRbarsID;
        #endregion

        #region Constructors
        public Section(
            double topRbarDiameter,
            int numberOfTopRbars,
            double bottomRbarDiameter,
            int numberOfBottomRbars,
            string name
            )
        {
            this._TopRbarDiameter = topRbarDiameter;
            this._NumberOfTopRbars = numberOfTopRbars;
            this._BottomRbarDiameter = bottomRbarDiameter;
            this._NumberOfBottomRbars = numberOfBottomRbars;
            this._Name = name;
            this.Draw = false;
        }

        #endregion
    }
}