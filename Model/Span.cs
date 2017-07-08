using netDxf;
using netDxf.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace KreslarzWPF.Model
{
    public class Span : INotifyPropertyChanged
    {
        #region Initial Values
        int _Id;
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                }
            }
        }

        Span _Prev;
        public Span Prev 
        { 
            get
            {
                return _Prev;
            }
            set
            {
                if (_Prev != value)
                {
                    _Prev = value;
                    _Prev.Next = this;
                }
            }
        }

        Span _Next;
        public Span Next
        {
            get
            {
                return _Next;
            }
            set
            {
                if (_Next != value)
                {
                    _Next = value;
                }
            }
        }

        public double SpanLength { get; set; }

        public Stirrup Stirrup { get; set; }

        string _LeftSupportType;
        public string LeftSupportType
        {
            get
            {
                return _LeftSupportType;
            }
            set
            {
                if (_LeftSupportType != value)
                {
                    _LeftSupportType = value;
                    if (_Prev != null)
                    {
                        _Prev.RightSupportType = value;
                    }
                }
            }
        }

        double _LeftSupportWidth;
        public double LeftSupportWidth
        {
            get
            {
                return _LeftSupportWidth;
            }
            set
            {
                if (_LeftSupportWidth != value)
                {
                    _LeftSupportWidth = value;
                    if (_Prev != null)
                    {
                        _Prev.RightSupportWidth = value;
                    }
                }
            }
        }

        string _RightSupportType;
        public string RightSupportType
        {
            get
            {
                return _RightSupportType;
            }
            set
            {
                if (_RightSupportType != value)
                {
                    _RightSupportType = value;
                    if (_Next != null)
                    {
                        _Next.LeftSupportType = value;
                    }
                }
            }
        }

        double _RightSupportWidth;
        public double RightSupportWidth
        {
            get
            {
                return _RightSupportWidth;
            }
            set
            {
                if (_RightSupportWidth != value)
                {
                    _RightSupportWidth = value;
                    if (_Next != null)
                    {
                        _Next.LeftSupportWidth = value;
                    }
                }
            }
        }

        public bool DrawLeftSection { get; set; }

        public bool DrawRightSection { get; set; }

        double _BottomRbarDiameter;
        public double BottomRbarDiameter
        {
            get
            {
                return _BottomRbarDiameter;
            }
            set
            {
                if (_BottomRbarDiameter != value)
                {
                    _BottomRbarDiameter = value;
                    if (_BottomJoinLeft)
                    {
                        _Prev.BottomRbarDiameter = value;
                    }
                    if (_BottomJoinRight)
                    {
                        _Next.BottomRbarDiameter = value;
                    }
                }
            }
        }

        int _NumberOfBottomRbars;
        public int NumberOfBottomRbars
        {
            get
            {
                return _NumberOfBottomRbars;
            }
            set
            {
                if (_NumberOfBottomRbars != value)
                {
                    _NumberOfBottomRbars = value;
                    if (_BottomJoinLeft)
                    {
                        _Prev.NumberOfBottomRbars = value;
                    }
                    if (_BottomJoinRight)
                    {
                        _Next.NumberOfBottomRbars = value;
                    }
                }
            }
        }

        double _BottomRbarLeftSupport;
        public double BottomRbarLeftSupport
        {
            get
            {
                return _BottomRbarLeftSupport;
            }
            set
            {
                if (_BottomRbarLeftSupport != value)
                {
                    if (!_BottomJoinLeft)
                    {
                        _BottomRbarLeftSupport = value;
                    }
                    else
                    {
                        _BottomRbarLeftSupport = 0;
                    }
                }
            }
        }

        double _BottomRbarRightSupport;
        public double BottomRbarRightSupport
        {
            get
            {
                return _BottomRbarRightSupport;
            }
            set
            {
                if (_BottomRbarRightSupport != value)
                {
                    if (!_BottomJoinRight)
                    {
                        _BottomRbarRightSupport = value;
                    }
                    else
                    {
                        _BottomRbarRightSupport = 0;
                    }
                }
            }
        }

        double _BottomRbarLeftHook;
        public double BottomRbarLeftHook
        {
            get
            {
                return _BottomRbarLeftHook;
            }
            set
            {
                if (_BottomRbarLeftHook != value)
                {
                    if (!_BottomJoinLeft)
                    {
                        _BottomRbarLeftHook = value;
                    }
                    else
                    {
                        _BottomRbarLeftHook = 0;
                    }
                }
            }
        }

        double _BottomRbarRightHook;
        public double BottomRbarRightHook
        {
            get
            {
                return _BottomRbarRightHook;
            }
            set
            {
                if (_BottomRbarRightHook != value)
                {
                    if (!_BottomJoinRight)
                    {
                        _BottomRbarRightHook = value;
                    }
                    else
                    {
                        _BottomRbarRightHook = 0;
                    }
                }
            }
        }

        bool _BottomJoinLeft;
        public bool BottomJoinLeft
        {
            get
            {
                return _BottomJoinLeft;
            }
            set
            {
                if (_BottomJoinLeft != value)
                {
                    if (_Prev != null)
                    {
                        _BottomJoinLeft = value;
                        _Prev.BottomJoinRight = value;
                        _Prev.BottomRbarDiameter = this.BottomRbarDiameter;
                        _BottomRbarLeftSupport = 0;
                        _BottomRbarLeftHook = 0;
                        RaisePropertyChanged("BottomRbarLeftSupport");
                        RaisePropertyChanged("BottomRbarLeftHook");
                    }
                }
            }
        }

        bool _BottomJoinRight;
        public bool BottomJoinRight
        {
            get
            {
                return _BottomJoinRight;
            }
            set
            {
                if (_BottomJoinRight != value)
                {
                    if (_Next != null)
                    {
                        _BottomJoinRight = value;
                        _Next.BottomJoinLeft = value;
                        _Next.BottomRbarDiameter = this.BottomRbarDiameter;
                        _BottomRbarRightSupport = 0;
                        _BottomRbarRightHook = 0;
                        RaisePropertyChanged("BottomRbarRightSupport");
                        RaisePropertyChanged("BottomRbarRightHook");
                    }
                }
            }
        }

        double _TopLeftRbarDiameter;
        public double TopLeftRbarDiameter
        {
            get
            {
                return _TopLeftRbarDiameter;
            }
            set
            {
                if (_TopLeftRbarDiameter != value)
                {
                    _TopLeftRbarDiameter = value;
                    if (_Prev != null)
                    {
                        _Prev.TopRightRbarDiameter = value;
                    }
                    if (_JoinTop)
                    {
                        this.TopRightRbarDiameter = value;
                        RaisePropertyChanged("TopRightRbarDiameter");
                    }
                }
            }
        }

        int _NumberOfTopLeftRbars;
        public int NumberOfTopLeftRbars
        {
            get
            {
                return _NumberOfTopLeftRbars;
            }
            set
            {
                if (_NumberOfTopLeftRbars != value)
                {
                    _NumberOfTopLeftRbars = value;
                    if (_Prev != null)
                    {
                        _Prev.NumberOfTopRightRbars = value;
                    }
                    if (_JoinTop)
                    {
                        this.NumberOfTopRightRbars = value;
                        RaisePropertyChanged("NumberOfTopRightRbars");
                    }
                }
            }
        }

        double _TopLeftRbarLeftOverhang;
        public double TopLeftRbarLeftOverhang
        {
            get
            {
                return _TopLeftRbarLeftOverhang;
            }
            set
            {
                if (_TopLeftRbarLeftOverhang != value)
                {
                    if (_Prev != null)
                    {
                        if (!_Prev.JoinTop)
                        {
                            _TopLeftRbarLeftOverhang = value;
                            _Prev.TopRightRbarLeftOverhang = value;
                        }
                        else
                        {
                            _TopLeftRbarLeftOverhang = 0;
                            _Prev.TopRightRbarLeftOverhang = 0;
                        }
                    }
                    else
                    {
                        _TopLeftRbarLeftOverhang = value;
                    }
                }
            }
        }

        double _TopLeftRbarRightOverhang;
        public double TopLeftRbarRightOverhang
        {
            get
            {
                return _TopLeftRbarRightOverhang;
            }
            set
            {
                if (_TopLeftRbarRightOverhang != value)
                {
                    if (!this.JoinTop)
                    {
                        _TopLeftRbarRightOverhang = value;
                        if (_Prev != null)
                        {
                            _Prev.TopRightRbarRightOverhang = value;
                        }
                    }
                    else
                    {
                        _TopLeftRbarRightOverhang = 0;
                        if (_Prev != null)
                        {
                            _Prev.TopRightRbarRightOverhang = 0;
                        }
                    }
                }
            }
        }

        double _TopRightRbarDiameter;
        public double TopRightRbarDiameter
        {
            get
            {
                return _TopRightRbarDiameter;
            }
            set
            {
                if (_TopRightRbarDiameter != value)
                {
                    _TopRightRbarDiameter = value;
                    if (_Next != null)
                    {
                        _Next.TopLeftRbarDiameter = value;
                    }
                    if (_JoinTop)
                    {
                        this.TopLeftRbarDiameter = value;
                        RaisePropertyChanged("TopLeftRbarDiameter");
                    }
                }
            }
        }

        int _NumberOfTopRightRbars;
        public int NumberOfTopRightRbars
        {
            get
            {
                return _NumberOfTopRightRbars;
            }
            set
            {
                if (_NumberOfTopRightRbars != value)
                {
                    _NumberOfTopRightRbars = value;
                    if (_Next != null)
                    {
                        _Next.NumberOfTopLeftRbars = value;
                    }
                    if (_JoinTop)
                    {
                        this.NumberOfTopLeftRbars = value;
                        RaisePropertyChanged("NumberOfTopLeftRbars");
                    }
                }
            }
        }

        double _TopRightRbarLeftOverhang;
        public double TopRightRbarLeftOverhang
        {
            get
            {
                return _TopRightRbarLeftOverhang;
            }
            set
            {
                if (_TopRightRbarLeftOverhang != value)
                {
                    if (!this.JoinTop)
                    {
                        _TopRightRbarLeftOverhang = value;
                        if (_Next != null)
                        {
                            _Next.TopLeftRbarLeftOverhang = value;
                        }
                    }
                    else
                    {
                        _TopRightRbarLeftOverhang = 0;
                        if (_Next != null)
                        {
                            _Next.TopLeftRbarLeftOverhang = 0;
                        }
                    }
                }
            }
        }

        double _TopRightRbarRightOverhang;
        public double TopRightRbarRightOverhang
        {
            get
            {
                return _TopRightRbarRightOverhang;
            }
            set
            {
                if (_TopRightRbarRightOverhang != value)
                {
                    if (_Next != null)
                    {
                        if (!_Next.JoinTop)
                        {
                            _TopRightRbarRightOverhang = value;
                            _Next.TopLeftRbarRightOverhang = value;
                        }
                        else
                        {
                            _TopRightRbarRightOverhang = 0;
                            _Next.TopLeftRbarRightOverhang = 0;
                        }
                    }
                    else
                    {
                        _TopRightRbarRightOverhang = value;
                    }
                }
            }
        }

        double _TopLeftRbarLeftHook;
        public double TopLeftRbarLeftHook
        {
            get
            {
                return _TopLeftRbarLeftHook;
            }
            set
            {
                if (_TopLeftRbarLeftHook != value)
                {
                    if (_Prev != null)
                    {
                        if (!_Prev.JoinTop)
                        {
                            _TopLeftRbarLeftHook = value;
                            _Prev.TopRightRbarLeftHook = value;
                        }
                        else
                        {
                            _TopLeftRbarLeftHook = 0;
                            _Prev.TopRightRbarLeftHook = 0;
                        }
                    }
                    else
                    {
                        _TopLeftRbarLeftHook = value;
                    }
                }
            }
        }

        double _TopLeftRbarRightHook;
        public double TopLeftRbarRightHook
        {
            get
            {
                return _TopLeftRbarRightHook;
            }
            set
            {
                if (_TopLeftRbarRightHook != value)
                {
                    if (!this.JoinTop)
                    {
                        _TopLeftRbarRightHook = value;
                        if (_Prev != null)
                        {
                            _Prev.TopRightRbarRightHook = value;
                        }
                    }
                    else
                    {
                        _TopLeftRbarRightHook = 0;
                        if (_Prev != null)
                        {
                            _Prev.TopRightRbarRightHook = 0;
                        }
                    }
                }
            }
        }

        double _TopRightRbarLeftHook;
        public double TopRightRbarLeftHook
        {
            get
            {
                return _TopRightRbarLeftHook;
            }
            set
            {
                if (_TopRightRbarLeftHook != value)
                {
                    if (!this.JoinTop)
                    {
                        _TopRightRbarLeftHook = value;
                        if (_Next != null)
                        {
                            _Next.TopLeftRbarLeftHook = value;
                        }
                    }
                    else
                    {
                        _TopRightRbarLeftHook = 0;
                        if (_Next != null)
                        {
                            _Next.TopLeftRbarLeftHook = 0;
                        }
                    }
                }
            }
        }

        double _TopRightRbarRightHook;
        public double TopRightRbarRightHook
        {
            get
            {
                return _TopRightRbarRightHook;
            }
            set
            {
                if (_TopRightRbarRightHook != value)
                {
                    if (_Next != null)
                    {
                        if (!_Next.JoinTop)
                        {
                            _TopRightRbarRightHook = value;
                            _Next.TopLeftRbarRightHook = value;
                        }
                        else
                        {
                            _TopRightRbarRightHook = 0;
                            _Next.TopLeftRbarRightHook = 0;
                        }
                    }
                    else
                    {
                        _TopRightRbarRightHook = value;
                    }
                }
            }
        }

        bool _JoinTop;
        public bool JoinTop
        {
            get
            {
                return _JoinTop;
            }
            set
            {
                if (_JoinTop != value)
                {
                    _JoinTop = value;
                    this.TopRightRbarDiameter = this.TopLeftRbarDiameter;
                    RaisePropertyChanged("TopRightRbarDiameter");
                    this.NumberOfTopRightRbars = this.NumberOfTopLeftRbars;
                    RaisePropertyChanged("NumberOfTopRightRbars");
                }
            }
        }

        //tmp
        double stirrupDiameter;
        //tmp

        public ObservableCollection<string> SupportTypes { get; set; }
        #endregion InitialValues

        #region Fixed Values
        protected double _SupportHeight = 25;
        protected double _SectionMarkOffsetH = 20;
        protected double _SectionMarkOffsetV = 10;
        protected double _SectionMarkWidth = 1;
        protected double _SectionMarkTextHeight = 8;
        protected double _Translation = 0;
        #endregion FixedValues

        #region Constructors
        public Span()
        {
            this.Stirrup = Stirrup.I;

            this.Id = 1;
            this.SupportTypes = new ObservableCollection<string> { "A", "B", "C" };

            this.SpanLength = 500;
            this.LeftSupportType = "A";
            this.LeftSupportWidth = 25;
            this.RightSupportWidth = 25;
            this.RightSupportType = "A";
            this.DrawLeftSection = true;
            this.DrawRightSection = false;

            this.BottomRbarDiameter = 16;
            this.NumberOfBottomRbars = 3;
            this.BottomRbarLeftSupport = 20;
            this.BottomRbarRightSupport = 20;
            this.BottomRbarLeftHook = 0;
            this.BottomRbarRightHook = 0;
            this.BottomJoinLeft = false;
            this.BottomJoinRight = false;

            this.TopLeftRbarDiameter = 16;
            this.NumberOfTopLeftRbars = 4;
            this.TopLeftRbarLeftOverhang = 20;
            this.TopLeftRbarRightOverhang = 0;
            this.TopRightRbarDiameter = 16;
            this.NumberOfTopRightRbars = 4;
            this.TopRightRbarLeftOverhang = 0;
            this.TopRightRbarRightOverhang = 20;
            this.TopLeftRbarLeftHook = 40;
            this.TopLeftRbarRightHook = 0;
            this.TopRightRbarLeftHook = 0;
            this.TopRightRbarRightHook = 40;
            this.JoinTop = true;
        }

        public Span(Span prev) : this()
        {
            this.Prev = prev;
            this.Id = prev.Id + 1;
        }
        #endregion

        #region Destructors
        public void Dispose()
        {
            Prev.Next = null;
        }
        #endregion

        #region Methods
        public void GetTranslation(Span span)
        {
            if (span == this)
            {
                _Translation = -50 - span.LeftSupportWidth - span.SpanLength;
            }
            else
            {
                _Translation += -span.LeftSupportWidth - span.SpanLength;
            }

            if (span.Next != null)
            {
                GetTranslation(span.Next); 
            }
            else
            {
                _Translation += -span.RightSupportWidth;
            }
        }
        public DxfDocument DrawStart()
        {
            Vector2[] points =
            {
                new Vector2(this._Translation, - _SupportHeight),
                new Vector2(this._Translation, Beam.I.Height + _SupportHeight),
                new Vector2(this._Translation, Beam.I.Height),
                new Vector2(this._Translation + this.LeftSupportWidth / 2, Beam.I.Height),
            };
            if (this.LeftSupportType == "A")
            {
                Beam.I.Dxf.AddEntity(new Line(
                   points[0],
                   points[1]));
            }
            else if (this.LeftSupportType == "B")
            {
                Beam.I.Dxf.AddEntity(new Line(
                   points[0],
                   points[2]));
                Beam.I.Dxf.AddEntity(new Line(
                   points[2],
                   points[3]));
            }
            return Beam.I.Dxf;
        }
        public DxfDocument DrawSideView()
        {
            //alter left support
            double x0, y0;
            double x4, y4;
            if (this.LeftSupportType == "A")
            {
                x0 = this._Translation + this.LeftSupportWidth;
                y0 = -_SupportHeight;
                x4 = x0;
                y4 = Beam.I.Height + _SupportHeight;
            }
            else if (this.LeftSupportType == "B")
            {
                x0 = this._Translation + this.LeftSupportWidth;
                y0 = -_SupportHeight;
                x4 = this._Translation + this.LeftSupportWidth / 2;
                y4 = Beam.I.Height;
            }
            else
            {
                x0 = this._Translation + this.LeftSupportWidth;
                y0 = Beam.I.Height / 2;
                x4 = x0;
                y4 = Beam.I.Height / 2;
            }
            //alter right support
            double x3, y3;
            double x7, y7;
            if (this.RightSupportType == "A")
            {
                x3 = this._Translation + this.SpanLength + this.LeftSupportWidth;
                y3 = -_SupportHeight;
                x7 = x3;
                y7 = Beam.I.Height + _SupportHeight;
            }
            else if (this.RightSupportType == "B")
            {
                x3 = this._Translation + this.SpanLength + this.LeftSupportWidth;
                y3 = -_SupportHeight;
                x7 = x3 + this.RightSupportWidth / 2;
                y7 = Beam.I.Height;
            }
            else
            {
                x3 = this._Translation + this.SpanLength + this.LeftSupportWidth;
                y3 = Beam.I.Height / 2;
                x7 = x3 + this.RightSupportWidth / 2;
                y7 = Beam.I.Height / 2;
            }

            Vector2[] points =
            {
                new Vector2(x0, y0),
                new Vector2(this._Translation+this.LeftSupportWidth, 0),
                new Vector2(this._Translation+this.LeftSupportWidth+this.SpanLength, 0),
                new Vector2(x3, y3),
                new Vector2(x4, y4),
                new Vector2(this._Translation+this.LeftSupportWidth, Beam.I.Height),
                new Vector2(this._Translation+this.LeftSupportWidth+this.SpanLength, Beam.I.Height),
                new Vector2(x7, y7),
                //to dimension
                new Vector2(this._Translation, 0),
            };

            Beam.I.Dxf.AddEntity(new Line(
                points[0],
                points[1]));
            Beam.I.Dxf.AddEntity(new Line(
                points[1],
                points[2]));
            Beam.I.Dxf.AddEntity(new Line(
                points[2],
                points[3]));
            Beam.I.Dxf.AddEntity(new Line(
                points[4],
                points[5]));
            Beam.I.Dxf.AddEntity(new Line(
                points[5],
                points[6]));
            Beam.I.Dxf.AddEntity(new Line(
                points[6],
                points[7]));


            if (this.LeftSupportType != "C")
            {
                LinearDimension dim81 = new LinearDimension(
                    points[8],
                    points[1],
                    -20,
                    0,
                    Beam.I.DimensionStyles.General);
                dim81.UserText = "<>";
                Beam.I.Dxf.AddEntity(dim81);
            }

            LinearDimension dim12 = new LinearDimension(
                    points[1],
                    points[2],
                    -20,
                    0,
                    Beam.I.DimensionStyles.General);
            dim12.UserText = "<>";
            Beam.I.Dxf.AddEntity(dim12);
            return Beam.I.Dxf;
        }
        public DxfDocument DrawEnd()
        {
            Vector2[] points =
            {
                new Vector2(-50, -this._SupportHeight),
                new Vector2(-50, Beam.I.Height + this._SupportHeight),
                new Vector2(-50, Beam.I.Height),
                new Vector2(-50 - this.RightSupportWidth / 2, Beam.I.Height),
                new Vector2(-50 - this.RightSupportWidth, 0),
                new Vector2(-50, 0),
            };

            if (this.RightSupportType == "A")
            {
                Beam.I.Dxf.AddEntity(new Line(
                   points[0],
                   points[1]));
            }
            else if (this.RightSupportType == "B")
            {
                Beam.I.Dxf.AddEntity(new Line(
                   points[0],
                   points[2]));
                Beam.I.Dxf.AddEntity(new Line(
                   points[2],
                   points[3]));
            }

            if (this.RightSupportType != "C")
            {
                LinearDimension dim45 = new LinearDimension(
                    points[4],
                    points[5],
                    -20,
                    0,
                    Beam.I.DimensionStyles.General);
                dim45.UserText = "<>";
                Beam.I.Dxf.AddEntity(dim45);
            }
            return Beam.I.Dxf;
        }
        public DxfDocument DrawSectionMarks(List<Section> sections)
        {
            List<LwPolylineVertex> vertexes = new List<LwPolylineVertex>() 
            {
                new LwPolylineVertex(this._Translation + this.LeftSupportWidth + this._SectionMarkOffsetH, Beam.I.Height + 3*this._SectionMarkOffsetV),
                new LwPolylineVertex(this._Translation + this.LeftSupportWidth + this._SectionMarkOffsetH, Beam.I.Height + 1*this._SectionMarkOffsetV),
                new LwPolylineVertex(this._Translation + this.LeftSupportWidth + this._SectionMarkOffsetH, -3*this._SectionMarkOffsetV),
                new LwPolylineVertex(this._Translation + this.LeftSupportWidth + this._SectionMarkOffsetH, -5*this._SectionMarkOffsetV),
                new LwPolylineVertex(this._Translation + this.LeftSupportWidth + this.SpanLength - this._SectionMarkOffsetH, Beam.I.Height + 3*this._SectionMarkOffsetV),
                new LwPolylineVertex(this._Translation + this.LeftSupportWidth + this.SpanLength - this._SectionMarkOffsetH, Beam.I.Height + 1*this._SectionMarkOffsetV),
                new LwPolylineVertex(this._Translation + this.LeftSupportWidth + this.SpanLength - this._SectionMarkOffsetH, -3*this._SectionMarkOffsetV),
                new LwPolylineVertex(this._Translation + this.LeftSupportWidth + this.SpanLength - this._SectionMarkOffsetH, -5*this._SectionMarkOffsetV),
            };

            Vector2[] textLocations =
            {
                new Vector2(this._Translation + this.LeftSupportWidth + this._SectionMarkOffsetH + 2, Beam.I.Height + 2*this._SectionMarkOffsetV - 4),
                new Vector2(this._Translation + this.LeftSupportWidth + this._SectionMarkOffsetH + 2, -4*this._SectionMarkOffsetV - 4),
                new Vector2(this._Translation + this.LeftSupportWidth + this.SpanLength - this._SectionMarkOffsetH + 2, Beam.I.Height + 2*this._SectionMarkOffsetV - 4),
                new Vector2(this._Translation + this.LeftSupportWidth + this.SpanLength - this._SectionMarkOffsetH + 2, -4*this._SectionMarkOffsetV - 4),
            };

            Section sectionL = new Section(
                this._TopLeftRbarDiameter / 10,
                this._NumberOfTopLeftRbars,
                this._BottomRbarDiameter / 10,
                this._NumberOfBottomRbars,
                string.Format("{0} - {0}L", this._Id));

            sections.Add(sectionL);

            Section sectionR = new Section(
                this._TopRightRbarDiameter / 10,
                this._NumberOfTopRightRbars,
                this._BottomRbarDiameter / 10,
                this._NumberOfBottomRbars,
                string.Format("{0} - {0}P", this._Id));

            sections.Add(sectionR);

            if (this.DrawLeftSection == true)
            {
                sectionL.Draw = true;

                LwPolyline markUpL = new LwPolyline(vertexes.GetRange(0, 2));
                markUpL.SetConstantWidth(this._SectionMarkWidth);
                Beam.I.Dxf.AddEntity(markUpL);

                Beam.I.Dxf.AddEntity(new Text(
                    string.Format("{0}L", this._Id),
                    textLocations[0],
                    this._SectionMarkTextHeight));

                LwPolyline markDownL = new LwPolyline(vertexes.GetRange(2, 2));
                markDownL.SetConstantWidth(this._SectionMarkWidth);
                Beam.I.Dxf.AddEntity(markDownL);

                Beam.I.Dxf.AddEntity(new Text(
                    string.Format("{0}L", this._Id),
                    textLocations[1],
                    this._SectionMarkTextHeight));
            }

            if (this.DrawRightSection == true)
            {
                sectionR.Draw = true;

                LwPolyline markUpR = new LwPolyline(vertexes.GetRange(4, 2));
                markUpR.SetConstantWidth(this._SectionMarkWidth);
                Beam.I.Dxf.AddEntity(markUpR);

                Beam.I.Dxf.AddEntity(new Text(
                    string.Format("{0}P", this._Id),
                    textLocations[2],
                    this._SectionMarkTextHeight));

                LwPolyline markDownR = new LwPolyline(vertexes.GetRange(6, 2));
                markDownR.SetConstantWidth(this._SectionMarkWidth);
                Beam.I.Dxf.AddEntity(markDownR);

                Beam.I.Dxf.AddEntity(new Text(
                    string.Format("{0}P", this._Id),
                    textLocations[3],
                    this._SectionMarkTextHeight));
            }
            return Beam.I.Dxf;
        }
        public void CollectBottomRbars(List<Rbar> rbars, List<Section> sections, ref int tmp)
        {
            double mod1 = 0, mod2 = 0, mod3 = 0, mod4 = 0;

            if (this.BottomJoinLeft == true)
            {
                mod1 = this.BottomRbarLeftSupport - this.LeftSupportWidth / 2;
                mod2 = -this.BottomRbarLeftHook;
            }

            if (this.BottomJoinRight == true)
            {
                mod3 = -this.BottomRbarRightSupport + this.RightSupportWidth / 2;
                mod4 = -this.BottomRbarRightHook;
            }

            sections[2 * this.Id - 2].lowerRbarsID = tmp + 1;
            sections[2 * this.Id - 1].lowerRbarsID = tmp + 1;

            if (!this.BottomJoinLeft && !this.BottomJoinRight)
            {
                rbars.Add(new Rbar(
                    this.NumberOfBottomRbars,
                    this.BottomRbarDiameter,
                    this._Translation + this.LeftSupportWidth - this.BottomRbarLeftSupport + mod1,
                    Beam.I.Cover + stirrupDiameter / 10 + this.BottomRbarDiameter / 10 / 2 + this.BottomRbarLeftHook + mod2,
                    this._Translation + this.LeftSupportWidth - this.BottomRbarLeftSupport,
                    Beam.I.Cover + stirrupDiameter / 10 + this.BottomRbarDiameter / 10 / 2,
                    this._Translation + this.LeftSupportWidth + this.SpanLength + this.BottomRbarRightSupport,
                    Beam.I.Cover + stirrupDiameter / 10 + this.BottomRbarDiameter / 10 / 2,
                    this._Translation + this.LeftSupportWidth + this.SpanLength + this.BottomRbarRightSupport + mod3,
                    Beam.I.Cover + stirrupDiameter / 10 + this.BottomRbarDiameter / 10 / 2 + this.BottomRbarRightHook + mod4));
                tmp += 1;
            }
            else if (this.BottomJoinLeft && !this.BottomJoinRight)
            {
                rbars[tmp].X2 = this._Translation + this.LeftSupportWidth + this.SpanLength + this.BottomRbarRightSupport;
                rbars[tmp].Y2 = Beam.I.Cover + stirrupDiameter / 10 + this.BottomRbarDiameter / 10 / 2;
                rbars[tmp].X3 = this._Translation + this.LeftSupportWidth + this.SpanLength + this.BottomRbarRightSupport + mod3;
                rbars[tmp].Y3 = Beam.I.Cover + stirrupDiameter / 10 + this.BottomRbarDiameter / 10 / 2 + this.BottomRbarRightHook + mod4;
                tmp += 1;
            }
            else if (!this.BottomJoinLeft && this.BottomJoinRight)
            {
                rbars.Add(new Rbar(
                    this.NumberOfBottomRbars,
                    this.BottomRbarDiameter,
                    this._Translation + this.LeftSupportWidth - this.BottomRbarLeftSupport + mod1,
                    Beam.I.Cover + stirrupDiameter / 10 + this.BottomRbarDiameter / 10 / 2 + this.BottomRbarLeftHook + mod2,
                    this._Translation + this.LeftSupportWidth - this.BottomRbarLeftSupport,
                    Beam.I.Cover + stirrupDiameter / 10 + this.BottomRbarDiameter / 10 / 2));
            }
        }
        public DxfDocument CollectTopRbarsL(List<Rbar> rbars)
        {
            double mod1 = 0, mod2 = 0, mod3 = 0;

            if (this.Id == 1)
            {
                mod1 = this.LeftSupportWidth / 2 - this.TopLeftRbarLeftOverhang;
            }
            if (this.JoinTop == true)
            {
                mod2 = this.TopLeftRbarRightOverhang + this.SpanLength;
                mod3 = this.TopLeftRbarRightHook;
            }

            if (this.Id == 1)
            {
                rbars.Add(new Rbar(
                    this.NumberOfTopLeftRbars,
                    this.TopLeftRbarDiameter,
                    this._Translation + this.LeftSupportWidth / 2 + mod1,
                    Beam.I.Height - Beam.I.Cover - stirrupDiameter / 10 - this.TopLeftRbarDiameter / 10 / 2 - this.TopLeftRbarLeftHook,
                    this._Translation + this.LeftSupportWidth / 2 + mod1,
                    Beam.I.Height - Beam.I.Cover - stirrupDiameter / 10 - this.TopLeftRbarDiameter / 10 / 2,
                    this._Translation + this.LeftSupportWidth + this.TopLeftRbarRightOverhang,
                    Beam.I.Height - Beam.I.Cover - stirrupDiameter / 10 - this.TopLeftRbarDiameter / 10 / 2,
                    this._Translation + this.LeftSupportWidth + this.TopLeftRbarRightOverhang + mod2,
                    Beam.I.Height - Beam.I.Cover - stirrupDiameter / 10 - this.TopLeftRbarDiameter / 10 / 2 - this.TopLeftRbarRightHook + mod3));

                //newRbar.offset = height + 70;
            }
            return Beam.I.Dxf;
        }
        public DxfDocument CollectTopRbarsR(List<Rbar> rbars, List<Section> sections, ref int tmp)
        {
            double mod1 = 0, mod2 = 0, mod3 = 0;

            if (this.Next == null)
            {
                mod1 = -this.RightSupportWidth / 2 + this.TopRightRbarRightOverhang;
            }

            if (this.JoinTop == true)
            {
                mod2 = this.TopRightRbarLeftOverhang - this.SpanLength / 2;
                mod3 = this.TopRightRbarLeftHook;
            }

            sections[2 * this.Id - 2].topRbarsID = tmp + 1;

            if (!this.JoinTop)
            {
                rbars.Add(new Rbar(
                    this.NumberOfTopRightRbars,
                    this.TopRightRbarDiameter,
                    this._Translation + this.LeftSupportWidth + this.SpanLength - this.TopRightRbarLeftOverhang + mod2,
                    Beam.I.Height - Beam.I.Cover - stirrupDiameter / 10 - this.TopRightRbarDiameter / 10 / 2 - this.TopRightRbarLeftHook + mod3,
                    this._Translation + this.LeftSupportWidth + this.SpanLength - this.TopRightRbarLeftOverhang,
                    Beam.I.Height - Beam.I.Cover - stirrupDiameter / 10 - this.TopRightRbarDiameter / 10 / 2,
                    this._Translation + this.LeftSupportWidth + this.SpanLength + this.RightSupportWidth / 2 + mod1,
                    Beam.I.Height - Beam.I.Cover - stirrupDiameter / 10 - this.TopRightRbarDiameter / 10 / 2,
                    this._Translation + this.LeftSupportWidth + this.SpanLength + this.RightSupportWidth / 2 + mod1,
                    Beam.I.Height - Beam.I.Cover - stirrupDiameter / 10 - this.TopRightRbarDiameter / 10 / 2 - this.TopRightRbarRightHook));

                //newRbar.offset = height + 70;
                tmp += 1;
            }
            else if (this.JoinTop)
            {
                rbars[tmp].X2 = this._Translation + this.LeftSupportWidth + this.SpanLength + this.RightSupportWidth / 2 + mod1;
                rbars[tmp].Y2 = Beam.I.Height - Beam.I.Cover - stirrupDiameter / 10 - this.TopRightRbarDiameter / 10 / 2;
                rbars[tmp].X3 = this._Translation + this.LeftSupportWidth + this.SpanLength + this.RightSupportWidth / 2 + mod1;
                rbars[tmp].Y3 = Beam.I.Height - Beam.I.Cover - stirrupDiameter / 10 - this.TopRightRbarDiameter / 10 / 2 - this.TopRightRbarRightHook;
            }

            sections[2 * this.Id - 1].topRbarsID = tmp + 1;
            return Beam.I.Dxf;
        }
        #endregion

        #region Event Handler
        void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}