using ERPProject.Model;
using System;
using System.Collections.Generic;
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
    }
}
