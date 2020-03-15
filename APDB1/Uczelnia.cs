using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APDB1
{
    public class Uczelnia
    {
        [XmlAttribute]
        public String createdAt = "15.03.2020";
        [XmlAttribute]
        public String author = "Adam Żmuda 19215";
        [XmlArrayItem("student")]
        public List<Student> studenci;
        [XmlArrayItem("studies")]
        public List<StudiesData> activeStudies;

        public Uczelnia(List<Student> list)
        {
            studenci = list;
            activeStudies = new List<StudiesData>();
            foreach (Student s in list){
                bool exist = false;
                foreach(StudiesData d in activeStudies)
                {
                    if (d.name.Equals(s.studies.name))
                    {
                        d.add();
                        exist = true;
                    }
                }
                if (!exist)
                    activeStudies.Add(new StudiesData(s.studies.name));
            }
        }
        public Uczelnia()
        {
            studenci = new List<Student>();
        }
    }
}
