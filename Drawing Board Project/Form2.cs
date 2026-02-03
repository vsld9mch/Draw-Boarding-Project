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

namespace projekt3
{
    public partial class Laboratorna : Form
    {
        
       
            const ushort PromieńPunktu = 2;
            Graphics Rysownica;
            Pen Pióro;
            SolidBrush Pędzel;
            Point Punkt;
            public Laboratorna()
            {
                InitializeComponent();

                this.Text = "Project_3";
                lblX.Text = "0";
                lblY.Text = "0";

                pbRysownica.Image = new Bitmap(pbRysownica.Width, pbRysownica.Height);
                Rysownica = Graphics.FromImage(pbRysownica.Image);

                pbRysownica.MouseDown += pbRysownica_MouseDown;
                pbRysownica.MouseUp += pbRysownica_MouseUp;
                pbRysownica.MouseMove += pbRysownica_MouseMove;

            Pióro = new Pen(Color.Red, 3F);
                Pióro.DashStyle = DashStyle.Dash;
                Pióro.StartCap = LineCap.Round;
                Pióro.EndCap = LineCap.Round;

                Pędzel = new SolidBrush(Color.Blue);

                Punkt = Point.Empty;
            }



            private void btnBack_Click(object sender, EventArgs e)
            {
                this.Hide();
                Form1 form1 = new Form1();
                form1.Show();
            }

            private void btnExit_Click(object sender, EventArgs e)
            {
                Application.Exit();
            }

            private void btnReset_Click(object sender, EventArgs e)
            {
                this.Hide();
                Laboratorna form2 = new Laboratorna();
                form2.Show();
            }

            private void Laboratory_FormClosed(object sender, FormClosedEventArgs e)
            {
                Application.Exit();
            }

            private void pbRysownica_MouseDown(object sender, MouseEventArgs e)
            {
                lblX.Text = e.Location.X.ToString();
                lblY.Text = e.Location.Y.ToString();

                if (e.Button == MouseButtons.Left)
                {
                    Punkt = e.Location;
                }
            }

