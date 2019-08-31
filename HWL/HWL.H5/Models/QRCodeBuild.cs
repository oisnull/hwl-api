using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.DrawingCore;
using System.IO;
using System.Linq;
using System.Web;

namespace HWL.H5.Models
{
    public class QRCodeBuild
    {
        public static void CreateQR(string url,string saveDir)
        {
            // 生成二维码的内容
            QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            QRCode qrcode = new QRCode(qrCodeData);

            // qrcode.GetGraphic 方法可参考最下发“补充说明”
            Bitmap qrCodeImage = qrcode.GetGraphic(5, System.DrawingCore.Color.Black, System.DrawingCore.Color.White, null, 15, 6, false);
            qrCodeImage.Save(saveDir + "/app.png");
        }
    }
}