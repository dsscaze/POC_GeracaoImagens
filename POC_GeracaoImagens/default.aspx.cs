using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POC_GeracaoImagens
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ImageGeneratorHelper imgHelper = new ImageGeneratorHelper();
            string img = imgHelper.ConvertTextToImage("Parabéns à irmã que sempre adiciona amor e luz à minha vida. Que este dia seja tão maravilhoso quanto você é!",
                "Verdana", 18, FontStyle.Bold, Color.Aquamarine, Color.White, 400, 500);
            
            //string imagePath = Path.Combine(Server.MapPath("~/prometheus.jpg"));
            //string fontPath = Path.Combine(Server.MapPath("~/Chunkfive.otf"));
            //string savePath = Path.Combine(Server.MapPath("~/new_prometheus.jpg"));

            //var imageWriter = new ImageWriter();
            //Bitmap image = imageWriter.LoadImage(imagePath);
            //FontFamily font = imageWriter.LoadFont(fontPath, "chunkfive");

            //imageWriter.WriteTextToImage(image, savePath, "Prometheus Caze", font);

            imgTeste.ImageUrl = img;
        }
    }
}