            private void pbRysownica_MouseUp(object sender, MouseEventArgs e)
            {
                lblX.Text = e.Location.X.ToString();
                lblY.Text = e.Location.Y.ToString();

                int LewyGórnyNarożnikX = (Punkt.X > e.Location.X) ? e.Location.X : Punkt.X;
                int LewyGórnyNarożnikY = (Punkt.Y > e.Location.Y) ? e.Location.Y : Punkt.Y;
                int Szerokość = Math.Abs(Punkt.X - e.Location.X);
                int Wysokość = Math.Abs(Punkt.Y - e.Location.Y);

                if (e.Button == MouseButtons.Left)
                {
                    if (rdbPunkt.Checked)
                    {
                        Rysownica.FillEllipse(Pędzel, Punkt.X - PromieńPunktu, Punkt.Y - PromieńPunktu, 2 * PromieńPunktu, 2 * PromieńPunktu);
                    }

                    if (rdbLiniaProsta.Checked)
                    {
                        Rysownica.DrawLine(Pióro, Punkt.X, Punkt.Y, e.Location.X, e.Location.Y);
                    }

                    if (rdbLiniaCiągła.Checked)
                    {
                        Rysownica.DrawLine(Pióro, Punkt.X, Punkt.Y, e.Location.X, e.Location.Y);
                    }

                    if (rdbWielokąt.Checked)
                    {
                        ushort StopieńWielokąta;
                        int PromieńWielokąta;
                        double KątMiędzyWierzchołkamiWielokąta;
                        double KątPolożeniaPierwszegoWierzchłkaWielokąta = 0.0;

                        numUD_LiczbaKątów.Enabled = false;

                        PromieńWielokąta = Szerokość;
                        StopieńWielokąta = (ushort)numUD_LiczbaKątów.Value;
                        KątMiędzyWierzchołkamiWielokąta = 360F / StopieńWielokąta;
                        Point[] WierzchołkamiWielokąta = new Point[StopieńWielokąta];

                        for (int i = 0; i < StopieńWielokąta; i++)
                        {
                            WierzchołkamiWielokąta[i].X = LewyGórnyNarożnikX + (int)(PromieńWielokąta * Math.Cos(Math.PI * (KątPolożeniaPierwszegoWierzchłkaWielokąta + i * KątMiędzyWierzchołkamiWielokąta) / 180));
                            WierzchołkamiWielokąta[i].Y = LewyGórnyNarożnikY - (int)(PromieńWielokąta * Math.Sin(Math.PI * (KątPolożeniaPierwszegoWierzchłkaWielokąta + i * KątMiędzyWierzchołkamiWielokąta) / 180));
                        }

                        Rysownica.DrawPolygon(Pióro, WierzchołkamiWielokąta);
                    }

                    if (rdbWielokątWypełniony.Checked)
                    {
                        ushort StopieńWielokątaWypełnionego;
                        int PromieńWielokątaWypełnionego;
                        double KątMiędzyWierzchołkamiWielokątaWypełnionego;
                        double KątPolożeniaPierwszegoWierzchłkaWielokątaWypełnionego = 0.0;

                        numUD_LiczbaKątów.Enabled = false;

                        PromieńWielokątaWypełnionego = Szerokość;
                        StopieńWielokątaWypełnionego = (ushort)numUD_LiczbaKątów.Value;
                        KątMiędzyWierzchołkamiWielokątaWypełnionego = 360F / StopieńWielokątaWypełnionego;
                        Point[] WierzchołkamiWielokątaWypełnionego = new Point[StopieńWielokątaWypełnionego];

                        for (int i = 0; i < StopieńWielokątaWypełnionego; i++)
                        {
                            WierzchołkamiWielokątaWypełnionego[i].X = LewyGórnyNarożnikX + (int)(PromieńWielokątaWypełnionego * Math.Cos(Math.PI * (KątPolożeniaPierwszegoWierzchłkaWielokątaWypełnionego + i * KątMiędzyWierzchołkamiWielokątaWypełnionego) / 180));
                            WierzchołkamiWielokątaWypełnionego[i].Y = LewyGórnyNarożnikY - (int)(PromieńWielokątaWypełnionego * Math.Sin(Math.PI * (KątPolożeniaPierwszegoWierzchłkaWielokątaWypełnionego + i * KątMiędzyWierzchołkamiWielokątaWypełnionego) / 180));
                        }

                        Rysownica.FillPolygon(Pędzel, WierzchołkamiWielokątaWypełnionego);
                    }

                    if (rdbTrójkątSierpińskiego.Checked)
                    {
                        ushort poziomRekurencji = (ushort)numUD_PoziomRekurencji.Value;
                        Point WierzchołeGórny = new Point(LewyGórnyNarożnikX + Szerokość / 2, LewyGórnyNarożnikY);
                        Point WierzchołeLewy = new Point(LewyGórnyNarożnikX, LewyGórnyNarożnikY + Wysokość);
                        Point WierzchołePrawy = new Point(LewyGórnyNarożnikX + Szerokość, LewyGórnyNarożnikY + Wysokość);

                    }
                }

                pbRysownica.Refresh();
            }

            private void pbRysownica_MouseMove(object sender, MouseEventArgs e)
            {
                lblX.Text = e.Location.X.ToString();
                lblY.Text = e.Location.Y.ToString();

                int LewyGórnyNarożnikX = (Punkt.X > e.Location.X) ? e.Location.X : Punkt.X;
                int LewyGórnyNarożnikY = (Punkt.Y > e.Location.Y) ? e.Location.Y : Punkt.Y;
                int Szerokość = Math.Abs(Punkt.X - e.Location.X);
                int Wysokość = Math.Abs(Punkt.Y - e.Location.Y);

                if (e.Button == MouseButtons.Left)
                {
                    if (rdbLiniaCiągła.Checked)
                    {
                        Rysownica.DrawLine(Pióro, Punkt.X, Punkt.Y, e.Location.X, e.Location.Y);
                        Punkt.X = e.Location.X;
                        Punkt.Y = e.Location.Y;
                    }
                }

                pbRysownica.Refresh();
            }

