﻿using Aviato.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace Aviato.ViewModels
{
    public class EditUsersViewModel
    {
        public EditUsersViewModel()
        {
            Mehanicar = new HashSet<Mehanicar>();
            Stjuard = new HashSet<Stjuard>();
        }
        public int Id { get; set; }
        [Column("ZaposleniObj")]
        public Zaposleni Zaposleni { get; set; }
        [Column("PilotObj")]
        public Pilot Pilot { get; set; }
        [Column("MehanicarObj")]
        public virtual ICollection<Mehanicar> Mehanicar { get; set; }
        //public int MehanicarId { get; set; }
        [Column("StjuardObj")]
        public virtual ICollection<Stjuard> Stjuard { get; set; }
        //public int StjuardId { get; set; }
    }
}