namespace WebApiProjectRECOVERY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Class
    {
        public int ID { get; set; }

        public string Homeroom { get; set; }

        public int GradeLevel { get; set; }

        public string GradeLetter { get; set; }

    }

    public partial class ClassIn
    {
        public string Homeroom { get; set; }

        public int GradeLevel { get; set; }

        public string GradeLetter { get; set; }
    }
}
