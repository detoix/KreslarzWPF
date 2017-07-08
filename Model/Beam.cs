using netDxf;
using System.Collections.Generic;
using System.ComponentModel;

namespace KreslarzWPF.Model
{
    class Beam : INotifyPropertyChanged
    {
        #region InitialValues
        public string Name { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Cover { get; set; }
        public DxfDocument Dxf { get; set; }
        public DimensionStyles DimensionStyles { get; private set; }
        #endregion InitialValues

        #region FixedValues
        public static Beam I;
        #endregion FixedValues

        #region Constructors
        private Beam() 
        {
            this.Name = "Belka 01";
            this.Height = 50;
            this.Width = 25;
            this.Cover = 2;
            this.DimensionStyles = DimensionStyles.Create();
        }
        #endregion

        #region Static Methods
        public static Beam Create()
        {
            if (I == null) { I = new Beam(); }
            return I;
        }
        #endregion

        #region EventHandler
        void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}