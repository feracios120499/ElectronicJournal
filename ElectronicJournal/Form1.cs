using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace ElectronicJournal
{
    public partial class Form1 : Form
    {
        SQLiteConnection sqlConnection = new SQLiteConnection("Data Source=journal.db");
        public Form1()
        {
            InitializeComponent();
        }
        private void LoadGroups()
        {
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Groups", sqlConnection);
            SQLiteDataReader dr = command.ExecuteReader();
            ClearGroup();
            while (dr.Read())
            {
                FillGroup(dr["id"].ToString(), dr["Name"].ToString());
            }
            sqlConnection.Close();
        }
        private void ClearGroup()
        {
            listBoxGroup.Items.Clear();
            comboBoxGroupsForStud.Items.Clear();
            comboBoxGroupsForMiss.Items.Clear();
            comboBoxGroupForRating.Items.Clear();
            comboBoxGroupForJournal.Items.Clear();
        }
        private void FillGroup(string id, string Name)
        {
            listBoxGroup.Items.Add(new Group(id, Name));
            comboBoxGroupsForStud.Items.Add(new Group(id, Name));
            comboBoxGroupsForMiss.Items.Add(new Group(id, Name));
            comboBoxGroupForRating.Items.Add(new Group(id, Name));
            comboBoxGroupForJournal.Items.Add(new Group(id, Name));
        }
        private void AddGroup(string Name)
        {
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("INSERT INTO Groups(Name) VALUES(@Name)", sqlConnection);
            command.Parameters.AddWithValue("Name", Name);
            command.ExecuteNonQuery();
            sqlConnection.Close();
            LoadGroups();
        }
        private void DeleteGroup(string idGroup)
        {
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("DELETE FROM Groups WHERE id=@id", sqlConnection);
            command.Parameters.AddWithValue("id", idGroup);
            command.ExecuteNonQuery();
            sqlConnection.Close();
            LoadGroups();
        }


        //Subject
        private void LoadSubject()
        {
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Subjects", sqlConnection);
            SQLiteDataReader dr = command.ExecuteReader();
            ClearSubject();
            while (dr.Read())
            {
                FillSubject(dr["id"].ToString(), dr["Name"].ToString());
            }
            sqlConnection.Close();
        }
        private void ClearSubject()
        {
            listBoxSubjects.Items.Clear();
            comboBoxSubForMiss.Items.Clear();
            comboBoxSubForRating.Items.Clear();
            comboBoxSubForJournal.Items.Clear();
        }
        private void FillSubject(string id, string Name)
        {
            listBoxSubjects.Items.Add(new Subject(id, Name));
            comboBoxSubForMiss.Items.Add(new Subject(id, Name));
            comboBoxSubForRating.Items.Add(new Subject(id, Name));
            comboBoxSubForJournal.Items.Add(new Subject(id, Name));
        }
        private void AddSubject(string Name)
        {
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("INSERT INTO Subjects(Name) VALUES(@Name)", sqlConnection);
            command.Parameters.AddWithValue("Name", Name);
            command.ExecuteNonQuery();
            sqlConnection.Close();
            LoadSubject();
        }
        private void DeleteSubject(string id)
        {
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("DELETE FROM Subjects WHERE id=@id", sqlConnection);
            command.Parameters.AddWithValue("id", id);
            command.ExecuteNonQuery();
            sqlConnection.Close();
            LoadSubject();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Введіть назву групи", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                return;
            }
            AddGroup(textBox1.Text);
            textBox1.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadGroups();
            LoadSubject();
        }
        private void DeleteStudent(string idStud)
        {
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("DELETE FROM Students WHERE id=@id", sqlConnection);
            command.Parameters.AddWithValue("id", idStud);
            command.ExecuteNonQuery();
            sqlConnection.Close();
            comboBoxGroupsForStud_SelectedIndexChanged(comboBoxGroupsForStud, null);
        }
        private void buttonAddStudent_Click(object sender, EventArgs e)
        {
            var group = comboBoxGroupsForStud.SelectedItem as Group;
            if (group == null || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Введіть ПІБ студента", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
                return;
            }
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("INSERT INTO Students(Name,IdGroup) VALUES(@Name,@id)", sqlConnection);
            command.Parameters.AddWithValue("Name", textBox2.Text);
            command.Parameters.AddWithValue("id", group.id);
            command.ExecuteNonQuery();
            sqlConnection.Close();
            comboBoxGroupsForStud_SelectedIndexChanged(comboBoxGroupsForStud, null);
            textBox2.Clear();
        }

        private void comboBoxGroupsForStud_SelectedIndexChanged(object sender, EventArgs e)
        {
            var group = comboBoxGroupsForStud.SelectedItem as Group;
            if (group == null)
            {
                return;
            }
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Students where IdGroup=@id", sqlConnection);
            command.Parameters.AddWithValue("id", group.id);
            SQLiteDataReader dr = command.ExecuteReader();
            listBoxStudents.Items.Clear();
            while (dr.Read())
            {
                listBoxStudents.Items.Add(new Students(dr["id"].ToString(), dr["Name"].ToString()));
            }
            sqlConnection.Close();
        }

        private void buttonGroup_Click(object sender, EventArgs e)
        {
            panelGroups.BringToFront();
        }

        private void buttonStudents_Click(object sender, EventArgs e)
        {
            panelStudents.BringToFront();
        }

        private void buttonSubject_Click(object sender, EventArgs e)
        {
            panelSubjects.BringToFront();
        }

        private void buttonAddSubject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSubject.Text) || string.IsNullOrWhiteSpace(textBoxSubject.Text))
            {
                MessageBox.Show("Введіть назву предмету", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxSubject.Focus();
                return;
            }
            AddSubject(textBoxSubject.Text);
            textBoxSubject.Clear();
        }

        private void buttonMiss_Click(object sender, EventArgs e)
        {
            panelMiss.BringToFront();
        }

        private void comboBoxGroupsForMiss_SelectedIndexChanged(object sender, EventArgs e)
        {
            var group = comboBoxGroupsForMiss.SelectedItem as Group;
            if (group == null)
            {
                return;
            }
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Students where IdGroup=@id", sqlConnection);
            command.Parameters.AddWithValue("id", group.id);
            SQLiteDataReader dr = command.ExecuteReader();
            listBoxStudentsForMiss.Items.Clear();
            while (dr.Read())
            {
                listBoxStudentsForMiss.Items.Add(new Students(dr["id"].ToString(), dr["Name"].ToString()));
            }
            sqlConnection.Close();
        }

        private void buttonAddMiss_Click(object sender, EventArgs e)
        {
            
            var group = comboBoxGroupsForMiss.SelectedItem as Group;
            var sub = comboBoxSubForMiss.SelectedItem as Subject;
            var date = dateTimePickerForMiss.Value;
            List<Students> students = new List<Students>();
            foreach (Students item in listBoxStudentsForMiss.SelectedItems)
            {
                students.Add(item);
            }
            if (group == null)
            {
                MessageBox.Show("Оберіть групу", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (sub == null)
            {
                MessageBox.Show("Оберіть предмет", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (students.Count == 0)
            {
                MessageBox.Show("Оберіть студентів", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<Students> studentForRemove = new List<Students>();
            //Проверяем не стоит ли оценка за эту дату и за этот предмет
            {
                foreach (var item in students)
                {
                    sqlConnection.Open();
                    SQLiteCommand command = new SQLiteCommand("SELECT * FROM Marks where IdStudent=@idStud and IdSubject=@idSub and DateMark=@date", sqlConnection);
                    command.Parameters.AddWithValue("idStud", item.id);
                    command.Parameters.AddWithValue("idSub", sub.id);
                    command.Parameters.AddWithValue("date", Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
                    SQLiteDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["Mark"].ToString() != "н/б")
                        {
                            if(MessageBox.Show($"У студента -{item.Name}- стоїть оцінка за цю дату та предмет,замінити?", "Попередження", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                SQLiteCommand command1 = new SQLiteCommand("UPDATE Marks set Mark=@mark where IdStudent=@id", sqlConnection);
                                command1.Parameters.AddWithValue("mark", "н/б");
                                command1.Parameters.AddWithValue("id", item.id);
                                command1.ExecuteNonQuery();
                            }
                        }
                        studentForRemove.Add(item);
                    }
                    sqlConnection.Close();
                }
                foreach(var item in studentForRemove)
                {
                    students.Remove(item);
                }
            }
            {
                sqlConnection.Open();
                foreach(var item in students)
                {
                    SQLiteCommand command = new SQLiteCommand("insert into Marks(IdGroup,IdStudent,IdSubject,Mark,DateMark) VALUES (@idGroup,@idStud,@idSub,'н/б',@date)", sqlConnection);
                    command.Parameters.AddWithValue("idGroup", group.id);
                    command.Parameters.AddWithValue("idStud", item.id);
                    command.Parameters.AddWithValue("idSub", sub.id);
                    command.Parameters.AddWithValue("date", Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
                    command.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
            MessageBox.Show("Відмітки про пропуск додані", "Доданно", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonSetRating_Click(object sender, EventArgs e)
        {
            panelSetRating.BringToFront();
        }

        private void textBoxRating_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void comboBoxGroupForRating_SelectedIndexChanged(object sender, EventArgs e)
        {
            var group = comboBoxGroupForRating.SelectedItem as Group;
            if (group == null)
            {
                return;
            }
            sqlConnection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Students where IdGroup=@id", sqlConnection);
            command.Parameters.AddWithValue("id", group.id);
            SQLiteDataReader dr = command.ExecuteReader();
            listBoxStudentForRating.Items.Clear();
            while (dr.Read())
            {
                listBoxStudentForRating.Items.Add(new Students(dr["id"].ToString(), dr["Name"].ToString()));
            }
            sqlConnection.Close();
        }

        private void buttonAddRating_Click(object sender, EventArgs e)
        {
            var group = comboBoxGroupForRating.SelectedItem as Group;
            var sub = comboBoxSubForRating.SelectedItem as Subject;
            var date = dateTimePickerForRating.Value;
            List<Students> students = new List<Students>();
            foreach (Students item in listBoxStudentForRating.SelectedItems)
            {
                students.Add(item);
            }
            if (group == null)
            {
                MessageBox.Show("Оберіть групу", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (sub == null)
            {
                MessageBox.Show("Оберіть предмет", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (students.Count == 0)
            {
                MessageBox.Show("Оберіть студентів", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxRating.Text) || string.IsNullOrWhiteSpace(textBoxRating.Text))
            {
                MessageBox.Show("Введіть оцінку", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<Students> studentForRemove = new List<Students>();
            //Проверяем не стоит ли оценка за эту дату и за этот предмет
            {
                foreach (var item in students)
                {
                    sqlConnection.Open();
                    SQLiteCommand command = new SQLiteCommand("SELECT * FROM Marks where IdStudent=@idStud and IdSubject=@idSub and DateMark=@date", sqlConnection);
                    command.Parameters.AddWithValue("idStud", item.id);
                    command.Parameters.AddWithValue("idSub", sub.id);
                    command.Parameters.AddWithValue("date", Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
                    SQLiteDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {

                            if (MessageBox.Show($"У студента -{item.Name}- стоїть оцінка або пропуск за цю дату та предмет,замінити?", "Попередження", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                SQLiteCommand command1 = new SQLiteCommand("UPDATE Marks set Mark=@mark where IdStudent=@id", sqlConnection);
                                command1.Parameters.AddWithValue("mark", textBoxRating.Text);
                                command1.Parameters.AddWithValue("id", item.id);
                                command1.ExecuteNonQuery();
                            
                        }
                        studentForRemove.Add(item);
                    }
                    sqlConnection.Close();
                }
                foreach (var item in studentForRemove)
                {
                    students.Remove(item);
                }
            }
            {
                sqlConnection.Open();
                foreach (var item in students)
                {
                    SQLiteCommand command = new SQLiteCommand("insert into Marks(IdGroup,IdStudent,IdSubject,Mark,DateMark) VALUES (@idGroup,@idStud,@idSub,@mark,@date)", sqlConnection);
                    command.Parameters.AddWithValue("idGroup", group.id);
                    command.Parameters.AddWithValue("idStud", item.id);
                    command.Parameters.AddWithValue("idSub", sub.id);
                    command.Parameters.AddWithValue("mark", textBoxRating.Text);
                    command.Parameters.AddWithValue("date", Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
                    command.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
            MessageBox.Show("Оцінки виставлені", "Доданно", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void butttonJournal_Click(object sender, EventArgs e)
        {
            panelJournal.BringToFront();
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            var group = comboBoxGroupForJournal.SelectedItem as Group;
            var sub = comboBoxSubForJournal.SelectedItem as Subject;
            var from = dateTimePickerFrom.Value;
            var to = dateTimePickerTo.Value;
            if (group == null)
            {
                MessageBox.Show("Оберіть групу", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (sub == null)
            {
                MessageBox.Show("Оберіть предмет", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (from > to)
            {
                MessageBox.Show("Початкова дата повина бути менше ніж кінцева", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<Marks> marks = new List<Marks>();
            List<Students> students = new List<Students>();
            {
                sqlConnection.Open();
                SQLiteCommand command = new SQLiteCommand("select * from Marks where DateMark between @dateFrom and @dateTo and IdGroup=@idGroup and IdSubject=@idSub order by DateMark", sqlConnection);
                command.Parameters.AddWithValue("dateFrom", Convert.ToDateTime(from).ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("dateTo", Convert.ToDateTime(to).ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("idGroup", group.id);
                command.Parameters.AddWithValue("idSub", sub.id);
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    marks.Add(new Marks(dr["Mark"].ToString(), Convert.ToDateTime(dr["DateMark"]), dr["IdStudent"].ToString()));
                }
                sqlConnection.Close();
            }
            {
                sqlConnection.Open();
                SQLiteCommand command = new SQLiteCommand("SELECT * FROM Students where IdGroup=@id", sqlConnection);
                command.Parameters.AddWithValue("id", group.id);
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    students.Add(new Students(dr["id"].ToString(),dr["Name"].ToString()));
                }
                sqlConnection.Close();
            }
            Journal journal = new Journal(students, marks);
            journal.ShowDialog();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDeleteGroup_Click(object sender, EventArgs e)
        {
            if (listBoxGroup.SelectedIndex >= 0)
            {
                DeleteGroup((listBoxGroup.SelectedItem as Group).id);
            }
            else
            {
                MessageBox.Show("Оберіть группу", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDeleteStudent_Click(object sender, EventArgs e)
        {

            if (listBoxStudents.SelectedIndex >= 0)
            {
                DeleteStudent((listBoxStudents.SelectedItem as Students).id);
            }
            else
            {
                MessageBox.Show("Оберіть студента", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDeleteSubject_Click(object sender, EventArgs e)
        {
            if (listBoxSubjects.SelectedIndex >= 0)
            {
                DeleteSubject((listBoxSubjects.SelectedItem as Subject).id);
            }
            else
            {
                MessageBox.Show("Оберіть предмет", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
