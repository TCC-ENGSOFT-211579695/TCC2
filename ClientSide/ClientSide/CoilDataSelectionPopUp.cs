using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientSide
{
    public partial class CoilDataSelectionPopUp : Form
    {
        public event EventHandler OnCoilSelected;
        private DataAccess dataAccess;

        public CoilDataSelectionPopUp()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            loadSelectionGrid();
        }

        private void btnCancelar_Click( object sender, EventArgs e )
        {
            Close();
        }

        private void buildgrdCoils( DataTable PrimaryData )
        {
            grdCoils.Columns.Clear();
            grdCoils.Rows.Clear();
            grdCoils.Columns.Add( "CoilCode", "Bobina" );
            grdCoils.Columns.Add( "TimeStamp", "Fim Prod." );
            if ( PrimaryData != null )
            {
                foreach ( DataRow dr in PrimaryData.Rows )
                {
                    grdCoils.Rows.Add( dr.ItemArray );
                }
            }
        }

        private void loadSelectionGrid()
        {
            buildgrdCoils( dataAccess.GetCoils().Tables[0] );
        }

        private void loadSelectionGrid( string coilCode )
        {
            buildgrdCoils( dataAccess.GetCoilBin(coilCode).Tables[0] );
        }

        private void btnSearch_Click( object sender, EventArgs e )
        {
            if ( !string.IsNullOrEmpty( txtCoil.Text ) )
            {
                loadSelectionGrid( txtCoil.Text ); return;
            }
            loadSelectionGrid();

        }

        private void btnSelect_Click( object sender, EventArgs e )
        {
            if ( grdCoils.SelectedRows.Count > 0 )
                OnCoilSelected?.Invoke( grdCoils?.SelectedRows?[0].Cells["CoilCode"].Value, e );
        }

        private void txtCoil_KeyUp( object sender, KeyEventArgs e )
        {
            FilterDataGridView( txtCoil, grdCoils );
        }        

        private void FilterDataGridView( TextBox textBox, DataGridView grdToFilter )
        {
            if ( textBox.Text.Trim().Any() )
            {
                foreach ( DataGridViewRow dgvr in grdToFilter.Rows )
                {
                    grdToFilter.Rows[dgvr.Index].Visible =
                        dgvr.Cells["CoilCode"].Value.ToString().Contains( textBox.Text.ToUpper().Trim() );
                }
            }
            else
            {
                foreach ( DataGridViewRow dgvr in grdToFilter.Rows )
                {
                    grdToFilter.Rows[dgvr.Index].Visible = true;
                }
            }
        }
    }
}