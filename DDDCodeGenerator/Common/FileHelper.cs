using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ShineTechQD.DDDCodeGenerator.Common
{
    public class FileHelper
    {

        public static string ReadLine(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader m_streamReader = new StreamReader(fs, Encoding.UTF8);
            //使用StreamReader类来读取文件
            m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
            // 从数据流中读取每一行，直到文件的最后一行
            string strLine = m_streamReader.ReadLine();
            //关闭此StreamReader对象
            m_streamReader.Close();
            fs.Close();
            return strLine;
        } 

        public static string ReadAll(string path)
        {
            string StrContent = "";
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader m_streamReader = new StreamReader(fs, Encoding.UTF8);
            //使用StreamReader类来读取文件
            m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
            //读取全部文本
            StrContent = m_streamReader.ReadToEnd();
            //关闭此StreamReader对象
            m_streamReader.Close();
            fs.Close();
            return StrContent;

        }

        public static void CreatText(string TextPath)
        {
            if (!File.Exists(TextPath))
            {
                File.CreateText(TextPath);
            }
        }

        public static void WriteAll(string path, string ContentStr)
        {
            //创建一个文件流，用以写入或者创建一个StreamWriter
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter m_streamWriter = new StreamWriter(fs, Encoding.UTF8);
            m_streamWriter.Flush();
            // 使用StreamWriter来往文件中写入内容
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            // 把richTextBox1中的内容写入文件
            m_streamWriter.Write(ContentStr);
            //关闭此文件
            m_streamWriter.Flush();
            m_streamWriter.Close();
            fs.Close();
        }

        public static bool DeleteFolder(string dirRoot )
        {
            string message = "";
            return DeleteFolder(dirRoot, ref message);
        }

        public static bool DeleteFolder(string dirRoot, ref string message)
        {
            //string deleteFileName = "_desktop.ini";//要删除的文件名称
            try
            {
                string[] rootDirs = Directory.GetDirectories(dirRoot); //当前目录的子目录：
                string[] rootFiles = Directory.GetFiles(dirRoot);        //当前目录下的文件： 
                foreach (string s2 in rootFiles)
                {
                    System.IO.File.Delete(s2);                      //删除文件                    
                }
                foreach (string s1 in rootDirs)
                {
                    DeleteFolder(s1, ref message);
                }
                return true;
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            }
            return false;
        }

        public static bool DeleteFile(string file, ref string message)
        {
            try
            {
                System.IO.File.Delete(file);
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            }
            return false;
        }

        public static void CheckAndCreateFolder(string folder)
        {
            if(!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        public static void ReCreateFolder(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            else
            {
                DeleteFolder(folder);
                Directory.CreateDirectory(folder);
            }
        }

    }
}