using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using System.IO;
using System.Diagnostics;
using org.pdfbox.pdmodel;
using org.pdfbox.util;


namespace PDFFormFilling
{
    public partial class PDFFormGeneration : Form
    {
        public PDFFormGeneration()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string pdfTemplate = @"D:\To Be Converted\sbimf.pdf";
            string newFile = @"D:\Converted\sbimf_filled.pdf";

            parseUsingPDFBox(pdfTemplate);
            PdfReader pdfReader = new PdfReader(pdfTemplate);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            
            pdfFormFields.SetField("Name", txtName.Text.ToString());
            pdfFormFields.SetField("DOB", txtDOB.Text.ToString());
            pdfFormFields.SetField("BranchCode", txtBranchCode.Text.ToString());
            pdfFormFields.SetField("PhoneNo", txtPhone.Text.ToString());
            pdfFormFields.SetField("Email", txtEmail.Text.ToString());
            pdfFormFields.SetField("SubBrockerCode", txtSubBrockerCode.Text.ToString());
            pdfFormFields.SetField("ARNNo", txtARN.Text.ToString());
            pdfFormFields.SetField("Folio", txtFolio.Text.ToString());

            pdfStamper.FormFlattening = false;

            // close the pdf
            pdfStamper.Close();

            Process.Start("D:\\Converted\\sbimf_filled.pdf");
        }
        private static string parseUsingPDFBox(string input)
        {
            string str = "";
            string[] strarr = null;
            PDDocument doc = PDDocument.load(@"D:\To Be Converted\Book1.pdf");
            PDFTextStripper stripper = new PDFTextStripper();
            str = stripper.getText(doc);
            strarr = str.Split('\n');
            MessageBox.Show(str);
            return stripper.getText(doc);
        }
    }
}
