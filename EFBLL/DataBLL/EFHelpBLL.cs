using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFBLL.IDataBLL;
using System.Linq.Expressions;
using System.Data.SqlClient;
using ModelSoure.DataDAL;
using ModelSoure.IDataDAL;

namespace EFBLL.DataBLL
{
    public abstract class EFHelpBLL<T> : IEFHelpBLL<T> where T:class,new()
    {

         protected IEFHelp<T> idal = new EFHelp<T>();


        #region 0.数据源
        public IQueryable<T> Entities
        {
            get
            {
                return idal.Entities;
            }
        }
        #endregion

        #region 1.添加
        public int Add(T model)
        {          
            return idal.Add(model);
        }
        #endregion

        #region 2.删除
        public int Del(T model)
        {
            return idal.Del(model);
        }
        #endregion

        #region 3.修改
        public int Modify(T model)
        {
            return idal.Modify(model);
        }
        #endregion


        //事务性的封装

        #region a1.批量处理SaveChange()
        public int SaveChange()
        {
            return idal.SaveChange();
        }
        #endregion

        #region b.新增
        public void AddNo(T model)
        {
            idal.AddNo(model);
        }
        #endregion

        #region c.删除(根据主键删除)
        public void DelNo(T model)
        {
            idal.DelNo(model);
        }
        #endregion

        #region d.根据条件删除
        public void DelByNo(Expression<Func<T, bool>> delWhere)
        {
            idal.DelByNo(delWhere);
        }
        #endregion

        #region e.修改
        public void ModifyNo(T model)
        {
            idal.ModifyNo(model);
        }
        #endregion

        //EF调用sql语句

        #region A.执行增加,删除,修改操作(或调用存储过程)
        public int ExecuteSql(string sql, params SqlParameter[] pars)
        {
            return idal.ExecuteSql(sql, pars);
        }

        #endregion

        #region B.执行查询操作
        public List<T> ExecuteQuery<T>(string sql, params SqlParameter[] pars)
        {
            return idal.ExecuteQuery<T>(sql, pars);
        }
        #endregion

        //大数据插入事物分离
        #region M.添加
        public int AddMore(T model)
        {
           return  idal.AddMore(model);
        }
        #endregion

        #region M1.批量处理SaveChange()
        public int SaveChangeMore()
        {
            return idal.SaveChangeMore();
        }
        #endregion
    }
}
