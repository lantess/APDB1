using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APDB1
{
    [XmlRoot("student")]
    public class Student
    {
        public String fname,
            lname,
            birthdate,
            email,
            mothersName,
            fathersName;
        [XmlAttribute]
        public String indexNumber;
        //public List<Studies> studies = new List<Studies>();
        public Studies studies;

        public Student()
        {

        }
        public Student(String str)
        {
            String[] data = str.Split(",");
            fname = data[0];
            lname = data[1];
            //studies.Add(new Studies(data[2], data[3]));
            studies = new Studies(data[2], data[3]);
            indexNumber = "s"+data[4];
            birthdate = data[5];
            email = data[6];
            mothersName = data[7];
            fathersName = data[8];

        }

        /*public bool compareStudent(String str)
        {
            return str.Split(",")[4].Equals(index);
        }

        public void addStudies(String str)
        {
            String[] data = str.Split(",");
            studies.Add(new Studies(data[2], data[3]));
        }*/
    }
}
