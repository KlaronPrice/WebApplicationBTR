using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationBTR.Models
{
    public class ShopInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ShopContext>
    {
        protected override void Seed(ShopContext context)
        {
            base.Seed(context);
        }
    }
}