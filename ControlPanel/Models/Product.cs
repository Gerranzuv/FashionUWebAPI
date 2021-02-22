﻿using ControlPanel.Extras;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControlPanel.Models
{
    public class Product : BasicModel
    {
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }


        [Display(Name = "Designer Name")]
        public string DesignerName { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Number of Items")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int NumberOfItems
        {
            get
            {
                if (this.Attachments != null && this.Attachments.Count > 0)
                    return this.Attachments.Sum(a => a.NumOfItems);
                else
                    return 0;
            }
            set { }
        }

        [Display(Name = "Available Colors")]
        public string AvailableColors { get; set; }

        [Display(Name = "Available Sizes")]
        public string AvailableSizes { get; set; }

        [Display(Name = "Length")]
        public int Length { get; set; }

        [Display(Name = "Waist Size")]
        public int WaistSize { get; set; }

        [Display(Name = "Sleeve Length")]
        public int SleeveLength { get; set; }

        [Display(Name = "Bust")]
        public int Bust { get; set; }

        [Display(Name = "Price")]
        public double Price { get; set; }

        [Display(Name = "Currency")]
        public String Currency { get; set; }

        [Display(Name = "Available")]
        public Boolean Available { get; set; }

        public List<Attachment> Attachments { get; set; }


        public List<Comment> Comments { get; set; }

        public string FabricType { get; set; }

        public double torsoLength { get; set; }

        public double legLength { get; set; }
    }
}