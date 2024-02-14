using ArgusCloseLoopTool.Entity;
using ArgusCloseLoopTool.Models;
using ArgusCloseLoopTool.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SqlSugar;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Windows.System;
using static ArgusCloseLoopTool.Services.LogHelper;

namespace ArgusCloseLoopTool.ViewModels;

public class UserRegistrationToolViewModel : ObservableObject
{
    #region 构造函数
    public UserRegistrationToolViewModel()
    {
        UserHelper userService = new();
        LoadData();
        LoginCommand = new RelayCommand(ExecuteLogin);
        PasswordVisibleCommand = new RelayCommand(PasswordVisible);
        DeleteSelectedRowCommand = new RelayCommand(DeleteSelectedRow);
        UpdateSelectedRowCommand = new RelayCommand(UpdateSelectedRow);
        AddUsersCommand = new RelayCommand(AddUsers);
        GetUserByNameCommand = new RelayCommand(GetUserByName);
        UserRegistrationModel.IsPasswordVisible = false;
    }

    #endregion
    #region 实例化
    public UserRegistrationModel UserRegistrationModel { get; set; } = new();
    public UserEntity LoginUser { get; set; } = new();
    public UserEntity NewSA { get; set; } = new();
    public LogEntity Log { get; set; } = new();
    public RelayCommand LoginCommand { get; private set; }
    public RelayCommand PasswordVisibleCommand { get; set; }
    public RelayCommand AddUsersCommand { get; set; }
    public RelayCommand DeleteSelectedRowCommand { get; set; }
    public RelayCommand UpdateSelectedRowCommand { get; set; }
    public RelayCommand GetUserByNameCommand { get; set; }