            private void rdbWielokąt_CheckedChanged(object sender, EventArgs e)
            {
                if (rdbWielokąt.Checked)
                {
                    //MessageBox.Show("Wykreślenie wielokąta foremnego wymaga podania stopnia wielokąta, czyli liczby katów wielokata (minimalny stopień wielokąta jest równy 3)", "Wykreślanie wielokąta foremnego", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblLiczbaKatów.Visible = true;
                    numUD_LiczbaKątów.Visible = true;
                    numUD_LiczbaKątów.Enabled = true;
                    numUD_LiczbaKątów.Minimum = 3;
                }

                else
                {
                    lblLiczbaKatów.Visible = false;
                    numUD_LiczbaKątów.Visible = false;
                }
            }

            private void rdbWielokątWypełniony_CheckedChanged(object sender, EventArgs e)
            {
                if (rdbWielokątWypełniony.Checked)
                {
                    //MessageBox.Show("Wykreślenie wielokąta wypełnionego foremnego wymaga podania stopnia wielokąta, czyli liczby katów wielokata (minimalny stopień wielokąta jest równy 3)", "Wykreślanie wielokąta foremnego", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblLiczbaKatów.Visible = true;
                    numUD_LiczbaKątów.Visible = true;
                    numUD_LiczbaKątów.Enabled = true;
                    numUD_LiczbaKątów.Minimum = 3;
                    btnKolorWypełnienia.Visible = true;
                    btnKolorWypełnienia.Enabled = true;
                }

                else
                {
                    lblLiczbaKatów.Visible = false;
                    numUD_LiczbaKątów.Visible = false;
                    btnKolorWypełnienia.Visible = false;
                }
            }

            private void btnKolorWypełnienia_Click(object sender, EventArgs e)
            {
                ColorDialog PaletaKolorów = new ColorDialog();
                PaletaKolorów.Color = btnKolorWypełnienia.BackColor;
                if (PaletaKolorów.ShowDialog() == DialogResult.OK)
                {
                    btnKolorWypełnienia.BackColor = PaletaKolorów.Color;
                }

                PaletaKolorów.Dispose();
            }

            private void rdbTrójkątSierpińskiego_CheckedChanged(object sender, EventArgs e)
            {
                if (rdbTrójkątSierpińskiego.Checked)
                {
                    //MessageBox.Show("Wykreślenie Trójkąta Sierpińskiego wymaga podania poziomu rekurencji (od 0 w górę) oraz wybrania koloru wypiełnienia\n" + "UWAGA: można też przyjąć ustawienia domyśle ...", "Kreślenie ...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    numUD_PoziomRekurencji.Value = 3;
                    btnKolorWypełnienia.BackColor = Color.Yellow;
                    lblPoziomRekurencji.Visible = true;
                    numUD_PoziomRekurencji.Visible = true;
                    btnKolorWypełnienia.Visible = true;
                    numUD_PoziomRekurencji.Enabled = true;
                    btnKolorWypełnienia.Enabled = true;
                }

                else
                {
                    lblPoziomRekurencji.Visible = false;
                    numUD_PoziomRekurencji.Visible = false;
                    btnKolorWypełnienia.Visible = false;
                }
            }

            void WykreślTrójkątSierpińskiego(Graphics Rysownica, int poziomRekurencji, Color KolorWypiełnienia, Point WierzchołeGórny, Point WierzchołeLewy, Point WierzchołePrawy)
            {
                if (poziomRekurencji == 0)
                {
                    Point[] WierzchołkiTrójkąta = { WierzchołeGórny, WierzchołeLewy, WierzchołePrawy };
                    using (SolidBrush Pędzel = new SolidBrush(KolorWypiełnienia))
                    {
                        Rysownica.FillPolygon(Pędzel, WierzchołkiTrójkąta);
                    }
                }

                else
                {
                    Point PunktSrodkowyLewejKrawędzi = new Point((WierzchołeGórny.X + WierzchołeLewy.X) / 2, (WierzchołeGórny.Y + WierzchołeLewy.Y) / 2);
                    Point PunktSrodkowyPrawejKrawędzi = new Point((WierzchołeGórny.X + WierzchołePrawy.X) / 2, (WierzchołeGórny.Y + WierzchołePrawy.Y) / 2);
                    Point PunktSrodkowyDolneKrawędzi = new Point((WierzchołeLewy.X + WierzchołePrawy.X) / 2, (WierzchołeLewy.Y + WierzchołePrawy.Y) / 2);

                    WykreślTrójkątSierpińskiego(Rysownica, poziomRekurencji - 1, KolorWypiełnienia, WierzchołeGórny, PunktSrodkowyLewejKrawędzi, PunktSrodkowyPrawejKrawędzi);
                    WykreślTrójkątSierpińskiego(Rysownica, poziomRekurencji - 1, KolorWypiełnienia, PunktSrodkowyLewejKrawędzi, WierzchołeLewy, PunktSrodkowyDolneKrawędzi);
                    WykreślTrójkątSierpińskiego(Rysownica, poziomRekurencji - 1, KolorWypiełnienia, PunktSrodkowyPrawejKrawędzi, PunktSrodkowyDolneKrawędzi, WierzchołePrawy);
                }
            }

