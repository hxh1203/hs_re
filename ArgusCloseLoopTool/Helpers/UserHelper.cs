using ArgusCloseLoopTool.Entity;
using ArgusCloseLoopTool.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace ArgusCloseLoopTool.Services
{
    public  class UserHelper
    {
        private static SqlSugarClient db;
        public UserHelper()
        {
            var ConnectionStr = "Data Source=localhost;Initial Catalog=HS.Argus.CloseLoopDataBaseA;Integrated Security=True;MultipleActiveResultSets=true";
            db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConnectionStr,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
            });
        }
        #region 用户增删改查
        // 添加用户
        public static async Task<bool> AddUserAsync(UserEntity user)
        {
            user.ID = Guid.NewGuid().ToString();
            var id = await db.Insertable(user).ExecuteReturnIdentityAsync();
            return id > 0;
        }

        // 删除用户
        public static async Task<int> DeleteBySelectedAsync(string id)
        {
            return await db.Deleteable<UserEntity>().Where(c => c.ID == id).ExecuteCommandAsync();
        }

        // 更新用户
        public static  async Task<int> UpdateUserAsync(UserEntity user)
        {
            return await db.Updateable(user).ExecuteCommandAsync();
        }

        // 查询用户通过姓名
        public static async Task<List<UserEntity>> GetUserByNameAsync(string userName)
        {
            var user = await db.Queryable<UserEntity>().Where(c => c.Name == userName).ToListAsync();
            return user;
        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static async Task<List<UserEntity>> GetUsersAsync()
        {
            return await db.Queryable<UserEntity>().ToListAsync();
        }
        // 检索用户通过用户名和密码
        public static async Task<List<UserEntity>> GetUserForLoginAsync(string loginName, string password)
        {
            return await db.Queryable<UserEntity>().Where(c => c.LoginName == loginName).Where(d => d.Password == password).ToListAsync();
        }
        /// <summary>
        /// 查找SA用户
        /// </summary>
        public static async Task<List<UserEntity>> GetSaAsync(string role)
        {
            return await db.Queryable<UserEntity>().Where(c => c.Role == "超级管理员").ToListAsync();
        }
        #endregion
        #region 日志增删改查
        //检索日志
        public async static Task<List<LogEntity>> QueryLogAsync()
        {
            return await db.Queryable<LogEntity>().ToListAsync();
        }
        // 插入日志
        public static async Task<int> InsertLogAsync(LogEntity log)
        {
            return await db.Insertable(log).ExecuteCommandAsync();
        }
        #endregion
    }
}
