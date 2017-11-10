﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace szakdoga.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreationDate { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)] -sajnos nem működik
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //így insertnél létrejön a kezdőérték, update-el frissítem
        public DateTime ModifyDate { get; set; }

        [Required]
        public string GUID { get; set; }
        public bool Deleted { get; set; }
    }
}