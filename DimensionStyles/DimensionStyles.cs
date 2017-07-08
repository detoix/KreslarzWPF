namespace KreslarzWPF
{
    class DimensionStyles
    {
        #region Properties
        public static DimensionStyles I;
        public netDxf.Tables.DimensionStyle General { get; set; }
        public netDxf.Tables.DimensionStyle Annotation { get; set; }
        public netDxf.Tables.DimensionStyle Rbars { get; set; }
        #endregion

        #region Constructors
        private DimensionStyles() 
        {
            this.General = new netDxf.Tables.DimensionStyle("General");
            this.General.TextHeight = 3.6;
            this.General.TextOffset = 1.8;
            this.General.ArrowSize = 2.4;
            this.General.LengthPrecision = 0;
            this.General.DimArrow1 = netDxf.Entities.DimensionArrowhead.ArchitecturalTick;
            this.General.DimArrow2 = netDxf.Entities.DimensionArrowhead.ArchitecturalTick;
            this.General.ExtLineExtend = 1.2;
            this.General.ExtLineOffset = 5;

            this.Annotation = new netDxf.Tables.DimensionStyle("Annotation");
            this.Annotation.TextHeight = 3.6;
            this.Annotation.ArrowSize = 0;
            this.Annotation.DimLineOff = true;
            this.Annotation.ExtLine1Off = true;
            this.Annotation.ExtLine2Off = true;
            this.Annotation.LengthPrecision = 0;

            this.Rbars = new netDxf.Tables.DimensionStyle("Rbars");
            this.Rbars.TextHeight = 3.6;
            this.Rbars.ArrowSize = 0;
            this.Rbars.DimLineOff = true;
            this.Rbars.ExtLine1Off = true;
            this.Rbars.ExtLine2Off = true;
            this.Rbars.LengthPrecision = 0;
        }
        #endregion

        #region Static Methods
        public static DimensionStyles Create()
        {
            if (I == null) { I = new DimensionStyles(); }
            return I;
        }
        #endregion
    }
}
