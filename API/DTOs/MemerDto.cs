using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using API.Entities;
using API.Extensions;

namespace API.Entities

{
    public class MemberDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string PhotoUrl { get; set; }

        public byte[] PasswordHash { get; set; }    

        public byte[] PasswordSalt { get; set; }

        public DateTime Age { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime LastActive { get; set; } =DateTime.Now;

        public string Gender { get; set; }

        public string Introduction { get; set; }

        public string LookingFor { get; set; }

        public string Interests { get; set; }

        public string City  { get; set; }

        public string Country { get; set; }

        public ICollection<PhotoDto> Photos { get; set; }
    }
}