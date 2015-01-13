using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Settings.Web.Models
{
    public class EntryVM
    {
        [Display(Name = "配置 Key")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9_]{0,99}$", ErrorMessage = "{0} 必须为 100 位以内字母、数字或下划线组成，且必须以字母开始")]
        public string Key { get; set; }

        [Display(Name = "配置 Value")]
        [StringLength(1000, ErrorMessage="{0} 长度不能超过 {1} 字符")]
        public string Value { get; set; }
    }
}