using ArgusCloseLoopTool.Entity;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgusCloseLoopTool.Models
{
    public partial class UserRegistrationModel : ObservableObject
    {
        public UserRegistrationModel()
        {
        }
        [ObservableProperty]
        private UserEntity _userEntity;
        /// <summary>
        /// 当前用户
        /// </summary>
        [ObservableProperty]
        private UserEntity _currentUser;
        /// <summary>
        /// 新用户
        /// </summary>
        [ObservableProperty]
        private UserEntity _newUser = new();
        /// <summary>
        /// 错误信息
        /// </summary>
        [ObservableProperty]
        private string _errorMessage;
        [ObservableProperty]
        private bool _loginTabIsEnabled;
        [ObservableProperty]
        private bool _quaryTabIsEnabled;
        [ObservableProperty]
        private bool _addTabIsEnabled;
        [ObservableProperty]
        private bool _permissionTabIsEnabled;
        [ObservableProperty]
        private bool _logTabIsEnabled;
        [ObservableProperty]
        private bool _myTabIsEnabled;
        [ObservableProperty]
        private int _selectedTabIndex;
        [ObservableProperty]
        private bool _isPasswordVisible;
        [ObservableProperty]
        private ObservableCollection<string> _comboBoxItems = new();
        [ObservableProperty]
        private UserEntity _selectedItem;
        [ObservableProperty]
        private string _userNameForSelect;
        [ObservableProperty]
        private bool _isCheckedA;
        [ObservableProperty]
        private bool _isCheckedB;
        [ObservableProperty]
        private ObservableCollection<UserEntity> _userList = new();
        [ObservableProperty]
        private ObservableCollection<LogEntity> _logList = new();
    }
}
