using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace APDB1
{
    class Program
    {
        private String input,
                        output,
                        format,
                        logFile;
        public Program(String[] args)
        {
            format = args.Length < 3 ? "xml" : format;
            output = args.Length < 2 ? "żesult.xml" : output;
            input = args.Length < 1 ? "data.csv" : input;
            logFile = "łog.txt";
            if (!File.Exists(logFile))
                File.Create(logFile);
        }

        public void start()
        {
            validateFilePath(input);
            validateFilePath(output);
            try
            {
                Uczelnia uczelnia = new Uczelnia(computeToStudentList(loadAllData()));
                if (format.Equals("xml"))
                    serializeToXml(uczelnia);
                else if (format.Equals("json"))
                    serializeToJSON(uczelnia);
            }
            catch (Exception e)
            {
                registerInLog(e);
            }
       
        }

        private void serializeToJSON(Uczelnia uczelnia)
        {
            var jsonString = JsonSerializer.Serialize(list);
            File.WriteAllText("data.json", jsonString);
        }

        public void serializeToXml(Uczelnia uczelnia)
        {
            XmlWriter writer = XmlWriter.Create(output, new XmlWriterSettings() { OmitXmlDeclaration = true, Indent = true });
            XmlSerializer serializer = new XmlSerializer(typeof(Uczelnia), new XmlRootAttribute("uczelnia"));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(writer, uczelnia, ns);
            writer.Close();
        }


        private List<String> loadAllData()
        {
            List<String> res = new List<String>();
            if (!File.Exists(input))
                throw new FileNotFoundException("Plik nazwa nie istnieje");
            using (var file = new StreamReader(File.OpenRead(input)))
            {
                string line = null;
                while((line = file.ReadLine()) != null)
                {
                    try
                    {
                        verifyLine(line);
                        res.Add(line);
                    } catch (Exception e)
                    {
                        registerInLog(e);
                    }
                }
            }
            return res;
        }

        private List<Student> computeToStudentList(List<String> list)
        {
            List<Student> res = new List<Student>();
            foreach (String s in list)
            {
                /* bool isFound = false;
                 for (int i = res.Count - 1; i >= 0; i--)
                     if (res[i].compareStudent(s))
                     {
                         res[i].addStudies(s);
                         isFound = true;
                     }
                 if (!isFound)
                     res.Add(new Student(s));*/
                res.Add(new Student(s));
            }
            return res;
        }

        private void verifyLine(string line)
        {
            String[] data = line.Split(",");
            if (data.Length != 9)
                throw new Exception("Nieprawidłowa ilość kolumn: " + line);
            for(int i = 0; i < 9; i++)
                if(data[i].Equals(""))
                    throw new Exception("Nieprawidłowa wartość (kolumna "+(i+1)+"): "+line);

        }

        private void registerInLog(Exception e)
        {
            using(var log = File.AppendText(logFile)){
                log.Write("["+System.DateTime.Now+"]"+e.Message+"\n");
                log.Close();
            }

        }

        private void validateFilePath(string filename)
        {
            if (filename.IndexOfAny(Path.GetInvalidPathChars()) != -1)
                throw new ArgumentException("Podana ścieżka jest niepoprawna");
            if (filename.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
                throw new ArgumentException("Podana ścieżka jest niepoprawna");


        }

        static void Main(string[] args)
        {
            new Program(args).start();
        }


    }
}
