//      UNIVERSIDAD CENTRAL DEL ECUADOR
//      PROGRAMA REALIZADO POR THALÍA VEGA

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.IO;
using System.Diagnostics;

namespace DetectorRostro
{
    public partial class FrmPrincipal : Form
    {
        //Declaramos las variables        
        Capture capturar;
        HaarCascade haar;
     
       
               public FrmPrincipal()
        {
            InitializeComponent();           
            }          

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            //Encender la camara

            capturar = new Capture(0);          
            timer1.Interval = 40;
            timer1.Enabled = true;
            haar = new HaarCascade("haarcascade_frontalface_default.xml");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            using (Image<Bgr, byte> imagen = capturar.QueryFrame())
            if (imagen != null)
            {
                //convertir a escala de gris
                Image<Gray, byte> gris = imagen.Convert<Gray, byte>();

                var faces = gris.DetectHaarCascade(haar, 1.4, 4, HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(imagen.Width / 8, imagen.Height / 8))[0];
                foreach (var fa in faces)
                {
                    imagen.Draw(fa.rect, new Bgr(253,50, 134), 5);
                }
                pictureBox1.Image = imagen.ToBitmap();
            } 

        }

    }
}
