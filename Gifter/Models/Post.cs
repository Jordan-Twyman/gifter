using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Gifter.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string Caption { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public int UserProfileId { get; set; }

        public UserProfile UserProfile { get; set; }

        public List<Comment> Comments { get; set; }

    }
}