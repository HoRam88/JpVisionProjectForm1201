using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Google.Cloud.Vision.V1;
using System.IO;
using System.Windows.Forms.VisualStyles;
using System.Drawing;

namespace JpVisionProjectForm1201
{
    internal class UseVision
    {
        Google.Cloud.Vision.V1.Image image;
        Bitmap bitmap;
        StringBuilder sb = new StringBuilder();

        public UseVision(String strFilename, String jsonPath)
        {
            string credentialsString = File.ReadAllText(jsonPath);
            ImageAnnotatorClient client = new ImageAnnotatorClientBuilder   // json형태의 키 저장
            {
                JsonCredentials = credentialsString
            }.Build();


            image = Google.Cloud.Vision.V1.Image.FromFile(strFilename);
            bitmap = new Bitmap(System.Drawing.Image.FromFile(strFilename));
            Graphics gr = Graphics.FromImage(bitmap);
            try
            {
                int count = 0;
                IReadOnlyList<EntityAnnotation> textAnnotations = client.DetectText(image);
                foreach (EntityAnnotation text in textAnnotations)      // 반환받은 결과값 정리
                {
                    Console.WriteLine($"Description: {text.Description}");
                    Google.Protobuf.Collections.RepeatedField<Vertex> vertices = text.BoundingPoly.Vertices;
                    Rectangle rectangle = new Rectangle(vertices[0].X, vertices[0].Y, vertices[2].X - vertices[0].X, vertices[2].Y - vertices[0].Y);
                    sb.AppendLine(text.Description);

                    switch (count)
                    {
                        case 0:
                            Pen greenPen = new Pen(Color.Green, 4);
                            gr.DrawRectangle(greenPen, rectangle);
                            count++;
                            break;

                        default:
                            Pen redPen = new Pen(Color.Red, 2);
                            gr.DrawRectangle(redPen, rectangle);
                            count++;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public string passText()
        {
            return sb.ToString();
        }

        public Bitmap passBitmap()
        {
            return bitmap;
        }

        public int passPoint()
        {
            return sb.ToString().Length;
        }
    }
}
