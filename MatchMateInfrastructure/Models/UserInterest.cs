﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace MatchMateInfrastructure.Models
{
    public class UserInterest
    {
        [Required]
        [Comment("Interest Id, part of the composite key")]
        public int InterestId { get; set; }

        [ForeignKey(nameof(InterestId))]
        public Interest Interest { get; set; } = null!;

        [Required]
        [Comment("User Id, part of the composite key")]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;
    }
}