    #endregion
    #region 初始化
    private async void LoadData()
    {
        List<UserEntity> sa = await UserHelper.GetSaAsync(App.CurrentUser.Role);
        if (sa.Count == 0)
        {
            NewSA.LoginName = "hssa";
            NewSA.Name = "sa";
            NewSA.Password = "sa@hs.com";
            NewSA.Email = "2461512807@qq.com";
            NewSA.Mobile = "18221532707";
            NewSA.DeptID = "1001";
            NewSA.DeptName = "软件";
            NewSA.PosName = "软件";
            NewSA.Role = "超级管理员";
            NewSA.AuthorityLevel = "0级权限";
            NewSA.InsertDateTime = DateTime.Now;
            NewSA.UpdateDateTime = DateTime.Now;
            await UserHelper.AddUserAsync(NewSA);
        }
        List<UserEntity> users = await UserHelper.GetUsersAsync();
        UserRegistrationModel.UserList = new ObservableCollection<UserEntity>(users.Select(u => new UserEntity(u)).OrderBy(u => u.AuthorityLevel).ToList());
        List<LogEntity> logs = await UserHelper.QueryLogAsync();
        UserRegistrationModel.LogList = new ObservableCollection<LogEntity>(logs.Select(v => new LogEntity(v)).OrderByDescending(log => log.INSERTTIME).ToList());
        UserRegistrationModel.LoginTabIsEnabled = true;
        UserRegistrationModel.QuaryTabIsEnabled = false;
        UserRegistrationModel.AddTabIsEnabled = false;
        UserRegistrationModel.PermissionTabIsEnabled = false;
        UserRegistrationModel.LogTabIsEnabled = false;
        UserRegistrationModel.MyTabIsEnabled = false;
    }
    #endregion
    #region 登录方法
    private async void ExecuteLogin()
    {
        if (UserRegistrationModel.IsCheckedA)
        {
            App.OperationType = "A面";
        }
        else if (UserRegistrationModel.IsCheckedB)
        {
            App.OperationType = "B面";
        }
        UserRegistrationModel.ErrorMessage = " ";
        if (string.IsNullOrEmpty(LoginUser.LoginName))
        {
            UserRegistrationModel.ErrorMessage = "用户名不能为空！";
            return;
        }
        if (string.IsNullOrEmpty(LoginUser.Password))
        {
            UserRegistrationModel.ErrorMessage = "密码不能为空！";
            return;
        }
        if (UserRegistrationModel.IsCheckedA && UserRegistrationModel.IsCheckedB)
        {
            UserRegistrationModel.ErrorMessage = "只能选A或者B，不能都选！";
            return;
        }
        if (UserRegistrationModel.IsCheckedA == false && UserRegistrationModel.IsCheckedB == false)
        {
            UserRegistrationModel.ErrorMessage = "请选择A面或者B面！";
            return;
        }
        var dbUsersList = await UserHelper.GetUserForLoginAsync(LoginUser.LoginName, LoginUser.Password);
        if (dbUsersList.Count == 0)
        {
            UserRegistrationModel.ErrorMessage = "用户名或密码错误，请重新登录！";
        }
        else
        {
            foreach (var users in dbUsersList)
            {
                string name = users.Name;
                string password = users.Password;
                if (name != null && password != null)
                {
                    App.CurrentUser = users;
                    UserRegistrationModel.CurrentUser = App.CurrentUser;
                    MessageBox.Show("登录成功");
                }
                if (UserRegistrationModel.CurrentUser.Role == "超级管理员")
                {
                    UserRegistrationModel.LoginTabIsEnabled = true;
                    UserRegistrationModel.QuaryTabIsEnabled = true;
                    UserRegistrationModel.AddTabIsEnabled = true;
                    UserRegistrationModel.PermissionTabIsEnabled = true;
                    UserRegistrationModel.LogTabIsEnabled = true;
                    UserRegistrationModel.MyTabIsEnabled = true;
                    UserRegistrationModel.SelectedTabIndex = 1;
                }
                else if (UserRegistrationModel.CurrentUser.Role == "管理员")
                {
                    UserRegistrationModel.LoginTabIsEnabled = false;
                    UserRegistrationModel.QuaryTabIsEnabled = true;
                    UserRegistrationModel.AddTabIsEnabled = true;
                    UserRegistrationModel.PermissionTabIsEnabled = false;
                    UserRegistrationModel.LogTabIsEnabled = true;
                    UserRegistrationModel.MyTabIsEnabled = true;
                    UserRegistrationModel.SelectedTabIndex = 1;
                }
                else if (UserRegistrationModel.CurrentUser.Role == "生产人员")
                {
                    UserRegistrationModel.LoginTabIsEnabled = false;
                    UserRegistrationModel.QuaryTabIsEnabled = false;
                    UserRegistrationModel.AddTabIsEnabled = false;
                    UserRegistrationModel.PermissionTabIsEnabled = false;
                    UserRegistrationModel.LogTabIsEnabled = true;
                    UserRegistrationModel.MyTabIsEnabled = true;
                    UserRegistrationModel.SelectedTabIndex = 1;
                }
                else if (UserRegistrationModel.CurrentUser.Role == "工艺人员")
                {
                    UserRegistrationModel.LoginTabIsEnabled = false;
                    UserRegistrationModel.QuaryTabIsEnabled = true;
                    UserRegistrationModel.AddTabIsEnabled = false;
                    UserRegistrationModel.PermissionTabIsEnabled = false;
                    UserRegistrationModel.LogTabIsEnabled = true;
                    UserRegistrationModel.MyTabIsEnabled = true;
                    UserRegistrationModel.SelectedTabIndex = 1;
                }
            }
        }
        if (App.CurrentUser.Role == "超级管理员")
        {
            UserRegistrationModel.ComboBoxItems.Add("管理员");
            UserRegistrationModel.ComboBoxItems.Add("生产人员");
            UserRegistrationModel.ComboBoxItems.Add("工艺人员");
        }
        else if (App.CurrentUser.Role == "管理员")
        {
            UserRegistrationModel.ComboBoxItems.Add("生产人员");
            UserRegistrationModel.ComboBoxItems.Add("工艺人员");
        }
    }

