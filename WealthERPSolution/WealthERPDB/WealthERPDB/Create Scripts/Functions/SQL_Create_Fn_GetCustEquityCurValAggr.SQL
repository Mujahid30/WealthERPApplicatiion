/****** Object:  UserDefinedFunction [dbo].[Fn_GetCustEquityCurValAggr]    Script Date: 06/24/2009 15:29:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fn_GetCustEquityCurValAggr]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Fn_GetCustEquityCurValAggr]
GO
/****** Object:  UserDefinedFunction [dbo].[Fn_GetCustEquityCurValAggr]    Script Date: 06/24/2009 15:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fn_GetCustEquityCurValAggr]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date, ,>  
-- Description: <Description, ,>  
-- =============================================  
CREATE FUNCTION [dbo].[Fn_GetCustEquityCurValAggr] 
(  
 -- Add the parameters for the function here  
 @C_CustomerId INT,
 @A_AdviserId INT 
)  
RETURNS INT  
AS  
BEGIN  
 -- Declare the return variable here  
 DECLARE @result INT;
  
 -- Add the T-SQL statements to compute the return value here  
 SELECT 
	@result = ISNULL(SUM(CENP_CurrentValue),0.00) FROM ViewEquityNP  
 WHERE 
	C_CustomerId = @C_CustomerId
	and  
	CENP_ValuationDate in 
	(  
		   select top 1(ADEL_ProcessDate)  
		   FROM    
		   dbo.AdviserDailyEODLog    
		   WHERE    
			A_AdviserId = @A_AdviserId  
			AND 
			ADEL_AssetGroup=''EQ''  
		   order by ADEL_ProcessDate desc  
     )    
 -- Return the result of the function  
 RETURN @result;  
  
END  ' 
END

GO
