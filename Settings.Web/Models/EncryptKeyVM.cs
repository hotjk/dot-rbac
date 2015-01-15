using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Settings.Web.Models
{
    public class EncryptKeyVM
    {
        [Display(Name = "公钥")]
        public string PublicKey { get; set; }

        [Display(Name = "私钥")]
        public string PrivateKey { get; set; }
    }
}