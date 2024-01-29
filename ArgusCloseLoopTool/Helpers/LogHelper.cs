using ArgusCloseLoopTool.Entity;
using ArgusCloseLoopTool.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace ArgusCloseLoopTool.Services
{

    public delegate Task LogDelegate(UserEntity newUser);
    public class LogHelper
    {
        public static async Task AddUserLogAsync(UserEntity newUser)
        {
            LogEntity log = CreateLog($"新增了一个[{newUser.Role}],他的名字是[{newUser.Name}]。");
            await InsertLogAsync(log);

        }
        public static async Task DeleteLogAsync(UserEntity deletedUser)
        {
            LogEntity log = CreateLog($"删除了一个用户[{deletedUser.Name}],他是一个[{deletedUser.Role}]。");
            await InsertLogAsync(log);
        }
        public static async Task UpdateLogAsync(UserEntity updatedUser)
        {
            LogEntity log = CreateLog( $"更改了[{updatedUser.Role}]用户[{updatedUser.Name}]的信息。");
            await InsertLogAsync(log);
        }

        private static LogEntity CreateLog(string operationRecord)
        {
            return new LogEntity
            {
                USERNAME = App.CurrentUser.Name,
                OPERATETYPE = App.OperationType,
                OPERATERECORD = operationRecord,
                INSERTTIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")
            };
        }

        private static async Task InsertLogAsync(LogEntity log)
        {
            await UserHelper.InsertLogAsync(log);
        }
        
    }
}
