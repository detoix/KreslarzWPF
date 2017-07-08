using KreslarzWPF.Helpers;
using KreslarzWPF.Model;
using netDxf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace KreslarzWPF.ViewModel
{
    class ViewModel : INotifyPropertyChanged
    {
        #region Properties and fields
        Span _SelectedSpan;
        public Span SelectedSpan 
        { 
            get
            {
                return _SelectedSpan;
            }
            set
            {
                if (_SelectedSpan != value)
                {
                    _SelectedSpan = value;
                    RaisePropertyChanged("SelectedSpan");
                }
            }
        }
        public Beam Beam { get; set; }
        public Stirrup Stirrup { get; set; }
        public ObservableCollection<Span> Spans { get; set; }
        public RelayCommand AddSpanCommand { get; set; }
        public RelayCommand DeleteSpanCommand { get; set; }
        public RelayCommand DrawCommand { get; set; }
        #endregion

        #region Constructors
        public ViewModel()
        {
            this.Beam = Beam.Create();
            this.Stirrup = Stirrup.Create();
            this.Spans = new ObservableCollection<Span> { new Span() };
            this.SelectedSpan = this.Spans[0];
            this.AddSpanCommand = new RelayCommand(AddSpan);
            this.DeleteSpanCommand = new RelayCommand(DeleteSpan);
            this.DrawCommand = new RelayCommand(Draw);
        }
        #endregion

        #region Methods
        void AddSpan(object parameter)
        {
            Spans.Add(new Span(Spans.LastOrDefault()));
        }
        void DeleteSpan(object parameter)
        {
            if (Spans.Count > 1)
            {
                Spans.Remove(Spans.LastOrDefault());
                SelectedSpan = Spans.LastOrDefault();
            }
        }
        void Draw(object parameter)
        {
            System.Windows.Forms.FolderBrowserDialog fileLocation
                = new System.Windows.Forms.FolderBrowserDialog();

            if (fileLocation.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            Beam.I.Dxf = new DxfDocument();
            var bottomRbars = new List<Rbar>();
            var bottomRbarsTmp = 0;
            var topRbars = new List<Rbar>();
            int topRbarsTmp = 0;
            var sections = new List<Section>();

            foreach (Span span in Spans)
            {
                span.GetTranslation(span);

                if (span.Id == 1)
                {
                    Beam.I.Dxf = span.DrawStart();
                }
                Beam.I.Dxf = span.DrawSideView();
                if (span.Next == null)
                {
                    Beam.I.Dxf = span.DrawEnd();
                }
                Beam.I.Dxf = span.DrawSectionMarks(sections);
                span.CollectBottomRbars(bottomRbars, sections, ref bottomRbarsTmp);
                span.CollectTopRbarsL(topRbars);
                span.CollectTopRbarsR(topRbars, sections, ref topRbarsTmp);
            }

            int tmp = 1;
            foreach (Rbar rbar in bottomRbars)
            {
                rbar.DrawRbar(tmp, true, 0);
                rbar.DrawRbar(tmp, false, -70);
                tmp += 1;
            }

            foreach (Rbar rbar in topRbars)
            {
                rbar.DrawRbar(tmp, true, 0);
                rbar.DrawRbar(tmp, false, Beam.I.Height + 70);
                tmp += 1;
            }

            try
            {
                Beam.I.Dxf.Save(String.Format("{0}/{1}.dxf",
                    fileLocation.SelectedPath,
                    Beam.I.Name));
                MessageBox.Show("Gotowe!");
            }
            catch
            {
                MessageBox.Show("Najpierw zamknij plik rysunku!");
            }
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