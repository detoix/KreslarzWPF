using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KreslarzWPF.Model
{
    public class Stirrup
    {
        public double Diameter { get; set; }
        public string Type { get; set; }
        public ObservableCollection<string> StirrupTypes { get; set; }

        #region FixedValues
        public static Stirrup I;
        #endregion FixedValues

        #region Constructors
        private Stirrup()
        {
            this.Diameter = 6;
            this.StirrupTypes = new ObservableCollection<string> { "1-cięte", "2-cięte" };
            this.Type = "1-cięte";
        }
        #endregion

        #region Static Methods
        public static Stirrup Create()
        {
            if (I == null) { I = new Stirrup(); }
            return I;
        }
        #endregion
    }
}