            private void zapiszaBitMapęWPlikuToolStripMenuItem_Click(object sender, EventArgs e)
            {
                SaveFileDialog OknoWyboruPlikuDoZapisu = new SaveFileDialog();
                OknoWyboruPlikuDoZapisu.Filter = "Image Files|*.jpg;*.jpeg;*.png|Allfiles(*.*.)|*.*";
                OknoWyboruPlikuDoZapisu.FilterIndex = 1;
                OknoWyboruPlikuDoZapisu.RestoreDirectory = true;
                OknoWyboruPlikuDoZapisu.InitialDirectory = "D:\\";
                OknoWyboruPlikuDoZapisu.Title = "Zapisz BitMapę";

                if (OknoWyboruPlikuDoZapisu.ShowDialog() == DialogResult.OK)
                {
                    int SzerokośćRysownicy = Convert.ToInt32(pbRysownica.Width);
                    int WysokośćRysownicy = Convert.ToInt32(pbRysownica.Height);

                    using (Bitmap Bmap = new Bitmap(SzerokośćRysownicy, WysokośćRysownicy))
                    {
                        pbRysownica.DrawToBitmap(Bmap, new Rectangle(0, 0, SzerokośćRysownicy, WysokośćRysownicy));
                        Bmap.Save(OknoWyboruPlikuDoZapisu.FileName, ImageFormat.Jpeg);
                    }
                }
            }

            private void odczytajBitMapęZPlikuToolStripMenuItem_Click(object sender, EventArgs e)
            {
                OpenFileDialog OknoWyboruPlikuDoOdczytu = new OpenFileDialog();
                OknoWyboruPlikuDoOdczytu.Title = "Odczytaj BitMapę";
                OknoWyboruPlikuDoOdczytu.Filter = "Image Files|*.jpg;*.jpeg;*.png|Allfiles(*.*)|*.*";
                OknoWyboruPlikuDoOdczytu.FilterIndex = 1;
                OknoWyboruPlikuDoOdczytu.RestoreDirectory = true;
                OknoWyboruPlikuDoOdczytu.InitialDirectory = "C:\\";

                if (OknoWyboruPlikuDoOdczytu.ShowDialog() == DialogResult.OK)
                {
                    pbRysownica.Image = new Bitmap(OknoWyboruPlikuDoOdczytu.FileName);
                    this.Controls.Add(pbRysownica);
                }
                OknoWyboruPlikuDoOdczytu.Dispose();
            }

            private void restartToolStripMenuItem_Click(object sender, EventArgs e)
            {
                this.Hide();
                Laboratorna form2 = new Laboratorna();
                form2.Show();
            }

            private void exitToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Application.Exit();
            }

            private void kolorLiniiToolStripMenuItem_Click(object sender, EventArgs e)
            {
                ColorDialog PaletaKolorow = new ColorDialog();
                PaletaKolorow.Color = Pióro.Color;

                if (PaletaKolorow.ShowDialog() == DialogResult.OK)
                {
                    Pióro.Color = PaletaKolorow.Color;
                }
            }

