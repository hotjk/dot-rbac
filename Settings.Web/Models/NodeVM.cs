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
    public class NodeVM
    {
        public int NodeId { get; set; }

        [Display(Name = "节点名称")]
        [Required(ErrorMessage = "{0} 必须填写")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9_]{0,99}$", ErrorMessage = "{0} 必须为 100 位以内字母、数字或下划线组成，且必须以字母开始")]
        public string Name { get; set; }
        
        [Display(Name = "节点配置")]
        public List<EntryVM> Entries { get; set; }

        public int Version { get; set; }

        private const int ENTRY_STEP = 4;

        static NodeVM()
        {
            Mapper.CreateMap<Node, NodeVM>();
            Mapper.CreateMap<NodeVM, Node>();
            Mapper.CreateMap<Entry, EntryVM>();
            Mapper.CreateMap<EntryVM, Entry>();
        }

        public bool IsValid(ModelStateDictionary ModelState)
        {
            bool isValid = true;
            for (int i = 0; i < this.Entries.Count; i++)
            {
                EntryVM entry = this.Entries[i];
                if (!string.IsNullOrWhiteSpace(entry.Value) && string.IsNullOrWhiteSpace(entry.Key))
                {
                    ModelState.AddModelError(this.GetExpressionText(x => this.Entries[i].Key),
                        string.Format(@"{0} 必须填写", this.GetDisplayName(x => this.Entries[i].Key)));
                    isValid = false;
                }
            }
            return isValid;
        }

        public static Node ToModel(NodeVM vm)
        {
            var node = Mapper.Map<Node>(vm);
            node.Entries = node.Entries.Where(n => !string.IsNullOrWhiteSpace(n.Key)).OrderBy(n => n.Key).ToList();
            return node;
        }

        public static NodeVM FromModel(Node node = null)
        {
            NodeVM vm;
            if (node == null)
            {
                vm = new NodeVM();
            }
            else
            {
                vm = Mapper.Map<NodeVM>(node);
            }

            if (vm.Entries == null)
            {
                vm.Entries = new List<EntryVM>(ENTRY_STEP);
            }
            for (int i = 0; i < ENTRY_STEP; i++)
            {
                vm.Entries.Add(new EntryVM());
            }
            return vm;
        }
    }
}