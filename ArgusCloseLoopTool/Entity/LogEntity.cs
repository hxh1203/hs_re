using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using SqlSugar;

namespace ArgusCloseLoopTool.Entity
{
    [SugarTable("HS_SYS_OPERATERECORD")]
    public partial class LogEntity: ObservableObject
    {
        public LogEntity()
        {
                
        }
        public LogEntity(LogEntity log)
        {
            USERID = log.USERID;
            USERNAME = log.USERNAME;
            OPERATETYPE = log.OPERATETYPE;
            OPERATERECORD = log.OPERATERECORD;
            INSERTTIME = log.INSERTTIME;
        }

        [SugarColumn(IsPrimaryKey = true)]
        public string USERID { get; set; }
        public string USERNAME { get; set; }
        public string OPERATETYPE { get; set; }
        public string OPERATERECORD { get; set; }
        public string INSERTTIME { get; set; }
    }
}
