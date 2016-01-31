using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PBL_HR {
    class MembersDB : DataContext {

        public Table<Members> Members;
        public MembersDB (string connection) : base(connection) { }

    }

    public class Members {
        
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [Indexed, MaxLength(25)]
        public string FirstName { get; set; }
        [Indexed, MaxLength(25)]
        public string LastName { get; set; }
        [Indexed, MaxLength(140)]
        public string School { get; set; }
        [Indexed, MaxLength(15)]
        public string State { get; set; }
        [Indexed, MaxLength(90)]
        public string Email { get; set; }
        [Indexed]
        public byte Grade { get; set; }
        [Indexed]
        public float AmountOwed { get; set; }
        [Indexed]
        public bool Active { get; set; }
    }
}
