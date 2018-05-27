using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectronicJournal
{
    public partial class Journal : Form
    {
        public Journal()
        {
            InitializeComponent();
        }
        public Journal(List<Students> students,List<Marks> marks)
        {
            InitializeComponent();
            int i = 0;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            foreach (var item in students)
            {
                
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = item.Name;
                dataGridView1[0, i].Tag = item.id;
                
                i++;
            }
            string date=null;
            i = 0;
            foreach(var item in marks)
            {
                if (date != item.Date.ToShortDateString())
                {
                    date = item.Date.ToShortDateString();
                    dataGridView1.Columns.Add(date, date);
                    i++;
                    dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                for(int j = 0; j < dataGridView1.RowCount-1; j++)
                {
                    if (dataGridView1[0, j].Tag.ToString() == item.IdStudent)
                    {
                        dataGridView1[i,j].Value = item.Mark;
                        break;
                    }
                }

            }
        }

        private void Journal_Load(object sender, EventArgs e)
        {
            
        }
    }
}
