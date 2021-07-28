namespace WebApiProjectRECOVERY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClassesToUser")]
    public partial class ClassesToUser
    {
        public int id { get; set; }

        public int id_user { get; set; }

        public int id_class { get; set; }

    }

    public partial class ClassesToUserIn
    {
        public int id_user { get; set; }

        public int id_class { get; set; }
    }
}