    #endregion
    #region 密码显示和隐藏方法
    private void PasswordVisible()
    {
        UserRegistrationModel.IsPasswordVisible = !UserRegistrationModel.IsPasswordVisible;
    }
    #endregion
    #region 新增用户、删除用户、修改用户
    private async void AddUsers()
    {
        if (UserRegistrationModel.NewUser.Role == "超级管理员")
        {
            UserRegistrationModel.NewUser.AuthorityLevel = "0级权限";
        }
        if (UserRegistrationModel.NewUser.Role == "管理员")
        {
            UserRegistrationModel.NewUser.AuthorityLevel = "1级权限";
        }
        if (UserRegistrationModel.NewUser.Role == "生产人员")
        {
            UserRegistrationModel.NewUser.AuthorityLevel = "2级权限";
        }
        if (UserRegistrationModel.NewUser.Role == "工艺人员")
        {
            UserRegistrationModel.NewUser.AuthorityLevel = "3级权限";
        }
        UserRegistrationModel.NewUser.InsertDateTime = DateTime.Now;
        UserRegistrationModel.NewUser.UpdateDateTime = DateTime.Now;
        if (UserRegistrationModel.NewUser.Name == null || UserRegistrationModel.NewUser.LoginName == null || UserRegistrationModel.NewUser.Password == null ||
            UserRegistrationModel.NewUser.Email == null || UserRegistrationModel.NewUser.Mobile == null || UserRegistrationModel.NewUser.DeptID == null ||
            UserRegistrationModel.NewUser.DeptName == null || UserRegistrationModel.NewUser.PosName == null || UserRegistrationModel.NewUser.Role == null ||
            UserRegistrationModel.NewUser.AuthorityLevel == null)
        {
            MessageBox.Show("存在未填写信息，请全部填写。");
        }
        else
        {
            if (!(UserRegistrationModel.UserList.Any(user => user.LoginName == UserRegistrationModel.NewUser.LoginName)))
            {
                await UserHelper.AddUserAsync(UserRegistrationModel.NewUser);
                LogDelegate logAddDelegate = new(AddUserLogAsync);
                await logAddDelegate(UserRegistrationModel.NewUser);
                UserRegistrationModel.UserList.Add(UserRegistrationModel.NewUser);
                MessageBox.Show("新增成功！");
                List<LogEntity> logs = await UserHelper.QueryLogAsync();
                UserRegistrationModel.LogList = new ObservableCollection<LogEntity>(logs.Select(v => new LogEntity(v)).OrderByDescending(log => log.INSERTTIME).ToList());
            }
            else
            {
                MessageBox.Show("用户已经存在，请更改注册信息。");
            }
        }
    }
    private async void DeleteSelectedRow()
    {
        if (UserRegistrationModel.SelectedItem != null)
        {
            if (UserRegistrationModel.SelectedItem is UserEntity usersInstance)
            {
                UserEntity user = usersInstance;
                await UserHelper.DeleteBySelectedAsync(UserRegistrationModel.SelectedItem.ID);
                LogDelegate logDelectDelegate = new(DeleteLogAsync);
                await logDelectDelegate(user);
                UserRegistrationModel.UserList.Remove(UserRegistrationModel.SelectedItem);
                List<LogEntity> logs = await UserHelper.QueryLogAsync();
                UserRegistrationModel.LogList = new ObservableCollection<LogEntity>(logs.Select(v => new LogEntity(v)).OrderByDescending(log => log.INSERTTIME).ToList());
            }
        }
    }
    private async void UpdateSelectedRow()
    {
        if (UserRegistrationModel.SelectedItem != null)
        {
            if (UserRegistrationModel.SelectedItem is UserEntity usersInstance)
            {
                UserEntity user = usersInstance;
                user.UpdateDateTime = DateTime.Now;
                await UserHelper.UpdateUserAsync(user);
                LogDelegate logUpdateDelegate = new(UpdateLogAsync);
                await logUpdateDelegate(user);
                UserRegistrationModel.UserList.Add(UserRegistrationModel.SelectedItem);
                UserRegistrationModel.UserList.Remove(UserRegistrationModel.SelectedItem);
                List<LogEntity> logs = await UserHelper.QueryLogAsync();
                UserRegistrationModel.LogList = new ObservableCollection<LogEntity>(logs.Select(v => new LogEntity(v)).OrderByDescending(log => log.INSERTTIME).ToList());
                MessageBox.Show("修改成功！");
            }
        }
              
    }
    #endregion
    #region
    public async void GetUserByName()
    {
        if (string.IsNullOrWhiteSpace(UserRegistrationModel.UserNameForSelect))
        {
            List<UserEntity> users = await UserHelper.GetUsersAsync();
            UserRegistrationModel.UserList = new ObservableCollection<UserEntity>(users.Select(u => new UserEntity(u)).OrderBy(u => u.AuthorityLevel).ToList());
        }
        else
        {
            List<UserEntity> user = await UserHelper.GetUserByNameAsync(UserRegistrationModel.UserNameForSelect);
            UserRegistrationModel.UserList = new ObservableCollection<UserEntity>(user.Select(u => new UserEntity(u)).ToList());
        }
    }
    #endregion
}

