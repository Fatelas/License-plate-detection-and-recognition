using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Drawing;
using System.IO;
using System.Runtime;

namespace SS_OpenCV
{
    class ImageClass
    {

        /// <summary>
        /// Image Negative using EmguCV library
        /// Slower method
        /// </summary>
        /// <param name="img">Image</param>
        //public static void Negative(Image<Bgr, byte> img)
        //{
        //    int x, y;

        //    Bgr aux;
        //    for ( y = 0; y < img.Height; y++)
        //    {
        //        for ( x = 0; x < img.Width; x++)
        //        {
        //            // acesso pela biblioteca : mais lento 
        //            aux = img[y, x];
        //            img[y, x] = new Bgr(255 - aux.Blue, 255 - aux.Green, 255 - aux.Red);
        //        }
        //    }
        //}


        public static void Negative(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrieve 3 colour components
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // store in the image
                            dataPtr[0] = (byte)(255 - blue);
                            dataPtr[1] = (byte)(255 - green);
                            dataPtr[2] = (byte)(255 - red);

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }


        /// <summary>
        /// Convert to gray
        /// Direct access to memory - faster method
        /// </summary>
        /// <param name="img">image</param>
        public static void ConvertToGray(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrieve 3 colour components
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // convert to gray
                            gray = (byte)Math.Round(((int)blue + green + red) / 3.0);

                            // store in the image
                            dataPtr[0] = gray;
                            dataPtr[1] = gray;
                            dataPtr[2] = gray;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void RedChannel(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrieve 3 colour components
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // store in the image
                            dataPtr[0] = red;
                            dataPtr[1] = red;
                            dataPtr[2] = red;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void BrightContrast(Image<Bgr, byte> img, int bright, double contrast)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                double blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;


                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {


                            //retrieve 3 colour components
                            blue = contrast * dataPtr[0] + bright;
                            if (blue < 0)
                            {
                                blue = 0;
                            }

                            if (blue > 255)
                            {
                                blue = 255;
                            }
                            green = contrast * dataPtr[1] + bright;
                            if (green < 0)
                            {
                                green = 0;
                            }

                            if (green > 255)
                            {
                                green = 255;
                            }
                            red = contrast * dataPtr[2] + bright;
                            if (red < 0)
                            {
                                red = 0;
                            }

                            if (red > 255)
                            {
                                red = 255;
                            }


                            // store in the image
                            dataPtr[0] = (byte)(blue);
                            dataPtr[1] = (byte)(green);
                            dataPtr[2] = (byte)(red);

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Translation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int dx, int dy)
        {
            unsafe
            {
                // obter apontador do inicio da imagem
                MIplImage m = img.MIplImage;
                MIplImage n = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr2 = (byte*)n.imageData.ToPointer();
                byte* aux;
                byte blue_o, green_o, red_o;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int x_origem, y_origem;
                int y, x;
                int padding = m.widthStep - m.nChannels * m.width;


                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        // calcula endereço do pixel no ponto (x,y)
                        x_origem = x - dx;
                        y_origem = y - dy;

                        aux = dataPtr2 + y_origem * n.widthStep + x_origem * n.nChannels;
                        if (x_origem >= 0 && y_origem >= 0 && x_origem < width && y_origem < height)
                        {
                            dataPtr[0] = aux[0];
                            dataPtr[1] = aux[1];
                            dataPtr[2] = aux[2];

                            /*blue_o = (byte)(dataPtr2 + y_origem * n.widthStep + x_origem * n.nChannels)[0];
                            green_o = (byte)(dataPtr2 + y_origem * n.widthStep + x_origem * n.nChannels)[1];
                            red_o = (byte)(dataPtr2 + y_origem * n.widthStep + x_origem * n.nChannels)[2];

                            (dataPtr + y * m.widthStep + x * m.nChannels)[0] = blue_o;
                            (dataPtr + y * m.widthStep + x * m.nChannels)[1] = green_o;
                            (dataPtr + y * m.widthStep + x * m.nChannels)[2] = red_o;*/

                        } else
                        {
                            dataPtr[0] = 0;
                            dataPtr[1] = 0;
                            dataPtr[2] = 0;
                            /*(dataPtr + y * m.widthStep + x * m.nChannels)[0] = 0;
                            (dataPtr + y * m.widthStep + x * m.nChannels)[1] = 0;
                            (dataPtr + y * m.widthStep + x * m.nChannels)[2] = 0;*/
                        }

                        dataPtr += n.nChannels;
                    }
                    dataPtr += padding;
                }
            }


        }

