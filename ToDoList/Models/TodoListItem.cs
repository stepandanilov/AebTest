﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class TodoListItem
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [UIHint("DescriptionDisplay")]
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Completed { get; set; }
    }
}
