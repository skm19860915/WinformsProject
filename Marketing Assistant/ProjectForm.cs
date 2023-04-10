using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marketing_Assistant
{
    public partial class ProjectForm : Form
    {
        private RequestBO.Project obj = new RequestBO.Project();
        public ProjectForm()
        {
        }

        public ProjectForm(RequestBO.Project obj):this()
        {
            
        }

        public void ProjectForm_Load()
        {
            lbusername.Text = obj.name;
            lbcontact.Text = obj.code;
            int length = obj.company_name.Length;
            for (int i = 0; i < length; i++)
            {
                lbproject.Text = lbproject.Text + obj.company_name[i];
            }
            lbwebsite.Text = obj.website;
            int lth = obj.company_name.Length;
            for (int i = 0; i < lth; i++)
            {
                lbemail.Text = lbemail.Text + "," + obj.contact_email[i];
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
           
            ////Task<bool> status = request.ProjectAuth();

            //if (status.Result == true)
            //{
            //    this.Hide();
            //    ProjectForm form = new ProjectForm();
            //    form.Show();
            //}
            //else
            //{

            //}
        }

        private void ProjectForm_Load(object sender, EventArgs e)
        {

        }
    }
}
