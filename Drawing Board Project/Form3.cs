using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace projekt3
{
    public partial class ProjectN3_68932 : Form
    {
        
        Graphics VDRysownica;
        Pen VDPióro;
        SolidBrush VDPędzel;
        Point VDPunkt;
        struct VDOpisKrzywejBeziera
        {
            public Point VDPunktP0;
            public Point VDPunktP1;
            public Point VDPunktP2;
            public Point VDPunktP3;
            public ushort VDNumerPunktuKontrolnego;
            public float VDPromienPunktuKontrolnego;
        }

        VDOpisKrzywejBeziera VDKrzywaBeziera;

        struct VDOpisKrzywejKardynalnej
        {
            public Point VDPunktP0;
            public Point VDPunktP1;
            public Point VDPunktP2;
            public Point VDPunktP3;
            public Point VDPunktP4;
            public ushort VDNumerPunktu;
            public float VDPromienPunktu;
        }
        
        VDOpisKrzywejKardynalnej VDKrzywaKard;
        
        Font VDFontOpisuPunktów = new Font("TimesNewRoman", 14, FontStyle.Regular);

        public ProjectN3_68932()
        {
            InitializeComponent();
            this.Text = "Project_3";

            VDlblX.Text = "0";
            VDlblY.Text = "0";

            VDpbRysownica.Image = new Bitmap(VDpbRysownica.Width, VDpbRysownica.Height);
            VDRysownica = Graphics.FromImage(VDpbRysownica.Image);

            VDpbRysownica.MouseDown += VDpbRysownica_MouseDown;
            VDpbRysownica.MouseUp += VDpbRysownica_MouseUp;
            VDpbRysownica.MouseMove += VDpbRysownica_MouseMove;

            VDPióro = new Pen(Color.Red, 3F);
            VDPióro.DashStyle = DashStyle.Dash;
            VDPióro.StartCap = LineCap.Round;
            VDPióro.EndCap = LineCap.Round;

            VDPędzel = new SolidBrush(Color.Blue);

        }
       

        
        private void Project_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void VDpbRysownica_MouseDown(object sender, MouseEventArgs e)
        {
            VDlblX.Text = e.Location.X.ToString();
            VDlblY.Text = e.Location.Y.ToString();

            if (e.Button == MouseButtons.Left)
            {
                VDPunkt = e.Location;
            }
        }
        private void VDpbRysownica_MouseUp(object sender, MouseEventArgs e)
        {
            VDlblX.Text = e.Location.X.ToString();
            VDlblY.Text = e.Location.Y.ToString();

            int VDLewyGórnyNarożnikX = (VDPunkt.X > e.Location.X) ? e.Location.X : VDPunkt.X;
            int VDLewyGórnyNarożnikY = (VDPunkt.Y > e.Location.Y) ? e.Location.Y : VDPunkt.Y;
            int VDSzerokość = Math.Abs(VDPunkt.X - e.Location.X);
            int VDWysokość = Math.Abs(VDPunkt.Y - e.Location.Y);

            if (e.Button == MouseButtons.Left)
            {
                if (VDrdbProstokąt.Checked)
                {
                    Rectangle Prostokąt = new Rectangle(VDLewyGórnyNarożnikX, VDLewyGórnyNarożnikY, VDSzerokość, VDWysokość);
                    VDRysownica.DrawRectangle(VDPióro, Prostokąt);
                }
                
                if (VDrdbProstokątWypełniony.Checked)
                {
                    Rectangle Prostokąt = new Rectangle(VDLewyGórnyNarożnikX, VDLewyGórnyNarożnikY, VDSzerokość, VDWysokość);
                    
                    VDRysownica.FillRectangle(VDPędzel, Prostokąt);
                }

                if (VDrdbKwadrat.Checked)
                {
                    int VDi = VDSzerokość;
                    Rectangle VDKwadrat = new Rectangle(VDLewyGórnyNarożnikX, VDLewyGórnyNarożnikY, VDi, VDi);
                    VDRysownica.DrawRectangle(VDPióro, VDKwadrat);

                }
                if (VDrdbKwadratWypełniony.Checked)
                {
                    int VDi = VDSzerokość;
                    Rectangle VDKwadrat = new Rectangle(VDLewyGórnyNarożnikX, VDLewyGórnyNarożnikY, VDi, VDi);
                    
                    VDRysownica.FillRectangle(VDPędzel, VDKwadrat);
                }

                if (VDrdbElipsa.Checked)
                {
                    VDRysownica.DrawEllipse(VDPióro, VDLewyGórnyNarożnikX, VDLewyGórnyNarożnikY, VDSzerokość * 2, VDWysokość);
                }

                if (VDrdbElipsaWypełniona.Checked)
                {
                    
                    VDRysownica.FillEllipse(VDPędzel, VDLewyGórnyNarożnikX, VDLewyGórnyNarożnikY, VDSzerokość * 2, VDWysokość);
                }
                if (VDrdbOkrag.Checked)
                
                {
                    int VDi = VDSzerokość;
                    VDRysownica.DrawEllipse(VDPióro, VDLewyGórnyNarożnikX, VDLewyGórnyNarożnikY, VDi + VDi, VDi + VDi);
                }

                if (VDrdbKołoWypełnione.Checked)
                
                {
                    int VDi = VDSzerokość;
                    
                    VDRysownica.FillEllipse(VDPędzel, VDLewyGórnyNarożnikX, VDLewyGórnyNarożnikY, VDi + VDi, VDi + VDi);
                }
                if (VDrdbKrzywaBeziera.Checked)
                {
                    if (VDgroupBox1.Enabled)
                    {
                        VDgroupBox1.Enabled = false;

                        VDKrzywaBeziera.VDNumerPunktuKontrolnego = 0;
                        VDKrzywaBeziera.VDPromienPunktuKontrolnego = 5;
                        VDKrzywaBeziera.VDPunktP0 = e.Location;

                        using (SolidBrush VDPędzel = new SolidBrush(Color.Black))
                        {
                            VDRysownica.FillEllipse(VDPędzel, e.Location.X - VDKrzywaBeziera.VDPromienPunktuKontrolnego, e.Location.Y - VDKrzywaBeziera.VDPromienPunktuKontrolnego, 2 * VDKrzywaBeziera.VDPromienPunktuKontrolnego, 2 * VDKrzywaBeziera.VDPromienPunktuKontrolnego);
                            VDRysownica.DrawString("p" + VDKrzywaBeziera.VDNumerPunktuKontrolnego.ToString(), VDFontOpisuPunktów, VDPędzel, e.Location);
                        }
                    }
                    else
                    {
                        VDKrzywaBeziera.VDNumerPunktuKontrolnego++;

                        switch (VDKrzywaBeziera.VDNumerPunktuKontrolnego)
                        {
                            case 1:
                                VDKrzywaBeziera.VDPunktP1 = e.Location;
                                break;
                            case 2:
                                VDKrzywaBeziera.VDPunktP2 = e.Location;
                                break;
                            case 3:
                                VDKrzywaBeziera.VDPunktP3 = e.Location;
                                break;
                        }

                        if (VDKrzywaBeziera.VDNumerPunktuKontrolnego < 3)
                        {
                            using (SolidBrush VDPędzel = new SolidBrush(Color.Red))
                            {
                                VDRysownica.FillEllipse(VDPędzel, e.Location.X - VDKrzywaBeziera.VDPromienPunktuKontrolnego, e.Location.Y - VDKrzywaBeziera.VDPromienPunktuKontrolnego, 2 * VDKrzywaBeziera.VDPromienPunktuKontrolnego, 2 * VDKrzywaBeziera.VDPromienPunktuKontrolnego);
                                VDRysownica.DrawString("p" + VDKrzywaBeziera.VDNumerPunktuKontrolnego.ToString(), VDFontOpisuPunktów, VDPędzel, e.Location);
                            }
                        }
                        
                        else
                        {
                            using (SolidBrush VDPędzel = new SolidBrush(Color.Black))
                            {
                                VDRysownica.FillEllipse(VDPędzel, e.Location.X - VDKrzywaBeziera.VDPromienPunktuKontrolnego, e.Location.Y - VDKrzywaBeziera.VDPromienPunktuKontrolnego, 2 * VDKrzywaBeziera.VDPromienPunktuKontrolnego, 2 * VDKrzywaBeziera.VDPromienPunktuKontrolnego);
                                VDRysownica.DrawString("p" + VDKrzywaBeziera.VDNumerPunktuKontrolnego.ToString(), VDFontOpisuPunktów, VDPędzel, e.Location);
                            }

                            VDRysownica.DrawBezier(VDPióro, VDKrzywaBeziera.VDPunktP0, VDKrzywaBeziera.VDPunktP1, VDKrzywaBeziera.VDPunktP2, VDKrzywaBeziera.VDPunktP3);
                            VDgroupBox1.Enabled = true;
                        }
                    }
                }

                if (VDrdbKrzywaKardynalna.Checked)
                {
                    if (VDgroupBox1.Enabled)
                    {
                        VDgroupBox1.Enabled = false;

                        VDKrzywaKard.VDNumerPunktu = 0;
                        VDKrzywaKard.VDPromienPunktu = 3;
                        VDKrzywaKard.VDPunktP0 = e.Location;

                        using (SolidBrush VDPędzel = new SolidBrush(Color.Black))
                        {
                            VDRysownica.FillEllipse(VDPędzel, e.Location.X - VDKrzywaKard.VDPromienPunktu, e.Location.Y - VDKrzywaKard.VDPromienPunktu, 2 * VDKrzywaKard.VDPromienPunktu, 2 * VDKrzywaKard.VDPromienPunktu);
                            VDRysownica.DrawString("p" + VDKrzywaKard.VDNumerPunktu.ToString(), VDFontOpisuPunktów, VDPędzel, e.Location);
                        }
                    }
                    
                    else
                    {
                        VDKrzywaKard.VDNumerPunktu += 1;

                        switch (VDKrzywaKard.VDNumerPunktu)
                        {
                            case 1:
                                VDKrzywaKard.VDPunktP1 = e.Location;
                                break;
                            case 2:
                                VDKrzywaKard.VDPunktP2 = e.Location;
                                break;
                            case 3:
                                VDKrzywaKard.VDPunktP3 = e.Location;
                                break;
                            case 4:
                                VDKrzywaKard.VDPunktP4 = e.Location;
                                break;
                        }
                        
                        if (VDKrzywaKard.VDNumerPunktu < 4)
                        {
                            using (SolidBrush VDPędzel = new SolidBrush(Color.Red))
                            {
                                VDRysownica.FillEllipse(VDPędzel, e.Location.X - VDKrzywaKard.VDPromienPunktu, e.Location.Y - VDKrzywaKard.VDPromienPunktu, 2 * VDKrzywaKard.VDPromienPunktu, 2 * VDKrzywaKard.VDPromienPunktu);
                                VDRysownica.DrawString("p" + VDKrzywaKard.VDNumerPunktu.ToString(), VDFontOpisuPunktów, VDPędzel, e.Location);
                            }
                        }
                        else
                        {
                            using (SolidBrush VDPędzel = new SolidBrush(Color.Black))
                            {
                                VDRysownica.FillEllipse(VDPędzel, e.Location.X - VDKrzywaKard.VDPromienPunktu, e.Location.Y - VDKrzywaKard.VDPromienPunktu, 2 * VDKrzywaKard.VDPromienPunktu, 2 * VDKrzywaKard.VDPromienPunktu);
                                VDRysownica.DrawString("p" + VDKrzywaKard.VDNumerPunktu.ToString(), VDFontOpisuPunktów, VDPędzel, e.Location);
                            }

                            PointF[] VDpoints = { VDKrzywaKard.VDPunktP0, VDKrzywaKard.VDPunktP1, VDKrzywaKard.VDPunktP2, VDKrzywaKard.VDPunktP3, VDKrzywaKard.VDPunktP4 };
                            VDRysownica.DrawCurve(VDPióro, VDpoints);

                            VDgroupBox1.Enabled = true;
                        }
                    }
                }
                if (VDrdbZamkniętaKrzywaKardynalna.Checked)
                {
                    if (VDgroupBox1.Enabled)
                    {
                        VDgroupBox1.Enabled = false;

                        VDKrzywaKard.VDNumerPunktu = 0;
                        VDKrzywaKard.VDPromienPunktu = 3;
                        VDKrzywaKard.VDPunktP0 = e.Location;

                        using (SolidBrush VDPędzel = new SolidBrush(Color.Black))
                        {
                            VDRysownica.FillEllipse(VDPędzel, e.Location.X - VDKrzywaKard.VDPromienPunktu, e.Location.Y - VDKrzywaKard.VDPromienPunktu, 2 * VDKrzywaKard.VDPromienPunktu, 2 * VDKrzywaKard.VDPromienPunktu);
                            VDRysownica.DrawString("p" + VDKrzywaKard.VDNumerPunktu.ToString(), VDFontOpisuPunktów, VDPędzel, e.Location);
                        }
                    }

                    else
                    {
                        VDKrzywaKard.VDNumerPunktu += 1;

                        switch (VDKrzywaKard.VDNumerPunktu)
                        {
                            case 1:
                                VDKrzywaKard.VDPunktP1 = e.Location;
                                break;
                            case 2:
                                VDKrzywaKard.VDPunktP2 = e.Location;
                                break;
                            case 3:
                                VDKrzywaKard.VDPunktP3 = e.Location;
                                break;
                            case 4:
                                VDKrzywaKard.VDPunktP4 = e.Location;
                                break;
                        }
                        
                        if (VDKrzywaKard.VDNumerPunktu < 4)
                        {
                            using (SolidBrush VDPędzel = new SolidBrush(Color.Red))
                            {
                                VDRysownica.FillEllipse(VDPędzel, e.Location.X - VDKrzywaKard.VDPromienPunktu, e.Location.Y - VDKrzywaKard.VDPromienPunktu, 2 * VDKrzywaKard.VDPromienPunktu, 2 * VDKrzywaKard.VDPromienPunktu);
                                VDRysownica.DrawString("p" + VDKrzywaKard.VDNumerPunktu.ToString(), VDFontOpisuPunktów, VDPędzel, e.Location);
                            }
                        }
                        
                        else
                        {
                            using (SolidBrush VDPędzel = new SolidBrush(Color.Black))
                            {
                                VDRysownica.FillEllipse(VDPędzel, e.Location.X - VDKrzywaKard.VDPromienPunktu, e.Location.Y - VDKrzywaKard.VDPromienPunktu, 2 * VDKrzywaKard.VDPromienPunktu, 2 * VDKrzywaKard.VDPromienPunktu);
                                VDRysownica.DrawString("p" + VDKrzywaKard.VDNumerPunktu.ToString(), VDFontOpisuPunktów, VDPędzel, e.Location);
                            }

                            PointF[] VDpoints = { VDKrzywaKard.VDPunktP0, VDKrzywaKard.VDPunktP1, VDKrzywaKard.VDPunktP2, VDKrzywaKard.VDPunktP3, VDKrzywaKard.VDPunktP4 };
                            VDRysownica.DrawClosedCurve(VDPióro, VDpoints);

                            VDgroupBox1.Enabled = true;
                        }
                    }
                }
                
                if (VDrdbWypełnionaZamkniętaKrzywaKardynalna.Checked)
                {
                    if (VDgroupBox1.Enabled)
                    {
                        VDgroupBox1.Enabled = false;

                        VDKrzywaKard.VDNumerPunktu = 0;
                        VDKrzywaKard.VDPromienPunktu = 5;
                        VDKrzywaKard.VDPunktP0 = e.Location;

                        using (SolidBrush VDPędzel = new SolidBrush(Color.Black))
                        {
                            VDRysownica.FillEllipse(VDPędzel, e.Location.X - VDKrzywaKard.VDPromienPunktu, e.Location.Y - VDKrzywaKard.VDPromienPunktu, 2 * VDKrzywaKard.VDPromienPunktu, 2 * VDKrzywaKard.VDPromienPunktu);
                            VDRysownica.DrawString("p" + VDKrzywaKard.VDNumerPunktu.ToString(), VDFontOpisuPunktów, VDPędzel, e.Location);
                        }
                    }
                    
                    else
                    {
                        VDKrzywaKard.VDNumerPunktu += 1;

                        switch (VDKrzywaKard.VDNumerPunktu)
                        {
                            case 1:
                                VDKrzywaKard.VDPunktP1 = e.Location;
                                break;
                            case 2:
                                VDKrzywaKard.VDPunktP2 = e.Location;
                                break;
                            case 3:
                                VDKrzywaKard.VDPunktP3 = e.Location;
                                break;
                            case 4:
                                VDKrzywaKard.VDPunktP4 = e.Location;
                                break;
                        }
                        
                        if (VDKrzywaKard.VDNumerPunktu < 4)
                        {
                            using (SolidBrush VDPędzel = new SolidBrush(Color.Red))
                            {
                                VDRysownica.FillEllipse(VDPędzel, e.Location.X - VDKrzywaKard.VDPromienPunktu, e.Location.Y - VDKrzywaKard.VDPromienPunktu, 2 * VDKrzywaKard.VDPromienPunktu, 2 * VDKrzywaKard.VDPromienPunktu);
                                VDRysownica.DrawString("p" + VDKrzywaKard.VDNumerPunktu.ToString(), VDFontOpisuPunktów, VDPędzel, e.Location);
                            }
                        }
                        
                        
                        else
                        {
                            using (SolidBrush VDPędzel = new SolidBrush(Color.Black))
                            {
                                VDRysownica.FillEllipse(VDPędzel, e.Location.X - VDKrzywaKard.VDPromienPunktu, e.Location.Y - VDKrzywaKard.VDPromienPunktu, 2 * VDKrzywaKard.VDPromienPunktu, 2 * VDKrzywaKard.VDPromienPunktu);
                                VDRysownica.DrawString("p" + VDKrzywaKard.VDNumerPunktu.ToString(), VDFontOpisuPunktów, VDPędzel, e.Location);
                            }

                            

                            PointF[] VDpoints = { VDKrzywaKard.VDPunktP0, VDKrzywaKard.VDPunktP1, VDKrzywaKard.VDPunktP2, VDKrzywaKard.VDPunktP3, VDKrzywaKard.VDPunktP4 };
                            VDRysownica.FillClosedCurve(VDPędzel, VDpoints);

                            VDgroupBox1.Enabled = true;
                        }
                    }
                }

                if (VDrdbŁukElpsy.Checked)
                {
                    VDRysownica.DrawArc(VDPióro, VDLewyGórnyNarożnikX, VDLewyGórnyNarożnikY, VDSzerokość, VDWysokość, e.Location.X, e.Location.Y);
                }

                if (VDrdbWycinekElpsy.Checked)
                {
                    VDRysownica.DrawPie(VDPióro, VDLewyGórnyNarożnikX, VDLewyGórnyNarożnikY, VDSzerokość, VDWysokość, e.Location.X, e.Location.Y);
                }

                if (VDrdbWypełnionyWycinekElpsy.Checked)
                {
                    
                    VDRysownica.FillPie(VDPędzel, VDLewyGórnyNarożnikX, VDLewyGórnyNarożnikY, VDSzerokość, VDWysokość, e.Location.X, e.Location.Y);
                }

                if (VDrdbWypełnionyObramowanyWycinekElpsy.Checked)
                {
                    
                    VDRysownica.FillPie(VDPędzel, VDLewyGórnyNarożnikX, VDLewyGórnyNarożnikY, VDSzerokość, VDWysokość, e.Location.X, e.Location.Y);
                    VDRysownica.DrawPie(VDPióro, VDLewyGórnyNarożnikX, VDLewyGórnyNarożnikY, VDSzerokość, VDWysokość, e.Location.X, e.Location.Y);
                }
            }

            VDpbRysownica.Refresh();
        }

        private void VDpbRysownica_MouseMove(object sender, MouseEventArgs e)
        {
            VDlblX.Text = e.Location.X.ToString();
            VDlblY.Text = e.Location.Y.ToString();

            VDpbRysownica.Refresh();
        }

        

        private void VDbtnKolorWypełnienia_Click(object sender, EventArgs e)
        {
            ColorDialog VDPaletaKolorów = new ColorDialog();
           
            if (VDPaletaKolorów.ShowDialog() == DialogResult.OK)
            {
                
            }

            VDPaletaKolorów.Dispose();
        }
        
        
        
        

        
       

       
        
       

       

       
       

       

        
        

        

       

        

        

       

       
       

        
       

       
        

       


        
        private void Project_Load(object sender, EventArgs e)
        {

        }

        private void zapiszaBitMapęWPlikuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog VDOknoWyboruPlikuDoZapisu = new SaveFileDialog();
            VDOknoWyboruPlikuDoZapisu.Filter = "Image Files|*.jpg;*.jpeg;*.png|Allfiles(*.*.)|*.*";
            VDOknoWyboruPlikuDoZapisu.FilterIndex = 1;
            VDOknoWyboruPlikuDoZapisu.RestoreDirectory = true;
            VDOknoWyboruPlikuDoZapisu.InitialDirectory = "D:\\";
            VDOknoWyboruPlikuDoZapisu.Title = "Zapisz BitMapę";

            if (VDOknoWyboruPlikuDoZapisu.ShowDialog() == DialogResult.OK)
            {
                int VDSzerokośćRysownicy = Convert.ToInt32(VDpbRysownica.Width);
                int VDWysokośćRysownicy = Convert.ToInt32(VDpbRysownica.Height);

                using (Bitmap VDBmap = new Bitmap(VDSzerokośćRysownicy, VDWysokośćRysownicy))
                {
                    VDpbRysownica.DrawToBitmap(VDBmap, new Rectangle(0, 0, VDSzerokośćRysownicy, VDWysokośćRysownicy));
                    VDBmap.Save(VDOknoWyboruPlikuDoZapisu.FileName, ImageFormat.Jpeg);
                }
            }
        }

        private void odczytajBitMapęZPlikuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog VDOknoWyboruPlikuDoOdczytu = new OpenFileDialog();
            VDOknoWyboruPlikuDoOdczytu.Title = "Odczytaj BitMapę";
            VDOknoWyboruPlikuDoOdczytu.Filter = "Image Files|*.jpg;*.jpeg;*.png|Allfiles(*.*)|*.*";
            VDOknoWyboruPlikuDoOdczytu.FilterIndex = 1;
            VDOknoWyboruPlikuDoOdczytu.RestoreDirectory = true;
            VDOknoWyboruPlikuDoOdczytu.InitialDirectory = "C:\\";

            if (VDOknoWyboruPlikuDoOdczytu.ShowDialog() == DialogResult.OK)
            {
                VDpbRysownica.Image = new Bitmap(VDOknoWyboruPlikuDoOdczytu.FileName);
                this.Controls.Add(VDpbRysownica);
            }
            VDOknoWyboruPlikuDoOdczytu.Dispose();

        }

        private void exitZFormularzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void restartProgramuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ProjectN3_68932 VDform3 = new ProjectN3_68932();
            VDform3.Show();
        }

        private void kolorLiniiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog VDPaletaKolorow = new ColorDialog();
            VDPaletaKolorow.Color = VDPióro.Color;

            if (VDPaletaKolorow.ShowDialog() == DialogResult.OK)
            {
                VDPióro.Color = VDPaletaKolorow.Color;
            }

        }

        private void kolorTłaRysownicyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog VDPaletaKolorow = new ColorDialog();
            VDPaletaKolorow.Color = VDpbRysownica.BackColor;

            if (VDPaletaKolorow.ShowDialog() == DialogResult.OK)
            {
                VDpbRysownica.BackColor = VDPaletaKolorow.Color;
            }

        }

        private void kolorWypełnieniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog VDPaletaKolorówWyp = new ColorDialog();

            if (VDPaletaKolorówWyp.ShowDialog() == DialogResult.OK)
            {

            }

        }

        

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            VDPióro.Width = 1;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            VDPióro.Width = 2;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            VDPióro.Width = 4;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            VDPióro.Width = 6;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            VDPióro.Width = 8;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            VDPióro.Width = 10;
        }

        private void liniaKreskowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VDPióro.DashStyle = DashStyle.Dash;
        }

        private void liniaKreskowokropkowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VDPióro.DashStyle = DashStyle.DashDot;
        }

        private void liniaKreskowokreskowokropkowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VDPióro.DashStyle = DashStyle.DashDotDot;
        }

        private void liniaKropkowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VDPióro.DashStyle = DashStyle.Dot;
        }

        private void flatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VDPióro.DashStyle = DashStyle.Solid;
        }

        

        private void trianagleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VDPióro.StartCap = LineCap.Triangle;
        }

        

        private void squareAnchorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VDPióro.StartCap = LineCap.SquareAnchor;
        }

        private void roundAnchorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VDPióro.EndCap = LineCap.RoundAnchor;
        }

        private void diamondAnchorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VDPióro.StartCap = LineCap.DiamondAnchor;
        }

        private void arrowAnchorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VDPióro.StartCap = LineCap.ArrowAnchor;
        }

        private void flatToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            VDPióro.DashStyle = DashStyle.Solid;
        }

        private void trianagleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            VDPióro.EndCap = LineCap.Triangle;
        }

        private void squareAnchorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            VDPióro.EndCap = LineCap.SquareAnchor;
        }

        private void roundAnchorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            VDPióro.StartCap = LineCap.RoundAnchor;
        }

        private void diamondAnchorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            VDPióro.EndCap = LineCap.DiamondAnchor;
        }

        private void arrowAnchorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            VDPióro.EndCap = LineCap.ArrowAnchor;
        }

        private void krójCzcionkiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog VDOknoCzcionki = new FontDialog();
            VDOknoCzcionki.Font = this.Font;
            if (VDOknoCzcionki.ShowDialog() == DialogResult.OK)
            {
                this.Font = VDOknoCzcionki.Font;
            }

            VDOknoCzcionki.Dispose();
        }

        private void rozmiarCzcionkiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog VDOknoCzcionki = new FontDialog();
            VDOknoCzcionki.Font = this.Font;
            if (VDOknoCzcionki.ShowDialog() == DialogResult.OK)
            {
                this.Font = VDOknoCzcionki.Font;
            }

            VDOknoCzcionki.Dispose();
        }

        private void kolorCzcionkiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog AMPaletaKolorów = new ColorDialog();
            AMPaletaKolorów.Color = this.ForeColor;
            if (AMPaletaKolorów.ShowDialog() == DialogResult.OK)
            {
                this.ForeColor = AMPaletaKolorów.Color;
            }

            AMPaletaKolorów.Dispose();
        }
    }
}
