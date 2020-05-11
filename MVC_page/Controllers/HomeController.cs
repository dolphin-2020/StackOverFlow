using MVC_page.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MVC_page.Controllers
{
    public class HomeController : Controller
    {
        private DolphinQueryEntities db = new DolphinQueryEntities();
        public ActionResult Index(int id=2,int pageSize=10, int pageIndex=1,int sort=1)
        {
            //if (id == null)
            //{
            //ViewBag.userId=id;
            //ViewBag.user = db.Users.Find(2);//anonymous
            //ViewBag.page = GetPage(pageSize, pageIndex);
            //return View();
            //}
            //else
            //{
            var x = db.Users.Select(u => u);
            //ViewBag.userName = new SelectList(db.Users, "Id", "Name");
            ViewBag.userId = id;
            var user = db.Users.Find(id);
            ViewBag.count = db.Asks.Count().ToString();
            ViewBag.user = user;
            if (sort ==1)
            {
                ViewBag.page = GetPage(pageSize, pageIndex);
                return View();
            }
            else
            {
                ViewBag.page = GetPage1(pageSize, pageIndex);
                return View();
            }
           
            //}
        }
        /* [HttpPost]
         public ActionResult Index(int id , int pageSize = 10, int pageIndex = 1)
         {
             var x = db.Users.Select(u => u);
             ViewBag.userName = new SelectList(db.Users, "Id", "Name");

             ViewBag.userId = id;
             var user = db.Users.Find(id);
             ViewBag.count = db.Asks.Count().ToString();
             ViewBag.user = user;
             ViewBag.page = GetPage(pageSize, pageIndex);
             return View();
         }*/

        public Page<AskInfo> GetPage1(int pageSize, int pageIndex)
        {
            int count = GetCount();
            var list = GetList1(pageSize, pageIndex);
            Page<AskInfo> page = new Page<AskInfo>(pageIndex, pageSize, count, list);
            return page;
        }

        public Page<AskInfo> GetPage(int pageSize, int pageIndex)
        {
            int count = GetCount();
            var list = GetList(pageSize, pageIndex);
            Page<AskInfo> page = new Page<AskInfo>(pageIndex, pageSize, count, list);
            return page;
        }

        public int GetCount()
        {
            string sql = @"SELECT COUNT(*) FROM Ask";
            int r = (int)DBHelper.ExecuteScalar(sql);
            return r;
        }

        public List<AskInfo> GetList1(int pageSize, int pageIndex)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            string sql = @"
                        select * from
                        (select * from(
                        select ROW_NUMBER()over(order by t3.numbers desc) as FakeId, * from 
                        (select * from
                        (select 0 as numbers, * from ask where id in
                        (select id from ask
                        except
                        (select a.id from ask a  join Answer An on a.Id=An.askId
                        group by a.id)
                        ))t1
                        UNION ALL SELECT * FROM
                        (select s.numbers ,ask.id,ask.title,ask.content,ask.userId,ask.createTime,ask.picturePath,ask.questiontypeAskId from ask join 
                        (select count(1)as numbers,a.id from ask a  join Answer An on a.Id=An.askId
                        group by a.id) s on ask.Id=s.Id)t2)t3
                        )newTable
                        where FakeId between @start and @end)temp 
                        join [User] on [User].id=temp.UserId ";
            SqlParameter[] p =
            {
                new SqlParameter("@start",start),
                new SqlParameter("end",end),
            };
            List<AskInfo> list = new List<AskInfo>();

            var x = DBHelper.GetDataTable(sql, p);
            for (int i = 0; i < x.Rows.Count; i++)
            {
                string t = x.Rows[i]["title"].ToString();
                string c = x.Rows[i]["content"].ToString();
                string Tcontent = c.Length > 230 ? c.Substring(0, 230) + "..." : c;
                string Ttitle = t.Length > 50 ? t.Substring(0, 50) + "..." : t;
                AskInfo temp = new AskInfo();
                temp.name = x.Rows[i]["name"].ToString();
                temp.title = Ttitle;
                temp.email = x.Rows[i]["email"].ToString();
                temp.picturepath = x.Rows[i]["picturepath"].ToString();
                temp.headImgpath = x.Rows[i]["headImgpath"].ToString();
                temp.content = Tcontent;
                temp.createtime = (DateTime)x.Rows[i]["createtime"];
                temp.Id = (int)x.Rows[i]["id"];

                list.Add(temp);
            }

            return list;
        }
        public List<AskInfo>GetList(int pageSize,int pageIndex)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            string sql = @"
                        select * from
                        (select * from(
                        select ROW_NUMBER()over(order by id desc) as FakeId, * from Ask 
                        )newTable
                        where FakeId between @start and @end)temp 
                        join [User] on [User].id=temp.UserId ";
            SqlParameter[] p = 
            { 
                new SqlParameter("@start",start),
                new SqlParameter("end",end),  
            };
            List<AskInfo> list=new List<AskInfo>();

            var x = DBHelper.GetDataTable(sql,p);
            for(int i = 0; i < x.Rows.Count; i++)
            {
                string t = x.Rows[i]["title"].ToString();
                string c = x.Rows[i]["content"].ToString();
                string Tcontent=c.Length>230?c.Substring(0,230)+"...":c;
                string Ttitle = t.Length>50? t.Substring(0, 50) + "...": t;
                AskInfo temp = new AskInfo();
                temp.name = x.Rows[i]["name"].ToString();
                temp.title = Ttitle;
                temp.email = x.Rows[i]["email"].ToString();
                temp.picturepath = x.Rows[i]["picturepath"].ToString();
                temp.headImgpath = x.Rows[i]["headImgpath"].ToString();
                temp.content = Tcontent;
                temp.createtime =(DateTime)x.Rows[i]["createtime"];
                temp.Id =(int)x.Rows[i]["id"];

                list.Add(temp);
            }
          
            return list;
        }

        public JsonResult GetUsers(string key)
        {
            JsonResult jr = new JsonResult();
            List<MainPage> list = GetuSerName(key);
            if (list.Count > 0)
            {
                jr.Data = list;
            }
            else
            {
                jr.Data = "0";
            }
            return jr;
        }
        private List<MainPage> GetuSerName(string key)
        {
            List<MainPage> list = new List<MainPage>();
            string sql = @"select [ID], [title] from [Ask]
                           where title like @key";
            SqlParameter[] p =
            {
                new SqlParameter ("@key",$"{key}%"),
            };

            SqlConnection conn = new SqlConnection(
                @"server=LAPTOP-UIJTGT3K\;database=DolphinQuery;integrated security=SSPI");
            conn.Open();
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddRange(p);
            SqlDataReader r = command.ExecuteReader(CommandBehavior.CloseConnection);
            
            while (r.Read())
            {
                MainPage u = new MainPage   ();
                u.UserID = (int)r["ID"];
                u.userName = r["title"].ToString();
                list.Add(u);
            }
            return list;
        }

        public ActionResult DisplayQuestion(int id,int userId)
        {
            ViewBag.askId = db.Asks.Find(id).Id;//askId
            ViewBag.userId = userId;
           
            var user = db.Users.Find(userId);
            ViewBag.count = db.Asks.Count().ToString();
            ViewBag.user = user;
            var x = db.Asks.Find(id);
            var AskAnswers =db.Users.Find( x.User.Id).Answers;

           int Upvote = 0;
           int Downvote = 0;
            
           foreach (var item in AskAnswers)
            {
              Upvote+= item.Votes.ToList().Count;
            }
            foreach (var item in x.Answers)
            {
                Downvote += item.MinusVotes.ToList().Count;
            }
            int total = Upvote - Downvote;
            ViewBag.TotalVote = total;

            //Bonus badges
            string badges = "";
           
            if (total / 100 > 0)
            {
                badges = "Gold Badge";
                
            }else if (total / 75 > 0)
            {
                badges = "Silver";
            }else if (total / 50 > 0)
            {
                badges = "Bronze";
            }else if (total / 25 > 0)
            {
                badges = "Wood";
            }else if (total >= 0&&total<25)
            {
                badges = "Clay";
            }
            else if(total<0)
            {
                badges = "Rubbish";
            }
            ViewBag.badges = badges;
            return View(x);
        }

        public string GetUserId(int id)
        {
            return id.ToString();
        }

        public ActionResult AddAnswer(int id)
        {
            if (id == 2)
            {
                return Redirect("/Home/longin");
            }
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddAnswer(int id, string title, string content)//ask
        {
            ViewBag.id = id;
            if (id == 2)//anonymous
            {
                return Redirect("/Home/longin");
            }
            else
            {
                var user = db.Users.Find(id);
                if (user == null) return HttpNotFound();
                var a = new Ask();
                a.UserId = id;
                a.title = title;
                a.content = content;
                a.createTime = DateTime.Now;
                db.Asks.Add(a);
                db.SaveChanges();
                return RedirectToAction("Index",new { id = id });
            }
        } 
        public ActionResult AddcurrentAnswer(int userId, int askId)//answer
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddcurrentAnswer(int userId, int askId, string content)
        {
            if (userId == 2)
            {
                return Redirect("/Home/longin");
            }
            else
            {
                var newAnswer = new Answer();
                newAnswer.UserId = userId;
                newAnswer.askId = askId;
                newAnswer.createTime = DateTime.Now;
                newAnswer.content = content;

                db.Answers.Add(newAnswer);
                db.SaveChanges();

                return RedirectToAction("DisplayQuestion", new { id = askId, userId = userId });
            }
            
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string Name,string Email,string Password,string AboutMe="")
        {
            int userId = 2;
            string sql = @"insert into [user] (Name,Email,password,headImgPath,backImgPath,AboutMe)values(@name, @email, @password, @headImgPath, @backImgPath, @aboutMe)";
            
                string[] headPath = { "~/Content/HeadImg/head01.jpg", "~/Content/HeadImg/head02.jpg", "~/Content/HeadImg/head03.jpg", "~/Content/HeadImg/head04.jpg", "~/Content/HeadImg/head05.jpg", "~/Content/HeadImg/head06.jpg", "~/Content/HeadImg/head07.jpg", "~/Content/HeadImg/head08.jpg", "~/Content/HeadImg/head09.jpg", "~/Content/HeadImg/head10.jpg", "~/Content/HeadImg/head11.jpg" };
            string[] backPath = { "~/Content/BackImg/head01.jpg", "~/Content/BackImg/head02.jpg", "~/Content/BackImg/head03.jpg", "~/Content/BackImg/head04.jpg", "~/Content/BackImg/head05.jpg", "~/Content/BackImg/head06.jpg", "~/Content/BackImg/head07.jpg", "~/Content/BackImg/head08.jpg", "~/Content/BackImg/head09.jpg", "~/Content/BackImg/head11.jpg" };
            Random r = new Random();
            r.Next(0, 10);
            var u = new User();
            u.Name = Name;
            u.Email = Email;
            u.password = Password;
            u.AccountCreatTime = DateTime.Now;
            u.headImgPath = headPath[r.Next(0, 11)];
            u.backImgPath = backPath[r.Next(0, 10)];
            if (AboutMe == "")
            {
                u.AboutMe = "This guy is laze and didn't give any intriduce about she or he";
            }
            else
            {
                u.AboutMe = AboutMe;
            }
            SqlParameter[] p =
            {
                new SqlParameter ("@name",u.Name),
                new SqlParameter ("@email",u.Email),
                new SqlParameter ("@password",u.password),
                new SqlParameter ("@headImgPath",u.headImgPath),
                new SqlParameter ("@backImgPath",u.backImgPath),
                new SqlParameter ("@aboutMe",u.AboutMe),
            };
            int result= DBHelper.ExecuteNonQuery(sql,p);
            if (result > 0)
            {
                string sql2 = @"select id from [User] where email=@email and password=@password and name=@name";
                SqlParameter[] q =
                {
                    new SqlParameter("@email",u.Email),
                    new SqlParameter("@name",u.Name),
                    new SqlParameter("@password",u.password)
                };
                var x= DBHelper.GetDataTable(sql2, q);
                
               for(int i = 0; i < x.Rows.Count; i++)
                {
                    userId = int.Parse(x.Rows[i]["id"].ToString());
                }
            }
            return RedirectToAction("Index",new { id = userId });
        }

        public ActionResult longin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult longin(string Name,string Email,string Password)
        {
            int userId = 2;
            string sql = @"select id from [User] where email=@email and password=@password and name=@name";
            SqlParameter[] p =
            {
                    new SqlParameter("@email",Email),
                    new SqlParameter("@name",Name),
                    new SqlParameter("@password",Password)
            };
            var x = DBHelper.GetDataTable(sql, p);

            for (int i = 0; i < x.Rows.Count; i++)
            {
                userId = int.Parse(x.Rows[i]["id"].ToString());
            }

            return RedirectToAction("Index", new { id = userId });
        }

        public ActionResult LoginOut()
        {
            return RedirectToAction("Index", new { id = 2 });
        }
     
        public ActionResult AddReply(int askId,int userId,int answerId,string c)
        {
            if (userId == 2)
            {
                return Redirect("/Home/longin");
            }
            var x = new AnswerReply();
            x.content = c;
            x.userId = userId;
            x.answerId = answerId;
            x.createTime = DateTime.Now;
            db.AnswerReplies.Add(x);
            db.SaveChanges();
            return RedirectToAction("DisplayQuestion", new { id = askId, userId = userId });
        }

        public ActionResult addVote(int id,int answerId,int userId)
        {
            if (userId != 2)
            {
                var exist = db.Votes.Any(u => u.userId == userId && u.answerId == answerId);
                if (exist)
                {
                    db.Votes.ToList().RemoveAll(u => u.userId == userId && u.answerId == answerId);
                    var d = db.Votes.Where(v => v.userId == userId && v.answerId == answerId).FirstOrDefault();
                    db.Votes.Remove(d);
                    db.SaveChanges();
                }
                else
                {
                    Vote vote = new Vote();
                    vote.answerId = answerId;
                    vote.userId = userId;
                    vote.color = "green";
                    vote.disabled = "";

                    var x = db.Votes.Add(vote);
                    db.SaveChanges();
                }

                return RedirectToAction("DisplayQuestion", new { id = id, userId = userId });
            }
            else
            {
                return Redirect("/Home/longin");
            }
           
        }

        public ActionResult MinusVote(int id, int answerId, int userId)
        {
            if (userId != 2)
            {
                var exist = db.MinusVotes.Any(u => u.userId == userId && u.answerId == answerId);
                if (exist)
                {
                    db.MinusVotes.ToList().RemoveAll(u => u.userId == userId && u.answerId == answerId);
                    var d = db.MinusVotes.Where(v => v.userId == userId && v.answerId == answerId).FirstOrDefault();
                    db.MinusVotes.Remove(d);
                    db.SaveChanges();
                }
                else
                {
                    MinusVote mvote = new MinusVote();
                    mvote.answerId = answerId;
                    mvote.userId = userId;
                    var x = db.MinusVotes.Add(mvote);
                    db.SaveChanges();
                }

                return RedirectToAction("DisplayQuestion", new { id = id, userId = userId });
            }
            else
            {
                return Redirect("/Home/longin");
            }
           
        }
    }
}