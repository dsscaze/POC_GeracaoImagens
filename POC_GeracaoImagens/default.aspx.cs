using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
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
                "Open Sans", 12, FontStyle.Bold, Color.Red, Color.White, 350, 350);

            imgTeste.ImageUrl = img;
        }
    }
}