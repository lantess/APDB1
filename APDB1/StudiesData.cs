using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APDB1
{
    public class StudiesData
    {
        [XmlAttribute]
        public String name;
        [XmlAttribute]
        public int numberOfStudents;

        public StudiesData()
        {
            name = "";
            numberOfStudents = 0;
        }

        public StudiesData(String name)
        {
            this.name = name;
            numberOfStudents = 1;
        }

        public void add()
        {
            numberOfStudents++;
        }

        public bool compare(String name)
        {
            return this.name.Equals(name);
        }
    }
}
