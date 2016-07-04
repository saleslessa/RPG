using DaemonCharacter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaemonCharacter.Controllers
{
    public class ItemAttributeController : Controller
    {
        private DaemonCharacterContext db = new DaemonCharacterContext();


        public ActionResult Create(int? id, int? idAttribute, int? value)
        {
            List<Models.Attributes> Attributes = db.Attributes.OrderBy(t => t.name).ToList();

            ViewBag.Attributes = new SelectList(Attributes
                , "id", "name"
                , idAttribute == null ? null : Attributes.Where(t => t.id == idAttribute).FirstOrDefault());

            ViewBag.id = id == null ? "0" : id.ToString();
            ViewBag.value = value == null ? "0" : value.ToString();

            return View();
        }

        public ActionResult ListItemAttribute(int idItem)
        {
            List<ItemAttribute> list = db.ItemAttributes.Where(t => t.item.id == idItem).ToList();

            return View(list);
        }



    }
}
