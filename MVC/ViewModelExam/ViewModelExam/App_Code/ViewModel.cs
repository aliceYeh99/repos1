using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModelExam.App_Code
{
    public class ViewModel
    {
         
//        做一個 SOHeaderViewModel

//public HEADER_ID 單號
//CREATEUSER 開單者 LOGONID
//UUID 唯一編號
//APPLIER 申請者 LOGONID
//DateTime REQUEST_DATE { get; set; }

//        USERS TABLE
//USERID 流水號
//LOGONID 工號
         public string HEADER_ID { get; set; }
         public string Remark { get; set; }
        public string CREATEUSER { get; set; }
        public string UUID { get; set; }
        public string APPLIER { get; set; }
        //public DateTime REQUEST_DATE { get; set; }
        // SELECT 
        //CONVERT(DATE, GETDATE()) date;
        //select 'VS001' HEADER_ID, 'BB' Remark, '10211002' CREATEUSER, '1688' UUID, '10211002' APPLIER from dual 
    }
}