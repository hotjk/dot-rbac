using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grit.Utility.Web.Extensions;
using Settings.Model;
using AutoMapper;

namespace Settings.Web.Models
{
    public class ClientVM
    {
        public int ClientId { get; set; }

        [Display(Name = "客户名称")]
        [Required(ErrorMessage = "{0} 必须填写")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9_]{0,99}$", ErrorMessage = "{0} 必须为 100 位以内字母、数字或下划线组成，且必须以字母开始")]
        public string Name { get; set; }

        [Display(Name = "客户公钥")]
        [Required(ErrorMessage = "{0} 必须填写")]
        [StringLength(2000, ErrorMessage="{0} 长度不能超过 {1} 字符")]
        public string PublicKey {get;set;}

        public int Version { get; set; }

        static ClientVM()
        {
            Mapper.CreateMap<Settings.Model.Client, ClientVM>();
            Mapper.CreateMap<ClientVM, Settings.Model.Client>();
        }

        public static Settings.Model.Client ToModel(ClientVM vm)
        {
            return Mapper.Map<Settings.Model.Client>(vm);
        }

        public static ClientVM FromModel(Settings.Model.Client m = null)
        {
            ClientVM vm;
            if (m == null)
            {
                vm = new ClientVM();
            }
            else
            {
                vm = Mapper.Map<ClientVM>(m);
            }
            return vm;
        }
    }
}