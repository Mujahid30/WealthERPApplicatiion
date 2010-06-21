using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using System.Configuration;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace BoCaching
{
  public class CachingBo
  {

    #region Class Variables
    int insertOrNot = Convert.ToInt32(ConfigurationSettings.AppSettings["IsCached"]);
    #endregion

    #region public string GetObjectKey(params string[] cacheParams)
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cacheParams"></param>
    /// <returns></returns>
    public string GetObjectKey(params string[] cacheParams)
    {
      //Declaration
      StringBuilder sbKey = new StringBuilder();

      try
      {
        //Using different cache keys for different type of objects
        foreach (string strParam in cacheParams)
        {
          sbKey.Append(strParam);
          if ((strParam != null) && (strParam.Trim().Length > 0))
          {
            sbKey.Append("_");
          }
        }

        //Remove the last underscore
        if (sbKey.Length > 0)
        {
          sbKey.Remove(sbKey.Length - 1, 1);
        }

      }
      catch (BaseApplicationException Ex)
      {
        throw Ex;
      }
      catch (Exception Ex)
      {
        NameValueCollection FunctionInfo = new NameValueCollection();

        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        exBase.AdditionalInformation = FunctionInfo;
        ExceptionManager.Publish(exBase);
        throw exBase;
      }
    #endregion

      return sbKey.ToString();
    }

    #region public void AddToCache(string strKey, object objToCache,DateTime dtAbsExpiration,double minutesToExpire)
    public void AddToCache(string strKey, object objToCache, DateTime dtAbsExpiration, double minutesToExpire)
    {
      //Declarations

      try
      {
        //1 - strKey - is the key which will be used by the Cache to store the object
        //2 - objToCache - is the object to be cached
        //3 - the third parameter is the cache dependency parameter whioch is set to null here
        //4 - There is NO Absolute Expiration
        //5 - Sliding Expiration time is being passed to this method in minutes

        //HttpRuntime.Cache.Add(strKey,objToCache,null,System.Web.Caching.Cache.NoAbsoluteExpiration,TimeSpan.FromMinutes(minutesToExpire),System.Web.Caching.CacheItemPriority.Default,null);

        //Get insert or not key
        

        if (insertOrNot == 1)
        {
          HttpRuntime.Cache.Insert(strKey, objToCache, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(minutesToExpire), System.Web.Caching.CacheItemPriority.NotRemovable, null);
          //HttpRuntime.Cache.Insert(strKey, objToCache, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(minutesToExpire), System.Web.Caching.CacheItemPriority.Default, null);
        }
      }
      catch (BaseApplicationException Ex)
      {
        throw Ex;
      }
      catch (Exception Ex)
      {
        NameValueCollection FunctionInfo = new NameValueCollection();
        FunctionInfo.Add("Method", "AddToCache");
        FunctionInfo.Add("StrKey", strKey);
        FunctionInfo.Add("MinutesToExpire", minutesToExpire.ToString());

        object[] objects = new object[1];
        objects[0] = objToCache;

        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        exBase.AdditionalInformation = FunctionInfo;
        ExceptionManager.Publish(exBase);
        throw exBase;
      }
    }
    #endregion

    #region public object LoadFromCache(string strKey)
    /// <summary>
    /// 
    /// </summary>
    /// <param name="strKey"></param>
    /// <returns></returns>
    public object LoadFromCache(string strKey)
    {
      //Declarations
      object objReturn = null;

      try
      {
        //Returns the cache object depending on the key
        objReturn = HttpRuntime.Cache[strKey];
      }
      catch (BaseApplicationException Ex)
      {
        throw Ex;
      }
      catch (Exception Ex)
      {
        NameValueCollection FunctionInfo = new NameValueCollection();
        FunctionInfo.Add("Method", "LoadFromCache");
        FunctionInfo.Add("StrKey", strKey);

        object[] objects = new object[1];
        objects[0] = objReturn;

        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        exBase.AdditionalInformation = FunctionInfo;
        ExceptionManager.Publish(exBase);
        throw exBase;
      }
      return objReturn;
    }
    #endregion

    #region public void RemoveFromCache(string strKey)
    /// <summary>
    /// 
    /// </summary>
    /// <param name="strKey"></param>
    public void RemoveFromCache(string strKey)
    {
      //Declarations

      try
      {
        HttpRuntime.Cache.Remove(strKey);
      }
      catch (BaseApplicationException Ex)
      {
        throw Ex;
      }
      catch (Exception Ex)
      {
        NameValueCollection FunctionInfo = new NameValueCollection();
        FunctionInfo.Add("Method", "RemoveFromCache");
        FunctionInfo.Add("StrKey", strKey);

        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        exBase.AdditionalInformation = FunctionInfo;
        ExceptionManager.Publish(exBase);
        throw exBase;
      }
    }
    #endregion

    #region public static double GetCacheExpirationTime(string strKey)
    /// <summary>
    /// 
    /// </summary>
    /// <param name="strKey"></param>
    /// <returns></returns>
    public double GetCacheExpirationTime(string strKey)
    {

      //Declarations
      StringBuilder sbCacheKey = new StringBuilder();
      string strCacheExp = String.Empty;
      double dbCacheExpTime = 0;


      try
      {

        //Get the webconfig key
        sbCacheKey.Append(strKey);
        sbCacheKey.Append("CacheExpTime");

        //Get the time from Web Config
        strCacheExp = ConfigurationSettings.AppSettings[sbCacheKey.ToString()];

        //Validate and Parse
        if ((strCacheExp != null) && (strCacheExp.Length != 0))
        {
          dbCacheExpTime = Double.Parse(strCacheExp);
        }
      }
      catch (Exception Ex)
      {
      }

      //Return
      return dbCacheExpTime;
    }
    #endregion

  }
}
