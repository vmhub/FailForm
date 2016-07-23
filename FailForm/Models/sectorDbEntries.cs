using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
namespace FailForm.Models
{    /// <summary>
    /// Helper class for getting and creating Sectors list
    /// </summary>
    public static class sectorDbEntries
    {
        private static List<Sector> holder;
        const int root = 0;
        static sectorDbEntries()
        {
            holder = new List<Sector>();
            getListFromDB().formTree().formList(0);
        }
        /// <summary>
        /// Gets sectors list from database
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Sector> getListFromDB()
        {
            IEnumerable<Sector> l;
            using (MyContext cont = new MyContext())
            {
                l = cont.Sectors.ToList();
            }
            return l;
        }
        /// <summary>
        /// List getter
        /// </summary>
        /// <returns></returns>
        public static List<Sector> getList()
        {
            return holder;
        }
        /// <summary>
        /// Forms a linked list-like Sector parent-child hierarchy
        /// </summary>
        /// <param name="col"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        private static IEnumerable<SectorDBTree<Sector>> formTree(this IEnumerable<Sector> col, int root = 0)
        {
            foreach (var itemz in col.Where(c => c.parentId == root))
            {
                yield return new SectorDBTree<Sector>
                {
                    item = itemz,
                    childz = formTree(col, itemz.Id)
                };
            }
        }
        /// <summary>
        /// Forms final list of html-compatible Sector objects
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="o"></param>
        private static void formList(this IEnumerable<SectorDBTree<Sector>> tree, int o)
        {
            foreach (var item in tree)
            {
                item.item.htmlName = htmlPadder(o) + item.item.Name;
                holder.Add(item.item);
                formList(item.childz, o + 1);
            }
        }
        /// <summary>
        /// Adds necessary padding based on tree depth
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private static string htmlPadder(int o)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < o; i++)
                sb.Append("\u00a0\u00a0");
            return sb.ToString();
        }



    }
}