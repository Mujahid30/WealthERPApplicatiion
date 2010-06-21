ALTER Procedure [dbo].[sproc_ProcessConditionFieldsForDataEvent]  
@targetId int,  
@schemeId int,  
@ael_fieldname varchar(1000),  
@ael_datacondition varchar(150),  
@aes_tablename varchar(1000),  
@presentCalculatedValue numeric (18,3) out  
  
AS  
  
Declare   
@bTran AS INT,        
@lErrCode AS INT,  
@delimeter as char(1),@pos as int,@piece as varchar(500),@query as nvarchar(4000),  
@tableName as varchar(150),@tbldelimiter as char(1),@colCount as int,  
@finalValue1 as numeric (18,3)  
  
-- Set  
SET @colCount = 0  
Set @delimeter = ','   
Set @tbldelimiter = ','   
Set @query = 'Select @finalValue1 = _columnName From _tableName Where ( ( _custId = ' +  
CONVERT(varchar(100),@targetId) + ') AND ( _schemeId = ' + CAST(@schemeId as Varchar(100)) +  
'))'  
  
-- Get the table Name From First Column  
set @pos =  patindex('%'+@delimeter+'%' , @aes_tablename)   
set @tableName = left(@aes_tablename, @pos - 1)    
  
  
-- Replace table Name  
SET @query = (Replace(@query,'_tableName',@tableName))  
  
  
-- Replace Select column Name  
SET @query = (Replace(@query,'_columnName',@ael_datacondition))  
  
  
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
 END  
      
  END    
      
   set @ael_fieldname = stuff(@ael_fieldname, 1, @pos, '')    
   set @pos =  patindex('%'+@delimeter+'%' , @ael_fieldname)    
  end    
    
 -- Loop ends   
  
--Execute Query  

EXEC SP_EXECUTESQL @query,N'@finalValue1 numeric(18,3) OUTPUT', @finalValue1 OUTPUT  
  SELECT @finalValue1
  
-- Set the return value from the proc to @finalValue  
SET @presentCalculatedValue = (Select @finalValue1)  
  
  
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
  
   