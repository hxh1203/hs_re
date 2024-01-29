using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace ArgusCloseLoopTool.Entity
{
    [SugarTable("HS_SYS_USER")]
    public partial class UserEntity:ObservableObject 
    {
        public UserEntity()
        {
            
        }
        public UserEntity(UserEntity user)
        {
            ID = user.ID;
            LoginName = user.LoginName;
            Name = user.Name;
            Password = user.Password;
            Email = user.Email;
            Mobile = user.Mobile;
            DeptID = user.DeptID;
            DeptName = user.DeptName;
            PosName = user.PosName;
            Role = user.Role;
            AuthorityLevel = user.AuthorityLevel;
            UpdateDateTime = user.UpdateDateTime;
            InsertDateTime = user.InsertDateTime;
        }

        /// <summary>
        /// Desc:编号
        /// Default:newid()
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public virtual string ID { get; set; }

        /// <summary>
        /// Desc:登录名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string LoginName { get; set; }

        /// <summary>
        /// Desc:真实姓名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:密码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Password { get; set; }

        /// <summary>
        /// Desc:邮箱
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Email { get; set; }

        /// <summary>
        /// Desc:手机号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Mobile { get; set; }

        /// <summary>
        /// Desc:所属部门
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DeptID { get; set; }

        /// <summary>
        /// Desc:部门名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DeptName { get; set; }

        /// <summary>
        /// Desc:职位名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PosName { get; set; }

        /// <summary>
        /// Desc:角色
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Role { get; set; }

        /// <summary>
        /// Desc:权限等级
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string AuthorityLevel { get; set; }

        /// <summary>
        /// Desc:更新时间
        /// Default:DateTime.Now
        /// Nullable:True
        /// </summary>           
        public DateTime UpdateDateTime { get; set; }

        /// <summary>
        /// Desc:插入时间
        /// Default:DateTime.Now
        /// Nullable:True
        /// </summary>           
        public DateTime InsertDateTime { get; set; }
    }
}



