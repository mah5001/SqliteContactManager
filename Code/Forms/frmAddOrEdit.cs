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

namespace SQLiteContacts.Forms
{
    public partial class frmAddOrEdit : Form
    {
        public int ID = 0;
        public frmAddOrEdit()
        {
            InitializeComponent();
        }

        private void frmAddOrEdit_Load(object sender, EventArgs e)
        {
            if (ID == 0)
            {
                this.Text = "Add New";
            }
            else
            {
                using(UnitOfWork db = new UnitOfWork())
                {
                    var t = db.Repository.GetByID(ID);
                    this.Text = $"Edit {t.FullName}";
                    txtN.Text = t.FullName;
                    txtP.Text = t.Phone;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var t = new Test() 
            {
                ID=ID,FullName=txtN.Text,Phone=txtP.Text
            };
            using (UnitOfWork db = new UnitOfWork())
            {
                if (ID == 0)
                {
                    db.Repository.Add(t);
                }
                else
                {
                    db.Repository.Update(t);
                }
                db.Save();
            }
            DialogResult = DialogResult.OK;
        }
    }
}
