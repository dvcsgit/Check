using Check.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Check
{
    public class Initializer : DropCreateDatabaseAlways<CheckContext>
    {
        protected override void Seed(CheckContext context)
        {
            //base.Seed(context);  

            #region Site
            var webFunctionNames = new List<WebFunctionName>
            {
                new WebFunctionName{WebFunctionId="Closed",Language="zh-cn",Name="结案"},
                new WebFunctionName{WebFunctionId="Closed",Language="us-en",Name="Closed"},
                new WebFunctionName{WebFunctionId="Create",Language="zh-cn",Name="新增"},
                new WebFunctionName{WebFunctionId="Create",Language="us-en",Name="Create"},
                new WebFunctionName{WebFunctionId="Delete",Language="zh-cn",Name="刪除"},
                new WebFunctionName{WebFunctionId="Delete",Language="us-en",Name="Delete"},
                new WebFunctionName{WebFunctionId="Download",Language="zh-cn",Name="下载"},
                new WebFunctionName{WebFunctionId="Download",Language="us-en",Name="Download"},
                new WebFunctionName{WebFunctionId="Edit",Language="zh-cn",Name="编辑"},
                new WebFunctionName{WebFunctionId="Edit",Language="us-en",Name="Edit"},
                new WebFunctionName{WebFunctionId="Query",Language="zh-cn",Name="查询"},
                new WebFunctionName{WebFunctionId="Query",Language="us-en",Name="Query"},
                new WebFunctionName{WebFunctionId="Upload",Language="zh-cn",Name="上传"},
                new WebFunctionName{WebFunctionId="Upload",Language="us-en",Name="Upload"},
                new WebFunctionName{WebFunctionId="Verify",Language="zh-cn",Name="复核"},
                new WebFunctionName{WebFunctionId="Verify",Language="us-en",Name="Verify"},
            };

            var webfunctions = new List<WebFunction>
            {
                new WebFunction{WebFunctionId="Closed", WebFunctionNames=webFunctionNames.Where(wfd=>wfd.WebFunctionId=="Closed").ToList()},
                new WebFunction{WebFunctionId="Create", WebFunctionNames=webFunctionNames.Where(wfd=>wfd.WebFunctionId=="Create").ToList()},
                new WebFunction{WebFunctionId="Delete", WebFunctionNames=webFunctionNames.Where(wfd=>wfd.WebFunctionId=="Delete").ToList()},
                new WebFunction{WebFunctionId="Download", WebFunctionNames=webFunctionNames.Where(wfd=>wfd.WebFunctionId=="Download").ToList()},
                new WebFunction{WebFunctionId="Edit", WebFunctionNames=webFunctionNames.Where(wfd=>wfd.WebFunctionId=="Edit").ToList()},
                new WebFunction{WebFunctionId="Query", WebFunctionNames=webFunctionNames.Where(wfd=>wfd.WebFunctionId=="Query").ToList()},
                new WebFunction{WebFunctionId="Upload", WebFunctionNames=webFunctionNames.Where(wfd=>wfd.WebFunctionId=="Upload").ToList()},
                new WebFunction{WebFunctionId="Verify", WebFunctionNames=webFunctionNames.Where(wfd=>wfd.WebFunctionId=="Verify").ToList()},
            };            

            var webPermissionNames = new List<WebPermissionName>
            {
                new WebPermissionName{WebPermissionId="A",Language="zh-cn",Name="基本资料设置"},
                new WebPermissionName{WebPermissionId="A",Language="us-en",Name="Basic information"},
                new WebPermissionName{WebPermissionId="AA",Language="zh-cn",Name="组织架构资料管理"},
                new WebPermissionName{WebPermissionId="AA",Language="us-en",Name="Organization schema"},               
                new WebPermissionName{WebPermissionId="AB",Language="zh-cn",Name="权限群组数据管理"},
                new WebPermissionName{WebPermissionId="AB",Language="us-en",Name="Rights groups data"},
                new WebPermissionName{WebPermissionId="AC",Language="zh-cn",Name="用户数据管理"},
                new WebPermissionName{WebPermissionId="AC",Language="us-en",Name="User data management"},
                new WebPermissionName{WebPermissionId="AD",Language="zh-cn",Name="表单流程核签设置"},
                new WebPermissionName{WebPermissionId="AD",Language="us-en",Name="Process approving"},
                
                new WebPermissionName{WebPermissionId="B",Language="zh-cn",Name="基本数据管理"},
                new WebPermissionName{WebPermissionId="B",Language="us-en",Name=""},
                new WebPermissionName{WebPermissionId="BA",Language="zh-cn",Name="基本资料管理"},
                new WebPermissionName{WebPermissionId="BA",Language="us-en",Name=""},
                new WebPermissionName{WebPermissionId="BAA",Language="zh-cn",Name="捻丝机机台管理"},
                new WebPermissionName{WebPermissionId="BAA",Language="us-en",Name=""},
                new WebPermissionName{WebPermissionId="BAB",Language="zh-cn",Name="捻丝机纺位管理"},
                new WebPermissionName{WebPermissionId="BAB",Language="us-en",Name=""},
                new WebPermissionName{WebPermissionId="BAC",Language="zh-cn",Name="异常原因管理"},
                new WebPermissionName{WebPermissionId="BAC",Language="us-en",Name=""},                
                new WebPermissionName{WebPermissionId="BB",Language="zh-cn",Name="制程检核管理"},
                new WebPermissionName{WebPermissionId="BB",Language="us-_en",Name=""},
                new WebPermissionName{WebPermissionId="BBA",Language="zh-cn",Name="异常处理"},
                new WebPermissionName{WebPermissionId="BBA",Language="us-en",Name=""},
                new WebPermissionName{WebPermissionId="BBB",Language="zh-cn",Name="呈核签核"},
                new WebPermissionName{WebPermissionId="BBB",Language="us-en",Name=""},
                new WebPermissionName{WebPermissionId="BBC",Language="zh-cn",Name="异常发送Notes"},
                new WebPermissionName{WebPermissionId="BBC",Language="us-en",Name=""},
                
            };            
           
            var webpermissions = new List<WebPermission>
            {                
                new WebPermission{WebPermissionId="A",ParentId="*",Icon="fa-cogs",WebPermissionNames=webPermissionNames.Where(wpd=>wpd.WebPermissionId=="A").ToList(),Seq=1},
                new WebPermission{WebPermissionId="AA",ParentId="A",Controller="Organization",Action="Index",Icon="fa-sitemap",WebPermissionNames=webPermissionNames.Where(wpd=>wpd.WebPermissionId=="AA").ToList(),Seq=1},
                new WebPermission{WebPermissionId="AB",ParentId="A",Controller="Role",Action="Index",Icon="fa-users",WebPermissionNames=webPermissionNames.Where(wpd=>wpd.WebPermissionId=="AB").ToList(),Seq=2},
                new WebPermission{WebPermissionId="AC",ParentId="A",Controller="Person",Action="Index",Icon="fa-users",WebPermissionNames=webPermissionNames.Where(wpd=>wpd.WebPermissionId=="AC").ToList(),Seq=3},
                new WebPermission{WebPermissionId="AD",ParentId="A",Controller="Flow",Action="Index",Icon="fa-gavel",WebPermissionNames=webPermissionNames.Where(wpd=>wpd.WebPermissionId=="AD").ToList(),Seq=4},               
                new WebPermission{WebPermissionId="B",ParentId="*",Icon="fa-wrench",WebPermissionNames=webPermissionNames.Where(wpd=>wpd.WebPermissionId=="B").ToList(),Seq=1},
                new WebPermission{WebPermissionId="BA",ParentId="B",Icon="fa-cogs",WebPermissionNames=webPermissionNames.Where(wpd=>wpd.WebPermissionId=="BA").ToList(),Seq=1},
                new WebPermission{WebPermissionId="BAA",ParentId="BA",Area="Check",Controller="Equipment",Action="Index",Icon="fa-cogs",WebPermissionNames=webPermissionNames.Where(wpd=>wpd.WebPermissionId=="BAA").ToList(),Seq=1},
                new WebPermission{WebPermissionId="BAB",ParentId="BA",Area="Check",Controller="MeltSpin",Action="Index",Icon="fa-newspaper-o",WebPermissionNames=webPermissionNames.Where(wpd=>wpd.WebPermissionId=="BAB").ToList(),Seq=2},
                new WebPermission{WebPermissionId="BAC",ParentId="BA",Area="Check",Controller="AbnormalReason",Action="Index",Icon="fa-file",WebPermissionNames=webPermissionNames.Where(wpd=>wpd.WebPermissionId=="BAC").ToList(),Seq=3},                
                new WebPermission{WebPermissionId="BB",ParentId="B",Icon="fa-clipboard",WebPermissionNames=webPermissionNames.Where(wpn=>wpn.WebPermissionId=="BB").ToList(),Seq=2},
                new WebPermission{WebPermissionId="BBA",ParentId="BB",Area="Check",Controller="Route",Action="Index",Icon="fa-clipboard",WebPermissionNames=webPermissionNames.Where(wpn=>wpn.WebPermissionId=="BBA").ToList(),Seq=1},
                new WebPermission{WebPermissionId="BBB",ParentId="BB",Area="Check",Controller="ControlPoint",Action="Index",Icon="fa-rss-square",WebPermissionNames=webPermissionNames.Where(wpn=>wpn.WebPermissionId=="BBB").ToList(),Seq=2},
                new WebPermission{WebPermissionId="BBC",ParentId="BB",Area="Check",Controller="EquipmentCheckItem",Action="Index",Icon="fa-cogs",WebPermissionNames=webPermissionNames.Where(wpn=>wpn.WebPermissionId=="BBC").ToList(),Seq=3},
                
            };
            webpermissions.ForEach(wp => context.WebPermissions.Add(wp));

            var webPermissionWebFunctions = new List<RolePermissionFunction>
            {
                //new WebPermissionWebFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="A").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                //new WebPermissionWebFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="A").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                //new WebPermissionWebFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="A").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                //new WebPermissionWebFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="A").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                //new WebPermissionWebFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="A").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                //new WebPermissionWebFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="A").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                //new WebPermissionWebFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="A").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                //new WebPermissionWebFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="A").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AD").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AD").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AD").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AD").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AD").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AD").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AD").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="AD").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},
               
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},               

                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},

                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},

                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},

                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Administrator",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},

                

                //new WebPermissionWebFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="B").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                //new WebPermissionWebFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="B").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                //new WebPermissionWebFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="B").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                //new WebPermissionWebFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="B").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                //new WebPermissionWebFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="B").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                //new WebPermissionWebFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="B").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                //new WebPermissionWebFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="B").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                //new WebPermissionWebFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="B").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},
                
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Manager",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},

                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BAC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},
                
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},

                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBA").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},

                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBB").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},

                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Closed").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Create").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Delete").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Download").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Edit").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Query").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Upload").First()},
                new RolePermissionFunction{RoleId="Staff",WebPermission=webpermissions.Where(wp=>wp.WebPermissionId=="BBC").First(),WebFunction=webfunctions.Where(wf=>wf.WebFunctionId=="Verify").First()},                

            };            

            var roles = new List<Role>
            {                                
                new Role{RoleId="Administrator", Name="系统管理员",WebPermissionWebFunctions=webPermissionWebFunctions.Where(wpwf=>wpwf.RoleId=="Administrator").ToList()},
                new Role{RoleId="Manager",Name="主管",WebPermissionWebFunctions=webPermissionWebFunctions.Where(wpwf=>wpwf.RoleId=="Manager").ToList()},
                new Role{RoleId="Staff",Name="员工",WebPermissionWebFunctions=webPermissionWebFunctions.Where(wpwf=>wpwf.RoleId=="Staff").ToList()}
            };
            roles.ForEach(r => context.Roles.Add(r));

            var organizations = new List<Organization>
            {                
                new Organization{OrganizationId=new Guid("AA61236E-EF81-E911-B553-9C22DA7561B4"), ParentId=new Guid(),OId="BiCheng",Name="昆山必成"},
                new Organization{OrganizationId=new Guid("DAEA2882-0ACD-4967-9C66-1B22320247F1"),ParentId=new Guid("AA61236E-EF81-E911-B553-9C22DA7561B4"),OId="One",Name="一厂"},
                new Organization{OrganizationId=Guid.NewGuid(),ParentId=new Guid("DAEA2882-0ACD-4967-9C66-1B22320247F1"),OId="MeltSpin1",Name="一厂熔纺课"},
                new Organization{OrganizationId=Guid.NewGuid(),ParentId=new Guid("DAEA2882-0ACD-4967-9C66-1B22320247F1"),OId="Manufacture1",Name="一厂加工课"},
                new Organization{OrganizationId=new Guid("EE6BE7E9-EF73-4425-9C28-4CD4038DD000"),ParentId=new Guid("AA61236E-EF81-E911-B553-9C22DA7561B4"),OId="Two",Name="二厂"},
                new Organization{OrganizationId=Guid.NewGuid(),ParentId=new Guid("EE6BE7E9-EF73-4425-9C28-4CD4038DD000"),OId="MeltSpin2",Name="二厂熔纺课"},
                new Organization{OrganizationId=Guid.NewGuid(),ParentId=new Guid("EE6BE7E9-EF73-4425-9C28-4CD4038DD000"),OId="Manufacture2",Name="二厂加工课"},
                new Organization{OrganizationId=new Guid("71D52600-AE1B-4A2A-B6BD-B95D17881C82"),ParentId=new Guid("AA61236E-EF81-E911-B553-9C22DA7561B4"),OId="Three",Name="三厂"},
                new Organization{OrganizationId=Guid.NewGuid(),ParentId=new Guid("71D52600-AE1B-4A2A-B6BD-B95D17881C82"),OId="MeltSpin3",Name="三厂熔纺课"},
                new Organization{OrganizationId=Guid.NewGuid(),ParentId=new Guid("71D52600-AE1B-4A2A-B6BD-B95D17881C82"),OId="Manufacture3",Name="三厂加工课"},
                new Organization{OrganizationId=new Guid("6FE25E60-4C0D-4EC1-83D5-56A0CAD5CCC5"),ParentId=new Guid("AA61236E-EF81-E911-B553-9C22DA7561B4"),OId="Four",Name="四厂"},
                new Organization{OrganizationId=Guid.NewGuid(),ParentId=new Guid("6FE25E60-4C0D-4EC1-83D5-56A0CAD5CCC5"),OId="MeltSpin4",Name="四厂熔纺课"},
                new Organization{OrganizationId=Guid.NewGuid(),ParentId=new Guid("6FE25E60-4C0D-4EC1-83D5-56A0CAD5CCC5"),OId="Manufacture4",Name="四厂加工课"},
                new Organization{OrganizationId=new Guid("1EED437E-2CEB-49A2-961D-6EF7D4649471"),ParentId=new Guid("AA61236E-EF81-E911-B553-9C22DA7561B4"),OId="Technics",Name="技术处"},                                
            };
            organizations.ForEach(o => context.Organizations.Add(o));

            var people = new List<Person>
            {
                new Person{LoginId="admin",Name="admin",Password="0",OrganizationId=new Guid("AA61236E-EF81-E911-B553-9C22DA7561B4"),Roles=roles},
                new Person{LoginId="mana",Name="mana",Password="0",Organization=organizations.Where(o=>o.OId=="MeltSpin1").FirstOrDefault(),Roles=roles.Where(r=>r.RoleId=="Manager").ToList()},
                new Person{LoginId="sta",Name="sta",Password="0",Organization=organizations.Where(o=>o.OId=="Manufacture1").FirstOrDefault(),Roles=roles.Where(r=>r.RoleId=="Staff").ToList()}
            };
            people.ForEach(a => context.People.Add(a));
            #endregion

            #region Check
            new List<Equipment>
            {
                new Equipment{EId="ms001",Position="1",Type="MeltSpin",Organization=organizations.Where(o=>o.OId=="MeltSpin1").FirstOrDefault(),Person=people.Where(p=>p.LoginId=="mana").FirstOrDefault(),Enable="true",LastModifyTime=DateTime.Now},
                new Equipment{EId="mf001",Position="2",Type="Manufacture",Organization=organizations.Where(o=>o.OId=="Manufacture1").FirstOrDefault(),Person=people.Where(p=>p.LoginId=="sta").FirstOrDefault(),Enable="true",LastModifyTime=DateTime.Now},
            }.ForEach(e => context.Equipments.Add(e));
            #endregion
        }
    }
}
