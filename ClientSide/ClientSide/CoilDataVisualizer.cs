using DataCompression;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ClientSide
{
    public partial class CoilDataVisualizer : Form
    {
        private CoilDataSelectionPopUp coilDataSelectionPopUp;
        DataAccess dataAccess;

        public CoilDataVisualizer()
        {
            InitializeComponent();
            ChartLayout();
            dataAccess = new DataAccess();
        }

        private void ChartLayout()
        {
            cht1.ChartAreas[0].AxisX.InterlacedColor = Color.Black;
            cht1.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            cht1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            cht1.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.IncreaseFont | LabelAutoFitStyles.WordWrap;
            cht1.ChartAreas[0].CursorX.IsUserEnabled = true;
            cht1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            cht1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            cht1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            cht1.ChartAreas[0].AxisY.ScrollBar.Enabled = true;
            cht1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
            cht1.ChartAreas[0].AxisY.LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.IncreaseFont | LabelAutoFitStyles.WordWrap;
            cht1.ChartAreas[0].CursorY.IsUserEnabled = true;
            cht1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            cht1.ChartAreas[0].AxisY.ScaleView.Zoomable = false;
            cht1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
            cht1.ChartAreas[0].AxisX.IsMarginVisible = true;

            var cursorY = cht1.ChartAreas[0].CursorY;

            cursorY.LineColor = Color.Transparent;
            cursorY.SelectionColor = Color.SlateGray;

            cht1.ChartAreas[0].CursorX.SelectionColor = Color.SlateGray;
            var cursorX = cht1.ChartAreas[0].CursorX;

            cursorX.LineDashStyle = ChartDashStyle.Dash;
            cursorX.LineColor = Color.White;

            cht1.ChartAreas[0].CursorX.SelectionColor = Color.SlateGray;
            cht1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb( 30, 33, 36 );

            cht1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb( 30, 33, 36 );
            cht1.ChartAreas[0].CursorY.SelectionColor = Color.SlateGray;

            BuildLegendLayout( cht1 );
        }

        private void BuildLegendLayout( Chart cht )
        {
            cht.Legends[0].Position.Auto = false;
            cht.Legends[0].Position.X = 0;
            cht.Legends[0].Position.Y = 100;
            cht.Legends[0].Position.Width = 100;
            cht.Legends[0].Position.Height = 15;
            cht.Legends[0].ForeColor = Color.White;
            cht.Legends[0].BackColor = Color.LightSlateGray;
            cht.Legends[0].TitleFont = new Font( "Microsoft Sans Serif", 12F, FontStyle.Bold );
        }

        private void coilDataSelection_OnCoilSelected( object sender, EventArgs e )
        {
            var coilCode = sender.ToString();

            var dtSelectedCoil = Blob.ConvertToDataSet((byte[])dataAccess.GetCoilDataBlob(coilCode).Tables[0].Rows[0][0]).Tables[0];

            if ( dtSelectedCoil.Rows.Count <= 0 )
            {
                MessageBox.Show( $"Sem informações para a bobina {coilCode}", "Atenção", MessageBoxButtons.OK );
                return;
            }

            cht1.Series.Clear();

            int r = 0;
            int g = 250;
            foreach ( DataColumn dc in dtSelectedCoil.Columns )
            {
                if ( dc.ColumnName != "Timestamp" )
                {
                    var serie = new Series
                    {
                        Name = dc.ColumnName,
                        ChartArea = cht1.ChartAreas[0].Name,
                        ChartType = SeriesChartType.Line,
                        Color = Color.FromArgb(r,g,200)
                    };
                    cht1.Series.Add( serie );
                    r += 30;
                    g -= 30;

                    foreach ( DataRow dr in dtSelectedCoil.Rows )
                    {
                        try
                        {
                            cht1.Series[dc.ColumnName].Points.AddXY( dr["Timestamp"].ToString(), dr[dc.ColumnName] );
                        }
                        catch ( Exception ex )
                        {
                            MessageBox.Show( $"Sinal: {dc.ColumnName} não encontrado", "Atenção", MessageBoxButtons.OK );
                            break;
                        }

                    }
                }
            }

            lblCoil.Text = coilCode;
        }

        private void cht_MouseUp( object sender, MouseEventArgs e )
        {
            var cht = (Chart)sender;
            var XclickedValue = cht.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
            var YclickedValue = cht.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);

            if ( cht.Series.Any() )
            {
                foreach ( Series serie in cht.Series )
                {
                    if ( XclickedValue >= cht.ChartAreas[0].AxisX.Minimum &&
                        XclickedValue <= cht.ChartAreas[0].AxisX.Maximum &&
                        YclickedValue >= cht.ChartAreas[0].AxisY.Minimum &&
                        YclickedValue <= cht.ChartAreas[0].AxisY.Maximum &&
                        Convert.ToInt32( XclickedValue ) > 0 &&
                        cht.Series[0].Points.Count > Convert.ToInt32( XclickedValue ) )
                    {
                        var YclickedPointValue =decimal.Round(Convert.ToDecimal(cht.Series[serie.Name].Points[Convert.ToInt32(XclickedValue) - 1].YValues.First()), 1);
                        cht.Series[serie.Name].LegendText = $"{serie.Name} [{YclickedPointValue}]";
                    }
                }
            }
        }

        private void SignalSelection_OnSignalsAdd( object sender, EventArgs e )
        {
            var series = (Dictionary<string, object>)sender;
            var idChart = Convert.ToInt32(series["idChart"]);
            var Signal = series["Signals"].ToString();
            var Color = Convert.ToInt32(series["Color"]);
        }

        private void btnFilter_Click( object sender, EventArgs e )
        {
            if ( coilDataSelectionPopUp == null || coilDataSelectionPopUp.IsDisposed )
            {
                coilDataSelectionPopUp = new CoilDataSelectionPopUp();
                coilDataSelectionPopUp.OnCoilSelected += coilDataSelection_OnCoilSelected;
                coilDataSelectionPopUp.Show();
            }
            else
            {
                coilDataSelectionPopUp.BringToFront();
            }
        }
    }
}
