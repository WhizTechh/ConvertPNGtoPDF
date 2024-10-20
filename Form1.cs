//By WhizTech

using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertPNGtoPDF
{
    public partial class Form1 : Form
    {
        string pngfilepath = @"";
        string pdfoutputfilename = @"pdf"; // Can't be empty

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // Select File
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PNG Files (*.png)|*.png";
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                pngfilepath = ofd.FileName;
                selectedFilePathTxtbox.Text = ofd.FileName;
                pictureBox1.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e) // Download PDF
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF Files (*.pdf)|*.pdf";
                sfd.Title = "Save PDF File";

                string pngfilepath_without_pngfileformatname = pngfilepath.Replace(".png", "");
                sfd.FileName = Path.GetFileName(pngfilepath_without_pngfileformatname + ".pdf");
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(pdfoutputfilename, sfd.FileName, true);
                }
                MessageBox.Show("PDF File successfully downloaded!");
            }
            catch (Exception ex) {
                MessageBox.Show("Error: "+ex);
            }
        }

        private void button3_Click(object sender, EventArgs e) // Convert File
        {
            try
            {
                PdfWriter writer = new PdfWriter(pdfoutputfilename);
                PdfDocument pdfdoc = new PdfDocument(writer);

                Document doc = new Document(pdfdoc);
                ImageData imgdata = ImageDataFactory.Create(pngfilepath);

                iText.Layout.Element.Image img = new iText.Layout.Element.Image(imgdata);
                doc.Add(img);
                doc.Close();
                MessageBox.Show("File successfully converted!");
            }
            catch (Exception ex) {
                MessageBox.Show("Error: "+ex);
            }
        }

    }
}
