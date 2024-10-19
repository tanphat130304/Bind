using Bind;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Bind
{
    public partial class Form1 : Form
    {
        private BindingSource bindingSource = new BindingSource(); // Khởi tạo BindingSource
        private SchoolContext context = new SchoolContext(); // Đối tượng DbContext

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadMajors();
            dgv1.CellClick += dgv1_CellClick;

        }

        // Nạp dữ liệu sinh viên từ cơ sở dữ liệu vào DataGridView
        private void LoadData()
        {
            // Gắn nguồn dữ liệu từ cơ sở dữ liệu vào BindingSource
            bindingSource.DataSource = context.Students.ToList();

            // Gắn BindingSource vào DataGridView
            dgv1.DataSource = bindingSource;
        }

        // Nạp danh sách các ngành học vào ComboBox
        private void LoadMajors()
        {
            List<string> majors = new List<string>
            {
                "Công nghệ thông tin",
                "Kỹ thuật điện",
                "Kinh doanh",
                "Quản trị nhân sự",
                "Ngôn ngữ Anh"
            };

            cmbMajor.DataSource = majors;
        }

        // Thêm sinh viên mới
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtAge.Text, out int age))
            {
                var student = new Students
                {
                    FullName = txtFullName.Text,
                    Age = age,
                    Major = cmbMajor.SelectedItem?.ToString() ?? "N/A"
                };

                context.Students.Add(student);
                context.SaveChanges();
                LoadData(); // Cập nhật lại DataGridView
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số cho tuổi.");
            }
        }

        // Xóa sinh viên đang được chọn
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgv1.CurrentRow != null)
            {
                var student = dgv1.CurrentRow.DataBoundItem as Students;
                if (student != null)
                {
                    context.Students.Remove(student);
                    context.SaveChanges();
                    LoadData();
                }
            }
        }

        // Sửa thông tin sinh viên đang được chọn
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgv1.CurrentRow != null)
            {
                var student = dgv1.CurrentRow.DataBoundItem as Students;
                if (student != null && int.TryParse(txtAge.Text, out int age))
                {
                    student.FullName = txtFullName.Text;
                    student.Age = age;
                    student.Major = cmbMajor.SelectedItem?.ToString() ?? "N/A";
                    context.SaveChanges();
                    LoadData();
                }
            }
        }

        // Hiển thị thông tin sinh viên khi chọn một dòng trong DataGridView
        private void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy dòng hiện tại
                DataGridViewRow row = dgv1.Rows[e.RowIndex];

                // Lấy dữ liệu từ dòng và cập nhật các ô text và combobox
                txtFullName.Text = row.Cells["FullName"].Value.ToString(); 
                txtAge.Text = row.Cells["Age"].Value.ToString();          
                cmbMajor.SelectedItem = row.Cells["Major"].Value.ToString(); 
            }
        }

        // Điều hướng sinh viên: nút "Next"
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (bindingSource.Position < bindingSource.Count - 1)
            {
                bindingSource.MoveNext();
                UpdateTextBoxes();
            }
        }

        // Điều hướng sinh viên: nút "Previous"
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (bindingSource.Position > 0)
            {
                bindingSource.MovePrevious();
                UpdateTextBoxes();
            }
        }

        // Cập nhật các ô text dựa trên sinh viên hiện tại
        private void UpdateTextBoxes()
        {
            var student = bindingSource.Current as Students;
            if (student != null)
            {
                txtFullName.Text = student.FullName;
                txtAge.Text = student.Age.ToString();
                cmbMajor.SelectedItem = student.Major;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
