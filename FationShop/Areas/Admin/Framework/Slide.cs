﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FationShop.Areas.Admin.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class Slide
    {
        public string ID { get; set; }
        [Display(Name = "Ảnh")]
        public string Image { get; set; }
        public HttpPostedFileBase ImageFile1 { get; set; }
        [Display(Name = "Thứ tự hiển thị")]
        public Nullable<int> DisplayOrder { get; set; }
        [Display(Name = "Trạng thái hiển thị")]
        public Nullable<bool> Status { get; set; }
    }
}