ALTER Procedure [dbo].[sproc_ProcessConditionFieldsForTransEvent]  
@targetId int,  
@schemeId int,  
@ael_EventCode varchar(50),  
@ael_fieldname varchar(1000),  
@aes_tablename varchar(1000),  
@triggerConditionMatch int out  
  
AS  
  
Declare   
@bTran AS INT,        
@lErrCode AS INT,  
@delimeter as char(1),@pos as int,@piece as varchar(500),@query as nvarchar(4000),  
@tableName as varchar(150),@tbldelimiter as char(1),@colCount as int,@finalValue1 as int,
@finalValue as int  
  
-- Set  
SET @triggerConditionMatch = 0 -- default value is 0 (means does not match)  
SET @pos = -1  
SET @colCount = 0  
Set @delimeter = ','   
Set @tbldelimiter = ','   
Set @query = 'Select @finalValue1=1  From _tableName Where ( ( _custId = ' +  
CONVERT(varchar(100),@targetId) + ') AND ( _schemeId = ' + CAST(@schemeId as Varchar(100)) +  
')) '
--AND ( _transtrigger = ''' + @ael_EventCode +  
--'''))'  
  
-- Get the table Name From First Column  
set @pos =  patindex('%'+@delimeter+'%' , @aes_tablename)   
  
-- Cross check for Pos  
IF(@pos > 0)  
 BEGIN   
  SET @tableName = left(@aes_tablename, @pos - 1)   
 END  
ELSE  
 BEGIN  
  SET @tableName = RTRIM(LTRIM(@aes_tablename))  
 END  
  
  
-- Replace table Name  
SET @query = (Replace(@query,'_tableName',@tableName))  
  
  
-- Get Columns  
  
 -- Need to tack a delimiter onto the end of the input string if one doesn't exist    
 if right(rtrim(@ael_fieldname),1) <> @delimeter    
  set @ael_fieldname = @ael_fieldname  + @delimeter    
    
 set @pos =  patindex('%'+@delimeter+'%' , @ael_fieldname)    
     
 -- Loop to insert all values for one column    
  while @pos <> 0    
  begin    
   set @piece = left(@ael_fieldname, @pos - 1)    
   set @piece  = LTRIM(RTRIM(@piece))    
    
    
  -- Insertion in tblMovieCategory    
  IF(Len(@piece)>0)   
  BEGIN    
   -- Increment ColCount  
  SET @colCount = @colCount + 1  
   -- Replace Columns in the Query  
 SET @query = CASE   
  WHEN (@colCount =1) THEN ((Replace(@query,'_custId',@piece)))  
        WHEN  (@colCount =2) THEN ((Replace(@query,'_schemeId',@piece)))  
   WHEN  (@colCount =3) THEN ((Replace(@query,'_transtrigger',@piece)))  
 END  
      
  END    
      
   set @ael_fieldname = stuff(@ael_fieldname, 1, @pos, '')    
   set @pos =  patindex('%'+@delimeter+'%' , @ael_fieldname)    
  end    
    
 -- Loop ends   
  
--Execute Query  
EXEC SP_EXECUTESQL @query,N'@finalValue1 int OUTPUT',@finalValue1  output 

Select @query,@finalValue1

  
-- Set the return value from the proc to @finalValue  
IF(@finalValue1 IS NOT NULL)  
BEGIN  
 SET @triggerConditionMatch = 1  
END  
  
  
Success:        
 If (@bTran = 1 And @@Trancount > 0)        
 Begin                                      
  Commit Tran        
 End        
 Return 0        
        
 Goto Done        
        
Error:        
 If (@bTran = 1 And @@Trancount > 0)        
 Begin        
  Rollback Transaction        
 End        
 Return @lErrCode        
  
          
Done:      
  

 