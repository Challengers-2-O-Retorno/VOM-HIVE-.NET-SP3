﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VOM_HIVE.API.DTO.Product
{
    public class ProductCreateDto
    {
        public string? nm_product { get; set; }

        public string? category_product { get; set; }
    }
}