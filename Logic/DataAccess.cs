using ERPProject.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPProject.Logic
{
    public class DataAccess
    {
        public static List<User> Getusers()//Model(DB안의 USER이라는 테이블)
        {
            List<User> users;

            using (var ctx = new ERPEntities()) //ERPModel.Context.cs와 연결이 됨 ctx는 context의 약자
            {
                users = ctx.User.ToList(); //ctx는 ERPEnitities의 내용을 가지고 있기 때문에 안에 user가 들어있다.
                //위의 문장은 DB의 명령어 SELECT * FROM USER와 같다.
            }
            return users;
        }

        public static List<Stock> Getstock()
        {
            List<Stock> stocks;

            using (var ctx = new ERPEntities())
            {
                stocks = ctx.Stock.ToList();
            }
            return stocks;
        }

        public static List<BookOutItem> GetbookOutItems()
        {
            List<BookOutItem> bookOutItems;

            using (var ctx = new ERPEntities())
            {
                bookOutItems = ctx.BookOutItem.ToList();
            }
            return bookOutItems;
        }


        /// <summary>
        /// 입력, 수정 동시에!
        /// </summary>
        /// <param name="user"></param>
        /// <returns>0 또는 1이상</returns>
        public static int SetUser(User user) //data를 수정할 때 
        {
            using (var ctx = new ERPEntities())
            {
                ctx.User.AddOrUpdate(user);
                return ctx.SaveChanges(); //commit
            }
        }

        public static List<Item> Getitems()
        {
            List<Item> items;

            using (var ctx = new ERPEntities())
            {
                items = ctx.Item.ToList();
            }
            return items;
        }

        public static List<Store> GetStores()
        {
            List<Store> stores;

            using (var ctx = new ERPEntities())
            {
                stores = ctx.Store.ToList();
            }
            return stores;
        }
        public static List<Stock> GetStocks()
        {
            List<Stock> stocks;

            using (var ctx = new ERPEntities())
            {
                stocks = ctx.Stock.ToList();
            }
            return stocks;
        }
        public static int SetStores(Store store)
        {
            using (var ctx = new ERPEntities())
            {
                ctx.Store.AddOrUpdate(store);
                return ctx.SaveChanges(); //
            }
        }
        public static int SetItems(Item item)
        {
            using (var ctx = new ERPEntities())
            {
                ctx.Item.AddOrUpdate(item);
                return ctx.SaveChanges(); //
            }
        }
    }
}
