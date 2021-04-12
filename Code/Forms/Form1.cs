using SQLite.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLiteContacts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindGrid();
        }
        public void BindGrid()
        {
            dgList.AutoGenerateColumns = false;
            using (UnitOfWork db = new UnitOfWork())
            {
                dgList.DataSource = db.Repository.GetAll();
            }
        }

        private void btnBind_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgList.CurrentRow != null)
            {
                using(UnitOfWork db = new UnitOfWork())
                {
                    db.Repository.Delete(int.Parse(dgList.CurrentRow.Cells["ID"].Value.ToString()));
                    BindGrid();
                    db.Save();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Forms.frmAddOrEdit frm = new Forms.frmAddOrEdit();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                BindGrid();
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgList.CurrentRow != null)
            {
                Forms.frmAddOrEdit frm = new Forms.frmAddOrEdit();
                frm.ID = int.Parse(dgList.CurrentRow.Cells["ID"].Value.ToString());
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    BindGrid();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if(txtSearch.Text != "")
            {
                using (UnitOfWork db = new UnitOfWork())
                {
                    dgList.DataSource = db.Repository.GetByFilter(txtSearch.Text);
                }
            }
            else
            {
                BindGrid();
            }
        }
    }
    }

