using System;
using System.Collections.Generic;
using System.Text;
using Auth.Service.Domain.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace Auth.Service.Persistence
{
    public static class DBContextFunction
    {

        public static IQueryable<T> If<T>(
           this IQueryable<T> source,
           bool condition,
           Func<IQueryable<T>, IQueryable<T>> transform
)
        {
            return condition ? transform(source) : source;
        }
        public static List<Variance> DetailedCompare<T>(this T val1, T val2)
        {
            List<Variance> variances = new List<Variance>();
            var fi = val1.GetType().GetProperties();

            foreach (var f in fi)
            {
                if (f.GetAccessors()[0].IsVirtual)
                    continue;
                
                Variance v = new Variance();
                v.Prop = f.Name;

                v.valA = f.GetValue(val1);
                v.valB = f.GetValue(val2);
                if (v.valA != null)
                {
                    if (!v.valA.Equals(v.valB))
                        variances.Add(v);
                }
                else if (v.valA != v.valB)
                    variances.Add(v);

            }
            return variances;
        }

        public static  List<T> ExcuteCreateRecords<T>(List<T> updateVal) where T : class
        {
            return ExcuteUpdateRecords<T>(updateVal, "", "create");
        }

        public static List<T> ExcuteDelRecords<T>(List<T> updateVal) where T : class
        {
            return ExcuteUpdateRecords<T>(updateVal, "", "delete");
        }
        public static List<T> ExcuteUpdateRecords<T>(List<T> updateVal, string primeKey) where T : class
        {
            return ExcuteUpdateRecords<T>(updateVal, primeKey, "update");
        }

        public static List<T> ExcuteUpdateRecords<T>(List<T> updateVal, string primeKey, string action) where T : class
        {
            using (var db = new DBContext())
            {

                
                var dbRd = ((DbSet<T>)typeof(DBContext).GetProperty(typeof(T).Name).GetValue(db));
                

                switch (action)
                {
                    case "create":


                        dbRd.AddRange(updateVal);



                        break;
                    case "update":
                        List<T> _list = dbRd.ToList();
                        if (_list.Count() == 0)
                        {
                            return null;
                        }
                        updateVal.ForEach(n =>
                        {
                            //var test =_list.Where(o =>
                            //{
                            //    Log.LogPersistence.WriteStateLog(typeof(T).GetProperty(primeKey).GetValue(o));
                            //    Log.LogPersistence.WriteStateLog(typeof(T).GetProperty(primeKey).GetValue(n));
                            //    if (typeof(T).GetProperty(primeKey).GetValue(o).Equals(typeof(T).GetProperty(primeKey).GetValue(n)))
                            //        return true;
                            //    else
                            //       return false;

                            //    }

                            //).ToList();
                            //Log.LogPersistence.WriteStateLog(test);
                            var B = typeof(T).GetProperty(primeKey).GetValue(n);
                            var A = _list.Where(o => typeof(T).GetProperty(primeKey).GetValue(o).Equals(
                                typeof(T).GetProperty(primeKey).GetValue(n))
                            );

                            var oldRd = _list.Where(o => typeof(T).GetProperty(primeKey).GetValue(o).Equals(typeof(T).GetProperty(primeKey).GetValue(n))).ToList()[0];
                            //var oldRd = _list.Where(o => o.Id == n.Id).ToList()[0];

                            var difference = DetailedCompare(oldRd, n);
                            foreach (var d in difference)
                                typeof(T).GetProperty(d.Prop).SetValue(oldRd, d.valB);


                        });
                        
                        break;
                    case "delete":
                        dbRd.RemoveRange(updateVal);
                        break;

                }

                db.SaveChanges();
            }
            return updateVal;
        }
    }
}