        public static void Rotation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float angle)
        {
            unsafe
            {
                // obter apontador do inicio da imagem
                MIplImage m = img.MIplImage;
                MIplImage n = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr2 = (byte*)n.imageData.ToPointer();
                byte* aux;
                byte blue_o, green_o, red_o;
                double width = img.Width;
                double height = img.Height;
                int x_origem, y_origem;
                int y, x;
                int padding = m.widthStep - m.nChannels * m.width;

                double cos_angle =Math.Cos(angle);
                double sin_angle =Math.Sin(angle);

                double hw = width / 2;
                double hh = height / 2;

                double xx, yy;

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        // calcula endereço do pixel no ponto (x,y)
                        xx = ((x - hw) * cos_angle - (hh - y)* sin_angle + hw);
                        yy = (hh - (x - hw) * sin_angle - (hh - y) * cos_angle);

                        x_origem = (int)Math.Round(xx);
                        y_origem = (int)Math.Round(yy);

                        aux = dataPtr2 + y_origem * n.widthStep + x_origem * n.nChannels;

                        if (x_origem >= 0 && y_origem >= 0 && x_origem < width && y_origem < height)
                        {
                            dataPtr[0] = aux[0];
                            dataPtr[1] = aux[1];
                            dataPtr[2] = aux[2];
                            /*blue_o = (byte)(dataPtr2 + y_origem * n.widthStep + x_origem * n.nChannels)[0];
                            green_o = (byte)(dataPtr2 + y_origem * n.widthStep + x_origem * n.nChannels)[1];
                            red_o = (byte)(dataPtr2 + y_origem * n.widthStep + x_origem * n.nChannels)[2];

                            (dataPtr + y * m.widthStep + x * m.nChannels)[0] = blue_o;
                            (dataPtr + y * m.widthStep + x * m.nChannels)[1] = green_o;
                            (dataPtr + y * m.widthStep + x * m.nChannels)[2] = red_o;*/

                        }
                        else
                        {
                            dataPtr[0] = 0;
                            dataPtr[1] = 0;
                            dataPtr[2] = 0;
                            /*(dataPtr + y * m.widthStep + x * m.nChannels)[0] = 0;
                            (dataPtr + y * m.widthStep + x * m.nChannels)[1] = 0;
                            (dataPtr + y * m.widthStep + x * m.nChannels)[2] = 0;*/
                        }
                        dataPtr += n.nChannels;
                    }
                    dataPtr += padding;
                }
            }
        }

        public static void Scale(Image<Bgr, byte> imgDestino, Image<Bgr, byte> imgOrigem, float scaleFactor)
        {
            unsafe
            {
                // obter apontador do inicio da imagem
                MIplImage m = imgDestino.MIplImage;
                MIplImage n = imgOrigem.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtr2 = (byte*)n.imageData.ToPointer();
                byte* aux;
                byte blue_o, green_o, red_o;
                double width = imgDestino.Width;
                double height = imgDestino.Height;
                int x_origem, y_origem;
                int y, x;
                int padding = m.widthStep - m.nChannels * m.width;

                double xx, yy;

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        // calcula endereço do pixel no ponto (x,y)
                        xx = x / scaleFactor;
                        yy = y / scaleFactor;

                        x_origem = (int)Math.Round(xx);
                        y_origem = (int)Math.Round(yy);

                        aux = dataPtr2 + y_origem * n.widthStep + x_origem * n.nChannels;

                        if (x_origem >= 0 && y_origem >= 0 && x_origem < width && y_origem < height)
                        {
                            dataPtr[0] = aux[0];
                            dataPtr[1] = aux[1];
                            dataPtr[2] = aux[2];

                           /* blue_o = (byte)(dataPtr2 + y_origem * n.widthStep + x_origem * n.nChannels)[0];
                            green_o = (byte)(dataPtr2 + y_origem * n.widthStep + x_origem * n.nChannels)[1];
                            red_o = (byte)(dataPtr2 + y_origem * n.widthStep + x_origem * n.nChannels)[2];

                            (dataPtr + y * m.widthStep + x * m.nChannels)[0] = blue_o;
                            (dataPtr + y * m.widthStep + x * m.nChannels)[1] = green_o;
                            (dataPtr + y * m.widthStep + x * m.nChannels)[2] = red_o;*/

                        }
                        else
                        {
                            dataPtr[0] = 0;
                            dataPtr[1] = 0;
                            dataPtr[2] = 0;

                            /*(dataPtr + y * m.widthStep + x * m.nChannels)[0] = 0;
                            (dataPtr + y * m.widthStep + x * m.nChannels)[1] = 0;
                            (dataPtr + y * m.widthStep + x * m.nChannels)[2] = 0;*/
                        }
                        dataPtr += n.nChannels;
                    }
                    dataPtr += padding;
                }
            }
        }

        public static void Scale_point_xy(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, double scaleFactor, int centerX, int centerY)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                // obter apontador do inicio da imagem
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image

                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtrCopy = (byte*)mCopy.imageData.ToPointer(); // Pointer to the copied image

                //byte blue, green, red;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int widthStep = m.widthStep;
                int widthStepCopy = mCopy.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y, posX, posY;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            posX = (int)Math.Round((x / scaleFactor) + centerX - ((width / 2) / scaleFactor));
                            posY = (int)Math.Round((y / scaleFactor) + centerY - ((height / 2) / scaleFactor));

                            // calcula endereço do pixel no ponto (x,y)
                            if (posX <= width - 1 && posX >= 0 && posY <= height - 1 && posY >= 0)
                            {
                                (dataPtr + y * widthStep + x * nChan)[0] = (byte)(dataPtrCopy + (posY) * widthStepCopy + (posX) * nChan)[0];
                                (dataPtr + y * widthStep + x * nChan)[1] = (byte)(dataPtrCopy + (posY) * widthStepCopy + (posX) * nChan)[1];
                                (dataPtr + y * widthStep + x * nChan)[2] = (byte)(dataPtrCopy + (posY) * widthStepCopy + (posX) * nChan)[2];
                            }
                            else
                            {
                                (dataPtr + y * widthStep + x * nChan)[0] = 0;
                                (dataPtr + y * widthStep + x * nChan)[1] = 0;
                                (dataPtr + y * widthStep + x * nChan)[2] = 0;
                            }
                        }
                    }
                }

            }
        }

        public static void Mean(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage n = img.MIplImage;
                MIplImage m = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtrDes = (byte*)n.imageData.ToPointer();
                int blue, green, red;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int y, x;

                dataPtr += n.widthStep + nChan;

                dataPtrDes += n.widthStep + nChan;

                for (y = 1; y < height - 1; y++)
                {
                    for (x = 1; x < width - 1; x++)
                    {
                        blue = dataPtr[0];
                        green = dataPtr[1];
                        red = dataPtr[2];

                        blue += (dataPtr + nChan)[0];
                        green += (dataPtr + nChan)[1];
                        red += (dataPtr + nChan)[2];

                        blue += (dataPtr - nChan)[0];
                        green += (dataPtr - nChan)[1];
                        red += (dataPtr - nChan)[2];

                        blue += (dataPtr - m.widthStep)[0];
                        green += (dataPtr - m.widthStep)[1];
                        red += (dataPtr - m.widthStep)[2];

                        blue += (dataPtr + m.widthStep)[0];
                        green += (dataPtr + m.widthStep)[1];
                        red += (dataPtr + m.widthStep)[2];

                        blue += (dataPtr - m.widthStep - nChan)[0];
                        green += (dataPtr - m.widthStep - nChan)[1];
                        red += (dataPtr - m.widthStep - nChan)[2];

                        blue += (dataPtr - m.widthStep + nChan)[0];
                        green += (dataPtr - m.widthStep + nChan)[1];
                        red += (dataPtr - m.widthStep + nChan)[2];

                        blue += (dataPtr + m.widthStep - nChan)[0];
                        green += (dataPtr + m.widthStep - nChan)[1];
                        red += (dataPtr + m.widthStep - nChan)[2];

                        blue += (dataPtr + m.widthStep + nChan)[0];
                        green += (dataPtr + m.widthStep + nChan)[1];
                        red += (dataPtr + m.widthStep + nChan)[2];

                        blue = (int)(Math.Round(blue / 9.0));
                        green = (int)(Math.Round(green / 9.0));
                        red = (int)(Math.Round(red / 9.0));

                        dataPtrDes[0] = (byte)(blue);
                        dataPtrDes[1] = (byte)(green);
                        dataPtrDes[2] = (byte)(red);

                        // advance the pointer to the next pixel
                        dataPtr += nChan;
                        dataPtrDes += nChan;

                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += 2 * nChan;
                    dataPtr += padding;

                    dataPtrDes += 2 * nChan;
                    dataPtrDes += padding;
                }

                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += nChan;
                dataPtrDes += nChan;

                // primeira linha
                for (x = 1; x < width - 1; x++)
                {
                    blue = (dataPtr[0]) * 2;
                    green = (dataPtr[1]) * 2;
                    red = (dataPtr[2]) * 2;

                    blue += ((dataPtr + nChan)[0]) * 2;
                    green += ((dataPtr + nChan)[1]) * 2;
                    red += ((dataPtr + nChan)[2]) * 2;

                    blue += ((dataPtr - nChan)[0])*2;
                    green += ((dataPtr - nChan)[1]) * 2;
                    red += ((dataPtr - nChan)[2]) * 2;

                    blue += (dataPtr + m.widthStep)[0];
                    green += (dataPtr + m.widthStep)[1];
                    red += (dataPtr + m.widthStep)[2];

                    blue += (dataPtr + m.widthStep - nChan)[0];
                    green += (dataPtr + m.widthStep - nChan)[1];
                    red += (dataPtr + m.widthStep - nChan)[2];

                    blue += (dataPtr + m.widthStep + nChan)[0];
                    green += (dataPtr + m.widthStep + nChan)[1];
                    red += (dataPtr + m.widthStep + nChan)[2];

                    blue = (int)(Math.Round(blue / 9.0));
                    green = (int)(Math.Round(green / 9.0));
                    red = (int)(Math.Round(red / 9.0));

                    dataPtrDes[0] = (byte)(blue);
                    dataPtrDes[1] = (byte)(green);
                    dataPtrDes[2] = (byte)(red);

                    // advance the pointer to the next pixel
                    dataPtr += nChan;
                    dataPtrDes += nChan;
                }

                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += nChan + ((height - 1) * n.widthStep);
                dataPtrDes += nChan + ((height - 1) * n.widthStep);

                //ultima linha
                for (x = 1; x < width - 1; x++)
                {
                    blue = (dataPtr[0]) * 2;
                    green = (dataPtr[1]) * 2;
                    red = (dataPtr[2]) * 2;

                    blue += ((dataPtr + nChan)[0]) * 2;
                    green += ((dataPtr + nChan)[1]) * 2;
                    red += ((dataPtr + nChan)[2]) * 2;

                    blue += ((dataPtr - nChan)[0]) * 2;
                    green += ((dataPtr - nChan)[1]) * 2;
                    red += ((dataPtr - nChan)[2]) * 2;

                    blue += (dataPtr - m.widthStep)[0];
                    green += (dataPtr - m.widthStep)[1];
                    red += (dataPtr - m.widthStep)[2];

                    blue += (dataPtr - m.widthStep - nChan)[0];
                    green += (dataPtr - m.widthStep - nChan)[1];
                    red += (dataPtr - m.widthStep - nChan)[2];

                    blue += (dataPtr - m.widthStep + nChan)[0];
                    green += (dataPtr - m.widthStep + nChan)[1];
                    red += (dataPtr - m.widthStep + nChan)[2];

                    blue = (int)(Math.Round(blue / 9.0));
                    green = (int)(Math.Round(green / 9.0));
                    red = (int)(Math.Round(red / 9.0));

                    dataPtrDes[0] = (byte)(blue);
                    dataPtrDes[1] = (byte)(green);
                    dataPtrDes[2] = (byte)(red);

                    // advance the pointer to the next pixel
                    dataPtr += nChan;
                    dataPtrDes += nChan;
                }

                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += n.widthStep;
                dataPtrDes += n.widthStep;

                //coluna esquerda
                for (y = 1; y < height - 1; y++)
                {
                    blue = (dataPtr[0]) * 2;
                    green = (dataPtr[1]) * 2;
                    red = (dataPtr[2]) * 2;

                    blue += ((dataPtr + nChan)[0]);
                    green += ((dataPtr + nChan)[1]);
                    red += ((dataPtr + nChan)[2]);

                    blue += (dataPtr + m.widthStep)[0] * 2;
                    green += (dataPtr + m.widthStep)[1] * 2;
                    red += (dataPtr + m.widthStep)[2] * 2;

                    blue += (dataPtr + m.widthStep + nChan)[0];
                    green += (dataPtr + m.widthStep + nChan)[1];
                    red += (dataPtr + m.widthStep + nChan)[2];

                    blue += (dataPtr - m.widthStep)[0] * 2;
                    green += (dataPtr - m.widthStep)[1] * 2;
                    red += (dataPtr - m.widthStep)[2] * 2;

                    blue += (dataPtr - m.widthStep + nChan)[0];
                    green += (dataPtr - m.widthStep + nChan)[1];
                    red += (dataPtr - m.widthStep + nChan)[2];

                    blue = (int)(Math.Round(blue / 9.0));
                    green = (int)(Math.Round(green / 9.0));
                    red = (int)(Math.Round(red / 9.0));

                    dataPtrDes[0] = (byte)(blue);
                    dataPtrDes[1] = (byte)(green);
                    dataPtrDes[2] = (byte)(red);

                    // advance the pointer to the next pixel
                    dataPtr += m.widthStep;
                    dataPtrDes += m.widthStep;
                }

                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += n.widthStep + ((width-1) * nChan);
                dataPtrDes += n.widthStep + ((width-1)  * nChan);

                //coluna direita
                for (y = 1; y < height - 1; y++)
                {
                    blue = (dataPtr[0]) * 2;
                    green = (dataPtr[1]) * 2;
                    red = (dataPtr[2]) * 2;

                    blue += ((dataPtr - nChan)[0]);
                    green += ((dataPtr - nChan)[1]);
                    red += ((dataPtr - nChan)[2]);

                    blue += (dataPtr + m.widthStep)[0] * 2;
                    green += (dataPtr + m.widthStep)[1] * 2;
                    red += (dataPtr + m.widthStep)[2] * 2;

                    blue += (dataPtr + m.widthStep - nChan)[0];
                    green += (dataPtr + m.widthStep - nChan)[1];
                    red += (dataPtr + m.widthStep - nChan)[2];

                    blue += (dataPtr - m.widthStep)[0] * 2;
                    green += (dataPtr - m.widthStep)[1] * 2;
                    red += (dataPtr - m.widthStep)[2] * 2;

                    blue += (dataPtr - m.widthStep - nChan)[0];
                    green += (dataPtr - m.widthStep - nChan)[1];
                    red += (dataPtr - m.widthStep - nChan)[2];

                    blue = (int)(Math.Round(blue / 9.0));
                    green = (int)(Math.Round(green / 9.0));
                    red = (int)(Math.Round(red / 9.0));

                    dataPtrDes[0] = (byte)(blue);
                    dataPtrDes[1] = (byte)(green);
                    dataPtrDes[2] = (byte)(red);

                    // advance the pointer to the next pixel
                    dataPtr += m.widthStep;
                    dataPtrDes += m.widthStep;
                }

                //canto superior esquerdo
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                blue = (dataPtr[0]) * 4;
                green = (dataPtr[1]) * 4;
                red = (dataPtr[2]) * 4;

                blue += (dataPtr + nChan)[0] * 2;
                green += (dataPtr + nChan)[1] * 2;
                red += (dataPtr + nChan)[2] * 2;

                blue += (dataPtr + m.widthStep)[0] * 2;
                green += (dataPtr + m.widthStep)[1] * 2;
                red += (dataPtr + m.widthStep)[2] * 2;

                blue += (dataPtr + nChan + m.widthStep)[0];
                green += (dataPtr + nChan + m.widthStep)[1];
                red += (dataPtr + nChan + m.widthStep)[2];

                blue = (int)(Math.Round(blue / 9.0));
                green = (int)(Math.Round(green / 9.0));
                red = (int)(Math.Round(red / 9.0));

                dataPtrDes[0] = (byte)(blue);
                dataPtrDes[1] = (byte)(green);
                dataPtrDes[2] = (byte)(red);

                //canto superior direito
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += (width-1) * nChan;
                dataPtrDes += (width-1) * nChan;

                blue = (dataPtr[0]) * 4;
                green = (dataPtr[1]) * 4;
                red = (dataPtr[2]) * 4;

                blue += (dataPtr - nChan)[0] * 2;
                green += (dataPtr - nChan)[1] * 2;
                red += (dataPtr - nChan)[2] * 2;

                blue += (dataPtr + m.widthStep)[0] * 2;
                green += (dataPtr + m.widthStep)[1] * 2;
                red += (dataPtr + m.widthStep)[2] * 2;

                blue += (dataPtr - nChan + m.widthStep)[0];
                green += (dataPtr - nChan + m.widthStep)[1];
                red += (dataPtr - nChan + m.widthStep)[2];

                blue = (int)(Math.Round(blue / 9.0));
                green = (int)(Math.Round(green / 9.0));
                red = (int)(Math.Round(red / 9.0));

                dataPtrDes[0] = (byte)(blue);
                dataPtrDes[1] = (byte)(green);
                dataPtrDes[2] = (byte)(red);

                //canto inferior esquerdo
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += n.widthStep * (height-1);
                dataPtrDes += n.widthStep * (height-1);

                blue = (dataPtr[0]) * 4;
                green = (dataPtr[1]) * 4;
                red = (dataPtr[2]) * 4;

                blue += (dataPtr + nChan)[0] * 2;
                green += (dataPtr + nChan)[1] * 2;
                red += (dataPtr + nChan)[2] * 2;

                blue += (dataPtr - m.widthStep)[0] * 2;
                green += (dataPtr - m.widthStep)[1] * 2;
                red += (dataPtr - m.widthStep)[2] * 2;

                blue += (dataPtr + nChan - m.widthStep)[0];
                green += (dataPtr + nChan - m.widthStep)[1];
                red += (dataPtr + nChan - m.widthStep)[2];

                blue = (int)(Math.Round(blue / 9.0));
                green = (int)(Math.Round(green / 9.0));
                red = (int)(Math.Round(red / 9.0));

                dataPtrDes[0] = (byte)(blue);
                dataPtrDes[1] = (byte)(green);
                dataPtrDes[2] = (byte)(red);


                //canto inferior direito
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += n.widthStep * (height - 1) + nChan * (width - 1);
                dataPtrDes += n.widthStep * (height - 1) + nChan * (width - 1);

                blue = (dataPtr[0]) * 4;
                green = (dataPtr[1]) * 4;
                red = (dataPtr[2]) * 4;

                blue += (dataPtr - nChan)[0] * 2;
                green += (dataPtr - nChan)[1] * 2;
                red += (dataPtr - nChan)[2] * 2;

                blue += (dataPtr - m.widthStep)[0] * 2;
                green += (dataPtr - m.widthStep)[1] * 2;
                red += (dataPtr - m.widthStep)[2] * 2;

                blue += (dataPtr - nChan - m.widthStep)[0];
                green += (dataPtr - nChan - m.widthStep)[1];
                red += (dataPtr - nChan - m.widthStep)[2];

                blue = (int)(Math.Round(blue / 9.0));
                green = (int)(Math.Round(green / 9.0));
                red = (int)(Math.Round(red / 9.0));

                dataPtrDes[0] = (byte)(blue);
                dataPtrDes[1] = (byte)(green);
                dataPtrDes[2] = (byte)(red);


            }

        }

        public static void NonUniform(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float[,] matrix, float matrixWeight, float offset){
            unsafe
            {
                MIplImage n = img.MIplImage;
                MIplImage m = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtrDes = (byte*)n.imageData.ToPointer();
                float blue, green, red;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int wStep = m.widthStep;
                int padding = wStep - m.nChannels * m.width; // alinhament bytes (padding)
                int y, x;

                dataPtr += n.widthStep + nChan;

                dataPtrDes += n.widthStep + nChan;

                for (y = 1; y < height - 1; y++)
                {
                    for (x = 1; x < width - 1; x++)
                    {
                        blue = dataPtr[0] * matrix[1, 1];
                        green = dataPtr[1] * matrix[1, 1];
                        red = dataPtr[2] * matrix[1, 1];

                        blue += (dataPtr + nChan)[0] * matrix[1, 2];
                        green += (dataPtr + nChan)[1] * matrix[1, 2];
                        red += (dataPtr + nChan)[2] * matrix[1, 2];

                        blue += (dataPtr - nChan)[0] * matrix[1, 0];
                        green += (dataPtr - nChan)[1] * matrix[1, 0];
                        red += (dataPtr - nChan)[2] * matrix[1, 0];

                        blue += (dataPtr - wStep)[0] * matrix[0, 1];
                        green += (dataPtr - wStep)[1] * matrix[0, 1];
                        red += (dataPtr - wStep)[2] * matrix[0, 1];

                        blue += (dataPtr + wStep)[0] * matrix[2, 1];
                        green += (dataPtr + wStep)[1] * matrix[2, 1];
                        red += (dataPtr + wStep)[2] * matrix[2, 1];

                        blue += (dataPtr - wStep - nChan)[0] * matrix[0, 0];
                        green += (dataPtr - wStep - nChan)[1] * matrix[0, 0];
                        red += (dataPtr - wStep - nChan)[2] * matrix[0, 0];

                        blue += (dataPtr - wStep + nChan)[0] * matrix[0, 2];
                        green += (dataPtr - wStep + nChan)[1] * matrix[0, 2];
                        red += (dataPtr - wStep + nChan)[2] * matrix[0, 2];

                        blue += (dataPtr + wStep - nChan)[0] * matrix[2, 0];
                        green += (dataPtr + wStep - nChan)[1] * matrix[2, 0];
                        red += (dataPtr + wStep - nChan)[2] * matrix[2, 0];

                        blue += (dataPtr + wStep + nChan)[0] * matrix[2, 2];
                        green += (dataPtr + wStep + nChan)[1] * matrix[2, 2];
                        red += (dataPtr + wStep + nChan)[2] * matrix[2, 2];

                        blue = (float)(Math.Round(blue / matrixWeight + offset));
                        green = (float)(Math.Round(green / matrixWeight + offset));
                        red = (float)(Math.Round(red / matrixWeight + offset));

                        if (blue > 255)
                            blue = 255;
                        else if (blue < 0)
                            blue = 0;
                        if (green > 255)
                            green = 255;
                        else if (green < 0)
                            green = 0;
                        if (red > 255)
                            red = 255;
                        else if (red < 0)
                            red = 0;


                        dataPtrDes[0] = (byte)(blue);
                        dataPtrDes[1] = (byte)(green);
                        dataPtrDes[2] = (byte)(red);

                        // advance the pointer to the next pixel
                        dataPtr += nChan;
                        dataPtrDes += nChan;

                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += 2 * nChan + padding;
                    dataPtrDes += 2 * nChan + padding;
                }

                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += nChan;
                dataPtrDes += nChan;

                // primeira linha
                for (x = 1; x < width - 1; x++)
                {
                    blue = (dataPtr[0]) * (matrix[1, 1] + matrix[0, 1]);
                    green = (dataPtr[1]) * (matrix[1, 1] + matrix[0, 1]);
                    red = (dataPtr[2]) * (matrix[1, 1] + matrix[0, 1]);

                    blue += ((dataPtr + nChan)[0]) * (matrix[1, 2] + matrix[0, 2]);
                    green += ((dataPtr + nChan)[1]) * (matrix[1, 2] + matrix[0, 2]); ;
                    red += ((dataPtr + nChan)[2]) * (matrix[1, 2] + matrix[0, 2]); ;

                    blue += ((dataPtr - nChan)[0]) * (matrix[1, 0] + matrix[0, 0]); ;
                    green += ((dataPtr - nChan)[1]) * (matrix[1, 0] + matrix[0, 0]);
                    red += ((dataPtr - nChan)[2]) * (matrix[1, 0] + matrix[0, 0]);

                    blue += (dataPtr + wStep)[0] * matrix[2, 1];
                    green += (dataPtr + wStep)[1] * matrix[2, 1];
                    red += (dataPtr + wStep)[2] * matrix[2, 1];

                    blue += (dataPtr + wStep - nChan)[0] * matrix[2, 0];
                    green += (dataPtr + wStep - nChan)[1] * matrix[2, 0];
                    red += (dataPtr + wStep - nChan)[2] * matrix[2, 0];

                    blue += (dataPtr + wStep + nChan)[0] * matrix[2, 2];
                    green += (dataPtr + wStep + nChan)[1] * matrix[2, 2];
                    red += (dataPtr + wStep + nChan)[2] * matrix[2, 2];

                    blue = (float)(Math.Round(blue / matrixWeight + offset));
                    green = (float)(Math.Round(green / matrixWeight + offset));
                    red = (float)(Math.Round(red / matrixWeight + offset));

                    if (blue > 255)
                        blue = 255;
                    else if (blue < 0)
                        blue = 0;
                    if (green > 255)
                        green = 255;
                    else if (green < 0)
                        green = 0;
                    if (red > 255)
                        red = 255;
                    else if (red < 0)
                        red = 0;


                    dataPtrDes[0] = (byte)(blue);
                    dataPtrDes[1] = (byte)(green);
                    dataPtrDes[2] = (byte)(red);

                    // advance the pointer to the next pixel
                    dataPtr += nChan;
                    dataPtrDes += nChan;
                }

                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += nChan + ((height - 1) * n.widthStep);
                dataPtrDes += nChan + ((height - 1) * n.widthStep);

                //ultima linha
                for (x = 1; x < width - 1; x++)
                {
                    blue = (dataPtr[0]) * (matrix[1, 1] + matrix[2, 1]);
                    green = (dataPtr[1]) * (matrix[1, 1] + matrix[2, 1]);
                    red = (dataPtr[2]) * (matrix[1, 1] + matrix[2, 1]);

                    blue += ((dataPtr + nChan)[0]) * (matrix[1, 2] + matrix[2, 2]);
                    green += ((dataPtr + nChan)[1]) * (matrix[1, 2] + matrix[2, 2]);
                    red += ((dataPtr + nChan)[2]) * (matrix[1, 2] + matrix[2, 2]);

                    blue += ((dataPtr - nChan)[0]) * (matrix[1, 0] + matrix[2, 0]);
                    green += ((dataPtr - nChan)[1]) * (matrix[1, 0] + matrix[2, 0]);
                    red += ((dataPtr - nChan)[2]) * (matrix[1, 0] + matrix[2, 0]);

                    blue += (dataPtr - wStep)[0] * matrix[0, 1];
                    green += (dataPtr - wStep)[1] * matrix[0, 1];
                    red += (dataPtr - wStep)[2] * matrix[0, 1];

                    blue += (dataPtr - wStep - nChan)[0] * matrix[0, 0];
                    green += (dataPtr - wStep - nChan)[1] * matrix[0, 0];
                    red += (dataPtr - wStep - nChan)[2] * matrix[0, 0];

                    blue += (dataPtr - wStep + nChan)[0] * matrix[0, 2];
                    green += (dataPtr - wStep + nChan)[1] * matrix[0, 2];
                    red += (dataPtr - wStep + nChan)[2] * matrix[0, 2];

                    blue = (float)(Math.Round(blue / matrixWeight + offset));
                    green = (float)(Math.Round(green / matrixWeight + offset));
                    red = (float)(Math.Round(red / matrixWeight + offset));

                    if (blue > 255)
                        blue = 255;
                    else if (blue < 0)
                        blue = 0;
                    if (green > 255)
                        green = 255;
                    else if (green < 0)
                        green = 0;
                    if (red > 255)
                        red = 255;
                    else if (red < 0)
                        red = 0;


                    dataPtrDes[0] = (byte)(blue);
                    dataPtrDes[1] = (byte)(green);
                    dataPtrDes[2] = (byte)(red);

                    // advance the pointer to the next pixel
                    dataPtr += nChan;
                    dataPtrDes += nChan;
                }

                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += n.widthStep;
                dataPtrDes += n.widthStep;

                //coluna esquerda
                for (y = 1; y < height - 1; y++)
                {
                    blue = (dataPtr[0]) * (matrix[1, 1] + matrix[1, 0]);
                    green = (dataPtr[1]) * (matrix[1, 1] + matrix[1, 0]);
                    red = (dataPtr[2]) * (matrix[1, 1] + matrix[1, 0]);

                    blue += ((dataPtr + nChan)[0]) * matrix[1, 2];
                    green += ((dataPtr + nChan)[1]) * matrix[1, 2];
                    red += ((dataPtr + nChan)[2]) * matrix[1, 2];

                    blue += (dataPtr + wStep)[0] * (matrix[2, 1] + matrix[2, 0]);
                    green += (dataPtr + wStep)[1] * (matrix[2, 1] + matrix[2, 0]);
                    red += (dataPtr + wStep)[2] * (matrix[2, 1] + matrix[2, 0]);

                    blue += (dataPtr + wStep + nChan)[0] * matrix[2, 2];
                    green += (dataPtr + wStep + nChan)[1] * matrix[2, 2];
                    red += (dataPtr + wStep + nChan)[2] * matrix[2, 2];

                    blue += (dataPtr - wStep)[0] * (matrix[0, 1] + matrix[0, 0]);
                    green += (dataPtr - wStep)[1] * (matrix[0, 1] + matrix[0, 0]);
                    red += (dataPtr - wStep)[2] * (matrix[0, 1] + matrix[0, 0]);

                    blue += (dataPtr - wStep + nChan)[0] * matrix[0, 2];
                    green += (dataPtr - wStep + nChan)[1] * matrix[0, 2];
                    red += (dataPtr - wStep + nChan)[2] * matrix[0, 2];

                    blue = (float)(Math.Round(blue / matrixWeight + offset));
                    green = (float)(Math.Round(green / matrixWeight + offset));
                    red = (float)(Math.Round(red / matrixWeight + offset));

                    if (blue > 255)
                        blue = 255;
                    else if (blue < 0)
                        blue = 0;
                    if (green > 255)
                        green = 255;
                    else if (green < 0)
                        green = 0;
                    if (red > 255)
                        red = 255;
                    else if (red < 0)
                        red = 0;


                    dataPtrDes[0] = (byte)(blue);
                    dataPtrDes[1] = (byte)(green);
                    dataPtrDes[2] = (byte)(red);

                    // advance the pointer to the next pixel
                    dataPtr += m.widthStep;
                    dataPtrDes += m.widthStep;
                }

                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += n.widthStep + ((width - 1) * nChan);
                dataPtrDes += n.widthStep + ((width - 1) * nChan);

                //coluna direita
                for (y = 1; y < height - 1; y++)
                {
                    blue = (dataPtr[0]) * (matrix[1, 1] + matrix[1, 2]);
                    green = (dataPtr[1]) * (matrix[1, 1] + matrix[1, 2]);
                    red = (dataPtr[2]) * (matrix[1, 1] + matrix[1, 2]);

                    blue += ((dataPtr - nChan)[0]) * matrix[1, 0];
                    green += ((dataPtr - nChan)[1]) * matrix[1, 0];
                    red += ((dataPtr - nChan)[2]) * matrix[1, 0];

                    blue += (dataPtr + wStep)[0] * (matrix[2, 1] + matrix[2, 2]);
                    green += (dataPtr + wStep)[1] * (matrix[2, 1] + matrix[2, 2]);
                    red += (dataPtr + wStep)[2] * (matrix[2, 1] + matrix[2, 2]);

                    blue += (dataPtr + wStep - nChan)[0] * matrix[2, 0];
                    green += (dataPtr + wStep - nChan)[1] * matrix[2, 0];
                    red += (dataPtr + wStep - nChan)[2] * matrix[2, 0];

                    blue += (dataPtr - wStep)[0] * (matrix[0, 1] + matrix[0, 2]);
                    green += (dataPtr - wStep)[1] * (matrix[0, 1] + matrix[0, 2]);
                    red += (dataPtr - wStep)[2] * (matrix[0, 1] + matrix[0, 2]);

                    blue += (dataPtr - wStep - nChan)[0] * matrix[0, 0];
                    green += (dataPtr - wStep - nChan)[1] * matrix[0, 0];
                    red += (dataPtr - wStep - nChan)[2] * matrix[0, 0];

                    blue = (float)(Math.Round(blue / matrixWeight + offset));
                    green = (float)(Math.Round(green / matrixWeight + offset));
                    red = (float)(Math.Round(red / matrixWeight + offset));

                    if (blue > 255)
                        blue = 255;
                    else if (blue < 0)
                        blue = 0;
                    if (green > 255)
                        green = 255;
                    else if (green < 0)
                        green = 0;
                    if (red > 255)
                        red = 255;
                    else if (red < 0)
                        red = 0;


                    dataPtrDes[0] = (byte)(blue);
                    dataPtrDes[1] = (byte)(green);
                    dataPtrDes[2] = (byte)(red);

                    // advance the pointer to the next pixel
                    dataPtr += m.widthStep;
                    dataPtrDes += m.widthStep;
                }

                //canto superior esquerdo
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                blue = (dataPtr[0]) * (matrix[1 ,1] + matrix[0, 1] + matrix[0, 0] + matrix[1, 0]);
                green = (dataPtr[1]) * (matrix[1, 1] + matrix[0, 1] + matrix[0, 0] + matrix[1, 0]);
                red = (dataPtr[2]) * (matrix[1, 1] + matrix[0, 1] + matrix[0, 0] + matrix[1, 0]);

                blue += (dataPtr + nChan)[0] * (matrix[0,2] + matrix[1,2]);
                green += (dataPtr + nChan)[1] * (matrix[0, 2] + matrix[1, 2]);
                red += (dataPtr + nChan)[2] * (matrix[0, 2] + matrix[1, 2]);

                blue += (dataPtr + wStep)[0] * (matrix[2, 0] + matrix[2, 1]);
                green += (dataPtr + wStep)[1] * (matrix[2, 0] + matrix[2, 1]);
                red += (dataPtr + wStep)[2] * (matrix[2, 0] + matrix[2, 1]);

                blue += (dataPtr + nChan + wStep)[0] * matrix[2,2];
                green += (dataPtr + nChan + wStep)[1] * matrix[2, 2];
                red += (dataPtr + nChan + wStep)[2] * matrix[2, 2];

                blue = (float)(Math.Round(blue / matrixWeight + offset));
                green = (float)(Math.Round(green / matrixWeight + offset));
                red = (float)(Math.Round(red / matrixWeight + offset));

                if (blue > 255)
                    blue = 255;
                else if (blue < 0)
                    blue = 0;
                if (green > 255)
                    green = 255;
                else if (green < 0)
                    green = 0;
                if (red > 255)
                    red = 255;
                else if (red < 0)
                    red = 0;


                dataPtrDes[0] = (byte)(blue);
                dataPtrDes[1] = (byte)(green);
                dataPtrDes[2] = (byte)(red);

                //canto superior direito
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += (width - 1) * nChan;
                dataPtrDes += (width - 1) * nChan;

                blue = (dataPtr[0]) * (matrix[1, 1] + matrix[0, 1] + matrix[0, 2] + matrix[1, 2]);
                green = (dataPtr[1]) * (matrix[1, 1] + matrix[0, 1] + matrix[0, 2] + matrix[1, 2]);
                red = (dataPtr[2]) * (matrix[1, 1] + matrix[0, 1] + matrix[0, 2] + matrix[1, 2]);

                blue += (dataPtr - nChan)[0] * (matrix[1, 0] + matrix[0, 0]);
                green += (dataPtr - nChan)[1] * (matrix[1, 0] + matrix[0, 0]);
                red += (dataPtr - nChan)[2] * (matrix[1, 0] + matrix[0, 0]);

                blue += (dataPtr + wStep)[0] * (matrix[2, 1] + matrix[2, 2]);
                green += (dataPtr + wStep)[1] * (matrix[2, 1] + matrix[2, 2]);
                red += (dataPtr + wStep)[2] * (matrix[2, 1] + matrix[2, 2]);

                blue += (dataPtr - nChan + wStep)[0] * matrix[2, 0];
                green += (dataPtr - nChan + wStep)[1] * matrix[2, 0];
                red += (dataPtr - nChan + wStep)[2] * matrix[2, 0];

                blue = (float)(Math.Round(blue / matrixWeight + offset));
                green = (float)(Math.Round(green / matrixWeight + offset));
                red = (float)(Math.Round(red / matrixWeight + offset));

                if (blue > 255)
                    blue = 255;
                else if (blue < 0)
                    blue = 0;
                if (green > 255)
                    green = 255;
                else if (green < 0)
                    green = 0;
                if (red > 255)
                    red = 255;
                else if (red < 0)
                    red = 0;


                dataPtrDes[0] = (byte)(blue);
                dataPtrDes[1] = (byte)(green);
                dataPtrDes[2] = (byte)(red);

                //canto inferior esquerdo
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += n.widthStep * (height - 1);
                dataPtrDes += n.widthStep * (height - 1);

                blue = (dataPtr[0]) * (matrix[1, 1] + matrix[1, 0] + matrix[2, 0] + matrix[2, 1]);
                green = (dataPtr[1]) * (matrix[1, 1] + matrix[1, 0] + matrix[2, 0] + matrix[2, 1]);
                red = (dataPtr[2]) * (matrix[1, 1] + matrix[1, 0] + matrix[2, 0] + matrix[2, 1]);

                blue += (dataPtr + nChan)[0] * (matrix[1, 2] + matrix[2, 2]);
                green += (dataPtr + nChan)[1] * (matrix[1, 2] + matrix[2, 2]);
                red += (dataPtr + nChan)[2] * (matrix[1, 2] + matrix[2, 2]);

                blue += (dataPtr - wStep)[0] * (matrix[0, 1] + matrix[0, 0]);
                green += (dataPtr - wStep)[1] * (matrix[0, 1] + matrix[0, 0]);
                red += (dataPtr - wStep)[2] * (matrix[0, 1] + matrix[0, 0]);

                blue += (dataPtr + nChan - wStep)[0] * matrix[0, 2];
                green += (dataPtr + nChan - wStep)[1] * matrix[0, 2];
                red += (dataPtr + nChan - wStep)[2] * matrix[0, 2];

                blue = (float)(Math.Round(blue / matrixWeight + offset));
                green = (float)(Math.Round(green / matrixWeight + offset));
                red = (float)(Math.Round(red / matrixWeight + offset));

                if (blue > 255)
                    blue = 255;
                else if (blue < 0)
                    blue = 0;
                if (green > 255)
                    green = 255;
                else if (green < 0)
                    green = 0;
                if (red > 255)
                    red = 255;
                else if (red < 0)
                    red = 0;


                dataPtrDes[0] = (byte)(blue);
                dataPtrDes[1] = (byte)(green);
                dataPtrDes[2] = (byte)(red);


                //canto inferior direito
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += n.widthStep * (height - 1) + nChan * (width - 1);
                dataPtrDes += n.widthStep * (height - 1) + nChan * (width - 1);

                blue = (dataPtr[0]) * (matrix[1, 1] + matrix[1, 2] + matrix[2, 1] + matrix[2, 2]);
                green = (dataPtr[1]) * (matrix[1, 1] + matrix[1, 2] + matrix[2, 1] + matrix[2, 2]);
                red = (dataPtr[2]) * (matrix[1, 1] + matrix[1, 2] + matrix[2, 1] + matrix[2, 2]);

                blue += (dataPtr - nChan)[0] * (matrix[1, 0] + matrix[2, 0]);
                green += (dataPtr - nChan)[1] * (matrix[1, 0] + matrix[2, 0]);
                red += (dataPtr - nChan)[2] * (matrix[1, 0] + matrix[2, 0]);

                blue += (dataPtr - wStep)[0] * (matrix[0, 1] + matrix[0, 2]);
                green += (dataPtr - wStep)[1] * (matrix[0, 1] + matrix[0, 2]);
                red += (dataPtr - wStep)[2] * (matrix[0, 1] + matrix[0, 2]);

                blue += (dataPtr - nChan - wStep)[0] * matrix[0, 0];
                green += (dataPtr - nChan - wStep)[1] * matrix[0, 0];
                red += (dataPtr - nChan - wStep)[2] * matrix[0, 0];

                blue = (float)(Math.Round(blue / matrixWeight + offset));
                green = (float)(Math.Round(green / matrixWeight + offset));
                red = (float)(Math.Round(red / matrixWeight + offset));

                if (blue > 255)
                    blue = 255;
                else if (blue < 0)
                    blue = 0;
                if (green > 255)
                    green = 255;
                else if (green < 0)
                    green = 0;
                if (red > 255)
                    red = 255;
                else if (red < 0)
                    red = 0;


                dataPtrDes[0] = (byte)(blue);
                dataPtrDes[1] = (byte)(green);
                dataPtrDes[2] = (byte)(red);
            }
        }

        public static void Sobel(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage n = img.MIplImage;
                MIplImage m = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtrDes = (byte*)n.imageData.ToPointer();
                float blue, green, red, bluex, greenx, redx, bluey, greeny, redy;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int wStep = m.widthStep;
                int padding = wStep - m.nChannels * m.width; // alinhament bytes (padding)
                int y, x;

                dataPtr += n.widthStep + nChan;

                dataPtrDes += n.widthStep + nChan;

                for (y = 1; y < height - 1; y++)
                {
                    for (x = 1; x < width - 1; x++)
                    {

                        //SX

                        bluex = (dataPtr + nChan)[0] * -2;
                        greenx = (dataPtr + nChan)[1] * -2;
                        redx = (dataPtr + nChan)[2] * -2;

                        bluex += (dataPtr - nChan)[0] * 2;
                        greenx += (dataPtr - nChan)[1] * 2;
                        redx += (dataPtr - nChan)[2] * 2;

                        bluex += (dataPtr - wStep - nChan)[0];
                        greenx += (dataPtr - wStep - nChan)[1];
                        redx += (dataPtr - wStep - nChan)[2];

                        bluex -= (dataPtr - wStep + nChan)[0];
                        greenx -= (dataPtr - wStep + nChan)[1];
                        redx -= (dataPtr - wStep + nChan)[2];

                        bluex += (dataPtr + wStep - nChan)[0];
                        greenx += (dataPtr + wStep - nChan)[1];
                        redx += (dataPtr + wStep - nChan)[2];

                        bluex -= (dataPtr + wStep + nChan)[0];
                        greenx -= (dataPtr + wStep + nChan)[1];
                        redx -= (dataPtr + wStep + nChan)[2];

                        //SY

                        bluey = (dataPtr + wStep)[0] * 2;
                        greeny = (dataPtr + wStep)[1] * 2;
                        redy = (dataPtr + wStep)[2] * 2;

                        bluey += (dataPtr - wStep)[0] * -2;
                        greeny += (dataPtr - wStep)[1] * -2;
                        redy += (dataPtr - wStep)[2] * -2;

                        bluey -= (dataPtr - wStep - nChan)[0];
                        greeny -= (dataPtr - wStep - nChan)[1];
                        redy -= (dataPtr - wStep - nChan)[2];

                        bluey -= (dataPtr - wStep + nChan)[0];
                        greeny -= (dataPtr - wStep + nChan)[1];
                        redy -= (dataPtr - wStep + nChan)[2];

                        bluey += (dataPtr + wStep - nChan)[0];
                        greeny += (dataPtr + wStep - nChan)[1];
                        redy += (dataPtr + wStep - nChan)[2];

                        bluey += (dataPtr + wStep + nChan)[0];
                        greeny += (dataPtr + wStep + nChan)[1];
                        redy += (dataPtr + wStep + nChan)[2];

                        blue = Math.Abs(bluex) + Math.Abs(bluey);
                        green = Math.Abs(greenx) + Math.Abs(greeny);
                        red = Math.Abs(redx) + Math.Abs(redy);

                        if (blue > 255)
                            blue = 255;
                        else if (blue < 0)
                            blue = 0;
                        if (green > 255)
                            green = 255;
                        else if (green < 0)
                            green = 0;
                        if (red > 255)
                            red = 255;
                        else if (red < 0)
                            red = 0;


                        dataPtrDes[0] = (byte)(blue);
                        dataPtrDes[1] = (byte)(green);
                        dataPtrDes[2] = (byte)(red);

                        // advance the pointer to the next pixel
                        dataPtr += nChan;
                        dataPtrDes += nChan;

                    }
                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += 2 * nChan;
                    dataPtr += padding;

                    dataPtrDes += 2 * nChan;
                    dataPtrDes += padding;
                }

                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += nChan;
                dataPtrDes += nChan;

                // primeira linha
                for (x = 1; x < width - 1; x++)
                {

                    //SX

                    bluex = ((dataPtr + nChan)[0]) * -3;
                    greenx = ((dataPtr + nChan)[1]) * -3;
                    redx = ((dataPtr + nChan)[2]) * -3;

                    bluex += ((dataPtr - nChan)[0]) * 3;
                    greenx += ((dataPtr - nChan)[1]) * 3;
                    redx += ((dataPtr - nChan)[2]) * 3;

                    bluex += (dataPtr + wStep - nChan)[0];
                    greenx += (dataPtr + wStep - nChan)[1];
                    redx += (dataPtr + wStep - nChan)[2];

                    bluex -= (dataPtr + wStep + nChan)[0];
                    greenx -= (dataPtr + wStep + nChan)[1];
                    redx -= (dataPtr + wStep + nChan)[2];

                    //SY

                    bluey = ((dataPtr + wStep)[0]) * 2;
                    greeny = ((dataPtr + wStep)[1]) * 2;
                    redy = ((dataPtr + wStep)[2]) * 2;

                    bluey -= ((dataPtr)[0]) * 2;
                    greeny -= ((dataPtr)[1]) * 2;
                    redy -= ((dataPtr)[2]) * 2;

                    bluey += (dataPtr + wStep - nChan)[0];
                    greeny += (dataPtr + wStep - nChan)[1];
                    redy += (dataPtr + wStep - nChan)[2];

                    bluey += (dataPtr + wStep + nChan)[0];
                    greeny += (dataPtr + wStep + nChan)[1];
                    redy += (dataPtr + wStep + nChan)[2];

                    bluey -= (dataPtr - nChan)[0];
                    greeny -= (dataPtr - nChan)[1];
                    redy -= (dataPtr - nChan)[2];

                    bluey -= (dataPtr + nChan)[0];
                    greeny -= (dataPtr + nChan)[1];
                    redy -= (dataPtr + nChan)[2];


                    blue = Math.Abs(bluex) + Math.Abs(bluey);
                    green = Math.Abs(greenx) + Math.Abs(greeny);
                    red = Math.Abs(redx) + Math.Abs(redy);

                    if (blue > 255)
                        blue = 255;
                    else if (blue < 0)
                        blue = 0;
                    if (green > 255)
                        green = 255;
                    else if (green < 0)
                        green = 0;
                    if (red > 255)
                        red = 255;
                    else if (red < 0)
                        red = 0;


                    dataPtrDes[0] = (byte)(blue);
                    dataPtrDes[1] = (byte)(green);
                    dataPtrDes[2] = (byte)(red);

                    // advance the pointer to the next pixel
                    dataPtr += nChan;
                    dataPtrDes += nChan;
                }

                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += nChan + ((height - 1) * n.widthStep);
                dataPtrDes += nChan + ((height - 1) * n.widthStep);

                //ultima linha
                for (x = 1; x < width - 1; x++)
                {
                    //SX

                    bluex = ((dataPtr + nChan)[0]) * -3;
                    greenx = ((dataPtr + nChan)[1]) * -3;
                    redx = ((dataPtr + nChan)[2]) * -3;

                    bluex += ((dataPtr - nChan)[0]) * 3;
                    greenx += ((dataPtr - nChan)[1]) * 3;
                    redx += ((dataPtr - nChan)[2]) * 3;

                    bluex += (dataPtr - wStep - nChan)[0];
                    greenx += (dataPtr - wStep - nChan)[1];
                    redx += (dataPtr - wStep - nChan)[2];

                    bluex -= (dataPtr - wStep + nChan)[0];
                    greenx -= (dataPtr - wStep + nChan)[1];
                    redx -= (dataPtr - wStep + nChan)[2];

                    //SY

                    bluey = ((dataPtr - wStep)[0]) * -2;
                    greeny = ((dataPtr - wStep)[1]) * -2;
                    redy = ((dataPtr - wStep)[2]) * -2;

                    bluey += ((dataPtr)[0]) * 2;
                    greeny += ((dataPtr)[1]) * 2;
                    redy += ((dataPtr)[2]) * 2;

                    bluey -= (dataPtr - wStep - nChan)[0];
                    greeny -= (dataPtr - wStep - nChan)[1];
                    redy -= (dataPtr - wStep - nChan)[2];

                    bluey -= (dataPtr - wStep + nChan)[0];
                    greeny -= (dataPtr - wStep + nChan)[1];
                    redy -= (dataPtr - wStep + nChan)[2];

                    bluey += (dataPtr - nChan)[0];
                    greeny += (dataPtr - nChan)[1];
                    redy += (dataPtr - nChan)[2];

                    bluey += (dataPtr + nChan)[0];
                    greeny += (dataPtr + nChan)[1];
                    redy += (dataPtr + nChan)[2];

                    blue = Math.Abs(bluex) + Math.Abs(bluey);
                    green = Math.Abs(greenx) + Math.Abs(greeny);
                    red = Math.Abs(redx) + Math.Abs(redy);

                    if (blue > 255)
                        blue = 255;
                    else if (blue < 0)
                        blue = 0;
                    if (green > 255)
                        green = 255;
                    else if (green < 0)
                        green = 0;
                    if (red > 255)
                        red = 255;
                    else if (red < 0)
                        red = 0;


                    dataPtrDes[0] = (byte)(blue);
                    dataPtrDes[1] = (byte)(green);
                    dataPtrDes[2] = (byte)(red);

                    // advance the pointer to the next pixel
                    dataPtr += nChan;
                    dataPtrDes += nChan;
                }

                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += n.widthStep;
                dataPtrDes += n.widthStep;

                //coluna esquerda
                for (y = 1; y < height - 1; y++)
                {
                    //SX

                    bluex = ((dataPtr + nChan)[0]) * -2;
                    greenx = ((dataPtr + nChan)[1]) * -2;
                    redx = ((dataPtr + nChan)[2]) * -2;

                    bluex += ((dataPtr)[0]) * 2;
                    greenx += ((dataPtr)[1]) * 2;
                    redx += ((dataPtr)[2]) * 2;

                    bluex += (dataPtr - wStep)[0];
                    greenx += (dataPtr - wStep)[1];
                    redx += (dataPtr - wStep)[2];

                    bluex -= (dataPtr - wStep + nChan)[0];
                    greenx -= (dataPtr - wStep + nChan)[1];
                    redx -= (dataPtr - wStep + nChan)[2];

                    bluex += (dataPtr + wStep)[0];
                    greenx += (dataPtr + wStep)[1];
                    redx += (dataPtr + wStep)[2];

                    bluex -= (dataPtr + wStep + nChan)[0];
                    greenx -= (dataPtr + wStep + nChan)[1];
                    redx -= (dataPtr + wStep + nChan)[2];

                    //SY

                    bluey = ((dataPtr - wStep)[0]) * -3;
                    greeny = ((dataPtr - wStep)[1]) * -3;
                    redy = ((dataPtr - wStep)[2]) * -3;

                    bluey -= (dataPtr - wStep + nChan)[0];
                    greeny -= (dataPtr - wStep + nChan)[1];
                    redy -= (dataPtr - wStep + nChan)[2];

                    bluey += (dataPtr + wStep + nChan)[0];
                    greeny += (dataPtr + wStep + nChan)[1];
                    redy += (dataPtr + wStep + nChan)[2];

                    bluey += (dataPtr + wStep)[0] * 3;
                    greeny += (dataPtr + wStep)[1] * 3;
                    redy += (dataPtr + wStep)[2] * 3;

                    blue = Math.Abs(bluex) + Math.Abs(bluey);
                    green = Math.Abs(greenx) + Math.Abs(greeny);
                    red = Math.Abs(redx) + Math.Abs(redy);

                    if (blue > 255)
                        blue = 255;
                    else if (blue < 0)
                        blue = 0;
                    if (green > 255)
                        green = 255;
                    else if (green < 0)
                        green = 0;
                    if (red > 255)
                        red = 255;
                    else if (red < 0)
                        red = 0;


                    dataPtrDes[0] = (byte)(blue);
                    dataPtrDes[1] = (byte)(green);
                    dataPtrDes[2] = (byte)(red);

                    // advance the pointer to the next pixel
                    dataPtr += m.widthStep;
                    dataPtrDes += m.widthStep;
                }

                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += n.widthStep + ((width - 1) * nChan);
                dataPtrDes += n.widthStep + ((width - 1) * nChan);

                //coluna direita
                for (y = 1; y < height - 1; y++)
                {
                    //SX

                    bluex = ((dataPtr - nChan)[0]) * 2;
                    greenx = ((dataPtr - nChan)[1]) * 2;
                    redx = ((dataPtr - nChan)[2]) * 2;

                    bluex += ((dataPtr)[0]) * -2;
                    greenx += ((dataPtr)[1]) * -2;
                    redx += ((dataPtr)[2]) * -2;

                    bluex -= (dataPtr - wStep)[0];
                    greenx -= (dataPtr - wStep)[1];
                    redx -= (dataPtr - wStep)[2];

                    bluex += (dataPtr - wStep - nChan)[0];
                    greenx += (dataPtr - wStep - nChan)[1];
                    redx += (dataPtr - wStep - nChan)[2];

                    bluex -= (dataPtr + wStep)[0];
                    greenx -= (dataPtr + wStep)[1];
                    redx -= (dataPtr + wStep)[2];

                    bluex += (dataPtr + wStep - nChan)[0];
                    greenx += (dataPtr + wStep - nChan)[1];
                    redx += (dataPtr + wStep - nChan)[2];

                    //SY

                    bluey = ((dataPtr - wStep)[0]) * -3;
                    greeny = ((dataPtr - wStep)[1]) * -3;
                    redy = ((dataPtr - wStep)[2]) * -3;

                    bluey -= (dataPtr - wStep - nChan)[0];
                    greeny -= (dataPtr - wStep - nChan)[1];
                    redy -= (dataPtr - wStep - nChan)[2];

                    bluey += (dataPtr + wStep - nChan)[0];
                    greeny += (dataPtr + wStep - nChan)[1];
                    redy += (dataPtr + wStep - nChan)[2];

                    bluey += (dataPtr + wStep)[0] * 3;
                    greeny += (dataPtr + wStep)[1] * 3;
                    redy += (dataPtr + wStep)[2] * 3;

                    blue = Math.Abs(bluex) + Math.Abs(bluey);
                    green = Math.Abs(greenx) + Math.Abs(greeny);
                    red = Math.Abs(redx) + Math.Abs(redy);

                    if (blue > 255)
                        blue = 255;
                    else if (blue < 0)
                        blue = 0;
                    if (green > 255)
                        green = 255;
                    else if (green < 0)
                        green = 0;
                    if (red > 255)
                        red = 255;
                    else if (red < 0)
                        red = 0;


                    dataPtrDes[0] = (byte)(blue);
                    dataPtrDes[1] = (byte)(green);
                    dataPtrDes[2] = (byte)(red);

                    // advance the pointer to the next pixel
                    dataPtr += m.widthStep;
                    dataPtrDes += m.widthStep;
                }

                //canto superior esquerdo
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                //SX

                bluex = (dataPtr[0]) * 3;
                greenx = (dataPtr[1]) * 3;
                redx = (dataPtr[2]) * 3;

                bluex -= (dataPtr + nChan)[0] * 3;
                greenx -= (dataPtr + nChan)[1] * 3;
                redx -= (dataPtr + nChan)[2] * 3;

                bluex += (dataPtr + wStep)[0];
                greenx += (dataPtr + wStep)[1];
                redx += (dataPtr + wStep)[2];

                bluex -= (dataPtr + nChan + wStep)[0];
                greenx -= (dataPtr + nChan + wStep)[1];
                redx -= (dataPtr + nChan + wStep)[2];

                //SY

                bluey = (dataPtr[0]) * -3;
                greeny = (dataPtr[1]) * -3;
                redy = (dataPtr[2]) * -3;

                bluey -= (dataPtr + nChan)[0];
                greeny -= (dataPtr + nChan)[1];
                redy -= (dataPtr + nChan)[2];

                bluey += (dataPtr + wStep)[0]  * 3;
                greeny += (dataPtr + wStep)[1] * 3;
                redy += (dataPtr + wStep)[2] * 3;

                bluey += (dataPtr + nChan + wStep)[0];
                greeny += (dataPtr + nChan + wStep)[1];
                redy += (dataPtr + nChan + wStep)[2];

                blue = Math.Abs(bluex) + Math.Abs(bluey);
                green = Math.Abs(greenx) + Math.Abs(greeny);
                red = Math.Abs(redx) + Math.Abs(redy);

                if (blue > 255)
                    blue = 255;
                else if (blue < 0)
                    blue = 0;
                if (green > 255)
                    green = 255;
                else if (green < 0)
                    green = 0;
                if (red > 255)
                    red = 255;
                else if (red < 0)
                    red = 0;


                dataPtrDes[0] = (byte)(blue);
                dataPtrDes[1] = (byte)(green);
                dataPtrDes[2] = (byte)(red);

                // advance the pointer to the next pixel
                dataPtr += m.widthStep;
                dataPtrDes += m.widthStep;


                //canto superior direito
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += (width - 1) * nChan;
                dataPtrDes += (width - 1) * nChan;

                //SX

                bluex = (dataPtr[0]) * -3;
                greenx = (dataPtr[1]) * -3;
                redx = (dataPtr[2]) * -3;

                bluex += (dataPtr - nChan)[0] * 3;
                greenx += (dataPtr - nChan)[1] * 3;
                redx += (dataPtr - nChan)[2] * 3;

                bluex -= (dataPtr + wStep)[0];
                greenx -= (dataPtr + wStep)[1];
                redx -= (dataPtr + wStep)[2];

                bluex += (dataPtr - nChan + wStep)[0];
                greenx += (dataPtr - nChan + wStep)[1];
                redx += (dataPtr - nChan + wStep)[2];

                //SY

                bluey = (dataPtr[0]) * -3;
                greeny = (dataPtr[1]) * -3;
                redy = (dataPtr[2]) * -3;

                bluey -= (dataPtr - nChan)[0];
                greeny -= (dataPtr - nChan)[1];
                redy -= (dataPtr - nChan)[2];

                bluey += (dataPtr + wStep)[0] * 3;
                greeny += (dataPtr + wStep)[1] * 3;
                redy += (dataPtr + wStep)[2] * 3;

                bluey += (dataPtr - nChan + wStep)[0];
                greeny += (dataPtr - nChan + wStep)[1];
                redy += (dataPtr - nChan + wStep)[2];

                blue = Math.Abs(bluex) + Math.Abs(bluey);
                green = Math.Abs(greenx) + Math.Abs(greeny);
                red = Math.Abs(redx) + Math.Abs(redy);

                if (blue > 255)
                    blue = 255;
                else if (blue < 0)
                    blue = 0;
                if (green > 255)
                    green = 255;
                else if (green < 0)
                    green = 0;
                if (red > 255)
                    red = 255;
                else if (red < 0)
                    red = 0;


                dataPtrDes[0] = (byte)(blue);
                dataPtrDes[1] = (byte)(green);
                dataPtrDes[2] = (byte)(red);

                // advance the pointer to the next pixel
                dataPtr += m.widthStep;
                dataPtrDes += m.widthStep;


                //canto inferior esquerdo
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += n.widthStep * (height - 1);
                dataPtrDes += n.widthStep * (height - 1);

                //SX

                bluex = (dataPtr[0]) * 3;
                greenx = (dataPtr[1]) * 3;
                redx = (dataPtr[2]) * 3;

                bluex -= (dataPtr + nChan)[0] * 3;
                greenx -= (dataPtr + nChan)[1] * 3;
                redx -= (dataPtr + nChan)[2] * 3;

                bluex += (dataPtr - wStep)[0];
                greenx += (dataPtr - wStep)[1];
                redx += (dataPtr - wStep)[2];

                bluex -= (dataPtr + nChan - wStep)[0];
                greenx -= (dataPtr + nChan - wStep)[1];
                redx -= (dataPtr + nChan - wStep)[2];

                //SY

                bluey = (dataPtr[0]) * 3;
                greeny = (dataPtr[1]) * 3;
                redy = (dataPtr[2]) * 3;

                bluey += (dataPtr + nChan)[0];
                greeny += (dataPtr + nChan)[1];
                redy += (dataPtr + nChan)[2];

                bluey -= (dataPtr - wStep)[0] * 3;
                greeny -= (dataPtr - wStep)[1] * 3;
                redy -= (dataPtr - wStep)[2] * 3;

                bluey -= (dataPtr + nChan - wStep)[0];
                greeny -= (dataPtr + nChan - wStep)[1];
                redy -= (dataPtr + nChan - wStep)[2];

                blue = Math.Abs(bluex) + Math.Abs(bluey);
                green = Math.Abs(greenx) + Math.Abs(greeny);
                red = Math.Abs(redx) + Math.Abs(redy);

                if (blue > 255)
                    blue = 255;
                else if (blue < 0)
                    blue = 0;
                if (green > 255)
                    green = 255;
                else if (green < 0)
                    green = 0;
                if (red > 255)
                    red = 255;
                else if (red < 0)
                    red = 0;


                dataPtrDes[0] = (byte)(blue);
                dataPtrDes[1] = (byte)(green);
                dataPtrDes[2] = (byte)(red);

                // advance the pointer to the next pixel
                dataPtr += m.widthStep;
                dataPtrDes += m.widthStep;


                //canto inferior direito
                dataPtr = (byte*)m.imageData.ToPointer();
                dataPtrDes = (byte*)n.imageData.ToPointer();

                dataPtr += n.widthStep * (height - 1) + nChan * (width - 1);
                dataPtrDes += n.widthStep * (height - 1) + nChan * (width - 1);

                //SX

                bluex = (dataPtr[0]) * -3;
                greenx = (dataPtr[1]) * -3;
                redx = (dataPtr[2]) * -3;

                bluex += (dataPtr - nChan)[0] * 3;
                greenx += (dataPtr - nChan)[1] * 3;
                redx += (dataPtr - nChan)[2] * 3;

                bluex -= (dataPtr - wStep)[0];
                greenx -= (dataPtr - wStep)[1];
                redx -= (dataPtr - wStep)[2];

                bluex += (dataPtr - nChan - wStep)[0];
                greenx += (dataPtr - nChan - wStep)[1];
                redx += (dataPtr - nChan - wStep)[2];

                //SY

                bluey = (dataPtr[0]) * 3;
                greeny = (dataPtr[1]) * 3;
                redy = (dataPtr[2]) * 3;

                bluey += (dataPtr - nChan)[0];
                greeny += (dataPtr - nChan)[1];
                redy += (dataPtr - nChan)[2];

                bluey -= (dataPtr - wStep)[0] * 3;
                greeny -= (dataPtr - wStep)[1] * 3;
                redy -= (dataPtr - wStep)[2] * 3;

                bluey -= (dataPtr - nChan - wStep)[0];
                greeny -= (dataPtr - nChan - wStep)[1];
                redy -= (dataPtr - nChan - wStep)[2];

                blue = Math.Abs(bluex) + Math.Abs(bluey);
                green = Math.Abs(greenx) + Math.Abs(greeny);
                red = Math.Abs(redx) + Math.Abs(redy);

                if (blue > 255)
                    blue = 255;
                else if (blue < 0)
                    blue = 0;
                if (green > 255)
                    green = 255;
                else if (green < 0)
                    green = 0;
                if (red > 255)
                    red = 255;
                else if (red < 0)
                    red = 0;


                dataPtrDes[0] = (byte)(blue);
                dataPtrDes[1] = (byte)(green);
                dataPtrDes[2] = (byte)(red);

                // advance the pointer to the next pixel
                dataPtr += m.widthStep;
                dataPtrDes += m.widthStep;
            }
        }

        public static void Median(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            { 
                imgCopy.SmoothMedian(3).CopyTo(img);
            }
        }

        public static void Median_7(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                imgCopy.SmoothMedian(7).CopyTo(img);
            }
        }

        public static int[] Histogram_Gray(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int blue, green, red, gray;
                int x, y;
                int[] vector = new int[256]; 

                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrieve 3 colour components
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // convert to gray
                         gray = (int)Math.Round((blue + green + red) / 3.0);
                         vector[gray]++;

                        dataPtr += nChan;
                            
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                return vector;
                }
        }

        public static int[,] Histogram_RGB(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int blue, green, red;
                int x, y;
                int[,] matrix = new int[3,256];

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        //retrieve 3 colour components
                        blue = dataPtr[0];
                        green = dataPtr[1];
                        red = dataPtr[2];

                        //add in matrix
                        matrix[0, blue]++;
                        matrix[1, green]++;
                        matrix[2, red]++;


                        dataPtr += nChan;

                    }

                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }
                return matrix;
            }
        }

        public static int[,] Histogram_All(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int blue, green, red, gray;
                int x, y;
                int[,] matrix_all = new int[4, 256];

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        //retrieve 3 colour components
                        blue = dataPtr[0];
                        green = dataPtr[1];
                        red = dataPtr[2];

                        //add in matrix
                        matrix_all[1, blue]++;
                        matrix_all[2, green]++;
                        matrix_all[3, red]++;

                        gray = (int)Math.Round((blue + green + red) / 3.0);
                        matrix_all[0, gray]++;


                        dataPtr += nChan;

                    }

                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }
                return matrix_all;
            }
        }

        public static void ConvertToBW(Emgu.CV.Image<Bgr, byte> img, int threshold)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int blue, green, red, gray;
                int x, y;

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        //retrieve 3 colour components
                        blue = dataPtr[0];
                        green = dataPtr[1];
                        red = dataPtr[2];

                        // convert to gray
                        gray = (int)Math.Round((blue + green + red) / 3.0);

                        if(gray <= threshold)
                        {
                            dataPtr[0]=0;
                            dataPtr[1]=0;
                            dataPtr[2]=0;
                        } else
                        {
                            dataPtr[0] = 255;
                            dataPtr[1] = 255;
                            dataPtr[2] = 255;
                        }

                        dataPtr += nChan;

                    }

                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }
            }
        }

        public static void ConvertToBW_Otsu(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int t_max=0;
                float q1=0, q2=0, u1=0, u2=0;
                int x, y, trh;
                float var_atual = 0, var_max = 0;
                int[] vector = new int[256];
                int[] var = new int[256];

                vector = Histogram_Gray(img);

                for(trh=0; trh <= 255; trh++)
                {
                    q1 = 0;
                    q2 = 0;
                    u1 = 0;
                    u2 = 0;

                    for(x=0; x <= trh; x++)
                    {
                        q1 += vector[x];
                        u1 += (x * vector[x]);
                    }

                    if (q1 > 0)
                    {

                        u1 = u1 / q1;

                        for (x = trh+1; x <= 255; x++)
                        {
                            q2 += vector[x];
                            u2 += (x * vector[x]);
                        }

                        if (q2 > 0)
                        {

                            u2 = u2 / q2;

                            var_atual = (float)(q1 * q2 * Math.Pow(u1 - u2 ,2));

                            if (var_atual > var_max)
                            {
                                var_max = var_atual;
                                t_max = trh;
                            }
                        }
                    }

                }
                ConvertToBW(img, t_max);
            }
        }

        public static void Diferentiation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {

            {
                unsafe
                {
                    MIplImage m = img.MIplImage;
                    MIplImage n = imgCopy.MIplImage;

                    byte* dataPtrD = (byte*)m.imageData.ToPointer(); // Pointer to the image
                    byte* dataPtrO = (byte*)n.imageData.ToPointer();

                    int width = img.Width;
                    int height = img.Height;
                    int nChan = m.nChannels; // number of channels - 3
                    int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)

                    int nChan2 = n.nChannels;
                    int widthstep = n.widthStep;

                    int y, x, i1, i2, i3;

                    if (nChan == 3)
                    {

                        for (y = 0; y < height; y++)
                        {
                            for (x = 0; x < width; x++)
                            {
                                if (x == width - 1 && y == height - 1)
                                {
                                    dataPtrD[0] = 0;
                                    dataPtrD[1] = 0;
                                    dataPtrD[2] = 0;
                                    return;
                                }
                                else if (x == width - 1)
                                {
                                    i1 = Math.Abs(dataPtrO[0] - (dataPtrO + widthstep)[0]);
                                    i2 = Math.Abs(dataPtrO[1] - (dataPtrO + widthstep)[1]);
                                    i3 = Math.Abs(dataPtrO[2] - (dataPtrO + widthstep)[2]);
                                }
                                else if (y == height - 1)
                                {
                                    i1 = Math.Abs(dataPtrO[0] - (dataPtrO + nChan)[0]);
                                    i2 = Math.Abs(dataPtrO[1] - (dataPtrO + nChan)[1]);
                                    i3 = Math.Abs(dataPtrO[2] - (dataPtrO + nChan)[2]);
                                }
                                else
                                {
                                    i1 = Math.Abs(dataPtrO[0] - (dataPtrO + nChan)[0]) + Math.Abs(dataPtrO[0] - (dataPtrO + widthstep)[0]);
                                    i2 = Math.Abs(dataPtrO[1] - (dataPtrO + nChan)[1]) + Math.Abs(dataPtrO[1] - (dataPtrO + widthstep)[1]);
                                    i3 = Math.Abs(dataPtrO[2] - (dataPtrO + nChan)[2]) + Math.Abs(dataPtrO[2] - (dataPtrO + widthstep)[2]);
                                }

                                if (i1 > 255)
                                    dataPtrD[0] = 255;
                                else if (i1 < 0)
                                    dataPtrD[0] = 0;
                                else
                                    dataPtrD[0] = (byte)i1;

                                if (i2 > 255)
                                    dataPtrD[1] = 255;
                                else if (i2 < 0)
                                    dataPtrD[1] = 0;
                                else
                                    dataPtrD[1] = (byte)i2;

                                if (i3 > 255)
                                    dataPtrD[2] = 255;
                                else if (i3 < 0)
                                    dataPtrD[2] = 0;
                                else
                                    dataPtrD[2] = (byte)i3;

                                // advance the pointer to the next pixel
                                dataPtrD += nChan;
                                dataPtrO += nChan;
                            }

                            //at the end of the line advance the pointer by the aligment bytes (padding)
                            dataPtrD += padding; //avançar colunas
                            dataPtrO += padding;

                        }

                    }

                }
            }
        }

        /************************************************* Trabalho final *****************************************************/

        public static void LP_Recognition(
            Image<Bgr, byte> img, // imagem a alterar
            Image<Bgr, byte> imgCopy, // cópia da imagem
            int difficultyLevel, //nível de dificuldade
            string LPType, //tipo de matricula (A ou B)
            out Rectangle LP_Location, // rectangulo(x,y,largura, altura) contendo a matricula
            out Rectangle LP_Chr1, // rectangulo contendo o primeiro carater
            out Rectangle LP_Chr2, // rectangulo contendo o segundo carater
            out Rectangle LP_Chr3, // rectangulo contendo o terceiro carater
            out Rectangle LP_Chr4, // rectangulo contendo o quarto carater
            out Rectangle LP_Chr5, // rectangulo contendo o quinto carater
            out Rectangle LP_Chr6, // rectangulo contendo o sexto carater
            out string LP_C1, // valor do primeiro carater
            out string LP_C2, // valor do segundo carater
            out string LP_C3, // valor do terceiro carater
            out string LP_C4, // valor do quarto carater
            out string LP_C5, // valor do quinto carater
            out string LP_C6 // valor do sexto carater
            )
        {
            unsafe
            {
                //CvInvoke.cvShowImage("Mostra loc Plate", img);
                MIplImage m = img.MIplImage;
                MIplImage n = imgCopy.MIplImage;
                Image<Bgr, byte> img2 = img.Clone();
                Image<Bgr, byte> img3 = img.Clone();
                MIplImage j = img2.MIplImage;
                MIplImage l = img3.MIplImage;
                int[] vector_return = new int[4];


                if (difficultyLevel > 1)
                {
                    vector_return = Loc_plate(img, img);
                    LP_Location = new Rectangle(vector_return[0], vector_return[1], vector_return[2], vector_return[3]);
                    imgCopy = imgCopy.Copy(LP_Location);
                    img3 = img3.Copy(LP_Location);
                }
                else
                    LP_Location = new Rectangle(0, 0, m.width, m.height);


                loc_char(imgCopy, img3, vector_return, difficultyLevel, out LP_Chr1, out LP_Chr2, out LP_Chr3, out LP_Chr4, out LP_Chr5, out LP_Chr6);

                int i = 0;

                string[] file_names = Directory.GetFiles("BD\\");
                Image<Bgr, byte>[] char_BD = new Image<Bgr, byte>[34];
                string[] char_names = new string[34];


                for (i = 0; i < 34; i++)
                {

                    char_names[i] = Path.GetFileNameWithoutExtension(file_names[i]);
                    char_BD[i] = new Image<Bgr, byte>(file_names[i]);
                    char_BD[i] = char_BD[i].Resize(10, 20, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
                    ConvertToBW_Otsu(char_BD[i]);
                }



                ConvertToBW(img2, 110);


                //CvInvoke.cvShowImage("Mostra loc Plate", imgCopy);

                //CvInvoke.cvShowImage("Mostra 1", img2.Copy(LP_Chr1));
                //CvInvoke.cvShowImage("Mostra 2", img2.Copy(LP_Chr2));
                //CvInvoke.cvShowImage("Mostra 3", img2.Copy(LP_Chr3));
                //CvInvoke.cvShowImage("Mostra 4", img2.Copy(LP_Chr4));
                //CvInvoke.cvShowImage("Mostra 5", img2.Copy(LP_Chr5));
                //CvInvoke.cvShowImage("Mostra 6", img2.Copy(LP_Chr6));

                LP_C1 = rec_char(img2.Copy(LP_Chr1).Resize(10, 20, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR), char_BD, char_names);
                LP_C2 = rec_char(img2.Copy(LP_Chr2).Resize(10, 20, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR), char_BD, char_names);
                LP_C3 = rec_char(img2.Copy(LP_Chr3).Resize(10, 20, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR), char_BD, char_names);
                LP_C4 = rec_char(img2.Copy(LP_Chr4).Resize(10, 20, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR), char_BD, char_names);
                LP_C5 = rec_char(img2.Copy(LP_Chr5).Resize(10, 20, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR), char_BD, char_names);
                LP_C6 = rec_char(img2.Copy(LP_Chr6).Resize(10, 20, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR), char_BD, char_names);


                Console.WriteLine("\n" + LP_C1 + "\n");
                Console.WriteLine("\n" + LP_C2 + "\n");
                Console.WriteLine("\n" + LP_C3 + "\n");
                Console.WriteLine("\n" + LP_C4 + "\n");
                Console.WriteLine("\n" + LP_C5 + "\n");
                Console.WriteLine("\n" + LP_C6 + "\n");

            }
        }


        public static string rec_char(Image<Bgr, byte> img, Image<Bgr, byte>[] img_BD, string[] char_name)  //Compara imagens dos caracteres cortados da matricula pixel a pixel, o que tiver mais matches será o escolhido
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                //ConvertToBW_Otsu(img);
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)

                int i, x, y, p, p_max = 0;
                string LP_Char = "";

                for (i = 0; i < 34; i++)
                {
                    MIplImage m_BD = img_BD[i].MIplImage;
                    byte* dataPtr_BD = (byte*)m_BD.imageData.ToPointer();
                    int nChan_BD = m_BD.nChannels; // number of channels - 3
                    int padding_BD = m_BD.widthStep - m_BD.nChannels * m_BD.width; // alinhament bytes (padding)

                    dataPtr = (byte*)m.imageData.ToPointer();
                    p = 0;

                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            if (dataPtr[0] == dataPtr_BD[0])   //ver porque o 0 blue, 1 red e 2 green
                                p++;

                            dataPtr += nChan;
                            dataPtr_BD += nChan_BD;
                        }
                        dataPtr += nChan;
                        dataPtr_BD += nChan_BD;
                    }
                    if (p > p_max)
                    {
                        p_max = p;
                        LP_Char = char_name[i];
                    }
                }

                return LP_Char;
            }
        }

        public static void loc_char(Image<Bgr, byte> img,     //recebe imagem da matricula e faz o corte dos caracteres
            Image<Bgr, byte> imgCopy,
            int[] vector_return,
            int difficultyLevel,
            out Rectangle LP_Chr1, // rectangulo contendo o primeiro carater
            out Rectangle LP_Chr2, // rectangulo contendo o segundo carater
            out Rectangle LP_Chr3, // rectangulo contendo o terceiro carater
            out Rectangle LP_Chr4, // rectangulo contendo o quarto carater
            out Rectangle LP_Chr5, // rectangulo contendo o quinto carater
            out Rectangle LP_Chr6 // rectangulo contendo o sexto carater
            )
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)


                MIplImage c = imgCopy.MIplImage;

                int x, y, x_indice_inicial = 0, x_indice_final = 0, x_max = 0, treshold = 5;
                int[] vector_vert = new int[width];
                int[] X_vert = new int[12];
                int[] X_vert_teste = new int[width];
                int x_finish = 0;
                int num_x = 0, num_y = 0;
                int val_max_string = 0;

                vector_vert = Histogram_Vert(img);

                int[] vector_hor = new int[height];
                int[] Y_hor = new int[2];
                int[] Y_hor_teste = new int[2];
                int y_finish = 0;

                if (difficultyLevel == 1)
                    treshold = 6;

                for (x = 0; x < width; x++)
                {
                    if (val_max_string < vector_vert[x])
                    {
                        val_max_string = vector_vert[x];
                    }
                }

                for (x = 0; x < width && num_x < 12; x++)
                {
                    if (vector_vert[x] > 6 && x_finish == 0) //vê quando comeca a haver pixeis pretos
                    {
                        x_indice_inicial = x;
                        x_finish = 1;
                    }
                    else if (vector_vert[x] < treshold && x_finish == 1 || x == width - 1)
                    {

                        x_indice_final = x;

                        if (x_max > (val_max_string / 2.2))
                        {
                            X_vert[num_x] = x_indice_inicial;
                            X_vert[num_x + 1] = x_indice_final;
                            num_x = num_x + 2;
                        }
                        x_finish = 0;
                        x_max = 0;
                    }

                    if (x_finish == 1)
                    {
                        if (x_max < vector_vert[x])
                        {
                            x_max = vector_vert[x];
                        }
                    }
                }



                vector_hor = Histogram_Hor(imgCopy);

                CvInvoke.cvShowImage("Mostra loc", imgCopy);

                if (difficultyLevel > 1)
                {
                    Y_hor[0] = 0;
                    Y_hor[1] = height;
                }

                for (y = 0; y < height; y++)
                {
                    if (vector_hor[y] > 10 && y_finish == 0) //vê quando comeca a haver pixeis pretos
                    {
                        Y_hor_teste[num_y] = y;
                        y_finish = 1;
                    }
                    else if (vector_hor[y] < 10 && y_finish == 1)
                    {
                        Y_hor_teste[num_y + 1] = y;
                        y_finish = 0;

                        if ((Y_hor_teste[num_y + 1] - Y_hor_teste[num_y]) > (Y_hor[num_y + 1] - Y_hor[num_y])) // filtrar ruido
                        {
                            Y_hor[num_y] = Y_hor_teste[num_y];
                            Y_hor[num_y + 1] = Y_hor_teste[num_y + 1];

                        }
                    }


                }

                if (difficultyLevel > 1)
                {
                    LP_Chr1 = new Rectangle(vector_return[0] + X_vert[0], vector_return[1] + Y_hor[0], X_vert[1] - X_vert[0], Y_hor[1] - Y_hor[0]);
                    LP_Chr2 = new Rectangle(vector_return[0] + X_vert[2], vector_return[1] + Y_hor[0], X_vert[3] - X_vert[2], Y_hor[1] - Y_hor[0]);
                    LP_Chr3 = new Rectangle(vector_return[0] + X_vert[4], vector_return[1] + Y_hor[0], X_vert[5] - X_vert[4], Y_hor[1] - Y_hor[0]);
                    LP_Chr4 = new Rectangle(vector_return[0] + X_vert[6], vector_return[1] + Y_hor[0], X_vert[7] - X_vert[6], Y_hor[1] - Y_hor[0]);
                    LP_Chr5 = new Rectangle(vector_return[0] + X_vert[8], vector_return[1] + Y_hor[0], X_vert[9] - X_vert[8], Y_hor[1] - Y_hor[0]);
                    LP_Chr6 = new Rectangle(vector_return[0] + X_vert[10], vector_return[1] + Y_hor[0], X_vert[11] - X_vert[10], Y_hor[1] - Y_hor[0]);
                }
                else
                {
                    LP_Chr1 = new Rectangle(X_vert[0], Y_hor[0], X_vert[1] - X_vert[0], Y_hor[1] - Y_hor[0]);
                    LP_Chr2 = new Rectangle(X_vert[2], Y_hor[0], X_vert[3] - X_vert[2], Y_hor[1] - Y_hor[0]);
                    LP_Chr3 = new Rectangle(X_vert[4], Y_hor[0], X_vert[5] - X_vert[4], Y_hor[1] - Y_hor[0]);
                    LP_Chr4 = new Rectangle(X_vert[6], Y_hor[0], X_vert[7] - X_vert[6], Y_hor[1] - Y_hor[0]);
                    LP_Chr5 = new Rectangle(X_vert[8], Y_hor[0], X_vert[9] - X_vert[8], Y_hor[1] - Y_hor[0]);
                    LP_Chr6 = new Rectangle(X_vert[10], Y_hor[0], X_vert[11] - X_vert[10], Y_hor[1] - Y_hor[0]);
                }
            }
        }

        public static int[] Loc_plate(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy) //out Rectangle LP_Location)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int y;

                int[] vector_teste = new int[width];
                int[] vector = new int[height];
                int[] vector_y = new int[2];
                int[] vector_y_test = new int[2];
                int contador = 0, contador_test = 0;
                int i = 0;
                Image<Bgr, byte> img1;
                Image<Bgr, byte> img1Copy;
                Image<Bgr, byte> img2;
                Image<Bgr, byte> img2Copy;
                img1 = img.Copy();
                img1Copy = img1.Copy();
                img2 = img.Copy();

                //Fazer diferenciação vertical e ve a zona em que ha mais quantidade de brancos de maneira a cortar a parte superior e inferior da matricula

                Median_7(img, imgCopy);
                Diferentiation_Vertical(img, imgCopy);
                ConvertToBW(img, 50);


                vector = Histogram_Hor_white(img);

                for (y = 0; y < height; y++)
                {
                    if (vector[y] > width * 0.008 && vector[y] < width * 0.07)
                    {
                        if (i == 0)
                        {
                            vector_y_test[0] = y;
                            i = 1;
                        }
                        contador_test++;
                    }
                    else if (i == 1)
                    {
                        vector_y_test[1] = y;
                        if (contador < contador_test)
                        {
                            contador = contador_test;
                            vector_y[0] = vector_y_test[0];
                            vector_y[1] = vector_y_test[1];
                        }
                        contador_test = 0;
                        i = 0;
                    }
                }
                int y_inicial, y_final;
                y_inicial = vector_y[0];
                y_final = vector_y[1];


                //Fazer projecao horizontal da faixa resultante, e cortar quando se encontra o primeiro branco prominente


                img1 = img1.Copy(new Rectangle(0, vector_y[0], width, (vector_y[1] - vector_y[0])));
                img2 = img2.Copy(new Rectangle(0, vector_y[0], width, (vector_y[1] - vector_y[0])));

                MIplImage j = img1.MIplImage;
                byte* dataPtr_img1 = (byte*)j.imageData.ToPointer();
                int width_img1 = img1.Width;
                int height_img1 = img1.Height;

                img1Copy = img1Copy.Copy(new Rectangle(0, vector_y[0], width, (vector_y[1] - vector_y[0])));
                Diferentiation_Vertical(img1, img1Copy);
                ConvertToBW(img1, 50);

                vector_teste = Histogram_Vert_white(img1);


                contador_test = 0;
                contador = 0;
                i = 0;
                int x_first = 0;

                for (int l = 0; l < width; l++)
                {
                    if (vector_teste[l] > height_img1 * 0.45)
                    {
                        vector_y[0] = l;
                        x_first = l;
                        break;
                    }
                }
                vector_y[1] = width_img1;

                //Agora a matricula ja está cortada no limite da esquerda, a partir daqui fazer um metodo black and white, com um treshold baixo de maneira a nao haver muito preto resultante de reflexos ou sombras
                //Sabemos que as proximas 6 regioesa de pretos prominentes serao caracteres, logo apos estes seis a matricula acaba

                img2 = img2.Copy(new Rectangle(vector_y[0], 0, (vector_y[1] - vector_y[0]), height_img1));
                img2Copy = img2.Copy();

                MIplImage h = img2.MIplImage;
                byte* dataPtr_img1_2 = (byte*)h.imageData.ToPointer();
                int width_img1_2 = img2.Width;
                int height_img1_2 = img2.Height;
                int[] vector_x_teste = new int[width_img1_2];

                Median_7(img2, img2Copy);
                ConvertToBW(img2, 80);

                vector_x_teste = Histogram_Vert(img2);
                int entry = 0, max_teste = 0, maximo = 0, treshold = 0, x_final = 0, x_inicial = 0;
                int cont = 0, x_inicial_teste = 0;
                contador = 0;

                treshold = (int)Math.Round(height_img1_2 * 0.3);

                for (int s = 2; s < width_img1_2; s++)
                {
                    if ((vector_x_teste[s] < (height_img1_2 * 0.1)) && entry == 0)
                    {
                        entry = 1;
                        maximo = 0;
                    }
                    if (entry == 1)
                    {
                        if (vector_x_teste[s] > height_img1_2 * 0.1)
                        {
                            if (cont == 0)
                            {
                                x_inicial_teste = s;
                                cont = 1;
                            }
                            max_teste = vector_x_teste[s];
                            if (maximo < max_teste)
                                maximo = max_teste;
                        }
                        else if (vector_x_teste[s] < height_img1_2 * 0.1)
                        {
                            if (treshold < maximo)
                            {
                                contador++;
                            }
                            else
                            {
                                cont = 0;
                            }
                            if (contador == 1)
                            {
                                x_inicial = x_inicial_teste - 4;
                            }
                            if (contador == 6)
                            {
                                x_final = s + 4;
                                break;
                            }
                            maximo = 0;
                        }
                    }
                }

                int[] vector_return = new int[4];
                vector_return[0] = x_first + x_inicial;
                vector_return[1] = y_inicial;
                vector_return[2] = x_final - x_inicial;
                vector_return[3] = y_final - y_inicial;

                return vector_return; // retorna as posicoes para fazer o retangulo resultante da matricula 
            }
        }

        public static int[] Histogram_Vert(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                ConvertToBW_Otsu(img);
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int blue, green, red, gray;
                int x, y;
                int[] vector = new int[width];

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        //retrieve 3 colour components
                        blue = dataPtr[0];
                        green = dataPtr[1];
                        red = dataPtr[2];

                        // convert to gray
                        gray = (int)Math.Round((blue + green + red) / 3.0);
                        if (gray == 0)
                        {
                            vector[x]++;
                        }

                        dataPtr += nChan;

                    }

                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }
                return vector;
            }
        }

        public static int[] Histogram_Hor(Image<Bgr, byte> img)
        {
            unsafe
            {
                ConvertToBW_Otsu(img);
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int blue, green, red, gray;
                int x, y;
                int[] vector = new int[height];

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        //retrieve 3 colour components
                        blue = dataPtr[0];
                        green = dataPtr[1];
                        red = dataPtr[2];

                        // convert to gray
                        gray = (int)Math.Round((blue + green + red) / 3.0);
                        if (gray == 0)
                        {
                            vector[y]++;
                        }

                        dataPtr += nChan;

                    }

                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }
                return vector;
            }
        }

        public static void Diferentiation_Vertical(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)  // esta comentado o completo, feito so para vertical neste momento
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage n = imgCopy.MIplImage;

                byte* dataPtrD = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrO = (byte*)n.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)

                int nChan2 = n.nChannels;
                int widthstep = n.widthStep;

                int y, x, i1, i2, i3;

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        i1 = Math.Abs(dataPtrO[0] - (dataPtrO + nChan)[0]);
                        i2 = Math.Abs(dataPtrO[1] - (dataPtrO + nChan)[1]);
                        i3 = Math.Abs(dataPtrO[2] - (dataPtrO + nChan)[2]);


                        if (i1 > 255)
                            dataPtrD[0] = 255;
                        else if (i1 < 0)
                            dataPtrD[0] = 0;
                        else
                            dataPtrD[0] = (byte)i1;

                        if (i2 > 255)
                            dataPtrD[1] = 255;
                        else if (i2 < 0)
                            dataPtrD[1] = 0;
                        else
                            dataPtrD[1] = (byte)i2;

                        if (i3 > 255)
                            dataPtrD[2] = 255;
                        else if (i3 < 0)
                            dataPtrD[2] = 0;
                        else
                            dataPtrD[2] = (byte)i3;

                        // advance the pointer to the next pixel
                        dataPtrD += nChan;
                        dataPtrO += nChan;
                    }

                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtrD += padding; //avançar colunas
                    dataPtrO += padding;
                }

            }

        }

        public static int[] Histogram_Vert_white(Image<Bgr, byte> img)
        {
            unsafe
            {
                //ConvertToBW_Otsu(img);
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int widthstep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int blue, green, red, gray;
                int x, y;
                int[] vector = new int[width];

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        //retrieve 3 colour components
                        blue = dataPtr[0];
                        green = dataPtr[1];
                        red = dataPtr[2];

                        // convert to gray
                        gray = (int)Math.Round((blue + green + red) / 3.0);

                        if (gray == 255)
                        {
                            //vector[y] = vector[y] + gray;
                            vector[x]++;
                        }


                        dataPtr += nChan;

                    }

                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }
                return vector;
            }
        }

        public static int[] Histogram_Hor_white(Image<Bgr, byte> img)
        {
            unsafe
            {
                //ConvertToBW_Otsu(img);
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int blue, green, red, gray;
                int x, y;
                int[] vector = new int[height];


                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        //retrieve 3 colour components
                        blue = dataPtr[0];
                        green = dataPtr[1];
                        red = dataPtr[2];

                        // convert to gray
                        gray = (int)Math.Round((blue + green + red) / 3.0);
                        if (gray == 255)
                        {
                            //vector[y] = vector[y] + gray;
                            vector[y]++;
                        }

                        dataPtr += nChan;

                    }

                    //at the end of the line advance the pointer by the aligment bytes (padding)
                    dataPtr += padding;
                }
                return vector;
            }
        }

    }
}