            private void kolorTłaRysownicyToolStripMenuItem_Click(object sender, EventArgs e)
            {
                ColorDialog PaletaKolorow = new ColorDialog();
                PaletaKolorow.Color = pbRysownica.BackColor;

                if (PaletaKolorow.ShowDialog() == DialogResult.OK)
                {
                    pbRysownica.BackColor = PaletaKolorow.Color;
                }
            }

            private void kolorWypełnieniaToolStripMenuItem_Click(object sender, EventArgs e)
            {
                ColorDialog PaletaKolorówWyp = new ColorDialog();
                PaletaKolorówWyp.Color = btnKolorWypełnienia.BackColor;

                if (PaletaKolorówWyp.ShowDialog() == DialogResult.OK)
                {
                    btnKolorWypełnienia.BackColor = PaletaKolorówWyp.Color;
                }
            }

            private void toolStripMenuItem2_Click(object sender, EventArgs e)
            {
                Pióro.Width = 1;
            }

            private void toolStripMenuItem3_Click(object sender, EventArgs e)
            {
                Pióro.Width = 2;
            }

            private void toolStripMenuItem4_Click(object sender, EventArgs e)
            {
                Pióro.Width = 3;
            }

            private void toolStripMenuItem5_Click(object sender, EventArgs e)
            {
                Pióro.Width = 4;
            }

            private void toolStripMenuItem6_Click(object sender, EventArgs e)
            {
                Pióro.Width = 5;
            }

            private void liniaKreskowaToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Pióro.DashStyle = DashStyle.Dash;
            }

            private void liniaKreskowoKropkowaToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Pióro.DashStyle = DashStyle.DashDot;
            }

            private void liniaKreskowoKropkowaKropkowaToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Pióro.DashStyle = DashStyle.DashDotDot;
            }

            private void liniaKropkowaToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Pióro.DashStyle = DashStyle.Dot;
            }

            private void ćągłaToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Pióro.DashStyle = DashStyle.Solid;
            }

            private void roundToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Pióro.StartCap = LineCap.RoundAnchor;
            }

            private void arrowToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Pióro.StartCap = LineCap.ArrowAnchor;
            }

            private void diamondToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Pióro.StartCap = LineCap.DiamondAnchor;
            }

            private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Pióro.StartCap = LineCap.Triangle;
            }

            private void squareToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Pióro.StartCap = LineCap.SquareAnchor;
            }

            private void roundToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                Pióro.EndCap = LineCap.RoundAnchor;
            }

            private void arrowToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                Pióro.EndCap = LineCap.ArrowAnchor;
            }

            private void diamondToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                Pióro.EndCap = LineCap.DiamondAnchor;
            }

            private void triangleToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                Pióro.EndCap = LineCap.Triangle;
            }

            private void squareToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                Pióro.EndCap = LineCap.SquareAnchor;
            }

            private void krójCzcionkiToolStripMenuItem_Click(object sender, EventArgs e)
            {
                FontDialog OknoCzcionki = new FontDialog();
                OknoCzcionki.Font = this.Font;
                if (OknoCzcionki.ShowDialog() == DialogResult.OK)
                {
                    this.Font = OknoCzcionki.Font;
                }

                OknoCzcionki.Dispose();
            }

            private void rozmiarCzcionkiToolStripMenuItem_Click(object sender, EventArgs e)
            {
                FontDialog OknoCzcionki = new FontDialog();
                OknoCzcionki.Font = this.Font;
                if (OknoCzcionki.ShowDialog() == DialogResult.OK)
                {
                    this.Font = OknoCzcionki.Font;
                }

                OknoCzcionki.Dispose();
            }

            private void kolorCzcionkiToolStripMenuItem_Click(object sender, EventArgs e)
            {
                ColorDialog PaletaKolorów = new ColorDialog();
                PaletaKolorów.Color = this.ForeColor;
                if (PaletaKolorów.ShowDialog() == DialogResult.OK)
                {
                    this.ForeColor = PaletaKolorów.Color;
                }

                PaletaKolorów.Dispose();
            }

            private void atrybutyKreślonejCzcionkiToolStripMenuItem_Click(object sender, EventArgs e)
            {
                this.Text = "Project_3";
            }
        }
    

}

